using System;
using MathL;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Content;

namespace Game1.Class.Entity
{
    public class NPC: Entity
    {
        public RectangleF hitbox = new RectangleF(4000, 3000, 50, 50);
        public string name { get; set; }
        private Vector2 namePos;
        public int damage { get; set; }
        public int id { get; set; }
        public int speed { get; set; }
        public int hitboxWidth
        {
            set
            {
                hitbox.Width = value;
            }
        }
        public int hitboxHeight 
        {     
            set
            {
                 hitbox.Width = value; 
            } 
        }
        public int frameCount { get; set; }
        public float frameTime { get; set; }
        public int animationFrameWidth { get; set; }
        public int animationFrameHeight { get; set; }

        public string WalkLeftAnimationPath { get; set; }
        public string WalkRightAnimationPath { get; set; }
        public string WalkUpAnimationPath { get; set; }
        public string WalkDownAnimationPath { get; set; }
        public string IdlePath { get; set; }

        public Vector2 directionVector;
        public Movement direction;
        public SpriteFont font = Globals.Content.Load<SpriteFont>("Fonts/Minecraft");

        private Animation leftWalk;
        private Animation rightWalk;
        private Animation upWalk;
        private Animation downWalk;
        private Texture2D idle;

        private double totalElapsedMilliseconds;
        private const double changeDirectionTime = 5000;
        
        public override void Update()
        {
            if (Globals.gameState == State.State.Playing)
            {
                totalElapsedMilliseconds += Globals.gameTime.ElapsedGameTime.Milliseconds;

                if (totalElapsedMilliseconds >= changeDirectionTime)
                {
                    totalElapsedMilliseconds -= changeDirectionTime;
                    directionVector = GetRandomDirection();
                }

                hitbox.X += directionVector.X * speed * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;
                hitbox.Y += directionVector.Y * speed * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;

                direction = GetMoveDirection(directionVector);
                UpdateAnimation();

                namePos = new Vector2(hitbox.Center.X, hitbox.Center.Y - 35);
            }
        }

        private void UpdateAnimation()
        {
            if (direction == Movement.Left) leftWalk.Update();
            if (direction == Movement.Right) rightWalk.Update();
            if (direction == Movement.Up) upWalk.Update();
            if (direction == Movement.Down) downWalk.Update();
        }

        public override void Draw()
        {
            if (Globals.gameState == State.State.Playing || Globals.gameState == State.State.Inventory)
            {
                if (direction == Movement.Left) leftWalk.Draw(hitbox.ToRectangle());
                if (direction == Movement.Right) rightWalk.Draw(hitbox.ToRectangle());
                if (direction == Movement.Up) upWalk.Draw(hitbox.ToRectangle());
                if (direction == Movement.Down) downWalk.Draw(hitbox.ToRectangle());
                if (direction == Movement.Idle) Globals.spriteBatch.Draw(idle, hitbox.ToRectangle(), Color.White);
                Globals.spriteBatch.DrawString(font, name, namePos, Color.Black, 0,
                    new Vector2(font.MeasureString(name).X / 2, font.MeasureString(name).Y / 2),
                    1, SpriteEffects.None, 0f);
            }
        }
        public void LoadAnimation()
        {
            leftWalk = new Animation(Globals.Content.Load<Texture2D>(WalkLeftAnimationPath),
                new Vector2( animationFrameHeight, animationFrameWidth), frameCount, frameTime);
            rightWalk = new Animation(Globals.Content.Load<Texture2D>(WalkRightAnimationPath),
                new Vector2(animationFrameHeight, animationFrameWidth), frameCount, frameTime);
            upWalk = new Animation(Globals.Content.Load<Texture2D>(WalkUpAnimationPath),
                new Vector2(animationFrameHeight, animationFrameWidth), frameCount, frameTime);
            downWalk = new Animation(Globals.Content.Load<Texture2D>(WalkDownAnimationPath),
                new Vector2(animationFrameHeight, animationFrameWidth), frameCount, frameTime);
            idle = Globals.Content.Load<Texture2D>(IdlePath);
        }
        public Vector2 GetRandomDirection()
        {
            Random random = new Random();
            int chooseDir = random.Next(4);
            switch (chooseDir)
            {
                case 1: return new Vector2(-1, 0);
                case 2: return new Vector2(1, 0);
                case 3: return new Vector2(0, -1);
                case 4: return new Vector2(0, 1);
                default: return new Vector2(0, 0);
            }
        }
        public Movement GetMoveDirection(Vector2 direction)
        {
            if (direction.Y < 0) return Movement.Up;
            if (direction.Y > 0) return Movement.Down;
            if (direction.X < 0) return Movement.Left;
            if (direction.X > 0) return Movement.Right;
            return Movement.Idle;
        }
    }
}
