﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using CrownEngine.Content;

namespace CrownEngine.Engine
{
    public static class EngineHelpers
    {
        public static float Clamp(float val, float minVal, float maxVal)
        {
            if (val < minVal) val = minVal;
            if (val > maxVal) val = maxVal;

            return val;
        }

        public static void DrawAdditive(SpriteBatch spriteBatch, Texture2D tex, Vector2 position, Color color, float scale = 1f, float rotation = 0f)
        {
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);

            spriteBatch.Draw(tex, position, tex.Bounds, color, rotation, tex.TextureCenter(), scale, SpriteEffects.None, 0f);

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, null);
        }
    }
}