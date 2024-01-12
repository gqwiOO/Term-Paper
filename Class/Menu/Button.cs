using Game1;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;


namespace Menu
{
    public class Button
    {
        private SpriteFont _font;
        public string _text;
        public delegate void onClickDelegate();
        public onClickDelegate _onClick { get; set; }
        public Vector2 _position;
        
        public Button(SpriteFont font, string text, Vector2 position) 
        {
            _font = font;
            _text = text;
            _position = position;
            changePosWithMeasureString();
        }

        private void changePosWithMeasureString()
        {
            _position = new Vector2(_position.X - _font.MeasureString(_text).X / 2, _position.Y - _font.MeasureString(_text).Y / 2 );
        }

        public void Update()
        {
            if (Globals.mouseState.LeftButton == ButtonState.Pressed &&
                Globals.mouseState.X < _position.X + _font.MeasureString(_text).X &&
                Globals.mouseState.X > _position.X &&
                Globals.mouseState.Y < _position.Y + _font.MeasureString(_text).Y &&
                Globals.mouseState.Y > _position.Y &&
                !Game1.Game1.isLeftMouseButtonPressed
                )
            {
                _onClick.Invoke();
                Game1.Game1.isLeftMouseButtonPressed = true;
            }
        }
        public void Draw()
        {
            Globals.spriteBatch.DrawString(_font, _text, _position, Color.Black);
        }
    }
    
        
    
}