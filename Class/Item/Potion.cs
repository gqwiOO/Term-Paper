using Microsoft.Xna.Framework.Graphics;
using TermPaper.Class.Audio;

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

        public override void Use()
        {
            if (Globals.player._hp < 200 &&  !Globals.player._isDead)
            {
                Globals.player._hp += 40;
                if (Globals.player._hp > 200)
                {
                    Globals.player._hp = 200;
                }
            }
        }
    }
}
