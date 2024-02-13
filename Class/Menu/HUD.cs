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
using Audio;
using Keyboard = Microsoft.Xna.Framework.Input.Keyboard;
using TermPaper.Class.Font;

namespace Menu;

public class HUD
{
    private Fps _fps;
    private List<Byte> _hp = new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    private List<Byte> _stam = new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

    private Texture2D _fullHp = Globals.Content.Load<Texture2D>("HUD/HeartBar");
    private Texture2D _halfHp = Globals.Content.Load<Texture2D>("HUD/HeartBarDamaged");
    private Texture2D _emptyHp = Globals.Content.Load<Texture2D>("HUD/HeartBarEmpty");
    private Texture2D _fullStam = Globals.Content.Load<Texture2D>("HUD/staminaBar");
    private Texture2D _emptyStam = Globals.Content.Load<Texture2D>("HUD/staminaBarUsed");

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
    
    public void Draw()
    {
        if (Globals.gameState == State.Playing || Globals.gameState == State.Inventory)
        {
            Globals.spriteBatch.DrawString(Font.fonts["MainFont-24"], $"Pos: {Globals.player._hitBox.Center.X} " +
                                                                      $" {Globals.player._hitBox.Center.Y}",
                new Vector2(10, 250), Color.Black);
            _fps.DrawFps( new Vector2(10, 150), Color.Black);
             Globals.player.inventory.Draw();
            DrawHPBar();
            DrawStaminaBar();
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
    private int _currentItem;
    private Texture2D _inventorySlot = Globals.Content.Load<Texture2D>("HUD/inventorySlot");
    private Texture2D _currentInventorySlot = Globals.Content.Load<Texture2D>("HUD/currentInventorySlot");
    private Texture2D _helmetFrame = Globals.Content.Load<Texture2D>("HUD/HelmetFrame");
    private Texture2D _chestPlateFrame = Globals.Content.Load<Texture2D>("HUD/ChestPlateFrame");
    private Texture2D _bootsFrame = Globals.Content.Load<Texture2D>("HUD/BootsFrame");
    private Texture2D _arrowFrame = Globals.Content.Load<Texture2D>("HUD/ArrowFrame");
    
    public List<Item> inventory = new List<Item>
    {
        Data.Items.GetItemById(1),
        Data.Items.GetItemById(2),
        null,
        null,
        null
    };
    public List<int?> slotItemAmount = new List<int?>
    {
        1,
        5,
        null,
        null,
        null
    };
    
    public void decreaseItemAmountByOne(int index)
    {
        slotItemAmount[index] -= 1;
        if (slotItemAmount[index] == 0)
        {
            slotItemAmount[index] = null;
            inventory[index] = null;
        }
    }

    public int? getHealthPotionIndex()
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i] != null)
            {
                if (inventory[i].GetType().Equals(typeof(Potion))) return i;
            }
        }
        return null;
    }

    public Item getCurrentItem()
    {
        return inventory[_currentItem];
    }
    
    public bool addItem(Item item)
    {
        if (inventory.Count > 5)
        {
            return false;
        }
        inventory.Add(item);
        return true;
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
        if (Globals.gameState == State.Playing && !Globals.player.isDead)
        {
            UpdateKeyboardHotBar();
            if (inventory[_currentItem] != null)
            {
                inventory[_currentItem].Update();
            }
            if (Movement.Input.hasBeenPressed(Keys.E))
            {
                Globals.gameState = State.Inventory;
            }
        }
        else if (Globals.gameState == State.Inventory && !Globals.player.isDead)
        {
            UpdateInventory();
        }
    }

    private void UpdateKeyboardHotBar()
    {
        if (Keyboard.GetState().IsKeyDown(Keys.D1))
        {
            _currentItem = 0;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.D2))
        {
            _currentItem = 1;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.D3))
        {
            _currentItem = 2;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.D4))
        {
            _currentItem = 3;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.D5))
        {
            _currentItem = 4;
        }
        
        if (Keyboard.GetState().IsKeyDown(Keys.G) && _currentItem != null)
        {
            dropItem(_currentItem);
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
    }

    private static void UpdateInventory()
    {
        if (Movement.Input.hasBeenPressed(Keys.E))
        {
            Globals.gameState = State.Playing;
        }
    }

    public void Draw()
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            // Draw icon for current item in inventory
            if (i != _currentItem)
            {
                Globals.spriteBatch.Draw(_inventorySlot, new Rectangle(i * 80, 5, 80, 80), Color.White);
            }
            else
            {
                Globals.spriteBatch.Draw(_currentInventorySlot, new Rectangle(i * 80, 9, 80, 80), Color.White);
            }
            // Draw icons for items inventory
            if (inventory[i] != null)
            {
                if (i != _currentItem)
                {
                    inventory[i].DrawInInventory(new Rectangle(i * 83, 18, 64, 64));
                }
                else
                {
                    inventory[i].DrawInInventory(new Rectangle(i * 83, 23, 64, 64));
                }
            }
            // Draw item's amount in inventory 
            if (slotItemAmount[i] != null && slotItemAmount[i] != 1)
            {
                if (inventory[i]._isStackable)
                {
                    Globals.spriteBatch.DrawString(Font.fonts["MainFont-16"], slotItemAmount[i].ToString(),
                        new Vector2(i * 130, 60), Color.Black);
                }
            }
        }
        DrawInInventory();
    }
    public void DrawInInventory()
    {
        if (Globals.gameState == State.Inventory)
        {
            Globals.spriteBatch.Draw(_helmetFrame, new Rectangle(Game1.Game1._screenWidth - 100, 400, 80, 80),
                Color.White);
            Globals.spriteBatch.Draw(_chestPlateFrame, new Rectangle(Game1.Game1._screenWidth - 100, 480, 80, 80),
                Color.White);
            Globals.spriteBatch.Draw(_bootsFrame, new Rectangle(Game1.Game1._screenWidth - 100, 560, 80, 80),
                Color.White);
            Globals.spriteBatch.Draw(_arrowFrame, new Rectangle(Game1.Game1._screenWidth - 100, 660, 80, 80),
                Color.White);
        }
    }
}