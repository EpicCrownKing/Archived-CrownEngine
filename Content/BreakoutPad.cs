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
    public class BreakoutPad : Actor
    {
        public BreakoutPad(Vector2 pos, Vector2 vel, Stage stage) : base(pos, vel, stage)
        {
            position = pos;
            velocity = vel;

            myStage = stage;
        }

        public override int width => 24;
        public override int height => 3;

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public BreakoutBall bol;

        public override void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                velocity.X -= 0.2f;

            if (Keyboard.GetState().IsKeyDown(Keys.D))
                velocity.X += 0.2f;

            if (!Keyboard.GetState().IsKeyDown(Keys.D) && !Keyboard.GetState().IsKeyDown(Keys.A))
            {
                velocity.X *= 0.9f;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && bol == null)
            {
                bol = new BreakoutBall(Center + new Vector2(0, -8), Vector2.Normalize((Mouse.GetState().Position.ToVector2() / EngineGame.instance.windowScale) - Center + new Vector2(0, -8)) * 3, myStage);
                bol.pad = this;
                myStage.AddActor(bol);
            }

            velocity.X = EngineHelpers.Clamp(velocity.X, -2f, 2f);

            base.Update();

            position.X = EngineHelpers.Clamp(position.X, 0, EngineGame.instance.windowWidth - width);
        }

        public override void Load()
        {
            base.Load();
        }
    }
}
