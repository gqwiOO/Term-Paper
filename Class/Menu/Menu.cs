using System.Collections.Generic;

namespace Menu
{
    public class Menu
    {
        public List<Button> _buttons;
        public Menu(List<Button> buttons)
        {
            _buttons = buttons;
        }

        public void Update()
        {
            foreach (var button in _buttons)
            {
                button.Update();
            }
        }

        public void Draw()
        {
            foreach (var button in _buttons)
            {
                button.Draw();
            }
        }
    }
}

