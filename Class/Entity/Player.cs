using System;
using System.Collections.Generic;
using MathL;
using Menu;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Audio;
using Game1.Class.Item;
using Movement;

namespace Game1.Class.Entity
{
    public class Player: Entity
    {
        public bool _isDead;
        public Inventory inventory;
        public int _balance;
        private float _stamina;

        private Animation downWalk;
        private Animation upWalk;
        private Animation leftWalk;
        private Animation rightWalk;
        private Texture2D idle;
        private Movement direction;

        private int _maxHealth = 200;
        
        // Can be only Left or Right, using for attack animation if idle
        public Movement lastStrafeDirection = Movement.Right;
        
        private float _runningSpeed = 400;
        public Player()
        {
            _hp = _maxHealth;
            _speed = 300;
            _damage = 20;

            inventory = new Inventory();
            _hitBox = new RectangleF(4864, 3220, 64, 64);
            _stamina = 100;
            _balance = 0;
            
            downWalk = new Animation(Globals.Content.Load<Texture2D>("Player/DOWN_WALK"), new Vector2(16, 16), 4, 0.2f);
            upWalk = new Animation(Globals.Content.Load<Texture2D>("Player/UP_WALK"), new Vector2(16, 16), 4, 0.2f);
            leftWalk = new Animation(Globals.Content.Load<Texture2D>("Player/LEFT_WALK"), new Vector2(16, 16), 4, 0.2f);
            rightWalk = new Animation(Globals.Content.Load<Texture2D>("Player/RIGHT_WALK"), new Vector2(16, 16), 4, 0.2f);
            idle = Globals.Content.Load<Texture2D>("Player/IDLE");
        }

        public Player(int balance, Dictionary<int,List<int?>> inventory)
        {
            _balance = balance;
            
            _hp = _maxHealth;
            _speed = 300;
            _damage = 20;

            this.inventory = new Inventory(inventory);
            _hitBox = new RectangleF(4864, 3220, 64, 64);
            _stamina = 100;
            
            downWalk = new Animation(Globals.Content.Load<Texture2D>("Player/DOWN_WALK"), new Vector2(16, 16), 4, 0.2f);
            upWalk = new Animation(Globals.Content.Load<Texture2D>("Player/UP_WALK"), new Vector2(16, 16), 4, 0.2f);
            leftWalk = new Animation(Globals.Content.Load<Texture2D>("Player/LEFT_WALK"), new Vector2(16, 16), 4, 0.2f);
            rightWalk = new Animation(Globals.Content.Load<Texture2D>("Player/RIGHT_WALK"), new Vector2(16, 16), 4, 0.2f);
            idle = Globals.Content.Load<Texture2D>("Player/IDLE");
        }

        public float getStamina()
        {
            return _stamina;
        }

        public override void Update()
        {
            if (Globals.gameState == State.State.Playing && !isDead)
            {
                if (_hp <= 0)
                {
                    isDead = true;
                }
                UpdateMovement();
            }
            inventory.Update();
        }
        private void UpdateMovement()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) && _stamina >= 10)
                {
                    _hitBox.Y -= _runningSpeed * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;
                    // Double Update() for faster(x2) animation
                    upWalk.Update();
                    // If player moves in left or right, stamina will decrease only in if statement with Left or Right but not in Up or Down
                    if (direction != Movement.Left && direction != Movement.Right)
                    {
                        _stamina -= 20 * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;
                    }
                }
                else
                {
                    _hitBox.Y -= _speed * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;
                }
                direction = Movement.Up;
                upWalk.Update();
                Sound.PlaySoundEffect("walkingSound", 0.1f);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) && _stamina >= 10)
                {
                    _hitBox.Y += _runningSpeed * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;
                    // Double Update() for faster(x2) animation
                    downWalk.Update();
                    // If player moves in left or right, stamina will decrease only in if statement with Left or Right but not in Up or Down
                    if (direction != Movement.Left && direction != Movement.Right)
                    {
                        if(_stamina >= 10)_stamina -= 20 * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;
                    }
                }
                else
                {
                    _hitBox.Y += _speed * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;
                }
                
                direction = Movement.Down;
                downWalk.Update();
                Sound.PlaySoundEffect("walkingSound", 0.1f);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) && _stamina >= 10)
                {
                    _hitBox.X -= _runningSpeed * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;
                    // Double Update() for faster(x2) animation
                    leftWalk.Update();
                    if(_stamina >= 10)_stamina -= 20 * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;
                }
                else
                {
                    _hitBox.X -= _speed * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;
                }
                direction = Movement.Left;
                lastStrafeDirection = Movement.Left;
                leftWalk.Update();
                Sound.PlaySoundEffect("walkingSound", 0.1f);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) && _stamina >= 10)
                {
                    _hitBox.X += _runningSpeed * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;
                    // Double Update() for faster(x2) animation
                    rightWalk.Update();
                    if(_stamina >= 10)_stamina -= 20 * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;
                }
                else
                {
                    _hitBox.X += _speed * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;
                }
                direction = Movement.Right;
                lastStrafeDirection = Movement.Right;
                rightWalk.Update();
                Sound.PlaySoundEffect("walkingSound", 0.1f);
            }
            if (Keyboard.GetState().IsKeyUp(Keys.D) && Keyboard.GetState().IsKeyUp(Keys.A) &&
                Keyboard.GetState().IsKeyUp(Keys.S) && Keyboard.GetState().IsKeyUp(Keys.W))
            {
                direction = Movement.Idle;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.LeftShift))
            {
                if (_stamina < 100f)
                {
                    _stamina += 20f * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;
                }
            }
        }

        public override void Draw()
        {
            if (Globals.gameState == State.State.Playing && !_isDead ||Globals.gameState == State.State.Inventory)
            {
                if(direction == Movement.Down)downWalk.Draw(this._hitBox.ToRectangle());
                else if(direction == Movement.Up)upWalk.Draw(this._hitBox.ToRectangle());
                else if(direction == Movement.Left)leftWalk.Draw(this._hitBox.ToRectangle());
                else if(direction == Movement.Right)rightWalk.Draw(this._hitBox.ToRectangle());
                else if (direction == Movement.Idle) Globals.spriteBatch.Draw(idle, _hitBox.ToRectangle(), Color.White);
            }

            if (inventory.getBowIndex() != inventory.getCurrentItemIndex())
            {
                inventory.inventory[(int)inventory.getBowIndex()].Draw();
            }
            if (inventory.getCurrentItem() != null) inventory.getCurrentItem().Draw();
        }

        public void Revive()
        {
            isDead = false;
            _hp = _maxHealth;
            _hitBox.X = 4864;
            _hitBox.Y = 3220;
        }

        public void Sell(int cost)
        {
            _balance += cost;
        }

        public Movement getDirection()
        {
            return direction;
        }
    }
}
