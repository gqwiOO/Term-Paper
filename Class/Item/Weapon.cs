using System;
using MathL;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Movement;

namespace Game1.Class.Item
{
    public class Weapon : Item
    {
        // Json Fields
        public int damage{ get; set; }
        public float cooldown { get; set;}
        public int animationTime { get; set; }

        private float _currentCooldown;
        private SwordVector _swordVector = new SwordVector(15);
        
        // Can be only Left or Right
        private Entity.Movement _vectorSide;
        
        private bool isActive;
        public bool getACtiveStatus() => isActive;

        // By default uses angle for right side
        private float _swordAngle = -100;
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
            _swordVector.UpdatePosition();
            _hitbox = new RectangleF(_swordVector.getSecondPointVector().X,_swordVector.getSecondPointVector().Y,100,100);
            if (_currentCooldown < cooldown)
            {
                _currentCooldown += (float)Globals.gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            if (_currentCooldown < animationTime)
            {
                isActive = true;
                if (_vectorSide == Entity.Movement.Right)
                {
                    _swordVector.RightSideUpdate();
                    _swordAngle += 7;
                }
                else
                {
                    _swordVector.LeftSideUpdate();
                    _swordAngle -= 7;
                }
            }
            else
            {
                _swordVector.Reset();
                if (_vectorSide == Entity.Movement.Right) _swordAngle = -100;
                else _swordAngle = -80;
                
                isActive = false;
            }
            if (_currentCooldown > cooldown && Input.hasBeenLeftMouseButtonPressed() && !isActive ||
                isActive && _currentCooldown - animationTime > cooldown)
            {
                _vectorSide = Globals.player.lastStrafeDirection;
                _currentCooldown = 0f;
            }
            Console.WriteLine($"X: {_hitbox.Left} Y: {_hitbox.Bottom}");
            _hitbox.rotateRectangleBottomLeftOrigin(45);
        }
        public override void Draw()
        {
            if (isActive)
            {
                _swordVector.Draw();
                if (_vectorSide == Entity.Movement.Right)
                {
                    Globals.spriteBatch.Draw(_sprite, _hitbox.ToRectangle(), null, Color.White,
                        MathHelper.ToRadians(_swordAngle),
                        new Vector2(0,32), SpriteEffects.None, 0f);
                }
                else
                {
                    Globals.spriteBatch.Draw(_sprite, _hitbox.ToRectangle(), null, Color.White,
                        MathHelper.ToRadians(_swordAngle),
                        new Vector2(0,0), SpriteEffects.FlipVertically, 0f);
                }
            }
            _hitbox.Draw();
        }
    }
}
