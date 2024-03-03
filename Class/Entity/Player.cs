using System.Collections.Generic;
using MathL;
using Menu;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Audio;
using Movement;

namespace Game1.Class.Entity
{
    public class Player: Entity
    {
        private PlayerMovement _playerMovement;
        private EntityAnimation _animation;
        private bool _isDead = false;
        private int _balance;
        private float _stamina = 100f;
        private int _maxHealth = 200;
        
        public Direction lastStrafeDirection => _playerMovement.LastStrafeDirection;
        public Inventory inventory;
        
        
        public Player()
        {
            inventory = new Inventory();
            _balance = 0;
            BasePlayer();
        }

        
        public Player(int balance, Dictionary<int,List<int?>> inventory)
        {
            _balance = balance;
            this.inventory = new Inventory(inventory);
            BasePlayer();
        }
        
        
        public void BasePlayer()
        {
            _hitBox = new RectangleF(4864, 3220, 64, 64);
            _hp = _maxHealth; // TODO: Move this in load data file
            _animation = new EntityAnimation(
                new Animation(Globals.Content.Load<Texture2D>("Player/UP_WALK"), new Vector2(16, 16), 4, 0.2f),
                new Animation(Globals.Content.Load<Texture2D>("Player/DOWN_WALK"), new Vector2(16, 16), 4, 0.2f),
                new Animation(Globals.Content.Load<Texture2D>("Player/LEFT_WALK"), new Vector2(16, 16), 4, 0.2f),
                new Animation(Globals.Content.Load<Texture2D>("Player/RIGHT_WALK"), new Vector2(16, 16), 4, 0.2f),
                Globals.Content.Load<Texture2D>("Player/IDLE")
            );
            _playerMovement = new PlayerMovement(_hitBox, 300f, 500f);
        }

        public float Stamina => _stamina;
        public int Balance => _balance;

        public override void Update()
        {
            if (Globals.gameState == State.State.Playing && !isDead)
            {
                if (_hp <= 0)
                {
                    isDead = true;
                }

                _hitBox = _playerMovement.Hitbox;
                _playerMovement.Update(ref _stamina);
                _animation.Update(_playerMovement.Direction, _playerMovement.isRunning);
                inventory.Update();
            }
        }

        public override void Draw()
        {
            if (Globals.gameState == State.State.Playing && !_isDead ||Globals.gameState == State.State.Inventory)
            {
                _animation.Draw(_hitBox);
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

        public Direction getDirection()
        {
            return _playerMovement.Direction;
        }
    }
}
