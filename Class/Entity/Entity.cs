using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MathL;
namespace Game1.Class.Entity
{
    public abstract class Entity
    {
        public int _hp;
        public int id {get; set;}
        public RectangleF _hitBox;
        public bool isDead;
        
        public abstract void Update();

        public abstract void Draw();
    }
    public enum Direction
    {
        Left,
        Right,
        Up,
        Down,
        Idle
    }
}
