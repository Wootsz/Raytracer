using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace template
{
    public class Sphere : Primitive
    {
        public Vector3 position;
        public float radius;

        public Sphere(Vector3 position, float radius, float color)
        {
            this.position = position;
            this.radius = radius;
            this.color = color; 
        }

        public override Intersection Intersect(Ray ray)
        {
            float a,b,c;
            a = CalcMethods.DotProduct(ray.direction, ray.direction);
            b = 2 * CalcMethods.DotProduct(ray.direction, (ray.origin-position));
            c = CalcMethods.DotProduct((ray.origin - position),(ray.origin - position)) - radius * radius;

            float t1, t2;
            try { t1 = CalcMethods.ABCFormula(a, b, c, true); }
            catch { t1 = -1; }    //no intersections
            try { t2 = CalcMethods.ABCFormula(a, b, c, false); }
            catch { t2 = -1; }

            if (t1 < ray.t && t2 < ray.t && t1 > 0 && t2 > 0)
                ray.t = Math.Min(t1, t2);     
            else if (t1 < 0 && t2 > 0 && t2 < ray.t)
                ray.t = t2;
            else if (t1 > 0 && t2 < 0 && t1 < ray.t)
                ray.t = t1;
            else
                return null;

            Vector3 point = ray.origin + ray.t * ray.direction;
            return new Intersection(point, this);
        }
    }
}
