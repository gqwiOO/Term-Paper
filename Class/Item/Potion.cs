﻿using Microsoft.Xna.Framework.Graphics;

namespace Game1.Class.Item
{
    public class Potion: Item
    {
        public string name { get; set; }
        public int restoredHealth { get; set; }
        public int cooldown { get; set; }

        public int stackCapacity
        {
            get
            {
                return _stackCapacity;
            }
            set
            {
                _stackCapacity = value;
                _isStackable = true;
            }
        }

        private string path;
        public string spritePath
        {
            get
            {
                return path;
            }
            set
            {
                path = value;
                _sprite = Globals.Content.Load<Texture2D>(value);
            }
        }

        public override void Use()
        {
            if (Globals.player._hp < 200 &&  !Globals.player.isDead)
            {
                Globals.player._hp += restoredHealth;
                if (Globals.player.inventory.getHealthPotionIndex() != null)
                {
                    Globals.player.inventory.decreaseItemAmountByOne((int)Globals.player.inventory.getHealthPotionIndex());
                }
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
