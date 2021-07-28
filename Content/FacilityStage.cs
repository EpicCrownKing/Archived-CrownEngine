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
    public class Facility : Stage
    {
        public override Color bgColor => Color.Black;

        public override void Update()
        {
            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void Load()
        {
            AddActor(new Player(new Vector2(75, 100), Vector2.Zero, this));
            base.Load();
        }
    }
}
