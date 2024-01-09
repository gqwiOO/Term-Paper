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
        public uint _balance = 0;
        public Vector2 Position;
        public float _cooldown;
        public Player(Texture2D sprite)
        {
            _hp = 100;
            _speed = 4;
            _sprite = sprite;
            _damage = 20;
            _cooldown = 1f;
            _hitBox = new Rectangle(4864, 3220, 64, 64);
            
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
            _hitBox.X = 4864;
            _hitBox.Y = 3220;
        }
        public override void Update(GameTime gameTime, Player player)
        {
            
        }
    }
}
