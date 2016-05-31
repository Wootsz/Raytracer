using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using template;

namespace template
{
    public class Triangle : Primitive
    {
        public Vector3 p0, p1, p2;         //triangle hoeken posities

        public Triangle(Vector3 color, Vector3 p0, Vector3 p1, Vector3 p2, float specularity)
        {
            this.p0 = p0; this.p1 = p1; this.p2 = p2;
            this.specularity = specularity;
            this.color = color;
        }

        public override Intersection Intersect(Ray ray)
        {
            Vector3 N = CalcMethods.Normalize(CalcMethods.CrossProduct(p1 - p0, p2 - p0));
            float d = -(CalcMethods.DotProduct(N, p0));
            float t = -(CalcMethods.DotProduct(ray.origin, N) + d) / (CalcMethods.DotProduct(ray.direction, N));
            Vector3 point = ray.origin + t * ray.direction;
            return new Intersection(point, this, ray);
        }


    }
}
