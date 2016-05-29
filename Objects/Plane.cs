using System;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK;                           //!
using OpenTK.Graphics;

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
        public Plane(Vector3 normal, float distance, Vector3 color, float specularity)
        {
            this.normal = normal;
            this.distance = distance;
            this.color = color;
        }

        public override Intersection Intersect(Ray ray)
        {
            float t = -(CalcMethods.DotProduct(ray.origin, normal) + distance) / CalcMethods.DotProduct(ray.direction, normal);
            if (t > 0 && t < ray.t)
            {
                ray.t = t;
                Vector3 point = ray.origin + ray.t * ray.direction;
                return new Intersection(point, this, ray);
            }
            else return null;
        }

        public override bool Occlusion(Ray ray)
        {
            if (Intersect(ray) != null)
                return true;
            else return false;
        }
    }
}
