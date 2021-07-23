using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using CrownEngine.Content;

namespace CrownEngine.Engine
{
    public static class Extensions
    {
        public static float ToRadians(this float num) => num * (float)(3.14f / 180f);

        public static float ToRadians(this double num) => (float)(num * (3.14f / 180f));

        public static float ToRadians(this int num) => num * (float)(3.14f / 180f);

        public static float PositiveSin(this float num) => (num / 2f) + 0.5f;

        public static float PositiveSin(this double num) => (float)(num / 2f) + 0.5f;

        public static Vector2 TextureCenter(this Texture2D tex) => tex.Bounds.Size.ToVector2() / 2f;
    }
}
