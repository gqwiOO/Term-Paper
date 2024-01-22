using System;
using System.Collections.Generic;
using System.Linq;
using Game1;
using Game1.Class;
using Game1.Class.Entity;
using Game1.Class.Item;
using Game1.Class.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TermPaper.Class.Audio;
using Keyboard = Microsoft.Xna.Framework.Input.Keyboard;

namespace Menu;

public class HUD
{
    private Fps _fps;
    private Inventory _inventory;
    private List<Byte> _hp = new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    private List<Byte> _stam = new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public Texture2D _fullHp { get; set; }
    public Texture2D _halfHp { get; set; }
    public Texture2D _emptyHp { get; set; }

    public Texture2D _fullStam { get; set; }
    public Texture2D _emptyStam { get; set; }

    public HUD()
    {
        _fps = new Fps();
    }

    public void Update()
    {
        _fps.Update();
        UpdateHPBar();
        UpdateStaminaBar();
    }

    public void UpdateStaminaBar()
    {
        float playerStam = Globals.player._stamina;

        for (int i = 0; i < _stam.Count; i++)
        {
            if (playerStam >= 10)
            {
                _stam[i] = 1;
                playerStam -= 10;
            }
            else
            {
                _stam[i] = 0;
            }
        }
    }
    
    public void Draw(SpriteFont _font, Texture2D inventorySlot, Texture2D currentInventorySlot, Texture2D helmetFrame,
        Texture2D chestPlateFrame, Texture2D bootsFrame,Texture2D arrowFrame)
    {
        if (Globals.gameState == State.Playing || Globals.gameState == State.Inventory)
        {
            Globals.spriteBatch.DrawString(_font, $"Pos: {Globals.player._hitBox.X}  {Globals.player._hitBox.Y}",
                new Vector2(10, 250), Color.Black);
            _fps.DrawFps(_font, new Vector2(10, 150), Color.Black);
            _inventory.Draw(inventorySlot, currentInventorySlot);
            _inventory.DrawInInventory(helmetFrame, chestPlateFrame, bootsFrame, arrowFrame);

            DrawHPBar();
        }
    }

    private void DrawStaminaBar()
    {
        for (int i = 0; i < _hp.Count; i++)
        {
            if (_stam[i] == 1)
            {
                Globals.spriteBatch.Draw(_fullStam,
                    new Rectangle(Game1.Game1._screenWidth - 330 + i * 30, 50, 40, 40), Color.White);
            }
            else if (_stam[i] == 0)
            {
                Globals.spriteBatch.Draw(_emptyStam,
                    new Rectangle(Game1.Game1._screenWidth + i * 30 - 330, 50, 40, 40), Color.White);
            }
        }
    }

    private void DrawHPBar()
    {
        for (int i = 0; i < _hp.Count; i++)
        {
            if (_hp[i] == 2)
            {
                Globals.spriteBatch.Draw(_fullHp, new Rectangle(Game1.Game1._screenWidth - 330 + i*30, 10, 40,40), Color.White );
            }
            else if (_hp[i] == 1)
            {
                Globals.spriteBatch.Draw(_halfHp, new Rectangle(Game1.Game1._screenWidth + i*30 -330, 10, 40,40), Color.White );
            }
            else if (_hp[i] == 0)
            {
                Globals.spriteBatch.Draw(_emptyHp, new Rectangle(Game1.Game1._screenWidth + i*30 - 330, 10, 40,40), Color.White );
            }
        }
    }

    private void UpdateHPBar()
    {
        int playerHp = Globals.player._hp;
        for(int i = 0; i < _hp.Count; i++)
        {
            if (playerHp >= 20)
            {
                _hp[i] = 2;
                playerHp -= 20;
            }
            else if (playerHp % 20 > 0)
            {
                _hp[i] = 1;
                playerHp -= playerHp % 20;
            }
            else
            {
                _hp[i] = 0;
            }
        }
    }

    
}

public class Inventory
{
    public Player _player;
    private int currentItem;
    
    public List<Item> inventory = new List<Item>
    {
        Data.Items.GetById(1),
        null,
        null,
        null,
        null
    };


    public Item getCurrentItem()
    {
        return inventory[currentItem];
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
        if (inventory[index] != null)
        {
            Globals.player.Sell(10);
        }
        inventory[index] = null;
    }
    public void Update()
    {
        UpdateKeyboard();
        
        inventory[currentItem].Update();
        
    }

    private void UpdateKeyboard()
    {
        if (Keyboard.GetState().IsKeyDown(Keys.D1) && Globals.gameState == State.Playing)
        {
            currentItem = 0;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.D2) && Globals.gameState == State.Playing)
        {
            currentItem = 1;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.D3) && Globals.gameState == State.Playing)
        {
            currentItem = 2;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.D4) && Globals.gameState == State.Playing)
        {
            currentItem = 3;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.D5) && Globals.gameState == State.Playing)
        {
            currentItem = 4;
        }
        
        if (Keyboard.GetState().IsKeyDown(Keys.G) && Globals.gameState == State.Playing &&
            currentItem != null)
        {
            dropItem(currentItem);
        }
        if (Movement.Input.hasBeenPressed(Keys.H))
        {
            for (int i = 0; i < inventory.Count; i++)
            {
                if (inventory[i] is Potion)
                {
                    inventory[i].Use();
                }
            }
        }
        if (Movement.Keyboard.hasBeenPressed(Keys.E) && Globals.gameState == State.Playing)
        {
            Globals.gameState = State.Inventory;

        }
        else if (Movement.Keyboard.hasBeenPressed(Keys.E) && Globals.gameState == State.Inventory)
        {
            Globals.gameState = State.Playing;
        }
    }

    public void Draw(Texture2D inventorySlot, Texture2D currentInventorySlot, Texture2D helmetFrame, Texture2D chestPlateFrame, Texture2D bootsFrame, Texture2D arrowFrame)
    {
        
        for (int i = 0; i < inventory.Count; i++)
        {
            if (i != currentItem)
            {
                Globals.spriteBatch.Draw(inventorySlot, new Rectangle(i * 80, 5, 80, 80), Color.White);
            }
            else
            {
                Globals.spriteBatch.Draw(currentInventorySlot, new Rectangle(i * 80, 9, 80, 80), Color.White);
                
            }
            
            if (inventory[i] != null)
            {
                if (i != currentItem)
                {
                    inventory[i].DrawInInventory(new Rectangle(i * 83, 18, 64, 64));
                }
                else
                {
                    inventory[i].DrawInInventory(new Rectangle(i * 83, 23, 64, 64));
                    
                }
            }
        }

        DrawInInventory(helmetFrame, chestPlateFrame, bootsFrame, arrowFrame);

    }
    public void DrawInInventory(Texture2D helmetFrame, Texture2D chestPlateFrame, Texture2D bootsFrame, Texture2D arrowFrame)
    {
        if (Globals.gameState == State.Inventory)
        {
            Globals.spriteBatch.Draw(helmetFrame, new Rectangle(Game1.Game1._screenWidth - 100, 400, 80, 80),
                Color.White);
            Globals.spriteBatch.Draw(chestPlateFrame, new Rectangle(Game1.Game1._screenWidth - 100, 480, 80, 80),
                Color.White);
            Globals.spriteBatch.Draw(bootsFrame, new Rectangle(Game1.Game1._screenWidth - 100, 560, 80, 80),
                Color.White);
            Globals.spriteBatch.Draw(arrowFrame, new Rectangle(Game1.Game1._screenWidth - 100, 660, 80, 80),
                Color.White);
        }
    }
}