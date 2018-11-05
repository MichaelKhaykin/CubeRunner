using Microsoft.Xna.Framework;
using System.Text;
using System.Threading.Tasks;

namespace CameraLibrary
{
    public class Camera
    {
        private Vector3 position;
        public Vector3 Position
        {
            get => position;
            set
            {
                position = value;
                TranslationMatrix = Matrix.CreateTranslation(-Position);
            }
        }

        private float rotation;
        public float Rotation
        {
            get => rotation;
            set
            {
                rotation = value;
                RotationMatrix = Matrix.CreateRotationZ(rotation);
            }
        }
        private float zoom;
        public float Zoom
        {
            get => zoom;
            set
            {
                zoom = value;
                ScaleMatrix = Matrix.CreateScale(new Vector3(Zoom, Zoom, 0));
            }
        }
        private Vector3 cameraOffset;
        public Vector3 CameraOffset
        {
            get => cameraOffset;
            set
            {
                cameraOffset = value;
                OffsetMatrix = Matrix.CreateTranslation(cameraOffset);
            }
        }

        public Matrix TranslationMatrix { get; protected set; }
        public Matrix RotationMatrix { get; protected set; }
        public Matrix ScaleMatrix { get; protected set; }
        public Matrix OffsetMatrix { get; protected set; }

        public Matrix World => ScaleMatrix * RotationMatrix * TranslationMatrix * OffsetMatrix;

        public Camera(Vector3 position, Rectangle screenBounds)
        {
            Position = position;
            Zoom = 1f;
            Rotation = 0f;
            CameraOffset = new Vector3(screenBounds.Width / 2f, screenBounds.Height / 2f, 0);
        }

        public void Reset()
        {
            Zoom = 1f;
            Position = CameraOffset;
            Rotation = 0f;
        }

        public virtual void Update(GameTime gt)
        {
            
        }
    }
}