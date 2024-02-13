using System.Collections.Generic;
using System.Linq;

namespace Menu;

public class ButtonFactory
{
    private List<Button> buttonList;


    public ButtonFactory(List<Button> buttons)
    {
        buttonList = buttons;
    }
    public void AddButton(Button button)
    {
        this.buttonList.Add(button);
    }
    
    public bool isMouseOnAnyButton()
    {
        List<bool> isMouseOnAnyButtonBools = new List<bool>();
        foreach (Button button in buttonList)
        {
            isMouseOnAnyButtonBools.Add((button.isMouseOnButton()));
        }

        if (isMouseOnAnyButtonBools.Where(i => i).Count() > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}