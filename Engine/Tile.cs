using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using CrownEngine;

namespace CrownEngine.Engine
{
    public class Tile
    {
        public Texture2D texture;
        public int id;

        public void Draw()
        {
            //if(RollGame.activeLevel)
        }

        private Vector2 GetTileFrame(int i, int j)
        {
            if (GetTileValue(i, j) != 1) return new Vector2(-1, -1);

            if (GetTileValue(i - 1, j) == 0 && GetTileValue(i + 1, j) == 1 && GetTileValue(i, j - 1) == 1 && GetTileValue(i, j + 1) == 1)
            {
                return new Vector2(0, 8);
            }
            else if (GetTileValue(i - 1, j) == 1 && GetTileValue(i + 1, j) == 1 && GetTileValue(i, j - 1) == 0 && GetTileValue(i, j + 1) == 1)
            {
                return new Vector2(8, 0);
            }
            else if (GetTileValue(i - 1, j) == 0 && GetTileValue(i + 1, j) == 0 && GetTileValue(i, j - 1) == 0 && GetTileValue(i, j + 1) == 0)
            {
                return new Vector2(16, 24);
            }
            else if (GetTileValue(i - 1, j) == 1 && GetTileValue(i + 1, j) == 1 && GetTileValue(i, j - 1) == 1 && GetTileValue(i, j + 1) == 0)
            {
                return new Vector2(8, 16);
            }
            else if (GetTileValue(i - 1, j) == 1 && GetTileValue(i + 1, j) == 0 && GetTileValue(i, j - 1) == 1 && GetTileValue(i, j + 1) == 1)
            {
                return new Vector2(16, 8);
            }
            else if (GetTileValue(i - 1, j) == 0 && GetTileValue(i + 1, j) == 0 && GetTileValue(i, j - 1) == 0 && GetTileValue(i, j + 1) == 1)
            {
                return new Vector2(24, 0);
            }
            else if (GetTileValue(i - 1, j) == 0 && GetTileValue(i + 1, j) == 1 && GetTileValue(i, j - 1) == 0 && GetTileValue(i, j + 1) == 1)
            {
                return new Vector2(0, 0);
            }
            else if (GetTileValue(i - 1, j) == 1 && GetTileValue(i + 1, j) == 0 && GetTileValue(i, j - 1) == 0 && GetTileValue(i, j + 1) == 1)
            {
                return new Vector2(16, 0);
            }
            else if (GetTileValue(i - 1, j) == 0 && GetTileValue(i + 1, j) == 0 && GetTileValue(i, j - 1) == 1 && GetTileValue(i, j + 1) == 1)
            {
                return new Vector2(0, 24);
            }
            else if (GetTileValue(i - 1, j) == 0 && GetTileValue(i + 1, j) == 1 && GetTileValue(i, j - 1) == 1 && GetTileValue(i, j + 1) == 0)
            {
                return new Vector2(0, 16);
            }
            else if (GetTileValue(i - 1, j) == 1 && GetTileValue(i + 1, j) == 0 && GetTileValue(i, j - 1) == 1 && GetTileValue(i, j + 1) == 0)
            {
                return new Vector2(16, 16);
            }
            else if (GetTileValue(i - 1, j) == 0 && GetTileValue(i + 1, j) == 0 && GetTileValue(i, j - 1) == 1 && GetTileValue(i, j + 1) == 0)
            {
                return new Vector2(24, 24);
            }
            else if (GetTileValue(i - 1, j) == 0 && GetTileValue(i + 1, j) == 1 && GetTileValue(i, j - 1) == 0 && GetTileValue(i, j + 1) == 0)
            {
                return new Vector2(0, 32);
            }
            else if (GetTileValue(i - 1, j) == 1 && GetTileValue(i + 1, j) == 1 && GetTileValue(i, j - 1) == 0 && GetTileValue(i, j + 1) == 0)
            {
                return new Vector2(8, 24);
            }
            else if (GetTileValue(i - 1, j) == 1 && GetTileValue(i + 1, j) == 0 && GetTileValue(i, j - 1) == 0 && GetTileValue(i, j + 1) == 0)
            {
                return new Vector2(24, 24);
            }
            else if (GetTileValue(i - 1, j) == 1 && GetTileValue(i + 1, j) == 1 && GetTileValue(i, j - 1) == 1 && GetTileValue(i, j + 1) == 1)
            {
                return new Vector2(8, 8);
            }
            else
            {
                return new Vector2(-1, -1);
            }
        }

        private int GetTileValue(int i, int j)
        {
            if (i < 0 || j < 0 || i >= EngineGame.instance.activeStage.tilemap.GetLength(1) || j >= EngineGame.instance.activeStage.tilemap.GetLength(0))
            {
                return 1;
            }
            else
            {
                return EngineGame.instance.activeStage.tilemap[j, i].id;
            }
        }
    }
}
