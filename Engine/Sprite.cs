using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CrownEngine.Engine
{
    public class Sprite
    {
        public Texture2D texture;

        public virtual int width => 16;
        public virtual int height => 16;
        protected Vector2 scale = Vector2.One;
        protected float rotation = 0f;

        protected Color color = Color.White;

        protected Vector2 position = Vector2.Zero;

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Vector2((int)position.X, (int)position.Y), new Rectangle(0, 0, width, height), Color.White, rotation, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }
    }
}
