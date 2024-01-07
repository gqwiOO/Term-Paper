using System.Collections.Generic;
using Game1.Class.State;

namespace Menu
{
    public class Menu
    {
        public List<Button> _buttons;
        public State _menuState;
        public Menu(List<Button> buttons, State state)
        {
            _buttons = buttons;
            _menuState = state;
        }

        public void Update()
        {
            foreach (var button in _buttons)
            {
                button.Update();
            }
            // _buttons.ForEach(button => button.Update());
        }

        public void Draw()
        {
            foreach (var button in _buttons)
            {
                button.Draw();
            }
            // _buttons.ForEach(button => button.Draw());
        }
    }
    
    
    
}

