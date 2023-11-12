using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Text;

namespace tgpad;

// DrawableGameComponent - Inherits from GameComponent and implements IDrawable on top of it. 
// Use this for components that have a visual representation.
// IUpdateable: https://docs.monogame.net/api/Microsoft.Xna.Framework.IUpdateable.html
// IDrawable: https://docs.monogame.net/api/Microsoft.Xna.Framework.IDrawable.html
public class Button : DrawableGameComponent {
    
    Game1 game1;
    GameWindow gw;
    Texture2D texture2D;
    bool textureIsPresent = false;
    StringBuilder textBoxDisplayCharacters = new StringBuilder();
    
    
    int preferredW = 0;
    int preferredH = 0;

    bool boxHasFocus = false;
    bool isClicked = false;
    MouseState mouseState;
    // Rectangle component of this button
    Rectangle rect;
    
    int posX = 0;
    int posY = 0;
    int rectW = 1;
    int rectH = 1;

    // Microsoft.Xna.Framework.Input
    //                              Mouse
    //                              MouseState
    //                              MouseCursor

    // https://stackoverflow.com/questions/12051/calling-the-base-constructor-in-c-sharp
    // Modify your constructor to the following so that it calls the base class constructor properly:
    public
    Button(Game1 game1, int x, int y, int w, int h): base(game1){
        Console.WriteLine("CREATED Button");
        this.game1 = game1;
        this.posX = x;
        this.posY = y;
        this.rectW = w;
        this.rectH = h;
        this.rect = new Rectangle(x,y,w,h);
    }

    public
    Button(
        Game1 game1,
        Texture2D _texture2D,
        int x, int y, int w, int h
    ): base(game1) {
        this.game1 = game1;
        this.textureIsPresent = true;
        this.texture2D = _texture2D;
        this.posX = x;
        this.posY = y;
        this.rectW = w;
        this.rectH = h;
        this.rect = new Rectangle(x,y,w,h);
    }
    
    // IUpdateable
    public override
    void Update(GameTime dt) {
        //Console.WriteLine("BUTTON update()");
        this.mouseState = Mouse.GetState();

        CheckClickOnMyBox(
            mouseState.Position, 
            isClicked, 
            this.rect
        );
    }
    
    // IDrawable
    public override
    void Draw(GameTime dt) {
        //Console.WriteLine("BUTTON draw()");
        this.game1._spriteBatch.Begin();

        Texture2D _texture = new Texture2D(GraphicsDevice,1,1);
        
        // public Texture2D(GraphicsDevice graphicsDevice, int width, int height)
        

        var isClicked = mouseState.LeftButton == ButtonState.Pressed;
        if (!isClicked)
            _texture.SetData(new Color[] { Color.DarkSlateGray });
        else
            _texture.SetData(new Color[] { Color.White });
        //

        // If there is a texture we use the texture        
        if (textureIsPresent) {
            _texture = this.texture2D;
        } else {
            //_texture = new Texture2D(GraphicsDevice, 1, 1);
        }

        this.game1._spriteBatch.Draw(
            _texture, 
            new Rectangle(
                this.posX, this.posY, 
                this.rectW, this.rectH
            ), 
            Color.White
        );

        this.game1._spriteBatch.End();
        //base.Draw(dt);
    }



    // assisting methods.

    public 
    void RegisterFocusedButtonForTextInput(System.EventHandler<TextInputEventArgs> method)
    {
        gw.TextInput += method;
    }

    
    public
    void UnRegisterFocusedButtonForTextInput(System.EventHandler<TextInputEventArgs> method)
    {
        gw.TextInput -= method;
    }

    
    // these two are textbox specific.
    public 
    void CheckClickOnMyBox(Point mouseClick, bool isClicked, Rectangle r)
    {
        if (r.Contains(mouseClick) && isClicked)
        {
            Console.WriteLine("BOX clicked");
            boxHasFocus = !boxHasFocus;
            if (boxHasFocus)
                RegisterFocusedButtonForTextInput(OnInput);
            else
                UnRegisterFocusedButtonForTextInput(OnInput);
        }
    }

    public 
    void OnInput(object sender, TextInputEventArgs e)
    {
        var k = e.Key;
        var c = e.Character;
        textBoxDisplayCharacters.Append(c);
        Console.WriteLine(textBoxDisplayCharacters);
    }
}