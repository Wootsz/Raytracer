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
        public List<Sphere> spheres;
        public List<Light> lightSources;
        Sphere sphere1, sphere2, sphere3;
        Light light1;

        public Scene()
        {
            spheres = new List<Sphere>();
            lightSources = new List<Light>();

            sphere1 = new Sphere(new Vector3(0, 0, 10), 5, 65535);
            spheres.Add(sphere1);

            light1 = new Light();
            lightSources.Add(light1);
        }

        public float Intersect(Ray ray)
        {
            float c = 0;
            Intersection i;
            foreach(Sphere p in spheres)
            {
                i = p.Intersect(ray);
                if (i != null)
                    c = i.color;
            }
            return c;
        }
    }
}
