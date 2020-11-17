using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevMath
{
    public class Rigidbody
    {
        public Vector2 Velocity
        {
            get; private set;
        }

        public float mass = 1.0f;
        public float force = 150.0f;
        public float dragCoefficient = .47f;

        public void AddForce(Vector2 forceDirection, float deltaTime)
        {
            // f = m * a, a = f/m
            // Dus de vector force doorgeven aan de velocity. Niet in 1x maar over frames (deltaTime)

            // De richting waar de vector3 naar toe gaat moet met de force vermedigvuldigd worden (want met hoeveel force gaat ie die richting op?)
            // dat (richting * force -> richting.x * force en richting.y * force wat ik dus in de Vector2 class heb gedaan) moet dan gedeeld worden 
            // door de massa en tijd? -> ja want je wilt niet dat ie in 1 frame er al is. Verdelen over deltaTime (Time.deltaTime?). De groter de massa de groter de force

            // Speed
            Vector2 speed = (forceDirection * force) / (mass * deltaTime); // wrm is vector2 niet gekleurd?

            /*
            // Waardes van x en y berekenen 
            float nX = (1 / dragCoefficient) * (float)Math.Exp(-dragCoefficient / mass * deltaTime) *
                ((dragCoefficient * Velocity.x) + (mass * speed.x)) - (mass * speed.x) / dragCoefficient;
            // Y is hetzelfde als x maar dan met y
            float nY = (1 / dragCoefficient) * (float)Math.Exp(-dragCoefficient / mass * deltaTime) *
                ((dragCoefficient * Velocity.y) + (mass * speed.y)) - (mass * speed.y) / dragCoefficient; // Als het niet werkt dan zal de fout waarschijnlijk ergens hierin zitten...

            // Geef door aan de velocity van deze class
            Velocity = new Vector2(nX, nY);*/ // Zooi werkt niet, nog steeds lichtsnelheid..
            Velocity = new Vector2() // Werkt dit dan>? maar is precies hetzelfde
            {
                x = (float)(1.0 / (double)this.dragCoefficient * Math.Exp(-(double)this.dragCoefficient / (double)this.mass * (double)deltaTime) * ((double)this.dragCoefficient * (double)this.Velocity.x + (double)this.mass * (double)vector2.x) - (double)this.mass * (double)vector2.x / (double)this.dragCoefficient),
                y = (float)(1.0 / (double)this.dragCoefficient * Math.Exp(-(double)this.dragCoefficient / (double)this.mass * (double)deltaTime) * ((double)this.dragCoefficient * (double)this.Velocity.y + (double)this.mass * (double)vector2.y) - (double)this.mass * (double)vector2.y / (double)this.dragCoefficient)
            };
        }

        //http://hyperphysics.phy-astr.gsu.edu/hbase/mass.html
        //https://www.grc.nasa.gov/www/k-12/aerosim/LessonHS97/Massconv.html
    }
}
