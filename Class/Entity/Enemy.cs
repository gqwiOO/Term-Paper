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
        private Animation animation;
        

        public Enemy(Texture2D EnemySprite)
        {
            _sprite = EnemySprite;
            _hp = 100;
            _speed = 4;
            _damage = 20;
            _cooldown = 1;
            _hitBox = new Rectangle(4600,3000 , 100, 100);
            animation = new Animation(EnemySprite, new Vector2(16, 16), 4);
        }
        public override void Update()
        {
            if (_hitBox.Intersects(Globals.player._hitBox))
            {
                if (Globals.gameTime.TotalGameTime.TotalSeconds > lastTimeHitPlayer + _cooldown
                    && Globals.player._hp > 0
                    && !Globals.player._isDead
                    && isDead == false
                    )
                {
                    Globals.player._hp -= (_damage);
                    lastTimeHitPlayer = Globals.gameTime.TotalGameTime.TotalSeconds;
                }
            }
            if (Globals.mouseState.LeftButton == ButtonState.Pressed &&
                Globals.mouseState.X < _hitBox.X + _hitBox.Width &&
                Globals.mouseState.Y < _hitBox.Y + _hitBox.Height &&
                Globals.gameTime.TotalGameTime.TotalSeconds > lastTimeHitEnemy + _cooldown
                && _hp > 0
                && isDead == false)
            {
                _hp -= Globals.player._damage;
                lastTimeHitEnemy = Globals.gameTime.TotalGameTime.TotalSeconds;
            }

            if (_hp <= 0)
            {
                isDead = true;
            }        
            animation.Update();
        }
        
        
        public override void Draw()
        {
            if (Globals.gameState == State.State.Playing && isDead == false)
            {
                // Globals.spriteBatch.Draw(_sprite, _hitBox, Color.White);
                animation.Draw(_hitBox);
            }
            
        }

    }
}
