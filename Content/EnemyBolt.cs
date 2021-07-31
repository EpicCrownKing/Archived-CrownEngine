using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using CrownEngine.Engine;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace CrownEngine.Content
{
    public class EnemyBolt : PhysicsActor
    {
        public EnemyBolt(Vector2 pos, Vector2 vel, Stage stage, Actor myOwner) : base(pos, vel, stage)
        {
            position = pos;
            velocity = vel;

            myStage = stage;

            owner = myOwner;
        }

        public override int width => 6;
        public override int height => 6;

        public Actor owner;

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void PhysicsActorUpdate()
        {
            ManageCollision();

            base.PhysicsActorUpdate();
        }

        public override void PhysicsActorLoad()
        {
            base.PhysicsActorLoad();
        }

        private void ManageCollision()
        {
            for (int k = 0; k < myStage.gridsToUpdate.Count; k++)
            {
                for (int i = 0; i < myStage.gridsToUpdate[k].tileGrid.GetLength(1); i++)
                {
                    for (int j = 0; j < myStage.gridsToUpdate[k].tileGrid.GetLength(0); j++)
                    {
                        if (myStage.gridsToUpdate[k].tileGrid[j, i] > 0)
                        {
                            TileGrid grid = myStage.gridsToUpdate[k];
                            Rectangle tileRect = new Rectangle((i * grid.tileSize) + (int)grid.position.X, (j * grid.tileSize) + (int)grid.position.Y, grid.tileSize, grid.tileSize);

                            Point temp = new Point(height / 2, height / 2);
                            Rectangle playerRect = new Rectangle((int)position.X - temp.X, (int)position.Y - temp.Y, height, height);

                            if ((velocity.X > 0 && EngineHelpers.IsTouchingLeft(playerRect, tileRect, velocity)) ||
                                (velocity.X < 0 && EngineHelpers.IsTouchingRight(playerRect, tileRect, velocity)))
                                Kill();

                            if ((velocity.Y > 0 && EngineHelpers.IsTouchingTop(playerRect, tileRect, velocity)) ||
                                (velocity.Y < 0 && EngineHelpers.IsTouchingBottom(playerRect, tileRect, velocity)))
                                Kill();
                        }
                    }
                }
            }

            for(int k = 0; k < myStage.actors.Count; k++)
            {
                if(myStage.actors[k] != null && myStage.actors[k] != owner && myStage.actors[k] != this && myStage.actors[k].rect.Intersects(this.rect))
                {
                    (myStage.actors[k] as Player).hp--;
                    Kill();
                }
            }
        }
    }
}
