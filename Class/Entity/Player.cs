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
        SpriteEffects s = SpriteEffects.FlipHorizontally;
        
        public Player(Texture2D sprite)
        {
            _hp = 100;
            _speed = 8;
            _sprite = sprite;
            _hitBox = new Rectangle(3200, 2748, 128, 128);
            
        }
        public void  Update(GameTime gameTime)
        {
            if (Game1._state == State.State.Playing && _isDead != true)
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
                   s = SpriteEffects.FlipHorizontally;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    _hitBox.X += _speed;
                    s = SpriteEffects.None;
                }
                if (_hp <= 0)
                {
                    _isDead = true;
                }
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Game1._state == State.State.Playing && !_isDead)
            {
               spriteBatch.Draw(_sprite, _hitBox, null, Color.White, 0, new Vector2(50, 50), s, 0f);
            }
        }

        public void Revive()
        {
            _isDead = false;
            _hp = 100;
            _hitBox.X = 3200;
            _hitBox.Y = 2748;
        }
        public override void Update(GameTime gameTime, Player player)
        {
            
        }
    }
}
