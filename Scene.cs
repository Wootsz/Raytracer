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

            sphere1 = new Sphere(new Vector3(20, 0, 30), 5, 65535, 0);
            primitives.Add(sphere1);
            
            sphere2 = new Sphere(new Vector3(0, 0, 30), 5, 111114, 0);
            primitives.Add(sphere2);

            sphere3 = new Sphere(new Vector3(-20, 0, 30), 5, 177114, 0);
            primitives.Add(sphere3);

            plane1 = new Plane(new Vector3(0, 1, 0), 10, 0xffffff, 0);
            primitives.Add(plane1);

            light1 = new Light(new Vector3(-20, 5, 10), 1, 1, 1);
            lightSources.Add(light1);
        }

        public float Intersect(Ray ray)
        {
                float c = 0;
                Intersection smallest = null;
                foreach (Primitive p in primitives)
                {
                    Intersection i = p.Intersect(ray);
                    if (i != null && (smallest == null || CalcMethods.VectorLength(i.intersectionPoint) < CalcMethods.VectorLength(smallest.intersectionPoint)))
                        smallest = i;
                }
                /*if (smallest != null)
                    {
                        if (smallest.nearestPrimitive.specularity == 0)
                        {
                            foreach (Light light in lightSources)
                            {
                                Ray shadowray = new Ray(smallest.intersectionPoint, CalcMethods.Normalize(light.location - smallest.intersectionPoint));
                                c = smallest.nearestPrimitive.color * light.redIntensity; //blueintensity //greenintensity
                            }
                        }
                        else if (smallest.nearestPrimitive.specularity == 1)
                        {
                            Vector3 R = ray.direction - 2 * CalcMethods.DotProduct(ray.direction, smallest.normal) * smallest.normal;
                            Ray secondray = new Ray(smallest.intersectionPoint, CalcMethods.Normalize(R));
                            c = smallest.nearestPrimitive.specularity * Intersect(secondray) + (1 - smallest.nearestPrimitive.specularity) * smallest.nearestPrimitive.color;
                        }
                        else
                        {
                            Vector3 R = ray.direction - 2 * CalcMethods.DotProduct(ray.direction, smallest.normal) * smallest.normal;
                            Ray secondray = new Ray(smallest.intersectionPoint, CalcMethods.Normalize(R));
                            c = smallest.nearestPrimitive.specularity * Intersect(secondray) + (1 - smallest.nearestPrimitive.specularity) * smallest.nearestPrimitive.color;
                            foreach (Light light in lightSources)
                            {
                                Ray shadowray = new Ray(smallest.intersectionPoint, CalcMethods.Normalize(light.location - smallest.intersectionPoint));
                            }
                        }
                    }*/
            if (smallest != null)
                return smallest.nearestPrimitive.color;
            else { return 0; }
            
        }
    }
}
