using System.Collections.Generic;
using Game1;
using Microsoft.Xna.Framework.Graphics;

namespace TermPaper.Class.Font;

public static class Font
{
    public static Dictionary<string, SpriteFont> fonts = new Dictionary<string, SpriteFont>()
    {
        ["MainFont-24"] = Globals.Content.Load<SpriteFont>("Fonts/MainFont-24"),
        ["MainFont-16"] = Globals.Content.Load<SpriteFont>("Fonts/MainFont-16"),
    };
    
}