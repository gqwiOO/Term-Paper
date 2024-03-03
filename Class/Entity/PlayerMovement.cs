using Audio;
using MathL;
using Microsoft.Xna.Framework.Input;
using Movement;

namespace Game1.Class.Entity;

class PlayerMovement
    {
        private RectangleF _hitbox;
        private Direction _direction;
        private Direction lastStrafeDirection = Direction.Left;
        private readonly float _walkSpeed;
        private readonly float _runSpeed;
        
        public RectangleF Hitbox => _hitbox;
        public Direction Direction => _direction;
        public Direction LastStrafeDirection => lastStrafeDirection;
        public bool isRunning { get; private set; }

        public PlayerMovement( RectangleF hitbox, float walkSpeed, float runSpeed)
        {
            _direction = Direction.Left;
            _hitbox = hitbox;
            _walkSpeed = walkSpeed;
            _runSpeed = runSpeed;
        }

        
        public void Update(ref float stamina)
        {
            if (Input.isPressed(Keys.W))
            {
                if (Input.isPressed(Keys.LeftShift) && stamina >= 10)
                {
                    _hitbox.Y -= _runSpeed * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;
                    // If player moves in left or right, stamina will decrease only in if statement with Left or Right but not in Up or Down
                    if (_direction != Direction.Left && _direction != Direction.Right)
                    {
                        stamina -= 20 * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;
                    }

                    isRunning = true;
                }
                else
                {
                    _hitbox.Y -= _walkSpeed * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;
                    isRunning = false;
                }
                _direction = Direction.Up;
                Sound.PlaySoundEffect("walkingSound", 0.1f);
            }
            if (Input.isPressed(Keys.S))
            {
                if (Input.isPressed(Keys.LeftShift) && stamina >= 10)
                {
                    _hitbox.Y += _runSpeed * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;
                    // Double Update() for faster(x2) animation
                    // If player moves in left or right, stamina will decrease only in if statement with Left or Right but not in Up or Down
                    if (_direction != Direction.Left && _direction != Direction.Right)
                    {
                        if(stamina >= 10)stamina -= 20 * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;
                    }

                    isRunning = true;
                }
                else
                {
                    _hitbox.Y += _walkSpeed * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;
                    isRunning = false;
                }
                
                _direction = Direction.Down;
                Sound.PlaySoundEffect("walkingSound", 0.1f);
            }
            if (Input.isPressed(Keys.A))
            {
                if (Input.isPressed(Keys.LeftShift) && stamina >= 10)
                {
                    _hitbox.X -= _runSpeed * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;
                    stamina -= 20 * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;
                    isRunning = true;
                }
                else
                {
                    _hitbox.X -= _walkSpeed * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;
                    isRunning = false;
                }
                _direction = Direction.Left;
                lastStrafeDirection = Direction.Left;
                Sound.PlaySoundEffect("walkingSound", 0.1f);
            }
            if (Input.isPressed(Keys.D))
            {
                if (Input.isPressed(Keys.LeftShift) && stamina >= 10)
                {
                    _hitbox.X += _runSpeed * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;
                    stamina -= 20 * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;
                    isRunning = true;
                }
                else
                {
                    _hitbox.X += _walkSpeed * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;
                    isRunning = false;
                }
                _direction = Direction.Right;
                lastStrafeDirection = Direction.Right;
                Sound.PlaySoundEffect("walkingSound", 0.1f);
            }
            if (Keyboard.GetState().IsKeyUp(Keys.D) && Keyboard.GetState().IsKeyUp(Keys.A) &&
                Keyboard.GetState().IsKeyUp(Keys.S) && Keyboard.GetState().IsKeyUp(Keys.W))
            {
                _direction = Direction.Idle;
                isRunning = false;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.LeftShift))
            {
                if (stamina < 100f)
                {
                    stamina += 20f * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;
                }
            }
        }
    }