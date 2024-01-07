using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Vector2 = System.Numerics.Vector2;

namespace Game1.Class.Entity
{
    public abstract class Entity
    {
        public int _hp;
        public int _damage;
        public int _speed;
        public Texture2D _sprite;
        public Rectangle _hitBox;
        
        public abstract void Update(GameTime gameTime, Player player);

        public abstract void Draw(SpriteBatch spriteBatch);

    }
}
