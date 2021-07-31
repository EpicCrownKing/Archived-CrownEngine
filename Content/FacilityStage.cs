using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using CrownEngine.Engine;
using static CrownEngine.Engine.EngineHelpers;
using System.Diagnostics;

namespace CrownEngine.Content
{
    public class Facility : Stage
    {
        public override Color bgColor => Color.Black;

        public override void Update()
        {
            base.Update();
        }

        public Vector2 internalScreenPos;

        public Vector2 screenMovement;
        public Vector2 oldScreenPos;
        public int transTime = -1;

        public override void Draw(SpriteBatch spriteBatch)
        {
            if(actors[0].position.X < internalScreenPos.X - 80 && transTime <= 0)
            {
                oldScreenPos = internalScreenPos;
                screenMovement = -Vector2.UnitX;
                transTime = 16;
            }
            if (actors[0].position.X > internalScreenPos.X + 80 && transTime <= 0)
            {
                oldScreenPos = internalScreenPos;
                screenMovement = Vector2.UnitX;
                transTime = 16;
            }

            if (actors[0].position.Y < internalScreenPos.Y - 80 && transTime <= 0)
            {
                oldScreenPos = internalScreenPos;
                screenMovement = -Vector2.UnitY;
                transTime = 16;
            }
            if (actors[0].position.Y > internalScreenPos.Y + 80 && transTime <= 0)
            {
                oldScreenPos = internalScreenPos;
                screenMovement = Vector2.UnitY;
                transTime = 16;
            }

            if(transTime >= 0)
            {
                internalScreenPos = Vector2.SmoothStep(oldScreenPos, oldScreenPos + (screenMovement * 160), 1 - (transTime / 16f));
                transTime--;
            }

            Debug.Write(transTime);

            screenPosition = internalScreenPos - new Vector2(80, 120);

            base.Draw(spriteBatch);

            spriteBatch.Draw(GetTexture("Blank"), new Rectangle(0, 0, 160, 40), Color.Black);

            spriteBatch.Draw(GetTexture("MapFrame"), new Rectangle(119, 7, 26, 26), Color.White);

            Vector2 camGridPos = new Vector2(0 + internalScreenPos.X - 80, 40 + internalScreenPos.Y - 120);

            Vector2 point = (new Vector2(camGridPos.X + 80, camGridPos.Y - 40 + 120) / 160) + new Vector2(2, 2);

            if (roomsLayout[(int)point.Y, (int)point.X] == 1)
            {
                spriteBatch.Draw(GetTexture("MapIcon"), new Rectangle(128, 16, 8, 8), new Rectangle(0, 0, 8, 8), Color.White);
            }
            if (roomsLayout[(int)point.Y, (int)point.X] == 2)
            {
                spriteBatch.Draw(GetTexture("MapIcon"), new Rectangle(128, 16, 8, 8), new Rectangle(8, 0, 8, 8), Color.White);
            }

            if ((int)point.Y - 1 >= 0)
            {
                if (roomsLayout[(int)point.Y - 1, (int)point.X] == 1)
                {
                    spriteBatch.Draw(GetTexture("MapIcon"), new Rectangle(128, 8, 8, 8), new Rectangle(0, 8, 8, 8), Color.White);
                }
                if (roomsLayout[(int)point.Y - 1, (int)point.X] == 2)
                {
                    spriteBatch.Draw(GetTexture("MapIcon"), new Rectangle(128, 8, 8, 8), new Rectangle(8, 8, 8, 8), Color.White);
                }
            }

            if ((int)point.Y + 1 <= 4)
            {
                if (roomsLayout[(int)point.Y + 1, (int)point.X] == 1)
                {
                    spriteBatch.Draw(GetTexture("MapIcon"), new Rectangle(128, 24, 8, 8), new Rectangle(0, 8, 8, 8), Color.White);
                }
                if (roomsLayout[(int)point.Y + 1, (int)point.X] == 2)
                {
                    spriteBatch.Draw(GetTexture("MapIcon"), new Rectangle(128, 24, 8, 8), new Rectangle(8, 8, 8, 8), Color.White);
                }
            }

            if ((int)point.X - 1 >= 0)
            {
                if (roomsLayout[(int)point.Y, (int)point.X - 1] == 1)
                {
                    spriteBatch.Draw(GetTexture("MapIcon"), new Rectangle(120, 16, 8, 8), new Rectangle(0, 8, 8, 8), Color.White);
                }
                if (roomsLayout[(int)point.Y, (int)point.X - 1] == 2)
                {
                    spriteBatch.Draw(GetTexture("MapIcon"), new Rectangle(120, 16, 8, 8), new Rectangle(8, 8, 8, 8), Color.White);
                }
            }

            if ((int)point.X + 1 <= 4)
            {
                if (roomsLayout[(int)point.Y, (int)point.X + 1] == 1)
                {
                    spriteBatch.Draw(GetTexture("MapIcon"), new Rectangle(136, 16, 8, 8), new Rectangle(0, 8, 8, 8), Color.White);
                }
                if (roomsLayout[(int)point.Y, (int)point.X + 1] == 2)
                {
                    spriteBatch.Draw(GetTexture("MapIcon"), new Rectangle(136, 16, 8, 8), new Rectangle(8, 8, 8, 8), Color.White);
                }
            }

            spriteBatch.Draw(GetTexture("Player"), new Rectangle(12, 12, 8, 12), new Rectangle(0, 0, 8, 12), Color.White);
            spriteBatch.Draw(GetTexture("Player"), new Rectangle(24, 12, 8, 12), new Rectangle(0, 0, 8, 12), Color.White);
            spriteBatch.Draw(GetTexture("Player"), new Rectangle(36, 12, 8, 12), new Rectangle(0, 0, 8, 12), Color.White);
        }

        public int[,] roomsLayout = new int[5, 5];
        public override void Load()
        {
            AddActor(new Player(new Vector2(80, 120), Vector2.Zero, this));

            roomsLayout[2, 2] = 1;
            roomsLayout[3, 2] = 1;
            roomsLayout[4, 2] = 2;

            for (int k = 0; k < 4; k++)
            {
                for (int i = 0; i < roomsLayout.GetLength(1); i++)
                {
                    for (int j = 0; j < roomsLayout.GetLength(0); j++)
                    {
                        if(roomsLayout[j, i] == 1)
                        {
                            int rand = EngineGame.instance.random.Next(4);

                            if (rand == 0)
                                if(i + 1 < 5 && roomsLayout[j, i + 1] == 0)
                                    roomsLayout[j, i + 1] = 1;
                            if (rand == 1)
                                if (i > 0 && roomsLayout[j, i - 1] == 0)
                                    roomsLayout[j, i - 1] = 1;
                            if (rand == 2)
                                if (j + 1 < 5 && roomsLayout[j + 1, i] == 0)
                                    roomsLayout[j + 1, i] = 1;
                            if (rand == 3)
                                if (j > 0 && roomsLayout[j - 1, i] == 0)
                                    roomsLayout[j - 1, i] = 1;
                        }
                    }
                }
            }

            for (int i = 0; i < roomsLayout.GetLength(1); i++)
            {
                for (int j = 0; j < roomsLayout.GetLength(0); j++)
                {
                    if (roomsLayout[j, i] == 1)
                    {
                        int[,] thisRoomStructure = new int[,]
                        {
                             { 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1 },
                             { 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 },
                             { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                             { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                             { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                             { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                             { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                             { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                             { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                             { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                             { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                             { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                             { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                             { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                             { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                             { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                             { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                             { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                             { 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 },
                             { 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1 },
                        };

                        if (i + 1 >= 5 || roomsLayout[j, i + 1] != 1) //right
                        {
                            for(int k = 7; k < 13; k++)
                            {
                                thisRoomStructure[k, 19] = 1;
                            }
                        }
                        if (i <= 0 || roomsLayout[j, i - 1] != 1) //left
                        {
                            for (int k = 7; k < 13; k++)
                            {
                                thisRoomStructure[k, 0] = 1;
                            }
                        }

                        if (j + 1 >= 5 || roomsLayout[j + 1, i] == 0) //below
                        {
                            for (int k = 7; k < 13; k++)
                            {
                                thisRoomStructure[19, k] = 1;
                            }
                        }
                        if (j <= 0 || roomsLayout[j - 1, i] != 1) //above
                        {
                            for (int k = 7; k < 13; k++)
                            {
                                thisRoomStructure[0, k] = 1;
                            }
                        }


                        TileGrid grid = new TileGrid(new Vector2(0 + ((i - 2) * 160), 40 + ((j - 2) * 160)), 8, this, new Tile[]
                        {
                            null,
                            new Bricks(Color.White)
                        }, thisRoomStructure);

                        AddActor(grid);

                        gridsToUpdate.Add(grid);
                    }

                    if(roomsLayout[j, i] == 2)
                    {
                        int[,] thisRoomStructure = new int[,]
                        {
                             { 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1 },
                             { 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 },
                             { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                             { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                             { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                             { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                             { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                             { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                             { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                             { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                             { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                             { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                             { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                             { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                             { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                             { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                             { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                             { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                             { 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 },
                             { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                        };


                        TileGrid bossGrid = new TileGrid(new Vector2(0 + ((i - 2) * 160), 40 + ((j - 2) * 160)), 8, this, new Tile[]
                        {
                            null,
                            new Bricks(Color.Red)
                        }, thisRoomStructure);

                        AddActor(bossGrid);

                        gridsToUpdate.Add(bossGrid);
                    }
                }
            }

            AddActor(new BossShip(new Vector2(80, 484), Vector2.Zero, this));

            internalScreenPos = new Vector2(80, 120);

            base.Load();
        }
    }
}
