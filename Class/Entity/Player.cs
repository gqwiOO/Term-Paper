using Microsoft.Xna.Framework.Graphics;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Game1.Class.Entity
{
    public class Player: Entity 
    {
        public Rectangle _hitBox;
        public Texture2D _sprite;
        
        public Player(Texture2D sprite)
        {
            _speed = 8;
            _sprite = sprite;
            _hitBox = new Rectangle(500, 500, 128, 128);
        }
        public void Update()
        {
            if (Game1._state == State.State.Playing)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.W)) //Movement
                {
                    _hitBox.Y -= _speed;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    _hitBox.Y += _speed;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                   _hitBox.X -= _speed;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    _hitBox.X += _speed;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Game1._state == State.State.Playing)
            {
                spriteBatch.Draw(_sprite, _hitBox,Color.White);
            }
        }
    }
}
