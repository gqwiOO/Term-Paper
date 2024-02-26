using System;
using Game1.Class.Item;
using MathL;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Audio;
using TermPaper.Class.Font;

namespace Game1.Class.Entity
{
    public class Enemy: Entity
    {
        public RectangleF hitbox = new RectangleF(0, 0, 50, 50);
        public string name { get; set; }
        private Vector2 namePos;
        public int damage { get; set; }
        
        public int speed { get; set; }
        public int hitboxWidth
        {
            set
            {
                hitbox.Width = value;
            }
        }
        public int hitboxHeight 
        {     
            set
            {
                hitbox.Height = value; 
            } 
        }
        public int spawnX
        {
            set
            {
                hitbox.X = value;
                spawnPoint.X = value;
            }
        }
        public int spawnY
        {
            set
            {
                hitbox.Y = value;
                spawnPoint.Y = value;
            }
        }
        public int frameCount { get; set; }
        public float frameTime { get; set; }
        public int animationFrameWidth { get; set; }
        public int animationFrameHeight { get; set; }

        public string WalkLeftAnimationPath { get; set; }
        public string WalkRightAnimationPath { get; set; }
        public string WalkUpAnimationPath { get; set; }
        public string WalkDownAnimationPath { get; set; }
        public string IdlePath { get; set; }
        public Vector2 spawnPoint = new Vector2(4000, 3000);
        
        public Animation leftWalk;
        private Animation rightWalk;
        private Animation upWalk;
        private Animation downWalk;
        
        private float _visionRange = 500;

        private Movement direction;
        
        // Attack Time
        private int _attackCooldown;
        private int _attackTime;
        
        // Getting Damage
        private int tookDamageTime;
        private int cooldownTookDamage = 600;

        public Enemy(Texture2D EnemySprite)
        {
            _hp = 100;
            _speed = 200;
            _damage = 20;
            _attackCooldown = 1000;
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
            }
        }
        private void UpdateFollowPlayer()
        {
            if (hitbox.getDistance(Globals.player._hitBox) < _visionRange &&
                hitbox.getDistance(Globals.player._hitBox) > 10)
            {
                if (hitbox.isDistanceYMoreZero(Globals.player._hitBox))
                {
                    hitbox.Y += _speed * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;
                    direction = Movement.Down;
                    downWalk.Update();
                }
                if (hitbox.isDistanceYLessZero(Globals.player._hitBox))
                {
                    hitbox.Y -= _speed * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;
                    direction = Movement.Up;
                    upWalk.Update();
                }
                if (hitbox.isDistanceXMoreZero(Globals.player._hitBox))
                {
                    hitbox.X += _speed * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;
                    direction = Movement.Right;
                    rightWalk.Update();
                }
                if(hitbox.isDistanceXLessZero(Globals.player._hitBox))
                {
                    hitbox.X -= _speed * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;
                    direction = Movement.Left;
                    leftWalk.Update();
                }
            }
        }
        private void UpdateCollision()
        {
            if (hitbox.Intersects(Globals.player._hitBox))
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
            if (Globals.player.inventory.getCurrentItem() != null)
            {
                if (Globals.player.inventory.getCurrentItem().GetType().Equals(typeof(Weapon)))
                {
                    Weapon currentWeapon = (Weapon)Globals.player.inventory.getCurrentItem();
                    SwordVector vector = currentWeapon.getSwordVector();

                    if (vector.CollisionWithRectangle(hitbox.X, hitbox.Y, hitbox.Width, hitbox.Height) && currentWeapon.getACtiveStatus() && tookDamageTime > cooldownTookDamage)
                    {
                        _hp -= 20;
                        tookDamageTime = 0;
                    }
                }
            }
            if (hitbox.Intersects(Globals.player.arrow.Position) && tookDamageTime > cooldownTookDamage)
            {
                _hp -= 20;
                tookDamageTime = 0;
            }
        }
        public override void Draw()
        {
            if (Globals.gameState == State.State.Playing || Globals.gameState == State.State.Inventory)
            {
                DrawHP();
                switch (direction)
                {
                    case Movement.Left: leftWalk.Draw(hitbox.ToRectangle()); break;
                    case Movement.Right: rightWalk.Draw(hitbox.ToRectangle()); break;
                    case Movement.Up: upWalk.Draw(hitbox.ToRectangle()); break;
                    case Movement.Down: downWalk.Draw(hitbox.ToRectangle()); break;
                }
            }
        }
        private void DrawHP()
        {
                Globals.spriteBatch.DrawString(Font.fonts["MainFont-16"], $"HP: {this._hp}",new Vector2(this.hitbox.X,this.hitbox.Y - 40),
                    Color.Black);
        }

        public void LoadAnimation()
        {
            leftWalk = new Animation(Globals.Content.Load<Texture2D>(WalkLeftAnimationPath),
                new Vector2( animationFrameHeight, animationFrameWidth), frameCount, frameTime);
            rightWalk = new Animation(Globals.Content.Load<Texture2D>(WalkRightAnimationPath),
                new Vector2(animationFrameHeight, animationFrameWidth), frameCount, frameTime);
            upWalk = new Animation(Globals.Content.Load<Texture2D>(WalkUpAnimationPath),
                new Vector2(animationFrameHeight, animationFrameWidth), frameCount, frameTime);
            downWalk = new Animation(Globals.Content.Load<Texture2D>(WalkDownAnimationPath),
                new Vector2(animationFrameHeight, animationFrameWidth), frameCount, frameTime);
        }
    }
}
