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
    public class Bricks : Tile
    {
        public Bricks(Color _color)
        {
            color = _color;
        }
    }
}
