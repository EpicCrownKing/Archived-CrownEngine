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
    public class Player : Actor
    {
        public Player(Vector2 pos, Vector2 vel, Stage stage) : base(pos, vel, stage)
        {
            position = pos;
            velocity = vel;

            myStage = stage;
        }

        public override int width => 8;
        public override int height => 12;

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public float rotationVel;

        public override void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                rotationVel -= 0.02f;

            if (Keyboard.GetState().IsKeyDown(Keys.D))
                rotationVel += 0.02f;

            if (!Keyboard.GetState().IsKeyDown(Keys.D) && !Keyboard.GetState().IsKeyDown(Keys.A))
            {
                rotationVel *= 0.5f;
            }

            rotationVel = rotationVel.Clamp(-0.1f, 0.1f);

            rotation += rotationVel;

            if (Keyboard.GetState().IsKeyDown(Keys.W))
                velocity += (-Vector2.UnitY).RotatedBy(rotation) * 0.15f;
            else
                velocity *= 0.7f;

            velocity = velocity.ClampVectorMagnitude(3f);

            base.Update();
        }

        public override void Load()
        {
            base.Load();
        }
    }
}
