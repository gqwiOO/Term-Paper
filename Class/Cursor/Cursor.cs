using System.Collections.Generic;
using Data;
using Game1;
using Game1.Class.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TermPaper.Class.Cursor;

public static class Cursor
{
    private static List<Texture2D> cursorsSprites = new List<Texture2D>()
    {
        Globals.Content.Load<Texture2D>("Cursor/cursor_default"),
        Globals.Content.Load<Texture2D>("Cursor/cursor_select"),
    };
    private static int? currentCursorIndex;
    
    public static void setCursor(int index)
    {
        if (currentCursorIndex != index)
        {
            Mouse.SetCursor(MouseCursor.FromTexture2D(cursorsSprites[index], 0, 0));
            currentCursorIndex = index;
        }
    }
    
    public static void UpdateCursorStyle()
    {
        if (Globals.gameState == State.Playing)
        {
            if (Entities.isMouseOnAnyNPC())
            {
                Cursor.setCursor(1);
            }
            else
            {
                Cursor.setCursor(0);
            }
        }
        if (Globals.gameState == State.InShop)
        {
            Cursor.setCursor(0);
        }
    }
}