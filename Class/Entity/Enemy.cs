﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Class.Entity
{
    public class Enemy: Entity
    {
        public Rectangle _hitBox;
        
        public Enemy(Texture2D EnemySprite)
        {
            _sprite = EnemySprite;
            _speed = 4;
            _hitBox = new Rectangle(1500,700 , 128, 128);
        }
        public void Update()
        {
            
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (Game1._state == State.State.Playing)
            {
                spriteBatch.Draw(_sprite, _hitBox, Color.White);
            }
        }
    }
}
