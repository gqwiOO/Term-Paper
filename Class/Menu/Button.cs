using Game1;
using MathL;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Movement;
using Audio;
using TermPaper.Class.Font;


namespace Menu
{
    public class Button
    {
        private SpriteFont _font;
        public string _text;
        public delegate void onClickDelegate();
        public onClickDelegate _onClick { get; set; }
        public Vector2 _position;
        private RectangleF _hitBoxDefault;
        
        
        public Button(SpriteFont font, string text, Vector2 position) 
        {
            _font = font;
            _text = text;
            _hitBoxDefault = new RectangleF(position.X - _font.MeasureString(_text).X / 2,
                                            position.Y - _font.MeasureString(_text).Y / 2,
                                            _font.MeasureString(_text).X,
                                            _font.MeasureString(_text).Y);
            
        }
        public Button(string text, Vector2 position) 
        {
            _font = Font.fonts["MainFont-24"];
            _text = text;
            _hitBoxDefault = new RectangleF(position.X - _font.MeasureString(_text).X / 2,
                position.Y - _font.MeasureString(_text).Y / 2,
                _font.MeasureString(_text).X,
                _font.MeasureString(_text).Y);
        }


        public bool isMouseOnButton()
        {
            return Globals.mouseState.X <= _hitBoxDefault.Right &&
                   Globals.mouseState.X >= _hitBoxDefault.Left &&
                   Globals.mouseState.Y <= _hitBoxDefault.Bottom &&
                   Globals.mouseState.Y >= _hitBoxDefault.Top;
        }
        public bool isMouseOnButtonAndPressed()
        {
            return Input.hasBeenLeftMouseButtonPressed() &&
                   Globals.mouseState.X <= _hitBoxDefault.Right &&
                   Globals.mouseState.X >= _hitBoxDefault.Left &&
                   Globals.mouseState.Y <= _hitBoxDefault.Bottom &&
                   Globals.mouseState.Y >= _hitBoxDefault.Top;
        }
        public void Update()
        {
            if (isMouseOnButtonAndPressed()
                )
            {
                
                _onClick.Invoke();
                Sound.PlaySoundEffect("ClickSound", 0.6f);
            }

        }
        public void Draw()
        {
            Globals.spriteBatch.DrawString(_font, _text,
                new Vector2(_hitBoxDefault.X, _hitBoxDefault.Y), Color.Black);
        }
    }
    
        
    
}