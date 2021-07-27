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
    public class Breakout : Stage
    {
        public override Color bgColor => Color.DarkSlateBlue;

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
            AddActor(new BreakoutPad(new Vector2(EngineGame.instance.windowWidth / 2, 225), Vector2.Zero, this));

            for(int j = 4; j < 144; j += 18)
            {
                for (int k = 20; k < 50; k += 6)
                {
                    AddActor(new BreakoutBlock(new Vector2(j, k), Vector2.Zero, this));
                }
            }

            base.Load();
        }
    }
}
