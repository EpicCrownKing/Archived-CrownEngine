using System;
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
        public static void DrawAdditive(SpriteBatch spriteBatch, Texture2D tex, Vector2 position, Color color, float scale = 1f, float rotation = 0f)
        {
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);

            spriteBatch.Draw(tex, position, tex.Bounds, color, rotation, tex.TextureCenter(), scale, SpriteEffects.None, 0f);

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, null);
        }

        public static Texture2D GetTexture(string name)
        {
            return EngineGame.instance.Textures[name + ".png"];
        }

        public static bool IsTouchingLeft(Rectangle rect1, Rectangle rect2, Vector2 vel)
        {
            return rect1.Right + vel.X > rect2.Left &&
              rect1.Left < rect2.Left &&
              rect1.Bottom > rect2.Top &&
              rect1.Top < rect2.Bottom;
        }

        public static bool IsTouchingRight(Rectangle rect1, Rectangle rect2, Vector2 vel)
        {
            return rect1.Left + vel.X < rect2.Right &&
              rect1.Right > rect2.Right &&
              rect1.Bottom > rect2.Top &&
              rect1.Top < rect2.Bottom;
        }

        public static bool IsTouchingTop(Rectangle rect1, Rectangle rect2, Vector2 vel)
        {
            return rect1.Bottom + vel.Y > rect2.Top &&
              rect1.Top < rect2.Top &&
              rect1.Right > rect2.Left &&
              rect1.Left < rect2.Right;
        }

        public static bool IsTouchingBottom(Rectangle rect1, Rectangle rect2, Vector2 vel)
        {
            return rect1.Top + vel.Y < rect2.Bottom &&
              rect1.Bottom > rect2.Bottom &&
              rect1.Right > rect2.Left &&
              rect1.Left < rect2.Right;
        }

        public static bool IsColliding(Rectangle rect1, Rectangle rect2, Vector2 vel)
        {
            return IsTouchingLeft(rect1, rect2, vel) || IsTouchingRight(rect1, rect2, vel) || IsTouchingTop(rect1, rect2, vel) || IsTouchingBottom(rect1, rect2, vel);
        }

        public static Actor IsCollidingWithAnything(Rectangle rect, Vector2 velocity)
        {
            for (int i = 0; i < EngineGame.instance.activeStage.actors.Count; i++)
            {
                if (EngineGame.instance.activeStage.actors[i] != null)
                {
                    if (IsColliding(rect, EngineGame.instance.activeStage.actors[i].rect, velocity)) return EngineGame.instance.activeStage.actors[i];
                }
            }

            return null;
        }
    }
}
