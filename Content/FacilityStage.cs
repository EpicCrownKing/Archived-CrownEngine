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

        public int[,] roomLayout = new int[,]
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

        public override void Load()
        {
            AddActor(new Player(new Vector2(80, 120), Vector2.Zero, this));

            TileGrid grid = new TileGrid(new Vector2(0, 40), 8, this, new Tile[]
            {
                null,
                new Bricks(Color.White)
            }, roomLayout);

            AddActor(grid);

            gridsToUpdate.Add(grid);

            TileGrid grid2 = new TileGrid(new Vector2(-160, 40), 8, this, new Tile[]
            {
                null,
                new Bricks(Color.Red)
            }, roomLayout);

            AddActor(grid2);

            gridsToUpdate.Add(grid2);

            TileGrid grid3 = new TileGrid(new Vector2(0, -120), 8, this, new Tile[]
            {
                null,
                new Bricks(Color.Green)
            }, roomLayout);

            AddActor(grid3);

            gridsToUpdate.Add(grid3);

            TileGrid grid4 = new TileGrid(new Vector2(160, 40), 8, this, new Tile[]
            {
                null,
                new Bricks(Color.Blue)
            }, roomLayout);

            AddActor(grid4);

            gridsToUpdate.Add(grid4);

            TileGrid grid5 = new TileGrid(new Vector2(0, 200), 8, this, new Tile[]
            {
                null,
                new Bricks(Color.Yellow)
            }, roomLayout);

            AddActor(grid5);

            gridsToUpdate.Add(grid5);

            internalScreenPos = new Vector2(80, 120);

            base.Load();
        }
    }
}
