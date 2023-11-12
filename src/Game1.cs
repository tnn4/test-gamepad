using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Text;
using System.Collections.Generic;
                                // Dictionary


namespace tgpad;

public partial class Game1 : Game
{
    #region Data
    
    #region Input
    private
    InputHelper inputHelper;

    // Input Box
    public static 
    GameWindow gw;
    
    public static
    MouseState mouseState;
    
    bool boxHasFocus = false;
    
    StringBuilder textBoxDisplayCharacters = new StringBuilder();
    //
    #endregion

    #region Graphics
    // Graphics
    private 
    GraphicsDeviceManager _graphics;
    
    internal 
    SpriteBatch _spriteBatch;
    //
        #region Screen_Size
        // Screen Size
        // http://rbwhitaker.wikidot.com/monogame-drawing-text-with-spritefonts

        // screen size
        private
        int screenW;// = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;

        private
        int screenH;// = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

        private
        int preferredW = 800;

        private
        int preferredH = 450;
        //
        #endregion
    #endregion

    #region Timers
    // Timers
    private
    bool heartbeat = false;

    private
    double timeOut = 500;
    
    private
    double elapsedTime = 0;
    //
    #endregion



    #region Mouse_Coord
    // mouse coord
    private
    int mouseX = 0;

    private
    int mouseY = 0;
    //
    #endregion

    private
    bool gameIsPaused = false;

    // FPS
    private 
    int targetFrames = 60;
    //

    // Button
    private 
    Button btn1;
    //

    // Assets
    private 
    Dictionary<string, Texture2D> texture2DList;
    private
    Dictionary<string, Text> TextList;

    private
    SpriteFont font;

    private
    Texture2D tex1;
    private
    Texture2D arrowIcon;
    private
    Texture2D GamepadButtonABXY;
    // End Assets

    // Controller / Gamepad
    
    bool GamePad1Found = false;

    // Shoulders
    Microsoft.Xna.Framework.Input.ButtonState GamePadShoulder_L;
    Microsoft.Xna.Framework.Input.ButtonState GamePadShoulder_R;
    //

    // Triggers
    // see: https://docs.monogame.net/api/Microsoft.Xna.Framework.Input.GamePadTriggers.html
    // clamped: 0 < Tr < 1
    GamePadTriggers GamePadTriggers;

    bool GamePadHasTrigger_L;
    bool GamePadHasTrigger_R;
    bool GamePadTriggersOK;

    float GamePadTrigger_L;
    float GamePadTrigger_R;   
    //

    // Thumbsticks
    Vector2 leftThumbstickVector;
    Vector2 rightThumbstickVector;

    // Gamepad A B X Y
    Microsoft.Xna.Framework.Input.ButtonState GamePadState_A;
    Microsoft.Xna.Framework.Input.ButtonState GamePadState_B;
    Microsoft.Xna.Framework.Input.ButtonState GamePadState_X;
    Microsoft.Xna.Framework.Input.ButtonState GamePadState_Y;

    // Gamepad DPad
    /*
    #if DEBUG
    Microsoft.Xna.Framework.Input.ButtonState GamePadState_Dpad_U;
    Microsoft.Xna.Framework.Input.ButtonState GamePadState_Dpad_R;
    Microsoft.Xna.Framework.Input.ButtonState GamePadState_Dpad_D;
    Microsoft.Xna.Framework.Input.ButtonState GamePadState_Dpad_L;
    #endif
    */
    // usage: GamePad.GetState(PlayerIndex.One).DPad.Right == ButtonState.Pressed

    bool GamePad_A_Is_Pressed = false;
    bool GamePad_B_Is_Pressed = false;
    bool GamePad_X_Is_Pressed = false;
    bool GamePad_Y_Is_Pressed = false;


    bool GamePad_activated = false;
  

    string GamePadDisplayName = "";
    // End Controller

    // Player Index
    const
    int p1_Index = 0;
    const
    int p2_Index = 1;
    // End Player Index

    // DEBUG
    bool windowSizeLogged = false;
    //

    // Scenes
    
}
#endregion data