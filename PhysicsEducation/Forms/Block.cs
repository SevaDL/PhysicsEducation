using System.Drawing;

namespace PhysicsEducation.Forms
{
    public class Block
    {

        public Bitmap img;
        public float x;
        public float y;
        public float mass;
        public int size;
        
        public Block(int x, int y, float mass)
        {
            img = new Bitmap("Assets\\block.png");
            
            this.x = x;
            this.y = y;
            this.mass = mass;
            size = 30;
        }
    }
}