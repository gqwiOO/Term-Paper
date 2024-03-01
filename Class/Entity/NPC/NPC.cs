using System;
using System.Collections.Generic;
using MathL;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Movement;
using TermPaper.Class.Font;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace Game1.Class.Entity
{
    public class NPC: Entity
    {
        public RectangleF hitbox = new RectangleF(0, 0, 50, 50);
        public string name { get; set; }
        private Vector2 namePos;
        public int damage { get; set; }
        
        public bool hasShop { get; set; }
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
                 hitbox.Height = value; 
            } 
        }
        public int spawnX
        {
            set
            {
                hitbox.X = value;
                spawnPoint.X = value;
            }
        }
        public int spawnY
        {
            set
            {
                hitbox.Y = value;
                spawnPoint.Y = value;
            }
        }
        
        public List<int> shopItems { get; set; }
        public int frameCount { get; set; }
        public float frameTime { get; set; }
        public int animationFrameWidth { get; set; }
        public int animationFrameHeight { get; set; }

        public string WalkLeftAnimationPath { get; set; }
        public string WalkRightAnimationPath { get; set; }
        public string WalkUpAnimationPath { get; set; }
        public string WalkDownAnimationPath { get; set; }
        public string IdlePath { get; set; }

        public List<int> shopItemsID { get; set; }

        public Vector2 previousDir;
        public Vector2 directionVector;
        public Vector2 spawnPoint = new Vector2(4000, 3000);
        public Movement direction;

        public Vector2 leftVector = new Vector2(-1, 0);
        public Vector2 rightVector = new Vector2(1, 0);
        public Vector2 downVector = new Vector2(0, 1);
        public Vector2 upVector = new Vector2(0, -1);

        private Animation leftWalk;
        private Animation rightWalk;
        private Animation upWalk;
        private Animation downWalk;
        private Texture2D idle;

        public int walkRadius = 500;
        private double totalElapsedMilliseconds;
        private double changeDirectionTime;
        private BuyInterface _buyInterface;
        
        // Buy Interface vars
        public bool isShopOpened;

        public NPC()
        {
            _buyInterface = new BuyInterface(this);
        }
        
        public override void Update()
        {
            if(hasShop)_buyInterface.Update();
            if (Globals.gameState == State.State.Playing)
            {
                totalElapsedMilliseconds += Globals.gameTime.ElapsedGameTime.Milliseconds;

                if (totalElapsedMilliseconds >= changeDirectionTime)
                {
                    totalElapsedMilliseconds -= changeDirectionTime;
                    directionVector = GetRandomDirection(previousDir);
                    previousDir = directionVector;
                }

                hitbox.X += directionVector.X * speed * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;
                hitbox.Y += directionVector.Y * speed * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;

                direction = GetMoveDirection(directionVector);
                UpdateAnimation();

                namePos = new Vector2(hitbox.Center.X, hitbox.Center.Y - 35);

                if (isMouseOnNPC() && Input.isLeftButtonPressed() && hasShop)
                {
                    Globals.gameState = State.State.InShop;
                    isShopOpened = true;
                }
            }
        }

        public bool isMouseOnNPC()
        {
            return Input.isMouseInRectangle(hitbox);
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
                
                // Draw NPC name
                Globals.spriteBatch.DrawString(Font.fonts["MainFont-16"], name, namePos, Color.Black, 0,
                    new Vector2(Font.fonts["MainFont-16"].MeasureString(name).X / 2, Font.fonts["MainFont-16"].MeasureString(name).Y / 2),
                    1, SpriteEffects.None, 0f);
            }
            DrawHUD();
        }

        public void DrawHUD()
        {
            if (isShopOpened)
            {
                _buyInterface.Draw();
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
        public Vector2 GetRandomDirection(Vector2 previousDir)
        {
            Random random = new Random();
            changeDirectionTime = random.Next(4000, 7000);
            int chooseDir = random.Next(4);

            if (previousDir != Vector2.Zero) return Vector2.Zero;
            
            if (chooseDir == 1 && hitbox.X > spawnPoint.X + walkRadius) return leftVector;
            else if (chooseDir == 1 && hitbox.X < spawnPoint.X + walkRadius) return rightVector;
            
            if (chooseDir == 2 && hitbox.X < spawnPoint.X + walkRadius) return rightVector;
            else if (chooseDir == 2 && hitbox.X > spawnPoint.X + walkRadius) return leftVector;
            
            if (chooseDir == 3 && hitbox.Y < spawnPoint.Y + walkRadius) return downVector;
            else if (chooseDir == 3 && hitbox.Y > spawnPoint.Y + walkRadius) return upVector;
            
            if (chooseDir == 4 && hitbox.Y > spawnPoint.Y + walkRadius) return upVector;
            else if (chooseDir == 3 && hitbox.Y < spawnPoint.Y + walkRadius) return downVector;
            
            return Vector2.Zero;
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
