using System;
using Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Movement;
using TermPaper.Class.Font;

namespace Game1.Class.Entity;

public class BuyInterface
{
    private NPC npc;
    private int horizontalCell;
    private int verticalCell;
    
    public BuyInterface(NPC npc)
    {
        this.npc = npc;
    }
    public Texture2D _npcShopBackground = Globals.Content.Load<Texture2D>("NPC/Interface/shopInterfaceBackground");
    private int _widthNPCShopBackground = Game1._screenWidth;
    private int _heightNPCShopBackground = Game1._screenHeight;
    
    public void Update()
    {
        _widthNPCShopBackground = Game1._screenWidth;
        _heightNPCShopBackground = Game1._screenHeight;
        horizontalCell = _widthNPCShopBackground / 32;    // if 1920 -> 120, 1600 -> 100
        verticalCell = _heightNPCShopBackground / 32;    // if 1920 -> 120, 1600 -> 100
    }
    
    public void Draw()
    {
        if (Globals.gameState == State.State.InShop)
        {
            
            // Interface Background
            Globals.spriteBatch.Draw(_npcShopBackground,
                new Rectangle(0,0,_widthNPCShopBackground,_heightNPCShopBackground), null, Color.White,
                0,new Vector2(0, 0),SpriteEffects.None, 0f);
            
            // Shop name
            Globals.spriteBatch.DrawString(Font.fonts["MainFont-24"], $"{npc.name}'s shop", new Vector2(horizontalCell / 5  , verticalCell / 3), Color.Black);
            
            // Player HP
            Globals.spriteBatch.DrawString(Font.fonts["MainFont-24"], $"{Globals.Player._hp}", new Vector2(horizontalCell * 25, verticalCell * 5 ), Color.Red);
            
            // Player Balance
            Globals.spriteBatch.DrawString(Font.fonts["MainFont-24"], $"{Globals.Player.Balance}", new Vector2(horizontalCell * 29, verticalCell * 5 ), Color.Yellow);
            
            //Arrow Sprite
            Globals.spriteBatch.Draw(Globals.Content.Load<Texture2D>("Items/Weapon/Bow/Arrow"), new Rectangle(horizontalCell * 24, verticalCell * 8, 100,40 ), Color.Yellow);
            
            // Potion Sprite
            Globals.spriteBatch.Draw(Globals.Content.Load<Texture2D>("Items/Potion/smallPotion"), new Rectangle(horizontalCell * 28, verticalCell * 8, 100,100 ), Color.Yellow);
            
            // Potion amount
            if (Globals.Player.inventory.getHealthPotionIndex() != null)
            {
                Globals.spriteBatch.DrawString(Font.fonts["MainFont-24"],$"{Globals.Player.inventory.slotItemAmount[(int)Globals.Player.inventory.getHealthPotionIndex()]}", new Vector2(horizontalCell * 28, verticalCell * 10), Color.Black);
            }
            else
            {
                Globals.spriteBatch.DrawString(Font.fonts["MainFont-24"],$"0", new Vector2(horizontalCell * 28, verticalCell * 10), Color.Black);
            }
            
            // Arrow amount
            Globals.spriteBatch.DrawString(Font.fonts["MainFont-24"], $"100", new Vector2(horizontalCell * 24, verticalCell * 10 ),
                Color.Black);
            
            // Sword slot 
            Globals.spriteBatch.Draw(Globals.Content.Load<Texture2D>("HUD/inventorySlot"), new Rectangle(horizontalCell * 24, verticalCell * 12, 100,100 ), Color.Yellow);

            // Bow slot 
            Globals.spriteBatch.Draw(Globals.Content.Load<Texture2D>("HUD/inventorySlot"), new Rectangle(horizontalCell * 24, verticalCell * 16, 100,100 ), Color.Yellow);

            // Accessory slot
            Globals.spriteBatch.Draw(Globals.Content.Load<Texture2D>("HUD/inventorySlot"), new Rectangle(horizontalCell * 24, verticalCell * 20, 100,100 ), Color.Yellow);
            
            
            // Helmet slot
            Globals.spriteBatch.Draw(Globals.Content.Load<Texture2D>("HUD/inventorySlot"), new Rectangle(horizontalCell * 28, verticalCell * 12, 100,100 ), Color.Yellow);
            
            // Chestplate slot
            Globals.spriteBatch.Draw(Globals.Content.Load<Texture2D>("HUD/inventorySlot"), new Rectangle(horizontalCell * 28, verticalCell * 16, 100,100 ), Color.Yellow);

            // Boots slot
            Globals.spriteBatch.Draw(Globals.Content.Load<Texture2D>("HUD/inventorySlot"), new Rectangle(horizontalCell * 28, verticalCell * 20, 100,100 ), Color.Yellow);
            
            
            
            for(int j = 1; j<3;j++)
            {
                for (int i = 0; i < npc.shopItemsID.Count / 2; i++)
                {
                    // Inventory slots
                    Globals.spriteBatch.Draw(Globals.Content.Load<Texture2D>("HUD/inventorySlot"), new Rectangle(horizontalCell * (i + 1) * 3, verticalCell * 10 * j, 150,150 ), null,Color.White, 0f, new Vector2(16, 16), SpriteEffects.None, 1);
                    // Shop items
                    Items.GetItemById(npc.shopItemsID[i + ((j-1)*5)]).Draw(new Rectangle(horizontalCell * (i + 1) * 3, verticalCell * 10 * j, 100,100 ));
                }
            }
        }
    }
}