using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsLibrary
{
    public abstract class GameObject
    {
        protected Vector2 position;
        public virtual ref Vector2 Position => ref position;

        protected Color color;
        public virtual ref Color Color => ref color;

        protected Vector2 origin;
        public virtual ref Vector2 Origin => ref origin;

        protected Vector2 scale = Vector2.One;
        public virtual ref Vector2 Scale => ref scale;

        public virtual float Rotation { get; protected set; }
        public virtual float LayerDepth { get; protected set; }
        public virtual SpriteEffects SpriteEffects { get; protected set; }

        // TODO: Allow multiple actions to run concurrently
        public Queue<List<Command>> Commands { get; set; } = new Queue<List<Command>>();

        public GameObject(Vector2 position, Color color, Vector2 scale, Vector2 origin)
        {
            this.position = position;
            this.color = color;
            this.scale = scale;
            this.origin = origin;
        }

        public virtual void Update(GameObject objectToOperateOn, GameTime gameTime)
        {
            if (Commands.Count == 0)
            {
                return;
            }

            if (Commands.Peek().Count == 0)
            {
                Commands.Dequeue();
            }

            if (Commands.Count == 0)
            {
                return;
            }

            for (int j = 0; j < Commands.Peek().Count; j++)
            {
                var currentCommand = Commands.Peek()[j];
                if (currentCommand.Action.Invoke(objectToOperateOn, currentCommand, currentCommand.Parameters))
                {
                    Commands.Peek().Remove(currentCommand);
                    j--;
                }
            }
        }

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}