using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FontEffectsLibrary
{
    public class ShadowLabel : TextLabel
    {
        TextLabel shadowTextLabel;

        Vector2 lastPosition;

        public ShadowLabel(SpriteFont font, Vector2 position, List<Color> colors, float rateOfChange, string text, Vector2 scale) 
            : base(font, position, colors, rateOfChange, text, scale)
        {
            Init();
        }

        public ShadowLabel(SpriteFont font, Vector2 position, Color color, string text, Vector2 scale)
            : base(font, position, color, text, scale)
        {
            Init();
        }

        private void Init()
        {
            shadowTextLabel = new TextLabel(Font, new Vector2(Position.X - 2, Position.Y + 4), Color.Black, text, Scale);
        }

        public override void Update(GameTime gameTime)
        {
            if(lastPosition != Position)
            {
                shadowTextLabel.Position = new Vector2(Position.X - 2, Position.Y + 4);
            }


            lastPosition = Position;

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch sb)
        {
            shadowTextLabel.Draw(sb);

            base.Draw(sb);
        }
    }
}
