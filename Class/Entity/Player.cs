using Menu;
using Microsoft.Xna.Framework.Graphics;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace Game1.Class.Entity
{
    public class Player: Entity 
    {
        public Texture2D _sprite;
        public bool _isDead;
        public Inventory inventory;
        SpriteEffects s = SpriteEffects.FlipHorizontally;
        public int _balance ;
        public Vector2 Position;
        public float _cooldown;

        private Animation downWalk;
        private Animation upWalk;
        private Animation leftWalk;
        private Animation rightWalk;
        private Texture2D idle;
        private Movement direction;
        public Player(Texture2D downWalkTexture, Texture2D upWalkTexture,
                      Texture2D leftWalkTexture, Texture2D rightWalkTexture, Texture2D idleTexture
                      )
        {
            _hp = 200;
            _speed = 4;
            _damage = 20;
            _cooldown = 1f;
            
            
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
                    direction = Movement.Up;
                    _hitBox.Y -= _speed;
                    upWalk.Update();
                    if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                    {
                        _hitBox.Y -= _speed + 1;
                    }
                }
                if (Microsoft.Xna.Framework.Input.Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    direction = Movement.Down;
                    _hitBox.Y += _speed;
                    if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                    {
                        _hitBox.Y += _speed + 1;
                    }
                    downWalk.Update();

                }
                if (Keyboard.GetState().IsKeyDown(Keys.A) )
                {
                    direction = Movement.Left;
                    leftWalk.Update();
                   _hitBox.X -= _speed;
                   if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                   {
                       _hitBox.X -= _speed + 1;
                   }
                }
                if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    direction = Movement.Right;
                    rightWalk.Update();
                    _hitBox.X += _speed;
                    if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                    {
                      _hitBox.X += _speed + 1;
                    }
                }

                if (Keyboard.GetState().IsKeyUp(Keys.D) && Keyboard.GetState().IsKeyUp(Keys.A) &&
                    Keyboard.GetState().IsKeyUp(Keys.S) && Keyboard.GetState().IsKeyUp(Keys.W))
                {
                    direction = Movement.Idle;
                }
            }
        }
        public override void Draw()
        {
            if (Globals.gameState == State.State.Playing && !_isDead)
            {
                if(direction == Movement.Down)downWalk.Draw(this._hitBox);
                else if(direction == Movement.Up)upWalk.Draw(this._hitBox);
                else if(direction == Movement.Left)leftWalk.Draw(this._hitBox);
                else if(direction == Movement.Right)rightWalk.Draw(this._hitBox);
                else if (direction == Movement.Idle) Globals.spriteBatch.Draw(idle, _hitBox, Color.White);
            }
        }

        public void Revive()
        {
            _isDead = false;
            _hp = 200;
            _hitBox.X = 4864;
            _hitBox.Y = 3220;
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
