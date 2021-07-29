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
        public int transTime =  -1;

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
                Debug.Write(1 - (transTime / 16f));
                transTime--;
            }

            screenPosition = internalScreenPos - new Vector2(80, 120);

            base.Draw(spriteBatch);

            spriteBatch.Draw(EngineHelpers.GetTexture("Blank"), new Rectangle(0, 0, 160, 40), Color.Black);
        }

        public override void Load()
        {
            AddActor(new Player(new Vector2(80, 120), Vector2.Zero, this));

            int[,] roomsLayout = new int[5, 5];

            roomsLayout[2, 2] = 1;

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
                                if(i + 1 < 5)
                                    roomsLayout[j, i + 1] = 1;
                            if (rand == 1)
                                if (i > 0)
                                    roomsLayout[j, i - 1] = 1;
                            if (rand == 2)
                                if (j + 1 < 5)
                                    roomsLayout[j + 1, i] = 1;
                            if (rand == 3)
                                if (j > 0)
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

                        for (int k = 7; k < 13; k++)
                        {
                            thisRoomStructure[k, 19] = 0;
                        }
                        for (int k = 7; k < 13; k++)
                        {
                            thisRoomStructure[k, 0] = 0;
                        }
                        for (int k = 7; k < 13; k++)
                        {
                            thisRoomStructure[19, k] = 0;
                        }
                        for (int k = 7; k < 13; k++)
                        {
                            thisRoomStructure[0, k] = 0;
                        }

                        if (i + 1 >= 5 || roomsLayout[j, i + 1] == 0) //right
                        {
                            for(int k = 7; k < 13; k++)
                            {
                                thisRoomStructure[k, 19] = 1;
                            }
                        }
                        if (i <= 0 || roomsLayout[j, i - 1] == 0) //left
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
                        if (j <= 0 || roomsLayout[j - 1, i] == 0) //above
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
                }
            }

            internalScreenPos = new Vector2(80, 120);

            base.Load();
        }
    }
}
