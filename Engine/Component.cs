using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace CrownEngine.Engine
{
    public class Component
    {
        public Actor myActor;

        public Component(Actor _myActor)
        {
            myActor = _myActor;
        }

        public virtual void Load()
        {

        }

        public virtual void Update()
        {

        }

        public virtual void Render(SpriteBatch spriteBatch)
        {
            
        }
    }
}
