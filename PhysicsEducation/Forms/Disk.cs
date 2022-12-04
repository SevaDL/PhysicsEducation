using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsEducation.Forms
{
    internal class Disk
    {
        public Bitmap img;
        public float width;
        public float height;
        public float x;
        public float y;

        public Disk(int x, int y)
        {
            this.img = new Bitmap("Assets\\disk.png");
            this.width = this.img.Width;
            this.height = this.img.Height;
            this.x = x;
            this.y = y;
        }
    }
}
