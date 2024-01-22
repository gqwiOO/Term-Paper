using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.Devices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;

namespace Game1.Class.Item
{
    public class Weapon : Item
    {

        public int damage{ get; set; }
        public float cooldown{ get; set; }
        public int animationFrameWidth{ get; set; }
        public int animationFrameHeight{ get; set; }
        public int frameCount{ get; set; }
        public float frameTime{ get; set; }
        
        public Animation attackAnimation{ get; set; }

        private Vector2 _leftPosition;
        private Vector2 _rightPosition;
        private Vector2 _currentPosition;
        private int attackDuration = 1;
        private double lastTimeUsed;

        private int diffX;
        private int currDiffX;

        public string attackAnimationPath
        {
            set
            {
                _animation = new Animation(Globals.Content.Load<Texture2D>(value),
                    new Vector2(animationFrameWidth, animationFrameHeight), frameCount, frameTime);
            }
        }

        public string iconPath
        {
            set
            {
                _icon = Globals.Content.Load<Texture2D>(value);
            }
        }


        public override void Use()
        {
            
        }
        
        public Weapon()
        {
        }

        public override void Update()
        {
            _leftPosition = new Vector2(Globals.player._hitBox.X + 4, Globals.player._hitBox.Y - 15);
            _rightPosition = new Vector2(Globals.player._hitBox.X + 30, Globals.player._hitBox.Y - 15);
            diffX = (int)_rightPosition.X - (int)_leftPosition.X;
            if (Globals.player.getDirection() == Entity.Movement.Right)
            {
                currDiffX = (int)_rightPosition.X - (int)_currentPosition.X;
            }
        }

        public override void Draw()
        {
            if (Globals.player.inventory.getCurrentItem().canUse())
            {
                Globals.spriteBatch.Draw(_icon,new Rectangle((int)_rightPosition.X, (int)_rightPosition.Y, 64,64), Color.White);
            }
        }
    }
}
