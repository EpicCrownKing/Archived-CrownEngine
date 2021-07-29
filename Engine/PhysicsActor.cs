using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace CrownEngine.Engine
{
    public class PhysicsActor : Actor
    {
        public Vector2 velocity;

        public PhysicsActor(Vector2 pos, Vector2 vel, Stage stage) : base(pos, stage)
        {
            position = pos;
            velocity = vel;

            myStage = stage;
        }

        public virtual void PhysicsActorLoad()
        {
            
        }

        public override void Load()
        {
            texture = EngineHelpers.GetTexture(GetType().Name);

            PhysicsActorLoad();

            base.Load();
        }

        public virtual void PhysicsActorUpdate()
        {

        }

        public override void Update()
        {
            PhysicsActorUpdate();

            position += velocity;
        }
    }
}
