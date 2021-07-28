using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using CrownEngine;

namespace CrownEngine.Engine
{
    public class TileGrid : Sprite
    {
        public int[,] tileGrid;

        public int width;
        public int height;

        public int tileWidth;
        public int tileHeight;

        public bool solid;

        public Stage myStage;

        public override void Draw(SpriteBatch spriteBatch)
        {
            for(int i = 0; i < tileGrid.GetLength(1); i++)
            {
                for(int j = 0; j < tileGrid.GetLength(0); j++)
                {
                    spriteBatch.Draw(EngineGame.instance.activeStage.);
                }
            }
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

        public TileGrid(Vector2 _pos, Point size, bool _solid, Stage stage)
        {
            position = _pos;

            width = size.X;
            height = size.Y;

            solid = _solid;

            myStage = stage;
        }
    }
}
