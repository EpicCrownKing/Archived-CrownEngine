using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using CrownEngine;

namespace CrownEngine.Engine
{
    public class TileGrid : Actor
    {
        public int[,] tileGrid;

        public int tileSize;

        public Tile[] types;

        public TileGrid(Vector2 _pos, int size, Stage stage, Tile[] _types, int[,] grid) : base(_pos, stage)
        {
            position = _pos;

            myStage = stage;

            tileSize = size;

            types = _types;

            tileGrid = grid;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < tileGrid.GetLength(1); i++)
            {
                for (int j = 0; j < tileGrid.GetLength(0); j++)
                {
                    if (GetTileType(i, j) != null) 
                    {
                        Tile tile = GetTileType(i, j);

                        Vector2 pos = position + (new Vector2(i, j) * tileSize);

                        Vector2 frame = GetTileFrame(i, j, tileSize);
                        Rectangle rect = new Rectangle((int)frame.X, (int)frame.Y, tileSize, tileSize);

                        spriteBatch.Draw(EngineHelpers.GetTexture(GetTileType(i, j).GetType().Name), pos - myStage.screenPosition, rect, tile.color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                    }
                }
            }
        }

        private Vector2 GetTileFrame(int i, int j, int tileScale)
        {
            int above = GetTileValue(i, j - 1);
            int below = GetTileValue(i, j + 1);
            int left = GetTileValue(i - 1, j);
            int right = GetTileValue(i + 1, j);

            if(above == 0 && below == 0 && left == 0 && right == 1)
            {
                return new Vector2(0, 0) * tileScale;
            }
            else if(above == 0 && below == 0 && left == 1 && right == 0)
            {
                return new Vector2(3, 0) * tileScale;
            }
            else if(above == 0 && below == 1 && left == 0 && right == 0)
            {
                return new Vector2(0, 1) * tileScale;
            }
            else if(above == 1 && below == 0 && left == 0 && right == 0)
            {
                return new Vector2(0, 4) * tileScale;
            }

            else if(above == 1 && below == 1 && left == 0 && right == 0)
            {
                return new Vector2(3, 2) * tileScale;
            }
            else if(above == 0 && below == 0 && left == 1 && right == 1)
            {
                return new Vector2(3, 1) * tileScale;
            }

            else if(above == 0 && below == 1 && left == 1 && right == 1)
            {
                return new Vector2(2, 3) * tileScale;
            }
            else if(above == 1 && below == 0 && left == 1 && right == 1)
            {
                return new Vector2(2, 5) * tileScale;
            }
            else if(above == 1 && below == 1 && left == 0 && right == 1)
            {
                return new Vector2(1, 4) * tileScale;
            }
            else if(above == 1 && below == 1 && left == 1 && right == 0)
            {
                return new Vector2(3, 4) * tileScale;
            }

            else if(above == 0 && below == 1 && left == 0 && right == 1)
            {
                return new Vector2(1, 3) * tileScale;
            }
            else if(above == 0 && below == 1 && left == 1 && right == 0)
            {
                return new Vector2(3, 3) * tileScale;
            }
            else if(above == 1 && below == 0 && left == 1 && right == 0)
            {
                return new Vector2(3, 5) * tileScale;
            }
            else if(above == 1 && below == 0 && left == 0 && right == 1)
            {
                return new Vector2(1, 5) * tileScale;
            }

            else if(above == 0 && below == 0 && left == 0 && right == 0)
            {
                return new Vector2(0, 5) * tileScale;
            }

            else if(above == 1 && below == 1 && left == 1 && right == 1)
            {
                int topLeft = GetTileValue(i - 1, j - 1);
                int topRight = GetTileValue(i + 1, j - 1);
                int bottomLeft = GetTileValue(i - 1, j + 1);
                int bottomRight = GetTileValue(i + 1, j + 1);

                if(topLeft == 1 && topRight == 1 && bottomLeft == 1 && bottomRight == 1)
                {
                    return new Vector2(2, 4) * tileScale;
                }

                else if(topLeft == 0 && topRight == 0 && bottomLeft == 1 && bottomRight == 1)
                {
                    return new Vector2(0, 2) * tileScale;
                }
                else if(bottomLeft == 0 && bottomRight == 0 && topLeft == 1 && topRight == 1)
                {
                    return new Vector2(0, 3) * tileScale;
                }
                else if(topLeft == 0 && bottomLeft == 0 && topRight == 1 && bottomRight == 1)
                {
                    return new Vector2(1, 0) * tileScale;
                }
                else if(topRight == 0 && bottomRight == 0 && topLeft == 1 && bottomLeft == 1)
                {
                    return new Vector2(2, 0) * tileScale;
                }

                else if(topLeft == 0 && bottomLeft == 1 && topRight == 1 && bottomRight == 1)
                {
                    return new Vector2(1, 1) * tileScale;
                }
                else if(topRight == 0 && bottomLeft == 1 && topLeft == 1 && bottomRight == 1)
                {
                    return new Vector2(2, 1) * tileScale;
                }
                else if(bottomLeft == 0 && topLeft == 1 && topLeft == 1 && bottomRight == 1)
                {
                    return new Vector2(1, 2) * tileScale;
                }
                else if(bottomRight == 0 && bottomLeft == 1 && topLeft == 1 && topLeft == 1)
                {
                    return new Vector2(2, 2) * tileScale;
                }
            }

            return new Vector2(2, 4) * tileScale;
        }

        public int GetTileValue(int i, int j)
        {
            if (i < 0) i = 0;
            if (j < 0) j = 0;

            if (i >= tileGrid.GetLength(1)) i = tileGrid.GetLength(1) - 1;
            if (j >= tileGrid.GetLength(0)) j = tileGrid.GetLength(0) - 1;

            if (tileGrid[j, i] > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public Tile GetTileType(int i, int j)
        {
            if (i < 0) i = 0;
            if (j < 0) j = 0;

            if (i >= tileGrid.GetLength(1)) i = tileGrid.GetLength(1) - 1;
            if (j >= tileGrid.GetLength(0)) j = tileGrid.GetLength(0) - 1;

            if (tileGrid[j, i] > 0)
            {
                return types[tileGrid[j, i]];
            }
            else
            {
                return types[0];
            }
        }
    }
}
