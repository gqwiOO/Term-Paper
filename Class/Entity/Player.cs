using Microsoft.Xna.Framework.Graphics;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Game1.Class.Entity
{
    public class Player: Entity 
    {
        public float _playerSpeed = 20f;
        public Vector2 _playerPosition;
        public Texture2D _playerSprite;
        public Player(Texture2D PlayerSprite)
        {
            _playerSprite = PlayerSprite;
        }
        public void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W)) //Movement
            {
                _playerPosition.Y -= _playerSpeed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                _playerPosition.Y += _playerSpeed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                _playerPosition.X -= _playerSpeed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                _playerPosition.X += _playerSpeed;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_playerSprite, _playerPosition, Microsoft.Xna.Framework.Color.White);
        }
    }
}
