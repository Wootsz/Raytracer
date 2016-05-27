using System;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK;                           //!
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace template
{
    public class Plane : Primitive
    {
        public Vector3 normal;
        public float distance;       

        /// <summary>
        /// Making a plane
        /// </summary>
        /// <param name="normal">A normal for the plane</param>
        /// <param name="distance">Distance from the origin</param>
        public Plane(Vector3 normal, float distance)
        {
            this.normal = normal;
            this.distance = distance;
        }

        protected override Intersection Intersect(Ray ray)
        {
            ray.t = -(CalcMethods.DotProduct(ray.origin, normal) + distance) / CalcMethods.DotProduct(ray.direction, normal);
            Vector3 point = ray.origin + ray.t * ray.direction;
            return new Intersection(point);
        }
    }
}
