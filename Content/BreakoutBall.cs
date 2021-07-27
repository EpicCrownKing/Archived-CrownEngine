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
    public class BreakoutBall : Actor
    {
        public BreakoutBall(Vector2 pos, Vector2 vel, Stage stage) : base(pos, vel, stage)
        {
            position = pos;
            velocity = vel;

            myStage = stage;
        }

        public override int width => 4;
        public override int height => 4;

        public override void Draw(SpriteBatch spriteBatch)
        {
            Texture2D tex = EngineHelpers.GetTexture("BreakoutBall");
            for (int i = 0; i < prevPos.Length; i++) 
            {
                spriteBatch.Draw(tex, prevPos[i], new Rectangle(0, 0, 4, 4), Color.Lerp(Color.Cyan, Color.Magenta, i / (prevPos.Length - 1)) * (1f / (2 * (i + 1))), rotation, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            }

            base.Draw(spriteBatch);
        }

        private int updatecount;

        public BreakoutPad pad;

        public Vector2[] prevPos = new Vector2[4];

        public override void Update()
        {
            updatecount++;

            color = Color.Cyan;

            if(position.X <= 0)
            {
                velocity.X = -velocity.X;
            }

            if (position.X >= EngineGame.instance.windowWidth - width)
            {
                velocity.X = -velocity.X;
            }

            if (position.Y <= 0)
            {
                velocity.Y = -velocity.Y;
            }

            if (position.Y >= 230)
            {
                pad.bol = null;
                Kill();
            }

            if (Collision.IsTouchingTop(rect, pad.rect, velocity))
            {
                velocity.Y = -velocity.Y;
                velocity.X = (((position.X - pad.rect.X) / 24f) - 0.5f) * 2f;
            }

            Actor colCheck = Collision.IsCollidingWithAnything(rect, velocity);
            if (colCheck != null)
            {
                if (Collision.IsTouchingTop(rect, colCheck.rect, velocity) || Collision.IsTouchingBottom(rect, colCheck.rect, velocity))
                {
                    velocity.Y = -velocity.Y;
                }

                if (Collision.IsTouchingLeft(rect, colCheck.rect, velocity) || Collision.IsTouchingRight(rect, colCheck.rect, velocity))
                {
                    velocity.X = -velocity.X;
                }

                colCheck.Kill();
            }

            if (updatecount % 2 == 0)
            {
                prevPos[3] = prevPos[2];
                prevPos[2] = prevPos[1];
                prevPos[1] = prevPos[0];
                prevPos[0] = position;
            }

            base.Update();
        }

        public override void Load()
        {
            base.Load();
        }
    }
}
