using System.Collections.Generic;
using System.Data;
using System.Linq;
using Game1.Class.State;

namespace Menu;

public class MainMenu
{
    public List<Menu> _menus { get; set; }

    public void Update()
    {
        _menus.Where(menu => menu._menuState == Game1.Game1._state).ToList().ForEach(menu => menu.Update());
    }
    
    public void Draw()
    {
        _menus.Where(menu => menu._menuState == Game1.Game1._state).ToList().ForEach(menu => menu.Draw());
    }
}