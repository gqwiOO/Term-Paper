using System.Collections.Generic;
using System.Linq;
using Game1.Class.Entity;

namespace Data;

public static class Entities
{
    public static List<NPC> entities;
    
    public static NPC GetById(int id)
    {
        return entities.Where(npc => npc.id == id).First();
    }

    public static bool isMouseOnAnyNPC()
    {
        List<bool> isMouseOnAnyNpcBools = new List<bool>();
        foreach (NPC npc in entities)
        {
            isMouseOnAnyNpcBools.Add(npc.isMouseOnNPC());
        }

        if (isMouseOnAnyNpcBools.Where(i => i).Count() > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static void LoadAnimation()
    {
        entities.ForEach(entity => entity.LoadAnimation());
    }
}

