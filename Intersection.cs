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
        public float color;

        public Intersection(Vector3 intersectionPoint, Primitive nearestPrimitive)
        {
            this.intersectionPoint = intersectionPoint;
            this.nearestPrimitive = nearestPrimitive;
            color = nearestPrimitive.color;
        }
    }
}
