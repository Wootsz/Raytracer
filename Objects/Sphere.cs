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

        public Sphere(Vector3 position, float radius)
        {
            this.position = position;
            this.radius = radius;
        }

        protected override Intersection Intersect(Ray ray)
        {
            𝐷 ∙ 𝐷 𝑡^2 + 2𝐷 ∙ 𝑂 − 𝐶 𝑡 + (𝑂 − 𝐶)^2 − 𝑟^2 = 0;

            return null;
        }
    }
}
