using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Class.Item
{
    public class Weapon:Item
    {
        public Weapon(string name, Texture2D icon, bool isStackable, byte stackCapacity)
        {
            _isStackable = isStackable;
            _stackCapacity = stackCapacity;
            _icon = icon;
            _name = name;
        }

        public override void Use()
        {
            
        }
    }
}
