using System.Collections.Generic;
using Game1.Class;
using Game1.Class.Entity;
using Game1.Class.Item;
using Game1.Class.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
        _inventory = new Inventory();
    }

    public void Update(GameTime gameTime, State gameState)
    {
        
        _fps.Update(gameTime);
        _inventory.Update(gameState);
    }

    public void Draw(SpriteBatch _spriteBatch,SpriteFont _font, Texture2D inventorySlot, Texture2D currentInventorySlot)
    {
        if (Game1.Game1._state == State.Playing)
        {
            _spriteBatch.DrawString(_font, $"HP: {_player._hp}", new Vector2(Game1.Game1._screenWidth - 200, 20), Color.Red);
            _spriteBatch.DrawString(_font, $"Pos: {_player._hitBox.X}  {_player._hitBox.Y}", new Vector2(10,250), Color.Black);
            _fps.DrawFps(_spriteBatch, _font,new Vector2(10, 150), Color.Black );
            _inventory.Draw(_spriteBatch, inventorySlot, currentInventorySlot);
        }
    }
}

public class Inventory
{
    public List<Item> inventory = new List<Item>
    {
        Game1.Game1.allItems[0],
        null,
        null,
        null,
        null
    };

    public int currentItem;

    public Inventory()
    {
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

    public void dropItem(int index)
    {
        inventory[index] = null;
    }


    public void Update(State gameState)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.D1) && gameState == State.Playing)
        {
            currentItem = 0;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.D2) && gameState == State.Playing)
        {
            currentItem = 1;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.D3) && gameState == State.Playing)
        {
            currentItem = 2;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.D4) && gameState == State.Playing)
        {
            currentItem = 3;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.D5) && gameState == State.Playing)
        {
            currentItem = 4;
        }
        
        if (Keyboard.GetState().IsKeyDown(Keys.G) && gameState == State.Playing &&
           currentItem != null)
        {
            dropItem(currentItem);
        }
    }

    public void Draw(SpriteBatch spriteBatch, Texture2D inventorySlot, Texture2D currentInventorySlot)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (i != currentItem)
            {
                spriteBatch.Draw(inventorySlot, new Rectangle(i * 64, 5, 64, 64), Color.White);
            }
            else
            {
                spriteBatch.Draw(currentInventorySlot, new Rectangle(i * 64, 5, 64, 64), Color.White);

            }
            
            
            if (inventory[i] != null)
            {
                inventory[i].DrawInInventory( new Rectangle(i*64 - 2,15,64,64), spriteBatch);
            }
        }
        
    }

}