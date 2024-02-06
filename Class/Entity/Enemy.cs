using Game1.Class.Item;
using MathL;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Audio;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using TermPaper.Class.Font;

namespace Game1.Class.Entity
{
    public class Enemy: Entity
    {
        private Animation animation;
        private float _visionRange = 500;
        
        // Attack Time
        private int _attackCooldown;
        private int _attackTime;
        
        // Getting Damage
        private int tookDamageTime;
        private int cooldownTookDamage = 600;

        public Enemy(Texture2D EnemySprite)
        {
            _sprite = EnemySprite;
            _hp = 100;
            _speed = 200;
            _damage = 20;
            _attackCooldown = 1000;
            _hitBox = new RectangleF(4600,3000 , 100, 100);
            animation = new Animation(EnemySprite, new Vector2(16, 16), 4, 0.3f);
        }
        public override void Update()
        {
            tookDamageTime += (int)Globals.gameTime.ElapsedGameTime.TotalMilliseconds;
            _attackTime += (int)Globals.gameTime.ElapsedGameTime.TotalMilliseconds;
            if (!isDead && Globals.gameState == State.State.Playing)
            {
                if (_hp <= 0)isDead = true;
                
                UpdateCollision();
                UpdateFollowPlayer();
                UpdateTakenDamage();
                animation.Update();
            }
        }
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
                if (_attackTime > _attackCooldown && Globals.player._hp > 0  && !Globals.player.isDead)
                {
                    Globals.player._hp -= _damage;
                    Sound.PlaySoundEffect("hurt", 1.0f);
                    _attackTime = 0;
                }
            }
        }
        private void UpdateTakenDamage()
        {
            Weapon currentWeapon = (Weapon)Globals.player.inventory.getCurrentItem();
            if (_hitBox.Intersects(currentWeapon._hitbox) && tookDamageTime > cooldownTookDamage &&
                _hitBox.Intersects(currentWeapon._hitbox) && currentWeapon.getACtiveStatus())
            {
                _hp -= 10;
                tookDamageTime = 0;
            }
        }
        public override void Draw()
        {
            if (Globals.gameState == State.State.Playing  || Globals.gameState == State.State.Inventory)
            {
                animation.Draw(_hitBox.ToRectangle());
                Globals.spriteBatch.DrawString(Font.fonts["MainFont-16"], $"HP: {this._hp}",new Vector2(this._hitBox.X,this._hitBox.Y - 40),
                    Color.Black);
            }
        }
    }
}
