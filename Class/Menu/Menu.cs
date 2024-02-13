using System.Collections.Generic;
using TermPaper.Class.Cursor;
using Game1;
using Game1.Class.State;

namespace Menu
{
    public class Menu
    {
        public List<Button> _buttons;
        public State _menuState;
        private ButtonFactory _buttonFactory;
        public Menu(List<Button> buttons, State state)
        {
            _buttons = buttons;
            _menuState = state;
            _buttonFactory = new ButtonFactory(buttons);
        }

        public void Update()
        {
            if (Globals.gameState == _menuState)
            {
                _buttons.ForEach(button => button.Update());
                UpdateButtons();
            }
            
        }
        
        public void UpdateButtons()
        {
            if (_buttonFactory.isMouseOnAnyButton())
            {
                Cursor.setCursor(1);
            }
            else
            {
                Cursor.setCursor(0);
            }
        }

        public void Draw()
        {
            _buttons.ForEach(button => button.Draw());
        }
    }
    
    
    
}

