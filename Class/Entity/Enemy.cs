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
        
        private Vector2 _nameTextPosition;
        private Animation leftWalk;
        private Animation rightWalk;
        private Animation upWalk;
        private Animation downWalk;
        
        private float _visionRange = 500;

        private Direction direction;
        
        // Attack Time
        public int attackCooldown { get; set; }
        private int _attackTime;
        
        // Getting Damage
        private int _takeDamageTimer;
        private int cooldownTookDamage = 125;
        public bool canBeDamaged { get; private set; }
        public string name { get; set; }
        
        public override void Update()
        {
            _takeDamageTimer += (int)Globals.gameTime.ElapsedGameTime.TotalMilliseconds;
            _attackTime += (int)Globals.gameTime.ElapsedGameTime.TotalMilliseconds;
            if (!isDead && Globals.gameState == State.State.Playing)
            {
                if (hp <= 0)
                {
                    isDead = true;
                }
                
                UpdateCollision();
                UpdateFollowPlayer();
                UpdateTakenDamage();
            }
        }
        private void UpdateFollowPlayer()
        {
            if (_hitBox.getDistance(Globals.Player._hitBox) < _visionRange &&
                _hitBox.getDistance(Globals.Player._hitBox) > 10)
            {
                if (_hitBox.isDistanceYMoreZero(Globals.Player._hitBox))
                {
                    _hitBox.Y += speed * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;
                    direction = Direction.Down;
                    downWalk.Update();
                }
                if (_hitBox.isDistanceYLessZero(Globals.Player._hitBox))
                {
                    _hitBox.Y -= speed * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;
                    direction = Direction.Up;
                    upWalk.Update();
                }
                if (_hitBox.isDistanceXMoreZero(Globals.Player._hitBox))
                {
                    _hitBox.X += speed * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;
                    direction = Direction.Right;
                    rightWalk.Update();
                }
                if(_hitBox.isDistanceXLessZero(Globals.Player._hitBox))
                {
                    _hitBox.X -= speed * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;
                    direction = Direction.Left;
                    leftWalk.Update();
                }
            }
        }
        private void UpdateCollision()
        {
            if (_hitBox.Intersects(Globals.Player._hitBox))
            {
                if (_attackTime > attackCooldown && Globals.Player._hp > 0  && !Globals.Player.isDead)
                {
                    Globals.Player._hp -= damage;
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
                    case Direction.Left: leftWalk.Draw(_hitBox.ToRectangle()); break;
                    case Direction.Right: rightWalk.Draw(_hitBox.ToRectangle()); break;
                    case Direction.Up: upWalk.Draw(_hitBox.ToRectangle()); break;
                    case Direction.Down: downWalk.Draw(_hitBox.ToRectangle()); break;
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
