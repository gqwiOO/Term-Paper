﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;


namespace Menu
{
    public class Button
    {
        // private bool _isPressed = false; 
        private SpriteFont _font;
        public string _text;
        public delegate void onClickDelegate();
        public onClickDelegate _onClick { get; set; }
        public Vector2 _position;

        public SpriteBatch _spriteBatch = Game1.Game1._spriteBatch;
        
        public Button(SpriteFont font, string text, Vector2 position) 
        {
            _font = font;
            _text = text;
            _position = position;
            changePosWithMeasureString();
        }

        private void changePosWithMeasureString()
        {
            Vector2 _ = _position;
            _position = new Vector2(_position.X - _font.MeasureString(_text).X / 2, _position.Y - _font.MeasureString(_text).Y / 2 );
        }

        public void Update()
        {
            if (Game1.Game1._mouseState.LeftButton == ButtonState.Pressed &&
                Game1.Game1._mouseState.X < (int)_position.X + (int)_font.MeasureString(_text).X &&
                Game1.Game1._mouseState.X > (int)_position.X &&
                Game1.Game1._mouseState.Y < (int)_position.Y + (int)_font.MeasureString(_text).Y &&
                Game1.Game1._mouseState.Y > (int)_position.Y 
                )
            {
                _onClick.Invoke();
                // _isPressed = true;
            }
        }

        public void Draw()
        {
            _spriteBatch.DrawString(_font, _text, _position, Color.Black);
        }
        
    }
    
        
    
}