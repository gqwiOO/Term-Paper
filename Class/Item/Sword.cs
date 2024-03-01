using System.Linq;
using Data;
using Game1.Class.Entity;
using MathL;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Movement;

namespace Game1.Class.Item
{
    public class Sword : Item
    {
        public Sword()
        {
            _hitbox = new RectangleF(0, 0, 100, 100);
            _swordVector = new SwordVector(141, 0, new Vector2(0, 0));
        }
        // Json Fields
        public int damage{ get; set; }
        public float cooldown { get; set;}
        public int animationTime { get; set; }
        
        private float _currentCooldown;
        private SwordHandVector _swordHandVector = new SwordHandVector(15);
        private SwordVector _swordVector;

        public SwordVector getSwordVector() => _swordVector;
        
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
            if (Globals.gameState == State.State.Playing)
            {
                _swordHandVector.UpdatePosition();
                _hitbox = new RectangleF(_swordHandVector.getSecondPointVector().X, _swordHandVector.getSecondPointVector().Y,
                    100, 100);
                
                _swordVector.Update(_swordHandVector.getSecondPointVector());
                if (_currentCooldown < cooldown)
                {
                    _currentCooldown += (float)Globals.gameTime.ElapsedGameTime.TotalMilliseconds;
                }
                if (_currentCooldown < animationTime)
                {
                    isActive = true;
                    if (_vectorSide == Entity.Movement.Right)
                    {
                        _swordHandVector.RightSideUpdate();
                        _swordAngle += 7;
                        _swordVector.Rotate((int)_swordAngle- 90);

                    }
                    else
                    {
                        _swordVector.Rotate((int)_swordAngle - 90);
                        _swordHandVector.LeftSideUpdate();
                        _swordAngle -= 7;
                    }
                }
                else
                {
                    _swordHandVector.Reset();
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
                IntersectsEnemy();
            }
        }

        
        public override void Draw()
        {
            if (Globals.gameState == State.State.Playing)
            {
                if (isActive)
                {
                    if (_vectorSide == Entity.Movement.Right)
                    {
                        Globals.spriteBatch.Draw(_sprite, _hitbox.ToRectangle(), null, Color.White,
                            MathHelper.ToRadians(_swordAngle),
                            new Vector2(0, 32), SpriteEffects.None, 0f);
                    }
                    else
                    {
                        Globals.spriteBatch.Draw(_sprite, _hitbox.ToRectangle(), null, Color.White,
                            MathHelper.ToRadians(_swordAngle),
                            new Vector2(0, 0), SpriteEffects.FlipVertically, 0f);
                    }
                }
            }
        }
        
        
        public bool IntersectsEnemy()
        {
            foreach (var enemy in Entities.entities.Where(npc => npc.GetType().Equals(typeof(Enemy))).ToList())
            {
                Sword currentSword = (Sword)Globals.player.inventory.getCurrentItem();
                SwordVector vector = currentSword.getSwordVector();
                
                if (vector.CollisionWithRectangle(enemy._hitBox.X, enemy._hitBox.Y, enemy._hitBox.Width, enemy._hitBox.Height) && currentSword.getACtiveStatus() && ((Enemy)enemy).canBeDamaged )
                {
                    ((Enemy)enemy).TakeDamage(20);
                    return true;
                }
            }
            return false;
        }
    }
}