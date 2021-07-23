using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using CrownEngine.Engine;
using static CrownEngine.Engine.EngineHelpers;

namespace CrownEngine.Content
{
    public class Player : Actor
    {
        private int jumpTimer;
        private float gravityFactor = 0.3f;

        public override int width => 16;
        public override int height => 16;

        private int squashHeight;

        public Player(Vector2 pos, Vector2 vel) : base(pos, vel)
        {
            position = pos;
            velocity = vel;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int)position.X + (int)(16 - (scale.X * 16)), (int)position.Y + (int)(16 - (scale.Y * 16)), (int)(scale.X * 16), (int)(scale.Y * 16)), new Rectangle(0, 0, width, height), Color.White);
        }

        public override void Update()
        {
            velocity.Y += gravityFactor;
            velocity.Y = EngineHelpers.Clamp(velocity.Y, -100f, 8f);

            if (Keyboard.GetState().IsKeyDown(Keys.A))
                velocity.X -= 0.1f;

            if (Keyboard.GetState().IsKeyDown(Keys.D))
                velocity.X += 0.1f;

            if (!Keyboard.GetState().IsKeyDown(Keys.D) && !Keyboard.GetState().IsKeyDown(Keys.A))
            {
                velocity.X *= 0.3f;
            }

            velocity.X = EngineHelpers.Clamp(velocity.X, -1f, 1f);

            ManageCollision();

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && velocity.Y == 0 && jumpTimer == 0) //scale.X = 0.25, should be 8
            {
                velocity.Y -= 3;
                squashHeight = 0;
            }

            scale.Y = 1 - (squashHeight / 16f);

            if (squashHeight > 0) squashHeight--;

            if (velocity.Y < 0)
            {
                if (jumpTimer < 10 && Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    gravityFactor -= (gravityFactor / 10f);
                }

                jumpTimer++;
            }

            if (velocity.Y == 0 && oldVelocity.Y > 0)
            {
                jumpTimer = 0;
                squashHeight = (int)(oldVelocity.Y * 2); //yea cat
                gravityFactor = 0.3f;
            }

            position += velocity;
        }

        public Rectangle rect
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, width, height);
            }
        }

        private void ManageCollision()
        {
            Stage stage = EngineGame.instance.activeStage;

            for (int i = 0; i < stage.tilemap.GetLength(1); i++)
            {
                for (int j = 0; j < stage.tilemap.GetLength(0); j++)
                {
                    if (stage.tilemap[j, i].id == 1)
                    {
                        Rectangle tileRect = new Rectangle(i * 8, j * 8, 8, 8);

                        if ((velocity.X > 0 && Collision.IsTouchingLeft(rect, tileRect, velocity)) ||
                            (velocity.X < 0 && Collision.IsTouchingRight(rect, tileRect, velocity)))
                            velocity.X = 0;

                        if ((velocity.Y > 0 && Collision.IsTouchingTop(rect, tileRect, velocity)) ||
                            (velocity.Y < 0 && Collision.IsTouchingBottom(rect, tileRect, velocity)))
                            velocity.Y = 0;
                    }
                }
            }
        }
    }
}
