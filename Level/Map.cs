using System;
using System.Linq;
using Game1.Class.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TiledSharp;

namespace Game1.Level;

public class Map
{
    public TmxMap map;
    
    int tileWidth;
    int tileHeight;
    int tilesetTilesWide;
    int tilesetTilesHigh;
    private Texture2D tileSet;
    
    public Map(TmxMap map, Texture2D tileSet)
    {
        this.map = map;
        this.tileSet = tileSet;
        
        tileWidth = map.Tilesets[0].TileWidth;
        tileHeight = map.Tilesets[0].TileHeight;
        tilesetTilesWide = this.tileSet.Width / tileWidth;
        tilesetTilesHigh = this.tileSet.Height / tileHeight;
    }

    public void Draw()
    {
            for (var i = 0; i < map.Layers[0].Tiles.Count; i++)
            {
                int gid = map.Layers[0].Tiles[i].Gid;
                if (!(gid == 0))
                {
                    float x = (i % map.Width) * map.TileWidth;
                    float y = (float)Math.Floor(i / (double)map.Width) * map.TileHeight;
                    if (Math.Abs(Globals.player._hitBox.X - x) < 1200 && Math.Abs(Globals.player._hitBox.Y - y) < 1200)
                    {
                        int tileFrame = gid - 1;
                        int column = tileFrame % tilesetTilesWide;
                        int row = (int)Math.Floor((double)tileFrame / tilesetTilesHigh);

                        Globals.spriteBatch.Draw(tileSet,
                            new Rectangle((int)x, (int)y, tileWidth, tileHeight),
                            new Rectangle(tileWidth * column, tileHeight * row, tileWidth, tileHeight),
                            Color.White);
                    }
                }
            }
    }
}