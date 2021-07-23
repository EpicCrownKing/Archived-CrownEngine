using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace CrownEngine.Engine
{
    public class Actor : Sprite
    {
        public Vector2 velocity;
        public Vector2 oldVelocity;

        public Actor(Vector2 pos, Vector2 vel)
        {
            position = pos;
            velocity = vel;
        }

        public virtual void Update()
        {
            oldVelocity = velocity;

            position += velocity;
        }

        public virtual void Kill()
        {

        }
    }
}
