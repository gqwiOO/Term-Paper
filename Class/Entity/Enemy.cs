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
        public string name { get; set; }
        private Vector2 namePos;
        public int damage { get; set; }
        
        public int speed { get; set; }

        public int hp { get; set; }
        public int hitboxWidth
        {
            set
            {
                _hitBox.Width = value;
            }
        }
        public int hitboxHeight 
        {     
            set
            {
                _hitBox.Height = value; 
            } 
        }
        public int spawnX
        {
            set
            {
                _hitBox.X = value;
            }
        }
        public int spawnY
        {
            set
            {
                _hitBox.Y = value;
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
        private int _takeDamageTimer;
        private int cooldownTookDamage = 400;
        public bool canBeDamaged;

        public Enemy()
        {
            _damage = 20;
            _attackCooldown = 1000;
        }
        public override void Update()
        {
            _takeDamageTimer += (int)Globals.gameTime.ElapsedGameTime.TotalMilliseconds;
            _attackTime += (int)Globals.gameTime.ElapsedGameTime.TotalMilliseconds;
            if (!isDead && Globals.gameState == State.State.Playing)
            {
                if (hp <= 0)
                {
                    isDead = true;
                    Globals.player._balance += 5;
                }
                
                UpdateCollision();
                UpdateFollowPlayer();
                UpdateTakenDamage();
            }
        }
        private void UpdateFollowPlayer()
        {
            if (_hitBox.getDistance(Globals.player._hitBox) < _visionRange &&
                _hitBox.getDistance(Globals.player._hitBox) > 10)
            {
                if (_hitBox.isDistanceYMoreZero(Globals.player._hitBox))
                {
                    _hitBox.Y += speed * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;
                    direction = Movement.Down;
                    downWalk.Update();
                }
                if (_hitBox.isDistanceYLessZero(Globals.player._hitBox))
                {
                    _hitBox.Y -= speed * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;
                    direction = Movement.Up;
                    upWalk.Update();
                }
                if (_hitBox.isDistanceXMoreZero(Globals.player._hitBox))
                {
                    _hitBox.X += speed * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;
                    direction = Movement.Right;
                    rightWalk.Update();
                }
                if(_hitBox.isDistanceXLessZero(Globals.player._hitBox))
                {
                    _hitBox.X -= speed * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;
                    direction = Movement.Left;
                    leftWalk.Update();
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
            if (_takeDamageTimer > cooldownTookDamage)
            {
                canBeDamaged = true;
            }
        }

        public void TakeDamage(int damage)
        {
            hp -= damage;
            _takeDamageTimer = 0;
            canBeDamaged = false;
        }
        public override void Draw()
        {
            if (Globals.gameState == State.State.Playing || Globals.gameState == State.State.Inventory && !isDead)
            {
                DrawHP();
                switch (direction)
                {
                    case Movement.Left: leftWalk.Draw(_hitBox.ToRectangle()); break;
                    case Movement.Right: rightWalk.Draw(_hitBox.ToRectangle()); break;
                    case Movement.Up: upWalk.Draw(_hitBox.ToRectangle()); break;
                    case Movement.Down: downWalk.Draw(_hitBox.ToRectangle()); break;
                }
            }
        }
        private void DrawHP()
        {
                Globals.spriteBatch.DrawString(Font.fonts["MainFont-16"], $"HP: {this.hp}",new Vector2(this._hitBox.X,this._hitBox.Y - 40),
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
