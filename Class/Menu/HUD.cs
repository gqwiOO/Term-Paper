using System.Collections.Generic;
using Game1.Class;
using Game1.Class.Entity;
using Game1.Class.Item;
using Game1.Class.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Menu;

public class HUD
{
    public Player _player;
    private Fps _fps;
    private Inventory _inventory;
    public HUD(Player player)
    {
        _player = player;
        _fps = new Fps();
        _inventory = new Inventory(player);
    }

    public void Update(GameTime gameTime)
    {
        _fps.Update(gameTime);
    }

    public void Draw(SpriteBatch _spriteBatch,SpriteFont _font, Texture2D inventorySlot)
    {
        if (Game1.Game1._state == State.Playing)
        {
            _spriteBatch.DrawString(_font, $"HP: {_player._hp}", new Vector2(Game1.Game1._screenWidth - 200, 20), Color.Red);
            _spriteBatch.DrawString(_font, $"Pos: {_player._hitBox.X}  {_player._hitBox.Y}", new Vector2(10,250), Color.Black);
            _fps.DrawFps(_spriteBatch, _font,new Vector2(10, 150), Color.Black );
            _inventory.Draw(_spriteBatch, inventorySlot);
        }
    }
}

public class Inventory
{
    private Player _player;
    public List<Item> inventory= new List<Item>
    {
        null,null,null
    };

    public int currentItem;

    public Inventory(Player player)
    {
        _player = player;
        
    }

    public bool addItem(Item item)
    {
        if (inventory.Count > 5)
        {
            return false;
        }
        else
        {
            inventory.Add(item);
            return true;
        }
    }

    public void removeItem(Item item)
    {
        inventory.Remove(item);
    }
    public void removeItem(int index)
    {
        inventory.Remove(inventory[index]);
    }

    public void Draw(SpriteBatch spriteBatch, Texture2D inventorySlot)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            spriteBatch.Draw(inventorySlot, new Rectangle(i*64,5,64,64), Color.White);
        }
        
    }

}