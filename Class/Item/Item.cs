using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Class.Item
{
   public class Item
    {
        public bool _isStackable;
        public byte _stackCapacity;
        public Texture2D _icon;
        public int _price;


        public Item(bool isStackable, byte stackCapacity, Texture2D icon)
        {
            _isStackable = isStackable;
            _stackCapacity = stackCapacity;
            _icon = icon;
        }


        public void Sell()
        {
            
        }

        public void DrawInInventory(Rectangle _position, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_icon, _position, Color.White);
        }


    }
}
