#!/bin/bash


## FILE INFO ##
# Script:       mkshx.sh
# Description:  this bash script generates a bash script with boilerplate
# Author:       tnn4
# Generated on: ( Wed Oct 11 03:43:23 PM CDT 2023 )

me=$(basename $0)


## ARGUMENTS ##
# Process arguments/flags/switches here
process_args () {
    # if there are no arguments, do nothing
    if [[ $# -eq 0 ]];then
        help
        return
    fi
    args_no_last=${@:1:$#-1}
    
    for arg in $args_no_last; do
        # Process specific args here
        case $arg in
            *)
                echo "unknown arg"
                help_more
            ;;
        esac
    done
} 
# end process_args()


## HELP ##
# put help and usage instructions here
help_more() {
    # write more detailed usage instructions here
    echo "e.g. usage: $me --options"
}

help() {
    # write usage instructions here
    echo "e.g. usage: $me --options"
}

if [[ $1 == "-h" || $1 == "--help" ]];then
    help_more
    exit
fi

# END HELP

## MAIN ##

CSPROJ=".csproj"
# put main code here
main() {
    
    echo "[$me]: hello world!"
    process_args "$@"
    dotnet new mgdesktopgl --output "$1" && \
    cd "$1" && \
    # https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-8#simplified-output-paths
    dotnet new buildprops --use-artifacts && \
    mkdir src && \
    mv Game1.cs src && \
    mv Program.cs src

}

# import functions only and don't execute if --source-only is present
if [ "${1}" = "--source-only" ];then
    echo 'Functions imported. Script not executed'
else
    main "$@"
fi

# END MAIN
