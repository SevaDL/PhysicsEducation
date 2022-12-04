using System.Drawing;

namespace PhysicsEducation.Forms
{
    public class Flask
    {

        public Bitmap img;
        public float width;
        public float height;
        public float x;
        public float y;
        public float nu;
        public float ro;

        public Flask(int x, int y, float nu)
        {

            this.img = new Bitmap("Assets\\flask.png");
            this.width = this.img.Width;
            this.height = this.img.Height;
            this.x = 230 - img.Width/2;
            this.y = y;
            this.nu = nu;
            this.ro = 1000;
        }
    }
}