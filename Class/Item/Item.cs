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

namespace Game1.Class.Item
{
   public abstract class Item
    {
        public int id{ get; set; }
        public bool _isStackable;
        public byte _stackCapacity;
        public Texture2D _icon;
        public int _price;
        public string _name;
        private float _cooldown { get; set;}
        private float lastTimeUsed;
        public Animation _animation { get; set; }

        public abstract void Use();
        public void Sell()
        {
        }

        public float getCooldown()
        {
            return _cooldown;
        }

        public void DrawInInventory(Rectangle _position)
        {
            Globals.spriteBatch.Draw(_icon, _position, Color.White);
        }

        // public void DrawAnimation(Rectangle position)
        // {
        //     _animation.Draw(position);
        // }
        // public void UpdateAnimation()
        // {
        //     _animation.Update();
        //     
        // }
        //
        // public void Update()
        // {
        //     UpdateAnimation();
        // }

        public abstract void Update();
        
        public bool canUse()
        {
            if (Input.isLeftButtonPressed()/* && Globals.player.inventory.getCurrentItem().getCooldown() +
                Globals.player.inventory.getCurrentItem().getLastTimeUsed() >
                Globals.gameTime.ElapsedGameTime.TotalSeconds */)
            {
                return true;
            }
            return false;
        }

        public void setLastTimeUsed()
        {
            lastTimeUsed = Globals.gameTime.ElapsedGameTime.Seconds;
        }

        public float getLastTimeUsed()
        {
            return lastTimeUsed;
        }

        public abstract void Draw();
    }
}
