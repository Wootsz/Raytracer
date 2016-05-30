using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace template
{
    public class Intersection
    {
        public float intersectionDistance;
        public Primitive nearestPrimitive;
        public Vector3 normal, intersectionPoint;
        public Ray ray;

        public Intersection(Vector3 intersectionPoint, Primitive nearestPrimitive, Ray ray)
        {
            this.intersectionPoint = intersectionPoint;
            this.nearestPrimitive = nearestPrimitive;
            this.ray = ray;
            if (nearestPrimitive is Plane)
            {
                Plane p = nearestPrimitive as Plane;
                normal = p.normal;
            }
            else if (nearestPrimitive is Sphere)
            {
                Sphere s = nearestPrimitive as Sphere;
                normal = CalcMethods.Normalize(intersectionPoint - s.position);
                if (CalcMethods.DotProduct(normal, ray.direction) < 0)
                    normal *= -1;
            }
        }
    }
}
