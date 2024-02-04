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

    public static void LoadAnimation()
    {
        entities.ForEach(entity => entity.LoadAnimation());
    }
}

