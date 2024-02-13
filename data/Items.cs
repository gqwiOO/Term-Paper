using System;
using System.Collections.Generic;
using System.Linq;
using Game1.Class.Item;

namespace Data;

public static class Items
{
    public static List<Item> ItemList = new List<Item>();
    
    public static Item GetItemById(int id)
    {
        return ItemList.Where(item => item.id == id).First();
    }
}

