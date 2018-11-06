using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraLibrary
{
    public class FollowerCamera : Camera
    {
        public ICameraFollowable Target;
        public FollowerCamera(ICameraFollowable followMe, Rectangle screenBounds)
            : base(followMe.Position, screenBounds)
        {
            Target = followMe;
        }

        public override void Update(GameTime gt)
        {
            Position = Target.Position;
        }
    }
}