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
        Light light1, light2;

        public Scene()
        {
            primitives = new List<Primitive>();
            lightSources = new List<Light>();

            sphere1 = new Sphere(new Vector3(5, 0, 10), 5, new Vector3(100, 0, 0), 1);
            primitives.Add(sphere1);

            sphere2 = new Sphere(new Vector3(0, 0, 10), 5, new Vector3(0, 100, 0), 0.5f);
            primitives.Add(sphere2);

            sphere3 = new Sphere(new Vector3(-5, 0, 10), 5, new Vector3(0, 0, 100), 0);
            primitives.Add(sphere3);

            plane1 = new Plane(new Vector3(0, 1, 0), 10, new Vector3(255, 255, 255), 0);
            primitives.Add(plane1);

            light1 = new Light(new Vector3(-10, 20, -5), 1, 1, 1);
            lightSources.Add(light1);
        }

        /// <summary>
        /// For each ray, check if it intersects with any of the primitives
        /// </summary>
        /// <param name="ray">The ray to check</param>
        /// <returns>Returns the color of a pixel</returns>
        public Vector3 Intersect(Ray ray)
        {
                Vector3 c = Vector3.Zero;
                Intersection smallest = null;
                foreach (Primitive p in primitives)
                {
                    Intersection i = p.Intersect(ray);
                    if (i != null && (smallest == null || CalcMethods.VectorLength(i.intersectionPoint) < CalcMethods.VectorLength(smallest.intersectionPoint)))
                        smallest = i;
                }
                if (smallest != null)
                    //return LightIntensity(smallest) * smallest.nearestPrimitive.color;
                    return (1 - smallest.nearestPrimitive.specularity) * LightIntensity(smallest) * smallest.nearestPrimitive.color + 
                            smallest.nearestPrimitive.specularity * SecondaryRay(smallest, ray);
                else
                    return Vector3.Zero; 
        }

        /// <summary>
        /// Method for sending a shadow ray to each lightsource
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public Vector3 LightIntensity(Intersection i)
        {
            Vector3 lightintensity = Vector3.Zero;
            foreach (Light light in lightSources)
            {
                Ray shadowray = new Ray(i.intersectionPoint +  i.normal * 0.01f, CalcMethods.Normalize(light.location - i.intersectionPoint));
                //shadowray.t = CalcMethods.VectorLength(light.location - i.intersectionPoint);
                bool occlusion = false;
                foreach (Primitive p in primitives)
                {
                    if (p.Occlusion(shadowray))
                    {
                        occlusion = true;
                        break;
                    }
                }
                if (!occlusion)
                    lightintensity += new Vector3(light.redIntensity, light.greenIntensity, light.blueIntensity);
            }
            return lightintensity/lightSources.Count;
        }

        public Vector3 SecondaryRay(Intersection i, Ray ray)
        {
            Vector3 R = ray.direction - 2 * CalcMethods.DotProduct(ray.direction, i.normal) * i.normal;
            Ray secondray = new Ray(i.intersectionPoint + i.normal * 0.01f, CalcMethods.Normalize(R));
            return Intersect(secondray);
        }
    }
}
