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
                _spawnPoint.X = value;
            }
        }
        public int spawnY
        {
            set
            {
                hitbox.Y = value;
                _spawnPoint.Y = value;
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
        
        private Vector2 _namePos;
        private Vector2 _previousDir;
        private Vector2 _directionVector;
        private Vector2 _spawnPoint = new Vector2(4000, 3000);
        private Direction _direction;

        private Vector2 _leftVector = new Vector2(-1, 0);
        private Vector2 _rightVector = new Vector2(1, 0);
        private Vector2 _downVector = new Vector2(0, 1);
        private Vector2 _upVector = new Vector2(0, -1);

        private Animation _leftWalk;
        private Animation _rightWalk;
        private Animation _upWalk;
        private Animation _downWalk;
        private Texture2D _idle;

        private int _walkRadius = 500;
        private double _directionTimer;
        private double _changeDirectionTime;
        private BuyInterface _buyInterface;
        
        // Buy Interface vars
        private bool _isShopOpened;

        public NPC()
        {
            _buyInterface = new BuyInterface(this);
        }
        
        public override void Update()
        {
            if(hasShop)_buyInterface.Update();
            if (Globals.gameState == State.State.Playing)
            {
                _directionTimer += Globals.gameTime.ElapsedGameTime.Milliseconds;

                if (_directionTimer >= _changeDirectionTime)
                {
                    _directionTimer -= _changeDirectionTime;
                    _directionVector = GetRandomDirection(_previousDir);
                    _previousDir = _directionVector;
                }

                hitbox.X += _directionVector.X * speed * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;
                hitbox.Y += _directionVector.Y * speed * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;

                _direction = GetMoveDirection(_directionVector);
                UpdateAnimation();

                _namePos = new Vector2(hitbox.Center.X, hitbox.Center.Y - 35);

                if (isMouseOnNPC() && Input.isLeftButtonPressed() && hasShop)
                {
                    Globals.gameState = State.State.InShop;
                    _isShopOpened = true;
                }
            }
        }

        public bool isMouseOnNPC()
        {
            return Input.isMouseInRectangle(hitbox);
        }
        private void UpdateAnimation()
        {
            if (_direction == Direction.Left) _leftWalk.Update();
            if (_direction == Direction.Right) _rightWalk.Update();
            if (_direction == Direction.Up) _upWalk.Update();
            if (_direction == Direction.Down) _downWalk.Update();
        }

        public override void Draw()
        {
            if (Globals.gameState == State.State.Playing || Globals.gameState == State.State.Inventory)
            {
                if (_direction == Direction.Left) _leftWalk.Draw(hitbox.ToRectangle());
                if (_direction == Direction.Right) _rightWalk.Draw(hitbox.ToRectangle());
                if (_direction == Direction.Up) _upWalk.Draw(hitbox.ToRectangle());
                if (_direction == Direction.Down) _downWalk.Draw(hitbox.ToRectangle());
                if (_direction == Direction.Idle) Globals.spriteBatch.Draw(_idle, hitbox.ToRectangle(), Color.White);
                
                // Draw NPC name
                Globals.spriteBatch.DrawString(Font.fonts["MainFont-16"], name, _namePos, Color.Black, 0,
                    new Vector2(Font.fonts["MainFont-16"].MeasureString(name).X / 2, Font.fonts["MainFont-16"].MeasureString(name).Y / 2),
                    1, SpriteEffects.None, 0f);
            }
            DrawHUD();
        }

        public void DrawHUD()
        {
            if (_isShopOpened)
            {
                _buyInterface.Draw();
            }
        }
        public void LoadAnimation()
        {
            _leftWalk = new Animation(Globals.Content.Load<Texture2D>(WalkLeftAnimationPath),
                new Vector2( animationFrameHeight, animationFrameWidth), frameCount, frameTime);
            _rightWalk = new Animation(Globals.Content.Load<Texture2D>(WalkRightAnimationPath),
                new Vector2(animationFrameHeight, animationFrameWidth), frameCount, frameTime);
            _upWalk = new Animation(Globals.Content.Load<Texture2D>(WalkUpAnimationPath),
                new Vector2(animationFrameHeight, animationFrameWidth), frameCount, frameTime);
            _downWalk = new Animation(Globals.Content.Load<Texture2D>(WalkDownAnimationPath),
                new Vector2(animationFrameHeight, animationFrameWidth), frameCount, frameTime);
            _idle = Globals.Content.Load<Texture2D>(IdlePath);
        }
        public Vector2 GetRandomDirection(Vector2 previousDir)
        {
            Random random = new Random();
            _changeDirectionTime = random.Next(4000, 7000);
            int chooseDir = random.Next(4);

            if (previousDir != Vector2.Zero) return Vector2.Zero;
            
            if (chooseDir == 1 && hitbox.X > _spawnPoint.X + _walkRadius) return _leftVector;
            else if (chooseDir == 1 && hitbox.X < _spawnPoint.X + _walkRadius) return _rightVector;
            
            if (chooseDir == 2 && hitbox.X < _spawnPoint.X + _walkRadius) return _rightVector;
            else if (chooseDir == 2 && hitbox.X > _spawnPoint.X + _walkRadius) return _leftVector;
            
            if (chooseDir == 3 && hitbox.Y < _spawnPoint.Y + _walkRadius) return _downVector;
            else if (chooseDir == 3 && hitbox.Y > _spawnPoint.Y + _walkRadius) return _upVector;
            
            if (chooseDir == 4 && hitbox.Y > _spawnPoint.Y + _walkRadius) return _upVector;
            else if (chooseDir == 3 && hitbox.Y < _spawnPoint.Y + _walkRadius) return _downVector;
            
            return Vector2.Zero;
        }
        public Direction GetMoveDirection(Vector2 direction)
        {
            if (direction.Y < 0) return Direction.Up;
            if (direction.Y > 0) return Direction.Down;
            if (direction.X < 0) return Direction.Left;
            if (direction.X > 0) return Direction.Right;
            return Direction.Idle;
        }
    }
}
