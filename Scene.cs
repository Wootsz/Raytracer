using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace template
{
    public class Scene
    {
        public List<Primitive> primitives;
        public List<Light> lightSources;
        Sphere sphere1, sphere2, sphere3;
        Light light1;

        public Scene()
        {
            primitives = new List<Primitive>();
            lightSources = new List<Light>();

            sphere1 = new Sphere(new Vector3(1, 1, 1), 1, 65535);
            primitives.Add(sphere1);
            sphere2 = new Sphere(new Vector3(8, 0, 3), 5, 0);
            primitives.Add(sphere2);
            sphere3 = new Sphere(new Vector3(14, 0, 3), 5, 0);
            primitives.Add(sphere3);

            light1 = new Light();
            lightSources.Add(light1);
        }

        public float Intersect(Ray ray)
        {
            float c = 0;
            Intersection i;
            foreach(Primitive p in primitives)
            {
                i = p.Intersect(ray);
                if (i != null)
                    c = i.color;
            }
            return c;
        }
    }
}
