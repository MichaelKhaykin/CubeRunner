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
        public enum Directions
        {
            Up,
            Down,
            Left,
            Right
        }
        
        public ControlledCamera(Vector2 position, float zoom, float rotation, Rectangle screenBounds)
            : base(position, zoom, rotation, screenBounds)
        {

        }

        public override void Update(GameTime gt)
        {
            
        }
    }
}