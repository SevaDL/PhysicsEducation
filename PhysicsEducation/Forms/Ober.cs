using System.Drawing;

namespace PhysicsEducation.Forms
{
    public class Ober
    {

        public Image oberImg;
        public Image oberWeightImg;
        public float x;
        public float y;
        public float width;
        public float height;
        public float weightRadius;
        public float radius;
        public float weightWidth;
        public float massmah = 0.1f;
        public float massober = 0.05f;

        public Ober(int x, int y, float weightRadius)
        {
            oberImg = new Bitmap("Assets\\ober.png");
            oberWeightImg = new Bitmap("Assets\\oberWeight.png");
            this.weightWidth = oberWeightImg.Width;
            this.x = x - oberImg.Width/2;
            this.y = y - oberImg.Height/2;
            this.width = oberImg.Width;
            this.height = oberImg.Height;
            this.weightRadius = weightRadius;
            this.radius = 0.02f;
        }
    }
}