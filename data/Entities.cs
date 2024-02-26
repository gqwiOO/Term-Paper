using System.Collections.Generic;
using System.Linq;
using Game1.Class.Entity;

namespace Data;

public static class Entities
{
    public static List<Entity> entities = new List<Entity>();
    
    
    public static Entity GetById(int id)
    {
        return entities.Where(npc => npc.id == id).First();
    }

    public static bool isMouseOnAnyNPC()
    {
        List<bool> isMouseOnAnyNpcBools = new List<bool>();
        foreach (Entity npc in entities)
        {
            try
            {
                isMouseOnAnyNpcBools.Add(((NPC)npc).isMouseOnNPC());
            }
            catch
            {
                
            }
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

    public static void Update()
    {
        entities.ForEach(entity => entity.Update());
    }
    public static void LoadAnimation()
    {
        entities.Where(npc => npc.GetType().Equals(typeof(NPC))).ToList().ForEach(npc => ((NPC)npc).LoadAnimation());
        entities.Where(npc => npc.GetType().Equals(typeof(Enemy))).ToList().ForEach(npc => ((Enemy)npc).LoadAnimation());
    }

    public static void DrawNPCHUD()
    {
        entities.Where(npc => npc.GetType().Equals(typeof(NPC))).ToList().ForEach(npc => ((NPC)npc).DrawHUD());
    }
}

