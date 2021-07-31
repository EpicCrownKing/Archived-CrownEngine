using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using CrownEngine.Engine;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace CrownEngine.Content
{
    public class EnemyShip : PhysicsActor
    {
        public EnemyShip(Vector2 pos, Vector2 vel, Stage stage) : base(pos, vel, stage)
        {
            position = pos;
            velocity = vel;

            myStage = stage;
        }

        public override int width => 8;
        public override int height => 14;

        public int hp = 3;

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public float rotationVel;

        public override void PhysicsActorUpdate()
        {
            rotationVel = rotationVel.Clamp(-0.1f, 0.1f);

            rotation += rotationVel;

            if (EngineGame.instance.keyboardState.IsKeyDown(Keys.T) && !EngineGame.instance.oldKeyboardState.IsKeyDown(Keys.T))
            {
                myStage.AddActor(new EnemyBolt(position, (-Vector2.UnitY).RotatedBy(rotation) * 3, myStage, this));
            }

            velocity = velocity.ClampVectorMagnitude(3f);

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
                                velocity.X = 0;

                            if ((velocity.Y > 0 && EngineHelpers.IsTouchingTop(playerRect, tileRect, velocity)) ||
                                (velocity.Y < 0 && EngineHelpers.IsTouchingBottom(playerRect, tileRect, velocity)))
                                velocity.Y = 0;
                        }
                    }
                }
            }
        }
    }
}
