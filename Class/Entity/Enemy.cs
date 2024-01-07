using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Class.Entity
{
    public class Enemy: Entity
    {
        public Rectangle _hitBox;
        public double _cooldown;
        private double lastTimeHitPlayer;
        
        public Enemy(Texture2D EnemySprite)
        {
            _sprite = EnemySprite;
            _speed = 4;
            _damage = 20;
            _cooldown = 1;
            _hitBox = new Rectangle(1500,700 , 100, 100);
        }
        public override void Update(GameTime gameTime, Player player)
        {
            if (_hitBox.Intersects(player._hitBox))
            {
                if (gameTime.TotalGameTime.TotalSeconds > lastTimeHitPlayer + _cooldown
                    && player._hp > 0
                    && !player._isDead
                    )
                {
                    player._hp -= (_damage);
                    lastTimeHitPlayer = gameTime.TotalGameTime.TotalSeconds;
                }
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Game1._state == State.State.Playing)
            {
                spriteBatch.Draw(_sprite, _hitBox, Color.White);
            }
        }

        public void ForwardPlayer(Player player)
        {
            
        }
    }
}
