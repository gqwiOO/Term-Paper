using System;
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
    private Player _player;
    private Texture2D tileSet;
    
    public Map(TmxMap map, Texture2D tileSet, Player player)
    {
        this.map = map;
        this.tileSet = tileSet;
        _player = player;
  
        tileWidth = map.Tilesets[0].TileWidth;
        tileHeight = map.Tilesets[0].TileHeight;
        tilesetTilesWide = this.tileSet.Width / tileWidth;
        tilesetTilesHigh = this.tileSet.Height / tileHeight;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        for (var i = 0; i < map.Layers[0].Tiles.Count ; i++)
        {
            int gid = map.Layers[0].Tiles[i].Gid;
            if (!(gid == 0))
            {
                float x = (i % map.Width) * map.TileWidth;
                float y = (float)Math.Floor(i / (double)map.Width) * map.TileHeight;
                if (Math.Abs(_player._hitBox.X - x) < 1200 && Math.Abs(_player._hitBox.Y - y) < 1200)
                {
                    int tileFrame = gid - 1;
                     int column = tileFrame % tilesetTilesWide;
                     int row = (int)Math.Floor((double)tileFrame / tilesetTilesHigh);
            
                    spriteBatch.Draw(tileSet,
                        new Rectangle((int)x, (int)y, tileWidth, tileHeight),
                        new Rectangle(tileWidth * column, tileHeight * row , tileWidth, tileHeight),
                        Color.White);
                }
            }
        }
    }
}