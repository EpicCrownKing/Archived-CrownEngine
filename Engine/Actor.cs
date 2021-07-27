using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace CrownEngine.Engine
{
    public class Actor : Sprite
    {
        public Vector2 velocity;

        public Stage myStage;

        public Actor(Vector2 pos, Vector2 vel, Stage stage)
        {
            position = pos;
            velocity = vel;

            myStage = stage;
        }

        public virtual void Load()
        {
            texture = EngineHelpers.GetTexture(GetType().Name);
        }

        public virtual void Update()
        {
            position += velocity;
        }

        public virtual void Kill()
        {
            myStage.RemoveActor(this);
        }
    }
}
