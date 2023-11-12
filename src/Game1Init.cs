using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Text;
using System.Collections.Generic;

namespace tgpad;

public partial class Game1: Game 
{
#region ctor
    public
    Game1()
    {
        Console.WriteLine($"CREATED {this.GetType().Name}");
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = Constants.DATA_PATH;
        IsMouseVisible = true;

        // Set frame cap
        this.targetFrames = 60;
        this.TargetElapsedTime = System.TimeSpan.FromSeconds(1d / this.targetFrames); //60);
        //

        // Input helper
        this.inputHelper = new InputHelper();
        //

        //Texture2D List
        this.texture2DList = new Dictionary<string,Texture2D>();
        //

        // TextList
        this.TextList = new Dictionary<string, Text>();
        
    }
#endregion ctor

#region Initialize

    protected override 
    void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
        /*
        int fps = 30;
        double interval = 1d/fps;
        this.TargetElapsedTime = System.TimeSpan.FromSeconds(interval);
        Console.WriteLine($"Frames capped to {fps}");
        */
        
        // Get Screen Size
        this.screenW = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        this.screenH= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        Console.WriteLine($"screenW = {screenW}");
        Console.WriteLine($"screenH = {screenH}");
        
    // Change resolution.
        // https://community.monogame.net/t/window-size-and-resolution-control/8905/12
        this._graphics.PreferredBackBufferWidth  = this.preferredW;
        this._graphics.PreferredBackBufferHeight = this.preferredH;
        this._graphics.ApplyChanges();
    // End

        // Create a button
        this.btn1 
            = new Button(
                this, 
                this.preferredW/2 , this.preferredH/2, 
                50,50 
            );
        //

    // Get Gamepad capabilities
        // see: https://community.monogame.net/t/ability-to-tell-gamepad-type-xbone-ps4-ect/9468
        // see: https://docs.monogame.net/api/Microsoft.Xna.Framework.Input.GamePadCapabilities.html
        // check for this every frame
        this.GamePadDisplayName = Microsoft.Xna.Framework.Input.GamePad.GetCapabilities(Game1.p1_Index).DisplayName;
        
        
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
    // End gamepad capabilities


    }
    // End Initialize()
#endregion Initialize

# region LoadContent

    protected override 
    void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        var screenWidth = this._graphics.PreferredBackBufferWidth;
        var screenHeight = this._graphics.PreferredBackBufferHeight;

        // TODO: use this.Content to load your game content here
        gw = this.Window;

        #region Content
        // Load our font
        font = Content.Load<SpriteFont>("monogram");
        // Load arrow buton sprite
        tex1 = Content.Load<Texture2D>("button-arrow-01-4x");
        // Load red button
        this.arrowIcon = Content.Load<Texture2D>("arrow-00");
        // Gamepad Buttons
        this.texture2DList.Add("gamepad-button-abxy", Content.Load<Texture2D>("gamepad-abxy-00-4x"));
        this.texture2DList.Add("gamepad-thumbstick-L", Content.Load<Texture2D>("gamepad-thumbstick-00-4x"));
        this.texture2DList.Add("gamepad-base", Content.Load<Texture2D>("gamepad-base-00-8x"));
        this.texture2DList.Add("gamepad-shoulder", Content.Load<Texture2D>("gamepad-shoulder-00-4x"));
        this.texture2DList.Add("gamepad-trigger", Content.Load<Texture2D>("gamepad-trigger-00-4x"));

        this.TextList.Add("Title", new Text(this, this.font, "Controller Tester", new Vector2(screenWidth/2-50, 0)));
        #endregion
    }
    // End LoadContent
#endregion LoadContent
}