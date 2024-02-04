using Microsoft.Xna.Framework.Graphics;
using TermPaper.Class.Audio;

namespace Game1.Class.Item
{
    public class Potion: Item
    {
        public Potion(string name, Texture2D sprite, bool isStackable, byte stackCapacity)
        {
            _name = name;
            _sprite = sprite;
            _isStackable = isStackable;
            _stackCapacity = stackCapacity;
        }

        public override void Use()
        {
            if (Globals.player._hp < 200 &&  !Globals.player.isDead)
            {
                Globals.player._hp += 40;
                if (Globals.player._hp > 200)
                {
                    Globals.player._hp = 200;
                }
            }
        }
        public override void Update()
        {
            
        }
        public override void Draw()
        {
            
        }
    }
}
