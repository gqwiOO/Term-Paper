using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MathL;
namespace Game1.Class.Entity
{
    public abstract class Entity
    {
        public int _hp;
        public int _damage;
        public int _speed;
        public RectangleF _hitBox;
        public bool isDead;
        
        public abstract void Update();

        public abstract void Draw();

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
