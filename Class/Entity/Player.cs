using MathL;
using Menu;
using Microsoft.Xna.Framework.Graphics;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Movement;


namespace Game1.Class.Entity
{
    public class Player: Entity 
    {
        public int _balance ;
        
        public Inventory inventory;
        public Vector2 Position;

        private Animation downWalk;
        private Animation upWalk;
        private Animation leftWalk;
        private Animation rightWalk;
        private Texture2D idle;
        private Movement direction;
        
        private float _runningSpeed = 400;

        private Vector2 _weaponPosLeft;
        private Vector2 _weaponPosright;
        public Player(Texture2D downWalkTexture, Texture2D upWalkTexture,
                      Texture2D leftWalkTexture, Texture2D rightWalkTexture, Texture2D idleTexture
                      )
        {
            _hp = 200;
            _speed = 300;
            _damage = 20;

            inventory = new Inventory();
            _hitBox = new RectangleF(4864, 3220, 64, 64);

            _weaponPosLeft = new Vector2(_hitBox.X + 0.2f * _hitBox.Width, _hitBox.Y + 0.7f * _hitBox.Height);
            _weaponPosright = new Vector2(_hitBox.X + 0.8f * _hitBox.Width, _hitBox.Y + 0.7f * _hitBox.Height);
            
            downWalk = new Animation(downWalkTexture, new Vector2(16, 16), 4, 0.2f);
            upWalk = new Animation(upWalkTexture, new Vector2(16, 16), 4, 0.2f);
            leftWalk = new Animation(leftWalkTexture, new Vector2(16, 16), 4, 0.2f);
            rightWalk = new Animation(rightWalkTexture, new Vector2(16, 16), 4, 0.2f);
            idle = idleTexture;
        }
        public override void Update()
        {
            if (Globals.gameState == State.State.Playing && isDead != true)
            {
                if (_hp <= 0)
                {
                    isDead = true;
                }
                UpdateMovement();
                inventory.Update();
            }
        }
        private void UpdateMovement()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                {
                    _hitBox.Y -= _runningSpeed * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;
                    upWalk.Update();
                }
                else
                {
                    _hitBox.Y -= _speed * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;
                }
                direction = Movement.Up;
                upWalk.Update();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                {
                    _hitBox.Y += _runningSpeed * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;
                    downWalk.Update();
                }
                else
                {
                    _hitBox.Y += _speed * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;
                }
                direction = Movement.Down;
                downWalk.Update();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                {
                    _hitBox.X -= _runningSpeed * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;
                    leftWalk.Update();
                }
                else
                {
                    _hitBox.X -= _speed * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;
                }
                direction = Movement.Left;
                leftWalk.Update();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                {
                    _hitBox.X += _runningSpeed * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;
                    rightWalk.Update();
                }
                else
                {
                    _hitBox.X += _speed * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;
                }
                direction = Movement.Right;
                rightWalk.Update();
            }
            if (Keyboard.GetState().IsKeyUp(Keys.D) && Keyboard.GetState().IsKeyUp(Keys.A) &&
                Keyboard.GetState().IsKeyUp(Keys.S) && Keyboard.GetState().IsKeyUp(Keys.W))
            {
                direction = Movement.Idle;
            }
        }

        public override void Draw()
        {
            if (Globals.gameState == State.State.Playing && !isDead)
            {
                if(direction == Movement.Down)downWalk.Draw(this._hitBox.ToRectangle());
                else if(direction == Movement.Up)upWalk.Draw(this._hitBox.ToRectangle());
                else if(direction == Movement.Left)leftWalk.Draw(this._hitBox.ToRectangle());
                else if(direction == Movement.Right)rightWalk.Draw(this._hitBox.ToRectangle());
                else if (direction == Movement.Idle) Globals.spriteBatch.Draw(idle, _hitBox.ToRectangle(), Color.White);
                
            }
            if(inventory.getCurrentItem() != null)inventory.getCurrentItem().Draw();
            
        }

        public void Revive()
        {
            isDead = false;
            _hp = 200;
            _hitBox.X = 4864;
            _hitBox.Y = 3220;
        }

        public void Sell(int cost)
        {
            _balance += cost;
        }

        public Movement getDirection()
        {
            return direction;
        }
    }


    
}
