using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Text;
using System.Collections.Generic;
                                // Dictionary


namespace tgpad;

public static class Constants {
    public const string DATA_PATH = "data";
}


public partial class Game1: Game {
#region Debug
    void Debug(GameTime dt) {
        #if DEBUG
        //Console.WriteLine($"{dt.ElapsedGameTime}");
        if (heartbeat) {
            Console.WriteLine("TICK");
        } else {
            Console.WriteLine("TOCk");
        }

        #endif
    }
    // End Debug
#endregion Debug

#region Update
    protected override 
    //
    void Update(GameTime dt)
    {
        
        #region exit_on_esc
        if (
            GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || 
            Keyboard.GetState().IsKeyDown(Keys.Escape)
        )
            Exit();
        #endregion
        


        #region Timeout
        // Timeout
        // https://learn.microsoft.com/en-us/dotnet/api/system.timespan?view=net-7.0
        this.elapsedTime += dt.ElapsedGameTime.TotalSeconds;
        #if DEBUG
        if (this.elapsedTime > this.timeOut) {
            Console.WriteLine("EXITING: MAX time reached");
            Exit();
        }
        //
        #endif
        #endregion
        
        #region Get_Gamepad_name
            this.GamePadDisplayName = Microsoft.Xna.Framework.Input.GamePad.GetCapabilities(Game1.p1_Index).DisplayName;
        #endregion

        #region Input_Helper   
        // Input helper to deal with repeated inputs
        this.inputHelper.Update();
        //this.btn1.Update(dt);
        // https://stackoverflow.com/questions/904454/how-to-slow-down-or-stop-key-presses-in-xna
        // the best way to implement this is to cache the keyboard/gamepad state from the update statement that just passed
        // PAUSE
        // if (Keyboard.GetState().IsKeyDown(Keys.Space)){
        if (inputHelper.IsNewPress(Keys.Space)) {
            Console.WriteLine("KEY pressed: Space ");
           
            
            if (gameIsPaused) {
                Console.WriteLine("Pausing game");
                this.targetFrames = 10;

            } else {
                Console.WriteLine("UnPausing game");
                this.targetFrames = 30;
            }
            // Set frame cap
            this.TargetElapsedTime = System.TimeSpan.FromSeconds(1d / this.targetFrames); //60);
            gameIsPaused = !gameIsPaused;
        }
        // End
        #endregion

        #region check_if_box_clicked
        // Check if box has been clicked    
        //  Get mouse input
        Game1.mouseState = Mouse.GetState();
        
        //  Get Mouse coordinates
        this.mouseX = mouseState.X;
        this.mouseY = mouseState.Y;
        
        var isClicked = mouseState.LeftButton == ButtonState.Pressed;
        #endregion

        #region test button
        this.btn1.Update(dt);
        #endregion
        
        #region get_shoulder_buttons
            this.GamePadShoulder_L = GamePad.GetState(Game1.p1_Index).Buttons.LeftShoulder;
            this.GamePadShoulder_R = GamePad.GetState(Game1.p1_Index).Buttons.RightShoulder;
        #endregion

        #region get_trigger_buttons
        // see: https://community.monogame.net/t/lt-and-rt-triggers-not-reacting-after-recent-update/18866
            if (this.GamePadTriggers != null) {
                // this.GamePadTrigger_L = this.GamePadTriggers.Left;  DOESN"T WORK WHY?
                // this.GamePadTrigger_R = this.GamePadTriggers.Right; DOESN"T WORK WHY?

                this.GamePadTrigger_L = GamePad.GetState(PlayerIndex.One).Triggers.Left;
                this.GamePadTrigger_R = GamePad.GetState(PlayerIndex.One).Triggers.Right;
            }

        #endregion

        // Get left thumbstick vector
        #region get_left_thumbstick_vector
        // https://docs.monogame.net/api/Microsoft.Xna.Framework.Input.GamePadState.html
            int p1_Index = 0;
            this.leftThumbstickVector = GamePad.GetState(p1_Index).ThumbSticks.Left;
        #endregion

        #region get_right_thumbstick_vector
            this.rightThumbstickVector = GamePad.GetState(p1_Index).ThumbSticks.Right;
        #endregion

        #region get_abxy
        // see: https://docs.monogame.net/api/Microsoft.Xna.Framework.Input.GamePadButtons.html
            this.GamePadState_A = GamePad.GetState(Game1.p1_Index).Buttons.A;
            this.GamePadState_B = GamePad.GetState(Game1.p1_Index).Buttons.B;
            this.GamePadState_X = GamePad.GetState(Game1.p1_Index).Buttons.X;
            this.GamePadState_Y = GamePad.GetState(Game1.p1_Index).Buttons.Y;
            if (this.GamePadState_A == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {
                this.GamePad_A_Is_Pressed = true;
                this.GamePad_activated = true;
            } else {
                this.GamePad_A_Is_Pressed = false;
            }
            
            if (this.GamePadState_B == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {
                this.GamePad_B_Is_Pressed = true;
                this.GamePad_activated = true;
            } else {
                this.GamePad_B_Is_Pressed = false;
            }
            
            if (this.GamePadState_X == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {
                this.GamePad_X_Is_Pressed = true;
                this.GamePad_activated = true;
            } else {
                this.GamePad_X_Is_Pressed = false;
            }
            
            if (this.GamePadState_Y == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {
                this.GamePad_Y_Is_Pressed = true;
                this.GamePad_activated = true;
            } else {
                this.GamePad_Y_Is_Pressed = false;
            }

            if ( 
                this.GamePad_A_Is_Pressed || this.GamePad_B_Is_Pressed || this.GamePad_X_Is_Pressed || this.GamePad_Y_Is_Pressed)
            {
                this.GamePad_activated = true;
            } else {
                this.GamePad_activated = false;
            }
        #endregion

        // Use base to access members that have been hidden or overiddent by members of the subclass
        // Foo.Baz, Bar:Foo overrides Baz, with base you can still get it, base.Baz() == Foo.Baz
        base.Update(dt);
    }
    //  END Update
#endregion Update
}


/*
private
void CheckGamepadTrigger()
{
    // Check GamePad Trigger Buttons
    // check HasLeftTrigger
    // check HasRightTrigger
    this.GamePadHasTrigger_L = Microsoft.Xna.Framework.Input.GamePad.GetCapabilities(Game1.p1_Index).HasLeftTrigger;
    this.GamePadHasTrigger_R = Microsoft.Xna.Framework.Input.GamePad.GetCapabilities(Game1.p1_Index).HasRightTrigger;
    if ( this.GamePadHasTrigger_L && this.GamePadHasTrigger_R ) {
        this.GamePadTriggers = new GamePadTriggers(0,0);
        this.GamePadTriggersOK = true;
        Console.WriteLine("OK Gamepad Triggers detected");
    } else {
        Console.WriteLine("WARN! Gamepad Triggers missing");
    }
}

*/