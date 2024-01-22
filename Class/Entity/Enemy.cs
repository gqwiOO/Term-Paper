using MathL;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Game1.Class.Entity
{
    public class Enemy: Entity
    {
        public float _cooldown;
        private double lastTimeHitPlayer;
        private double lastTimeHitEnemy;
        private Animation animation;
        private float _visionRange = 500;
        
        public Enemy(Texture2D EnemySprite)
        {
            _sprite = EnemySprite;
            _hp = 100;
            _speed = 200;
            _damage = 20;
            _cooldown = 1;
            _hitBox = new RectangleF(4600,3000 , 100, 100);
            animation = new Animation(EnemySprite, new Vector2(16, 16), 4, 0.3f);
        }
        public override void Update()
        {
            
            if (!isDead && Globals.gameState == State.State.Playing)
            {
                if (_hp <= 0)
                {
                    isDead = true;
                }      
                UpdateCollision();
                UpdateFollowPlayer();
                animation.Update();
            }
            
        }
        
        /// <summary>
        /// 
        /// </summary>
        private void UpdateFollowPlayer()
        {
            if (_hitBox.getDistance(Globals.player._hitBox) < _visionRange &&
                _hitBox.getDistance(Globals.player._hitBox) > 10)
            {
                if (_hitBox.isDistanceXMoreZero(Globals.player._hitBox))
                {
                    _hitBox.X += _speed * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;
                }
                if(_hitBox.isDistanceXLessZero(Globals.player._hitBox))
                {
                    _hitBox.X -= _speed * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;
                }
                if (_hitBox.isDistanceYMoreZero(Globals.player._hitBox))
                {
                    _hitBox.Y += _speed * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;
                }
                if (_hitBox.isDistanceYLessZero(Globals.player._hitBox))
                {
                    _hitBox.Y -= _speed * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;
                }
            }
        }
        private void UpdateCollision()
        {
            if (_hitBox.Intersects(Globals.player._hitBox))
            {
                if (Globals.gameTime.TotalGameTime.TotalSeconds > lastTimeHitPlayer + _cooldown
                    && Globals.player._hp > 0
                    && !Globals.player.isDead
                   )
                {
                    Globals.player._hp -= (_damage);
                    lastTimeHitPlayer = Globals.gameTime.TotalGameTime.TotalSeconds;
                }
            }
        }
        public override void Draw()
        {
            if (Globals.gameState == State.State.Playing)
            {
                animation.Draw(_hitBox.ToRectangle());
            }
        }
    }
}
