using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MathL;

namespace Game1.Class.Item
{
   public abstract class Item
    {
        public int id{ get; set; }
        public bool _isStackable;
        public int _stackCapacity;
        public Texture2D _sprite;
        public int _price;
        public Animation _animation { get; set; }

        public float cooldown { get; set;}
        public RectangleF _hitbox;

        public abstract void Use();
        public void Sell()
        {
        }
        public void Draw(Rectangle _position)
        {
            _hitbox = new RectangleF(_position.X, _position.Y, _position.Width, _position.Height);
            Globals.spriteBatch.Draw(_sprite, _position,null, Color.White, 0f, new Vector2(16,16), SpriteEffects.None, 0f);
        }

        public void DrawTopLeft(Rectangle _position)
        {
            Globals.spriteBatch.Draw(_sprite, _position, Color.White);
        }

        public void UpdateShopLogic()
        {
            if (Globals.gameState == State.State.InShop)
            {
                
            }
        }
        public abstract void Update();
        public abstract void Draw();

    }
}
