using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsEducation
{
    class Maxwellswheel
    {
        public float V; // скорость центра масс
        public float V0; // скорость центра масс
        public float a ;// ускоренее маятника
        public float m0; //масса стержня
        public float md; //масса диска
        public float mk; //масса кольца
        public float m ;//масса маятника
        
        public float J0; // момент инерции стержня
        public float Jk; // момент инерции кольца
        public float Jd; // момент инерции диска
        public float J ;// момент инерции маятника 
                     
        public float r ;// радиус стерженя
        public float R ;// радиус диска
        public float R1; //внутренний радиус кольца
        public float R2; //внешний радиус кольца
    }
}
