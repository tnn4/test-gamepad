using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Text;
using System.Collections.Generic;
                                // Dictionary

namespace tgpad;

public partial class Game1: Game 
{
#region Draw
    
    protected override 
    void Draw(GameTime dt)
    {
        // frame
        var i = 0;
        var scale = 0;
        // unit x
        var Ux = 0;
        // unit y
        var Uy = 0;
        float offset = 0;
        float offsetX = 0;
        float offsetY = 0;
        var windowW = this._graphics.PreferredBackBufferWidth;
        var windowH = this._graphics.PreferredBackBufferHeight;
        
        var isClicked = mouseState.LeftButton == ButtonState.Pressed;

        // Heartbeat
        //Console.WriteLine($"{dt.ElapsedGameTime}");
        if (heartbeat) {
            //Console.WriteLine("GRAY");
            GraphicsDevice.Clear(Color.Black);
            
        } else {
            //Console.WriteLine("BLACK");
            GraphicsDevice.Clear(Color.Black);
        }
        heartbeat = !heartbeat;
        // End

        // see: https://industrian.net/tutorials/using-sprite-sheets/
        /*
            // Draw the entire sprite.
            spriteBatch.Draw(charaset, new Vector2(100, 100), Color.White);
        
            // Create a sourceRectangle.
            Rectangle sourceRectangle = new Rectangle(0, 0, 48, 64);

            // Only draw the area contained within the sourceRectangle.
            spriteBatch.Draw(charaset, new Vector2(300, 100), sourceRectangle, Color.White);
        */

        this.TextList["Title"].Draw(dt);
        this._spriteBatch.Begin();
        
        // Draw Gamepad Base

        

        #region Gamepad_base
            
            var gamePadScale = 8;
            var Ux1 = 64 * gamePadScale;
            var Uy1 = 32 * gamePadScale;
            
            var gamepadBasePosX = 0 + 1/2*this.preferredW;
            var gamepadBasePosY = 0 + 1/2*this.preferredH;
            if (!this.GamePad_activated)
            {
                this._spriteBatch.Draw(
                    this.texture2DList["gamepad-base"],
                    new Vector2(120, 200), // Position + Offset_of_thumbstick
                    new Rectangle(
                        0,0,
                        64*gamePadScale, 32*gamePadScale
                    ), // source Rect
                    Color.White
                );
            } else {
                this._spriteBatch.Draw(
                    this.texture2DList["gamepad-base"],
                    new Vector2(120, 200), // Position + Offset_of_thumbstick
                    new Rectangle(
                        0+Ux1,0,
                        64*gamePadScale, 32*gamePadScale
                    ), // source Rect
                    Color.White
                );
            }

            if (this.windowSizeLogged == false) {
                Console.WriteLine($"screenW={windowW}, screenH={windowH}");
            }
            this.windowSizeLogged = true;
        #endregion Gamepad_base
        //

    // Text
    var textAnchorW = windowW*(1/8);
    var textAnchorH = windowH*(1/8);

    var author = "tnn4";
    double creditX=800*(4/5);
    double creditY=450*(4/5);    
    #region Credits text
        this._spriteBatch.DrawString(
            font,
            $"{author}",
            new Vector2( 770 ,  430 ),
            Color.White
        );
        // System.Console.WriteLine($"creditX={creditX},creditY={creditY}"); 
    #endregion
    // > Timeout text
    
    #region timer
        #if DEBUG
        var elapsedTimeR = Math.Round(this.elapsedTime, 2);
        this._spriteBatch.DrawString(
            font, 
            $"{elapsedTimeR}\n(timeout at {this.timeOut})",
            new Vector2(textAnchorW, textAnchorH),
            Color.White
        );
        #endif
    #endregion
        
    #region mouse_coordinates
        // > Mouse Coordinates
        this._spriteBatch.DrawString(
            font, 
            $"mouseX={this.mouseX}, mouseY={this.mouseY}",
            new Vector2(textAnchorW, textAnchorH+25),
            Color.White
        );
    #endregion

    #region gamepad display name   
    // Gampad display name:
        this._spriteBatch.DrawString(
            font,
            $"GamePad: {this.GamePadDisplayName}",
            new Vector2(textAnchorW, textAnchorH+55),
            Color.White
        );
    //
    #endregion

#region Gamepad_Trigger
    // Trigger Buttons:
        if (this.GamePadTriggersOK){
            this._spriteBatch.DrawString(
                font,
                $"GamePad Triggers: OK",
                new Vector2(textAnchorW, textAnchorH+70),
                Color.White
            );
            this._spriteBatch.DrawString(
                font,
                $"GamePad_Trigger_L={this.GamePadTrigger_L}, GamePad_Trigger_R={this.GamePadTrigger_R}",
                new Vector2(textAnchorW, textAnchorH+80),
                Color.White
            );
            scale = 4;
            Ux = 16*scale;
            Uy = 16*scale;
            var triggerOriginX = 150;
            var triggerOriginY = 160;
            
            
            // Gamepad Trigger L
            i = 0;
            if (this.GamePadTrigger_L == 1)
            {
                this._spriteBatch.Draw(
                    this.texture2DList["gamepad-trigger"],
                    new Vector2(triggerOriginX,triggerOriginY),
                    new Rectangle(
                        0+i*Ux,0+1*Uy,
                        Ux,Uy
                    ), // frame 1
                    Color.White
                );
            } else {
                
                this._spriteBatch.Draw(
                    this.texture2DList["gamepad-trigger"],
                    new Vector2(triggerOriginX,triggerOriginY),
                    new Rectangle(
                        0+i*Ux,0,
                        Ux,Uy
                    ), // frame 2
                    Color.White
                );
            }
            i = 1;
            offsetX = 390;
            // Gamepad Trigger R
            if (this.GamePadTrigger_R == 1)
            {
                this._spriteBatch.Draw(
                    this.texture2DList["gamepad-trigger"],
                    new Vector2(triggerOriginX+offsetX,triggerOriginY),
                    new Rectangle(
                        0+i*Ux,0+1*Uy,
                        Ux,Uy
                    ), // frame 1
                    Color.White
                );
            } else {
                
                this._spriteBatch.Draw(
                    this.texture2DList["gamepad-trigger"],
                    new Vector2(triggerOriginX+offsetX,triggerOriginY),
                    new Rectangle(
                        0+i*Ux,0,
                        Ux,Uy
                    ), // frame 2
                    Color.White
                );
            }
        }
#endregion Gamepad_Trigger

#region GamePad_Shoulder 
            this._spriteBatch.DrawString(
                font,
                $"GamePad_Shoulder_L={this.GamePadShoulder_L}, GamePad_Shoulder_R={this.GamePadShoulder_R}",
                new Vector2(textAnchorW, textAnchorH+90),
                Color.White
            );

            scale = 4;
            Ux = 16*scale;
            Uy = 16*scale;
            var shoulderOriginX = 210;
            var shoulderOriginY = 160;
            
            
            // Gamepad Shoulder L
            i = 0;
            if (this.GamePadShoulder_L == ButtonState.Pressed)
            {
                this._spriteBatch.Draw(
                    this.texture2DList["gamepad-shoulder"],
                    new Vector2(shoulderOriginX,shoulderOriginY),
                    new Rectangle(
                        0+i*Ux,0+1*Uy,
                        Ux,Uy
                    ), // frame 1
                    Color.White
                );
            } else {
                this._spriteBatch.Draw(
                    this.texture2DList["gamepad-shoulder"],
                    new Vector2(shoulderOriginX,shoulderOriginY),
                    new Rectangle(
                        0+i*Ux,0,
                        Ux,Uy
                    ), // frame 2
                    Color.White
                );
            }
            i = 1;
            offsetX = 270;
            // Gamepad Shoulder R
            if (this.GamePadShoulder_R == ButtonState.Pressed)
            {
                this._spriteBatch.Draw(
                    this.texture2DList["gamepad-shoulder"],
                    new Vector2(shoulderOriginX+offsetX,shoulderOriginY),
                    new Rectangle(
                        0+i*Ux,0+1*Uy,
                        Ux,Uy
                    ), // frame 1
                    Color.White
                );
            } else {
                
                this._spriteBatch.Draw(
                    this.texture2DList["gamepad-shoulder"],
                    new Vector2(shoulderOriginX+offsetX,shoulderOriginY),
                    new Rectangle(
                        0+i*Ux,0,
                        Ux,Uy
                    ), // frame 2
                    Color.White
                );
            }            
#endregion 


        // Test joystick
        #region left_thumbstick_vector

            // L Thumbstick Vectors
            var vx = this.leftThumbstickVector.X;
            var vy = this.leftThumbstickVector.Y;
            if (vx != 0 && vy != 0){

            }
            var vxR = Math.Round(vx,2);
            var vyR = Math.Round(vy,2);
            var atan_vyx = Math.Atan2(vy,vx); // double
            var atan_vyxF = (float)Math.Atan2(vy,vx);
            var atan_vyxR = Math.Round(atan_vyx,2); // this is radians (angle),the input for rotation

            // LStick  Text
            this._spriteBatch.DrawString(
                font,
                $"LStick_x = {vxR}, LStick_y: = {vyR}",
                new Vector2(this.preferredW*(1/6), this.preferredH*(1/2)+35),
                Color.White
            );
            this._spriteBatch.DrawString(
                font,
                $"=> Atan: {atan_vyxR}",
                new Vector2(this.preferredW*(1/6), this.preferredH*(1/2)+35+10),
                Color.White
            );
            //

            /*
                Draw overload with rotation:
                public void Draw (
                    Texture2D texture,
                    Vector2 position,
                    Nullable<Rectangle> sourceRectangle,
                    Color color,
                    float rotation,
                    Vector2 origin,
                    float scale,
                    SpriteEffects effects,
                    float layerDepth
                )
            */

            // see: https://docs.monogame.net/api/Microsoft.Xna.Framework.Graphics.SpriteBatch.html#Microsoft_Xna_Framework_Graphics_SpriteBatch_Draw_Microsoft_Xna_Framework_Graphics_Texture2D_Microsoft_Xna_Framework_Vector2_System_Nullable_Microsoft_Xna_Framework_Rectangle__Microsoft_Xna_Framework_Color_System_Single_Microsoft_Xna_Framework_Vector2_Microsoft_Xna_Framework_Vector2_Microsoft_Xna_Framework_Graphics_SpriteEffects_System_Single_
            
            // Draw Red arrow -->>
            // Thumbstick offset
            offset = 20;
            offsetX = vx*offset;
            offsetY = vy*offset;

            var redArrowPosX = 370;
            var redArrowPosY = 200;

            this._spriteBatch.Draw(
                this.arrowIcon,
                new Vector2(redArrowPosX + offsetX, redArrowPosY - offsetY), // position + offset of thumbstick
                new Rectangle(0,0,32*4,32*4),
                Color.White,
                -atan_vyxF, // rotation in radians, assumes start = ----> (1,0), ERR! cannot convert double to float
                new Vector2(32*4/2, 32*4/2), // origin at middle of sprite
                new Vector2(1,1),
                SpriteEffects.None,
                0f
            );

            // Draw Gamepad Thumbstick L
            var sx = 4;
            var thumbstickPosX = 170;
            var thumbstickPosY = 200;

            this._spriteBatch.Draw(
                this.texture2DList["gamepad-thumbstick-L"],
                new Vector2(thumbstickPosX + offsetX, thumbstickPosY - offsetY), // Position + Offset_of_thumbstick
                new Rectangle(0,0,32*sx, 32*sx),
                Color.White
            );

            // R Thumbstick Vectors
            var vx2 = this.rightThumbstickVector.X;
            var vy2 = this.rightThumbstickVector.Y;

            var vx2R = Math.Round(vx2,2);
            var vy2R = Math.Round(vy2,2);
            var atan_vyx2 = Math.Atan2(vy2,vx2); // double
            var atan_vyx2F = (float)Math.Atan2(vy2,vx2);
            var atan_vyx2R = Math.Round(atan_vyx2,2); // this is radians (angle),the input for rotation

            // Thumbstick offset
            var offset2 = 20;
            var offset2X = vx2*offset2;
            var offset2Y = vy2*offset2;

            // Draw Thumbstick R
            var thumbstick2PosX = 370;
            var thumbstick2PosY = 280;

            this._spriteBatch.Draw(
                this.texture2DList["gamepad-thumbstick-L"],
                new Vector2(thumbstick2PosX + offset2X, thumbstick2PosY - offset2Y), // Position + Offset_of_thumbstick
                new Rectangle(0,0,32*sx, 32*sx),
                Color.White
            );
        #endregion
        // End JoyStick
        

    #region DPad
        // Arrow buttons
        scale = 4;
        Ux1 = 16*scale;
        Uy1 = 16*scale;
        var dPadOriginX = 180;
        var dPadOriginY = 290;
        
        Rectangle sourceRect;
        // UP
        i = 0;
        if (!(GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed))
        {
            this._spriteBatch.Draw(
                tex1,
                new Vector2(
                    dPadOriginX,dPadOriginY),
                new Rectangle(
                    0+i*Ux1,0,
                    Ux1,Uy1
                ), // frame 1
                Color.White
            );
        } else {
            
            this._spriteBatch.Draw(
                tex1,
                new Vector2(dPadOriginX,dPadOriginY),
                new Rectangle(
                    0+i*Ux1,0+1*Uy1,
                    Ux1,Uy1
                ), // frame 2
                Color.White
            );
        }
        // RIGHT
        i=1;
        if (!(GamePad.GetState(PlayerIndex.One).DPad.Right == ButtonState.Pressed))
        {
            this._spriteBatch.Draw(
                tex1,
                new Vector2(dPadOriginX+30,dPadOriginY+30),
                new Rectangle(
                    0+i*Ux1,0,
                    Ux1,Uy1
                ), // frame 1
                Color.White
            );
        } else {
            this._spriteBatch.Draw(
                tex1,
                new Vector2(dPadOriginX+30,dPadOriginY+30),
                new Rectangle(
                    0+i*Ux1,0+1*Uy1,
                    Ux1,Uy1
                ), // frame 2
                Color.White
            );
        }
        i=2;
        if (!(GamePad.GetState(PlayerIndex.One).DPad.Down == ButtonState.Pressed))
        {
            this._spriteBatch.Draw(
                tex1,
                new Vector2(dPadOriginX,dPadOriginY+60),
                new Rectangle(
                    0+i*Ux1,0,
                    Ux1,Uy1
                ), // frame 1
                Color.White
            );
            } else {
            this._spriteBatch.Draw(
                tex1,
                new Vector2(dPadOriginX,dPadOriginY+60),
                new Rectangle(
                    0+i*Ux1,0+1*Uy1,
                    Ux1,Uy1
                ), // frame 2
                Color.White
            );
        }
        i=3;
        if (!(GamePad.GetState(PlayerIndex.One).DPad.Left == ButtonState.Pressed))
        {
            this._spriteBatch.Draw(
                tex1,
                new Vector2(dPadOriginX-30,dPadOriginY+30),
                new Rectangle(
                    0+i*Ux1,0,
                    Ux1,Uy1
                ), // frame 1
                Color.White
            );
        } else {
            this._spriteBatch.Draw(
                tex1,
                new Vector2(dPadOriginX-30,dPadOriginY+30),
                new Rectangle(
                    0+i*Ux1,0+1*Uy1,
                    Ux1,Uy1
                ), // frame 2
                Color.White
            );
        }
        // End
    #endregion DPad


    #region ABXY_Buttons
        // Gamepad A B X Y
        
        // Test Y
        
        scale = 4;
        Ux = 16*scale; // U for unit
        Uy = 16*scale;

        var abxy_originX = 470;
        var abxy_originY = 220;

        i = 0;
        if (!this.GamePad_Y_Is_Pressed)
        {
            sourceRect 
                = new Rectangle(
                    0+(i*Ux),0,
                    Ux,Uy
            );
            
            this._spriteBatch.Draw(
                this.texture2DList["gamepad-button-abxy"],
                new Vector2(abxy_originX+i*Ux,abxy_originY),
                sourceRect, // frame 1
                Color.White
            );
        } else {

            sourceRect
                = new Rectangle(
                    0+(i*Ux),0+(Uy),
                    Ux,Uy
            );

            this._spriteBatch.Draw(
                this.texture2DList["gamepad-button-abxy"],
                new Vector2(abxy_originX+i*Ux,abxy_originY),
                sourceRect, // frame 2
                Color.White
            );
        }


        // Test B
        i= 1;
        if (!this.GamePad_B_Is_Pressed)
        {
            sourceRect 
                = new Rectangle(
                    0+(i*Ux),0,
                    Ux,Uy
            );
            
            this._spriteBatch.Draw(
                this.texture2DList["gamepad-button-abxy"],
                new Vector2(abxy_originX+i*Ux-15,abxy_originY+20),
                sourceRect, // frame 1
                Color.White
            );
        } else {

            sourceRect
                = new Rectangle(
                    0+(i*Ux),0+(Uy),
                    Ux,Uy
            );

            this._spriteBatch.Draw(
                this.texture2DList["gamepad-button-abxy"],
                new Vector2(abxy_originX+i*Ux-15,abxy_originY+20),
                sourceRect, // frame 2
                Color.White
            );
        }
        // Test A
        i = 2;
        if (!this.GamePad_A_Is_Pressed)
        {
            sourceRect 
                = new Rectangle(
                    0+(i*Ux),0,
                    Ux,Uy
            );
            
            this._spriteBatch.Draw(
                this.texture2DList["gamepad-button-abxy"],
                new Vector2(abxy_originX, abxy_originY+2*20),
                sourceRect, // frame 1
                Color.White
            );
        } else {

            sourceRect
                = new Rectangle(
                    0+(i*Ux),0+(Uy),
                    Ux,Uy
            );

            this._spriteBatch.Draw(
                this.texture2DList["gamepad-button-abxy"],
                new Vector2(abxy_originX, abxy_originY+2*20),
                sourceRect, // frame 2
                Color.White
            );
        }
        // Test X
        i = 3;
        if (!this.GamePad_X_Is_Pressed)
        {
            sourceRect 
                = new Rectangle(
                    0+(i*Ux),0,
                    Ux,Uy
            );
            
            this._spriteBatch.Draw(
                this.texture2DList["gamepad-button-abxy"],
                new Vector2(abxy_originX-Ux+15,abxy_originY+20),
                sourceRect, // frame 1
                Color.White
            );
        } else {

            sourceRect
                = new Rectangle(
                    0+(i*Ux),0+(Uy),
                    Ux,Uy
            );

            this._spriteBatch.Draw(
                this.texture2DList["gamepad-button-abxy"],
                new Vector2(abxy_originX-Ux+15,abxy_originY+20),
                sourceRect, // frame 2
                Color.White
            );
        }
        // End A B X Y
    #endregion ABXY_Buttons

        this._spriteBatch.End();
        // End
        
        // Button Class
        // this.btn1.Draw(dt);
        // End

        //this.btn1.Draw2(dt);
        base.Draw(dt);
    }
    // END Draw()
#endregion Draw
}