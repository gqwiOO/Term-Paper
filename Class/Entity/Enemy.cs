using System;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;

namespace Game1.Class.Entity
{
    public class Enemy: Entity
    {
        public Rectangle _hitBox;
        public float _cooldown;
        private double lastTimeHitPlayer;
        public bool isDead = false;
        private double lastTimeHitEnemy;
        

        public Enemy(Texture2D EnemySprite)
        {
            _sprite = EnemySprite;
            _hp = 100;
            _speed = 4;
            _damage = 20;
            _cooldown = 1;
            _hitBox = new Rectangle(4600,3000 , 100, 100);
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
                    player._hitBox.X -= 100; 
                    lastTimeHitPlayer = gameTime.TotalGameTime.TotalSeconds;
                }
            }
            Console.WriteLine(_hp);
            Console.WriteLine(Game1._mouseState.X > _hitBox.X);
            if (Game1._mouseState.LeftButton == ButtonState.Pressed &&
                Game1._mouseState.X < _hitBox.X + _hitBox.Width &&
                Game1._mouseState.X > _hitBox.X &&
                Game1._mouseState.Y < _hitBox.Y + _hitBox.Height &&
                gameTime.TotalGameTime.TotalSeconds > lastTimeHitEnemy + _cooldown
                && _hp > 0
                && isDead == false)
            {
                _hp -= player._damage;
                lastTimeHitEnemy = gameTime.TotalGameTime.TotalSeconds;
            }

            if (_hp <= 0)
            {
                isDead = true;
            }              
        }
        
        
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Game1._state == State.State.Playing && isDead == false)
            {
                spriteBatch.Draw(_sprite, _hitBox, Color.White);
            }
        }

    }
}
