using System;
using Menu;
using Microsoft.Xna.Framework.Graphics;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TermPaper.Class.Audio;


namespace Game1.Class.Entity
{
    public class Player: Entity 
    {
        public Texture2D _sprite;
        public bool _isDead;
        SpriteEffects s = SpriteEffects.FlipHorizontally;
        public int _balance ;
        public Vector2 Position;
        public float _cooldown;
        public float _stamina;
        private double lastTimeSprint;
        public float _sprintCooldown;
        public int _runningSpeed;

        private Animation downWalk;
        private Animation upWalk;
        private Animation leftWalk;
        private Animation rightWalk;
        private Texture2D idle;
        private Movement currentDirection;
        private Movement previousDirection;
        public Player(Texture2D downWalkTexture, Texture2D upWalkTexture,
                      Texture2D leftWalkTexture, Texture2D rightWalkTexture, Texture2D idleTexture
                      )
        {
            _hp = 200;
            _speed = 4;
            _damage = 20;
            _cooldown = 1f;
            _stamina = 100;
            _sprintCooldown = 0.1f;
            _runningSpeed = 5;
            
            
            
            _hitBox = new Rectangle(4864, 3220, 64, 64);
            downWalk = new Animation(downWalkTexture, new Vector2(16, 16), 4, 0.2f);
            upWalk = new Animation(upWalkTexture, new Vector2(16, 16), 4, 0.2f);
            leftWalk = new Animation(leftWalkTexture, new Vector2(16, 16), 4, 0.2f);
            rightWalk = new Animation(rightWalkTexture, new Vector2(16, 16), 4, 0.2f);
            idle = idleTexture;
        }
        public override void Update()
        {
            if (Globals.gameState == State.State.Playing && _isDead != true)
            {
                if (_hp <= 0)
                {
                    _isDead = true;
                }
                // Movement 
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    previousDirection = currentDirection;
                    currentDirection = Movement.Up;
                    _hitBox.Y -= _speed;
                    upWalk.Update();
                    if (Keyboard.GetState().IsKeyDown(Keys.LeftShift)&& _stamina > 0 )
                    {
                        _hitBox.Y -= _runningSpeed;
                        _stamina -= 20 * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;                    }
                }
                if (Microsoft.Xna.Framework.Input.Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    previousDirection = currentDirection;
                    currentDirection = Movement.Down;
                    _hitBox.Y += _speed;
                    if (Keyboard.GetState().IsKeyDown(Keys.LeftShift)&& _stamina > 0)
                    {
                        _hitBox.Y += _runningSpeed;
                        _stamina -= 20 * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;                    }
                    downWalk.Update();

                }
                if (Keyboard.GetState().IsKeyDown(Keys.A) )
                {
                    previousDirection = currentDirection;
                    currentDirection = Movement.Left;
                    leftWalk.Update();
                   _hitBox.X -= _speed;
                   if (Keyboard.GetState().IsKeyDown(Keys.LeftShift)&& _stamina > 0)
                   {
                       _hitBox.X -= _runningSpeed;
                       _stamina -= 20 * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;
                   }
                }
                if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    previousDirection = currentDirection;
                    currentDirection = Movement.Right;
                    rightWalk.Update();
                    _hitBox.X += _speed;
                    if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) && _stamina > 0)
                    {
                      _hitBox.X += _runningSpeed;
                      _stamina -= 20 * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;                    }
                }
                if (Keyboard.GetState().IsKeyUp(Keys.D) && Keyboard.GetState().IsKeyUp(Keys.A) &&
                    Keyboard.GetState().IsKeyUp(Keys.S) && Keyboard.GetState().IsKeyUp(Keys.W))
                {
                    currentDirection = Movement.Idle;
                }
                if (Keyboard.GetState().IsKeyUp(Keys.LeftShift) && Globals.gameTime.TotalGameTime.TotalSeconds > lastTimeSprint + _sprintCooldown)
                {
                    _stamina += 1;
                    lastTimeSprint = Globals.gameTime.TotalGameTime.TotalSeconds;
                }
            }
        }
        public override void Draw()
        {
            if (Globals.gameState == State.State.Playing && !_isDead ||Globals.gameState == State.State.Inventory)
            {
                if(currentDirection == Movement.Down)downWalk.Draw(this._hitBox);
                else if(currentDirection == Movement.Up)upWalk.Draw(this._hitBox);
                else if(currentDirection == Movement.Left)leftWalk.Draw(this._hitBox);
                else if(currentDirection == Movement.Right)rightWalk.Draw(this._hitBox);
                else if (currentDirection == Movement.Idle) Globals.spriteBatch.Draw(idle, _hitBox, Color.White);
            }
        }

        public void Revive()
        {
            _isDead = false;
            _hp = 200;
            _hitBox.X = 4864;
            _hitBox.Y = 3220;
            Sound._spawnSound.Play();
        }

        public void Sell(int cost)
        {
            _balance += cost;
        }
    }


    public enum Movement
    {
        Left,
        Right,
        Up,
        Down,
        Idle
    }
}
