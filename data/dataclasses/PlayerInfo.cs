using System.Collections.Generic;
using Newtonsoft.Json;
using SharpDX;

namespace Game1.data.dataclasses;

public class PlayerInfo
{
    public Dictionary<int, int> inventory{ get; set; }
    public int hp{ get; set; }
    public int balance { get; set; }
    public Vector2 position{ get; set; }


    public void Load()
    {
        
    }

    public void Save()
    {
        
    }
}