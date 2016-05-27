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
        Primitive sphere1, sphere2, sphere3;
        Light light1;

        public Scene()
        {
            primitives = new List<Primitive>();
            lightSources = new List<Light>();

            sphere1 = new Sphere(new Vector3(3, 0, 3), 2);
            primitives.Add(sphere1);
            sphere2 = new Sphere(new Vector3(8, 0, 3), 5);
            primitives.Add(sphere2);
            sphere3 = new Sphere(new Vector3(14, 0, 3), 5);
            primitives.Add(sphere3);

            light1 = new Light();
            lightSources.Add(light1);
        }

        public Intersection Intersect()
        {
            foreach(Primitive p in primitives)
            {

            }
            return null;
        }
    }
}
