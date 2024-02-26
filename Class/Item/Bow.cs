using MathL;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Movement;

namespace Game1.Class.Item;

public class Bow: Item
{
     // Json Fields
        public int damage{ get; set; }
        public float cooldown { get; set;}
        public int animationTime { get; set; }

        private float _currentCooldown;

        private bool isActive;
        public bool getACtiveStatus() => isActive;
        private Ammunition ammunition;
        private int _timer = 3000;
        public int LifeSpan = 3000;
        public override void Use()
        {
        }
        public string spritePath
        {
            set
            {
                _sprite = Globals.Content.Load<Texture2D>(value);
            }
        }

        public override void Update()
        {
            if (Globals.gameState == State.State.Playing)
            {
                ammunition = Globals.player.arrow;
                _timer += (int)Globals.gameTime.ElapsedGameTime.TotalMilliseconds;
        
                if (_timer > LifeSpan) ammunition.IsRemoved = true;

                if (Input.hasBeenLeftMouseButtonPressed() && _timer > LifeSpan)
                {
                    ammunition.Position.X = (int)Globals.player._hitBox.X;
                    ammunition.Position.Y = (int)Globals.player._hitBox.Y;
                    ammunition.IsRemoved = false;
                    _timer = 0;
                }
            }
        }

        public override void Draw()
        {

        }
}