using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Class.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Class.Item
{
    public class Potion: Item
    {
        public Potion(string name, Texture2D icon, bool isStackable, byte stackCapacity)
        {
            _name = name;
            _icon = icon;
            _isStackable = isStackable;
            _stackCapacity = stackCapacity;
        }

        public void Heal()
        {
            if (Globals.player._hp < 200 &&  !Globals.player._isDead)
            {
                Globals.player._hp += 40;
            }
        }
    }
}
