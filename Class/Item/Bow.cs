using System;
using Microsoft.Xna.Framework.Graphics;
using Movement;
using SharpDX;

namespace Game1.Class.Item;

public class Bow: Item
{
     // Json Fields
        public int damage{ get; set; }
        public float cooldown { get; set;}
        public int animationTime { get; set; }

        private int _arrowFlyingDuration = 5000;
        private int _timer = 0;
        
        private bool isActive;

        private Arrow arrow;
        public bool getACtiveStatus() => isActive;
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
                if (Input.hasBeenLeftMouseButtonPressed())
                {
                    arrow = new Arrow(MathL.MathL.GetUnitVector2Mouse());
                }

                if (arrow != null)
                {
                    _timer += (int)Globals.gameTime.ElapsedGameTime.TotalMilliseconds;
                    arrow.MoveArrow();
                    if (_timer > _arrowFlyingDuration)
                    {
                        _timer = 0;
                        arrow = null;
                    }
                }
            }
        }

        
        public override void Draw()
        {
            if (arrow != null)
            {
                arrow.Draw();
                Debug.Debug.DrawInfo(_timer.ToString(), 100);
            }
        }
}