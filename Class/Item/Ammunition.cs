
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using MathL;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Movement;
using TermPaper.Class.Font;

namespace Game1.Class.Item
{
    public class Ammunition
    {
        public bool IsRemoved;
        public RectangleF Position = new RectangleF(4000, 4000, 40, 20);
        public Texture2D arrowSprite = Globals.Content.Load<Texture2D>("Items/Weapon/Bow/Arrow");
        
        public void Update()
        {
            Position.X += 300 * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;
        }
        public void Draw()
        {
            if(!IsRemoved) Globals.spriteBatch.Draw(arrowSprite, Position.ToRectangle(),Color.White); 
        }
    }
}
