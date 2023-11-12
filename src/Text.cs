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
public class Text : DrawableGameComponent {
    
    Game1 game1;

    SpriteFont Font;
    
    // represents a mutable string of characters
    // as string StringBuilder.ToString()
    StringBuilder StringBuilder = new StringBuilder();
    string DisplayText;
    
    int preferredW = 0;
    int preferredH = 0;

    // Rectangle component of this button
    Rectangle rect;
    Vector2 Position = new Vector2(0,0);
    int posX = 0;
    int posY = 0;


    // Microsoft.Xna.Framework.Input
    //                              Mouse
    //                              MouseState
    //                              MouseCursor

    // https://stackoverflow.com/questions/12051/calling-the-base-constructor-in-c-sharp
    // Modify your constructor to the following so that it calls the base class constructor properly:
    public
    Text(Game1 game1, SpriteFont font, string text, Vector2 position): base(game1){
        Console.WriteLine("CREATED Button");
        this.game1 = game1;
        
        this.Font = font;

        this.StringBuilder.Append(text);
        this.DisplayText = this.StringBuilder.ToString();

        this.Position = position;
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

        this.game1._spriteBatch.DrawString(
            this.Font, 
            this.DisplayText,
            this.Position,
            Color.White
        );

        this.game1._spriteBatch.End();
        //base.Draw(dt);
    }



    // assisting methods.


}