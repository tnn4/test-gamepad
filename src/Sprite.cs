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
public class Sprite : DrawableGameComponent {
    
    Game1 game1;
    GameWindow gw;
    Texture2D texture2D;
    bool textureIsPresent = false;


    bool boxHasFocus = false;
    bool isClicked = false;
    MouseState mouseState;
    // Rectangle component of this button
    Rectangle rect;
    
    int posX = 0;
    int posY = 0;
    int rectW = 1;
    int rectH = 1;

    int scale = 1;
    int Ux = 1;
    int Uy = 1;
    float OriginX;
    float OriginY;

    // Microsoft.Xna.Framework.Input
    //                              Mouse
    //                              MouseState
    //                              MouseCursor

    // https://stackoverflow.com/questions/12051/calling-the-base-constructor-in-c-sharp
    // Modify your constructor to the following so that it calls the base class constructor properly:
    public
    Sprite(Game1 game1, int x, int y, int w, int h): base(game1){
        Console.WriteLine("CREATED Sprite");
        this.game1 = game1;
        this.posX = x;
        this.posY = y;
        this.rectW = w;
        this.rectH = h;
        this.rect = new Rectangle(x,y,w,h);
    }

    public
    Sprite(
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

    }
    
    // IDrawable
    public override
    void Draw(GameTime dt) {
        //Console.WriteLine("BUTTON draw()");
        this.game1._spriteBatch.Begin();

        var i = 0;
        scale = 4;
        Ux = 16*scale;
        Uy = 16*scale;
        this.posX = 150;
        this.posY = 160;
        // var offsetX = 0;
        // var offsetY = 0;
        
        // Gamepad Trigger L
        i = 0;

        this.game1._spriteBatch.Draw(
            this.texture2D,
            new Vector2(this.posX,this.posY),
            new Rectangle(
                0+i*Ux,0,
                Ux,Uy
            ), // frame 2
            Color.White
        );

        this.game1._spriteBatch.Draw(
            this.texture2D,
            new Vector2(this.posX,this.posY),
            new Rectangle(
                0+i*Ux,0+1*Uy,
                Ux,Uy
            ), // frame 1
            Color.White
        );


        this.game1._spriteBatch.End();
        //base.Draw(dt);
    }

    // IDrawable
}