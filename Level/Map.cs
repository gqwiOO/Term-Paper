using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Xml.Linq;
using Game1.Class.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Sprites;
using TiledSharp;

namespace Game1.Level;

public class Map
{
    public TmxMap map;
    
    Texture2D tileset;
    
    int tileWidth;
    int tileHeight;
    int tilesetTilesWide;
    int tilesetTilesHigh;
    private Player _player;
    
    public Map(string path, Texture2D tileset, Player player)
    {
        map = new TmxMap(path);
        tileWidth = map.Tilesets[0].TileWidth;
        tileHeight = map.Tilesets[0].TileHeight;
        tilesetTilesWide = tileset.Width / tileWidth;
        tilesetTilesHigh = tileset.Height / tileHeight;
        _player = player;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        for (var i = 0; i < map.Layers[0].Tiles.Count; i++)
        {
            int gid = map.Layers[0].Tiles[i].Gid;

            // Empty tile, do nothing
            if (gid == 0)
            {

            }
            else
            {
                int tileFrame = gid - 1;
                int column = tileFrame % tilesetTilesWide;
                int row = (int)Math.Floor((double)tileFrame / (double)tilesetTilesWide);

                float x = (i % map.Width) * map.TileWidth;
                float y = (float)Math.Floor(i / (double)map.Width) * map.TileHeight;

                if (Math.Abs(_player._hitBox.X - x) < 1000 && Math.Abs(_player._hitBox.Y - y) < 1000)
                {
                    Rectangle tilesetRec = new Rectangle(tileWidth * column, tileHeight * row, tileWidth, tileHeight);

                    spriteBatch.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec,
                        Color.White);
                }
                
            }
        }
    }
}