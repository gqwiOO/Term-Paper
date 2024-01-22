using MathL;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Class.Entity
{
    public class NPC: Entity
    {
        public string _name;
        
        private Animation downWalk;
        private Animation upWalk;
        private Animation leftWalk;
        private Animation rightWalk;
        private Texture2D idle;
        
        public NPC(Texture2D downWalkTexture, Texture2D upWalkTexture,
            Texture2D leftWalkTexture, Texture2D rightWalkTexture, Texture2D idleTexture
        )
        {
            _hp = 100;
            _speed = 4;
            _damage = 20;
            
            _hitBox = new RectangleF(4864, 3220, 64, 64);
            downWalk = new Animation(downWalkTexture, new Vector2(16, 16), 3, 0.2f);
            upWalk = new Animation(upWalkTexture, new Vector2(16, 16), 4, 0.2f);
            leftWalk = new Animation(leftWalkTexture, new Vector2(16, 16), 4, 0.2f);
            rightWalk = new Animation(rightWalkTexture, new Vector2(16, 16), 4, 0.2f);
            idle = idleTexture;
        }
        
        public override void Update()
        {
            
        }

        public void SelectDirection()
        {
            
        }

        public override void Draw()
        {
            
        }
    }
}
