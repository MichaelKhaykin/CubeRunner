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

        public ControlledCamera(Vector3 position, Rectangle screenBounds)
            : this(position, screenBounds, 1f, 1f) { }

        public ControlledCamera(Vector3 position, Rectangle screenBounds, float speed, float turnSpeed)
            : base(position, screenBounds)
        {
            Speed = speed;
            TurnSpeed = turnSpeed;
        }

        public void Update(Controls[] inputs, GameTime gt)
        {
            foreach (Controls direction in inputs)
            {
                switch (direction)
                {
                    case Controls.Up:
                        Position -= TranslationMatrix.Up * Speed;
                        break;
                    case Controls.Down:
                        Position += TranslationMatrix.Up * Speed;
                        break;
                    case Controls.Left:
                        Position += TranslationMatrix.Left * Speed;
                        break;
                    case Controls.Right:
                        Position -= TranslationMatrix.Left * Speed;
                        break;
                    case Controls.TurnLeft:
                        Rotation += TurnSpeed * (float)gt.ElapsedGameTime.TotalSeconds;
                        break;
                    case Controls.TurnRight:
                        Rotation -= TurnSpeed * (float)gt.ElapsedGameTime.TotalSeconds;
                        break;
                }
            }
        }
    }
}