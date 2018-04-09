using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace multiPlayer_shooter
{
    class Entity
    {
        public Color4 color;
        public Vector2 center;
        int size;

        public Entity(Vector2 c, int s)
        {
            center.X = c.X;
            center.Y = c.Y;
            size = s;
            color = new Color4(255, 255, 255, 255);
        }
        public Entity(Vector2 c, int s, Color4 rgb)
        {
            center.X = c.X;
            center.Y = c.Y;
            size = s;
            color = new Color4(rgb.R, rgb.G, rgb.B, rgb.A);
        }

        public virtual void draw(Game g)
        {
            int negX = g.Width / 2;
            int negY = g.Height / 2;

            Vector2 topRight = new Vector2(Utilities.NormalizedValue(center.X + size, negX), Utilities.NormalizedValue(center.Y + size, negY));
            Vector2 bottomLeft = new Vector2(Utilities.NormalizedValue(center.X - size, negX), Utilities.NormalizedValue(center.Y - size, negY));
            Vector2 bottomRight = new Vector2(Utilities.NormalizedValue(center.X + size, negX), Utilities.NormalizedValue(center.Y - size, negY));
            Vector2 topLeft = new Vector2(Utilities.NormalizedValue(center.X - size, negX), Utilities.NormalizedValue(center.Y + size, negY));

            GL.Begin(PrimitiveType.Quads);
            GL.Color4(color);

            GL.Vertex2(topRight);
            GL.Vertex2(topLeft);
            GL.Vertex2(bottomLeft);
            GL.Vertex2(bottomRight);

            GL.End();
        }
        public virtual void UP(int speed = 1)
        {
            center.Y += speed;
        }
        public virtual void DOWN(int speed = 1)
        {
            center.Y -= speed;
        }
        public virtual void RIGHT(int speed = 1)
        {
            center.X += speed;
        }
        public virtual void LEFT(int speed = 1)
        {
            center.X -= speed;
        }
    }
}
