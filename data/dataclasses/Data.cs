using System.Collections.Generic;
using System.IO;
using System.Text;
using Data;
using Game1.Class.Entity;
using Game1.Class.Item;
using Newtonsoft.Json;

namespace Game1.data.dataclasses;

public static class Data
{

    private static string savesPath = Path.Combine(Globals.project_path + "/data/player/");

    public static string getPlayerSavesPath()
    {
        return savesPath;
    }
    
    
    public static Player Load()
    {
        using StreamReader  fs =  new StreamReader(Path.Combine(savesPath, "lastsave.json"));
        string json = fs.ReadLine();
        PlayerInfo playerObj = JsonConvert.DeserializeObject<PlayerInfo>(json);
        Player obj = new Player(playerObj.balance, playerObj.inventory);
        return obj;
    }

    
    public static void Save(Player player)
    {
        //Get amount list of items without NULL
        Dictionary<int,List<int?>> itemsData = new Dictionary<int, List<int?>>();
        for (int i =0; i < player.inventory.slotItemAmount.Count;i++)
        {
            if(player.inventory.slotItemAmount[i] != null)itemsData.Add(i,new List<int?>(){Items.GetIdByItem(player.inventory.inventory[i]),
                player.inventory.slotItemAmount[i]});
            else
            {
                itemsData.Add(i,new List<int?>(){null,null});
            }
        }
        //Get IDs list of items without NULL
        List<int> ids = new List<int>();
        foreach (Item item in player.inventory.inventory)
        {
            if(item != null)ids.Add(item.id);
        }
        PlayerInfo playerObj = new PlayerInfo(player.Balance, itemsData);
        
        // Create folder with file and write it
        Directory.CreateDirectory((Path.GetDirectoryName(savesPath)));
        using FileStream fs = File.Create(Path.Combine(savesPath, "lastsave.json"));
        fs.Write(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(playerObj)));
    }
}


public class PlayerInfo
{
    public int balance { get; set; }
    public Dictionary<int,List<int?>> inventory{ get; set; }

    public PlayerInfo(int balance, Dictionary<int,List<int?>> inventory)
    {
        this.balance = balance;
        this.inventory = inventory;
    }
}