using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Movement;
using MathL;

namespace Game1.Class.Item
{
   public abstract class Item
    {
        public int id{ get; set; }
        public bool _isStackable;
        public byte _stackCapacity;
        public Texture2D _sprite;
        public int _price;
        public string _name;
        public Animation _animation { get; set; }

        public float cooldown { get; set;}
        public RectangleF _hitbox;

        public abstract void Use();
        public void Sell()
        {
        }
        public void DrawInInventory(Rectangle _position)
        {
            Globals.spriteBatch.Draw(_sprite, _position, Color.White);
        }
        public abstract void Update();
        public abstract void Draw();
    }
}
