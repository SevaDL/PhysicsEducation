using System.Drawing;
using System;

namespace PhysicsEducation.Forms
{
    public class Ball
    {

        public Bitmap img;
        public float x;
        public float y;
        public float mass;
        public float size;
        public float radius;
        public float ro;

        public Ball(int x, int y, float radius, float mass)
        {
            img = new Bitmap("Assets\\ball.png");
            this.size = radius * 2 * 1000;
            this.radius = radius;
            this.x = x - size / 2f;
            this.y = y - size / 2f;
            this.mass = mass;
            this.ro = 1500;
        }
        public void setRadius(float radius)
        {
            this.radius = radius;
            float tmp = this.size/2;
            this.size = radius * 2 * (float)Math.Pow(10, 5);
            this.x = this.x + tmp - size / 2f;
            this.y = this.y + tmp - size / 2f;
            //this.y = 160 - size / 2;
        }
    }
}