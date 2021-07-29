using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using CrownEngine;

namespace CrownEngine.Engine
{
    public class Tile
    {
        public Tile()
        {

        }

        public virtual Texture2D texture => EngineGame.instance.MissingTexture;
        public Color color = Color.White;
    }
}
