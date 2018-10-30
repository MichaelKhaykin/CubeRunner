using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraLibrary
{
    public class ControlledCamera : Camera
    {
        public enum Controls
        {
            Up,
            Down,
            Left,
            Right,
            TurnLeft,
            TurnRight
        }

        public float Speed;
        public float TurnSpeed;

        public ControlledCamera(Vector2 position, Rectangle screenBounds)
            : this(position, 1f, 0f, screenBounds, 1f, 1f) { }

        public ControlledCamera(Vector2 position, float zoom, float rotation, Rectangle screenBounds, float speed, float turnSpeed)
            : base(position, zoom, rotation, screenBounds)
        {
            Speed = speed;
            TurnSpeed = turnSpeed;
        }

        public void Update(Controls[] inputs, GameTime gt)
        {
            Vector2 translation = Vector2.Zero;
            foreach (Controls direction in inputs)
            {
                switch (direction)
                {
                    case Controls.Up:
                        translation.Y -= Speed;
                        break;
                    case Controls.Down:
                        translation.Y += Speed;
                        break;
                    case Controls.Left:
                        translation.X -= Speed;
                        break;
                    case Controls.Right:
                        translation.X += Speed;
                        break;
                    case Controls.TurnLeft:
                        Rotation += TurnSpeed * (float)gt.ElapsedGameTime.TotalSeconds;
                        break;
                    case Controls.TurnRight:
                        Rotation -= TurnSpeed * (float)gt.ElapsedGameTime.TotalSeconds;
                        break;
                }
            }

            Position += translation;

        }
    }
}