using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace CrownEngine.Engine
{
    public class Actor
    {
        public Stage myStage;

        public Texture2D texture;

        public virtual int width => 16;
        public virtual int height => 16;

        public Vector2 scale = Vector2.One;
        public float rotation = 0f;

        public Color color = Color.White;

        public Vector2 position = Vector2.Zero;

        public List<Component> components = new List<Component>();

        public Actor(Vector2 pos, Stage stage)
        {
            position = pos;

            myStage = stage;
        }

        public virtual void Load()
        {

        }

        public virtual void Update()
        {
            foreach(Component component in components)
            {
                component.Update();
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position - myStage.screenPosition, new Rectangle(0, 0, width, height), color, rotation, texture.Bounds.Size.ToVector2() / 2f, scale, SpriteEffects.None, 0f);

            foreach (Component component in components)
            {
                component.Render(spriteBatch);
            }
        }

        public virtual void Kill()
        {
            myStage.RemoveActor(this);
        }

        public Rectangle rect
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, width, height);
            }
        }

        public Vector2 Center
        {
            get
            {
                return new Vector2(position.X + width / 2, position.Y + height / 2);
            }
        }
    }
}
