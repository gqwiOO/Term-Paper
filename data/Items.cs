using System.Collections.Generic;
using System.Linq;
using Game1.Class.Item;

namespace Data;

public static class Items
{
    public static List<Weapon> Weapons;
    public static List<Potion> Potions;


    public static Weapon GetWeaponById(int id)
    {
        return Weapons.Where(weapon => weapon.id == id).First();
    }
    public static Potion GetPotionById(int id)
    {
        return Potions.Where(potion => potion.id == id).First();
    }

}

