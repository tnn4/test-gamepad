using System;

using System.Threading.Tasks;
using System.IO;
using System.IO.Pipes;
// System.IO.Pipes.< Anonymous | Named >Pipe< Client | Server >Stream
// AnonymousPipeServerStream exposes a stream around an anonymous pipe, supports both sync and async r/w ops
using System.Diagnostics;
                        //Process



class Program {
    
    private static
    System.Diagnostics.Process processHandle;

    static 
    void Main(string[] args) {

        using var game = new tgpad.Game1();
        game.Run();

        /*
        // Prevents infinite recursion
        if (args.Length != 0){
            if (args[0] == "1") {
                ExecutionPath_2(args);
            }
        } else {
            ExecutionPath_1();
        }
        */
    }

    private static
    void ExecutionPath_1() {
        // Self file info
        var SELF_FILE_NAME=System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        var VER=System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
        
        System.Diagnostics.Process pipeClient = new Process();
        
        
        // STATEMENT`using` ensures correct use of IDisposable instance
        // use the Dispose interface to explicitly release unmanaged resources in conjunction with garbage_collector
        // its common to want to create something disposable then dispose of it at the end
        using(System.IO.Pipes.AnonymousPipeServerStream pipeServer = 
            new System.IO.Pipes.AnonymousPipeServerStream(
                PipeDirection.In,
                HandleInheritability.Inheritable // <Inheritable | None> https://learn.microsoft.com/en-us/dotnet/api/system.io.handleinheritability?view=net-7.0
            )){
                System.Console.WriteLine($"[Server] Current Transmission Mode: {pipeServer.TransmissionMode}");
                // Pass the client process a handle to server
                pipeClient.StartInfo.Arguments =
                    pipeServer.GetClientHandleAsString();
                pipeClient.StartInfo.UseShellExecute = false;
                // System.Diagnostics.Process.Start(SELF, "1");
                // Spin off non-recursing version
                // Process.Start(SELF_FILE_NAME, "1");
                Process.Start(SELF_FILE_NAME, "1");
                pipeServer.DisposeLocalCopyOfClientHandle();
        }

    }

    private static
    void ExecutionPath_2(string[] args) {
        // Task i = GetInputAsync();
        // https://stackoverflow.com/questions/7353670/wcf-named-pipe-minimal-example
        // https://learn.microsoft.com/en-us/dotnet/standard/io/pipe-operations
        // We should use named pipes to communicate
        
        // https://stackoverflow.com/questions/2332127/realtime-console-output-redirection-using-process
        
        using (AnonymousPipeClientStream pipeClient = 
            new AnonymousPipeClientStream(
                PipeDirection.Out, //,args[0]
                args[0]
            )
        ){
            
            Console.WriteLine($"[Server] Current Transmission Mode: {pipeClient.TransmissionMode}");

            // Read line then send to server handle
            using (StreamWriter sWriter = new StreamWriter(pipeClient)){
                
                
                using (StreamReader sReader = new StreamReader(pipeClient)){
                    string buf = "";
                    do {
                    // Console.WriteLine();
                        sWriter.WriteLine(buf);
                    }while( (buf = sReader.ReadLine())  != "x" );
                }
            }
        }
    }
    
    /*
    private 
    async Task<string> GetInputAsync() {
        return Task.Run(() => System.Console.ReadLine());
    }
    */
}

/*
class Example {
    private static
    void RedirectionExample() {
        Process process = new Process();
        
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.ErrorDialog = false;
        process.StartInfo.RedirectStandardError = true;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.UseShellExecute = false;

        process.ErrorDataReceived += (sendingProcess, errorLine) => error.AppendLine(errorLine.Data);
        process.OutputDataReceived += (sendingProcess, dataLine) => SetMessage(dataLine.Data);

        process.Start();
        process.BeginErrorReadLine();
        process.BeginOutputReadLine();

        process.WaitForExit();
    }
}
*/
