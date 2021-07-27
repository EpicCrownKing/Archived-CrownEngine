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
    public class BreakoutBlock : Actor
    {
        public BreakoutBlock(Vector2 pos, Vector2 vel, Stage stage) : base(pos, vel, stage)
        {
            position = pos;
            velocity = vel;

            myStage = stage;
        }

        public override int width => 15;
        public override int height => 4;

        public override void Draw(SpriteBatch spriteBatch)
        {
            color = Color.Lerp(Color.Magenta, Color.Cyan, Math.Sin((position.X + position.Y) / 40).PositiveSin());
            base.Draw(spriteBatch);
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Load()
        {
            base.Load();
        }
    }
}
