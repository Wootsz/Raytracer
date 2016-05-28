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
        Plane plane1;
        Light light1;

        public Scene()
        {
            primitives = new List<Primitive>();
            lightSources = new List<Light>();

            sphere1 = new Sphere(new Vector3(20, 0, 10), 5, 65535);
            primitives.Add(sphere1);
            sphere2 = new Sphere(new Vector3(0, 0, 10), 5, 111114);
            primitives.Add(sphere2);
            sphere3 = new Sphere(new Vector3(-20, 0, 10), 5, 177114);
            primitives.Add(sphere3);
            plane1 = new Plane(new Vector3(0, 1, 0), 10, 0xffffff);
            primitives.Add(plane1);

            light1 = new Light(new Vector3(-20, 5, 10), 1, 1, 1);
            lightSources.Add(light1);
        }

        public float Intersect(Ray ray)
        {
            float c = 0;
            Intersection i;
            foreach (Primitive p in primitives)
            {
                i = p.Intersect(ray);
                if (i != null)
                    c = i.color;
            }
            return c;
        }
    }
}
