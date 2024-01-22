using System.Collections.Generic;
using System.Linq;
using Game1.Class.Item;

namespace Data;

public static class Items
{
    public static List<Weapon> Weapons;


    public static Weapon GetById(int id)
    {
        return Weapons.Where(weapon => weapon.id == id).First();
    }

}

