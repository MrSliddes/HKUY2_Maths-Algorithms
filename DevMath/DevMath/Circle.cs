using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevMath
{
    public class Circle
    {
        public Vector2 Position
        {
            get; set;
        }

        public float Radius
        {
            get; set;
        }

        // Als de circle overlaps dan is er een collision
        public bool CollidesWith(Circle circle)
        {
            var dx = circle.Position.x - this.Position.x;
            var dy = circle.Position.y - this.Position.y;
            var dist = Math.Sqrt(dx * dx + dy * dy);
            return dist < circle.Radius + Radius;
        }
    }
}
