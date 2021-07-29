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
    public class Spikes : Actor
    {
        public Spikes(Vector2 pos, Vector2 vel, Stage stage) : base(pos, stage)
        {
            position = pos;

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

        public override void Update()
        {
            ManageCollision();

            base.Update();
        }

        public override void Load()
        {
            base.Load();
        }

        private void ManageCollision()
        {
            for (int k = 0; k < myStage.actors.Count; k++)
            {
                
            }
        }
    }
}
