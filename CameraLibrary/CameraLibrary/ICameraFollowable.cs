using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraLibrary
{
    public interface ICameraFollowable
    {
        Vector3 Position { get; }
    }
}