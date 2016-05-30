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
        public int screenWidth, screenHeight, debugXtop, debugXbottom, debugYtop, debugYbottom;
        int count = 0;

        public Scene()
        {
            primitives = new List<Primitive>();
            lightSources = new List<Light>();

            sphere1 = new Sphere(new Vector3(20, 0, 30), 5, new Vector3(255, 0, 0), 1);
            primitives.Add(sphere1);

            sphere2 = new Sphere(new Vector3(0, 0, 30), 5, new Vector3(0, 255, 0), 0.5f);
            primitives.Add(sphere2);

            sphere3 = new Sphere(new Vector3(-20, 0, 30), 5, new Vector3(0, 0, 255), 0);
            primitives.Add(sphere3);

            plane1 = new Plane(new Vector3(0, 1, 0), 10, new Vector3(255, 255, 255), 0);
            primitives.Add(plane1);

            light1 = new Light(new Vector3(20, 10, -10), 1,1,1);
            lightSources.Add(light1);

            //light2 = new Light(new Vector3(10, 20, -5), 1f, 1f, 1f);
            //lightSources.Add(light2);

            screenHeight = Template.OpenTKApp.screen.height;
            screenWidth = Template.OpenTKApp.screen.width / 2;
            debugXbottom = -100;
            debugXtop = 100;
            debugYbottom = -100;
            debugYtop = 100;
        }

        /// <summary>
        /// For each ray, check if it intersects with any of the primitives
        /// </summary>
        /// <param name="ray">The ray to check</param>
        /// <returns>Returns the color of a pixel</returns>
        public Vector3 Intersect(Ray ray)
        {
            //Variable for calculating the smallest intersection
            Intersection smallest = null;
            //Check for all primitives
            foreach (Primitive p in primitives)
            {
                //Make a new intersection i of the ray with the primitive
                Intersection i = p.Intersect(ray);
                //If there is no intersection, skip this, else check if smallest == null or if the intersectionpoint is closer than smallest
                //If that is true, set smallest to the found intersectionpoint
                if (i != null && (smallest == null || CalcMethods.VectorLength(i.intersectionPoint) < CalcMethods.VectorLength(smallest.intersectionPoint)))
                    smallest = i;
            }
            //If there IS an intersection with an object
            if (smallest != null)
            {                
                float raylength = CalcMethods.VectorLength(smallest.intersectionPoint - ray.origin);
                Vector3 color = LightIntensity(smallest);
                color /= Math.Max(Math.Max(color.X, color.Y), color.Z);
                //return the color: the color of the primitive (between 0 - 255) * the intensity of the light (between 0 - 1)
                return color * smallest.nearestPrimitive.Color(smallest.intersectionPoint);
            }
            //If there were no intersections, return Vector3.Zero, which corresponds to a black color
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
            //add 1 to the counter for each (pair of) shadow ray(s)
            count++;
            //Make a Vector3 lightintensity and set it to 0, in case there were no lights reachable
            Vector3 lightintensity = Vector3.Zero;
            //Check all the light sources:
            foreach (Light light in lightSources)
            {            
                //Make a shadow ray: origin is the intersectionpoint + small offset, direction is the light source
                Ray shadowray = new Ray(i.intersectionPoint +  i.normal * 0.0001f, CalcMethods.Normalize(light.location - i.intersectionPoint));
                //Set the shadow ray's t for the debug output
                shadowray.t = CalcMethods.VectorLength(light.location - i.intersectionPoint);
                //Set the occlusion to false in the first place
                bool occlusion = false;
                foreach (Primitive p in primitives)
                {
                    Intersection intersect = p.Intersect(shadowray);
                    if (intersect != null ) //&& intersectie is dichterbij de lamp als de originele intersectie
                    {
                        if (CalcMethods.VectorLength(intersect.intersectionPoint - light.location) < CalcMethods.VectorLength(i.intersectionPoint - light.location))
                        {
                            occlusion = true;
                            break;
                        }
                    }
                }
                if (i.intersectionPoint.Y ==0  && count % 10 == 0)
                        Draw2DRay(shadowray, 101111);
                // INTENSITY FACTOR FOR LIGHT ANGLE CALCULATION, WIP, GROUND WORKS, SPHERES ARE A LITTLE WEIRD
                if (!occlusion)
                {
                    float intensityFactor = CalcMethods.DotProduct(shadowray.direction, i.normal);
                    float shadowraylength = CalcMethods.VectorLength(light.location - i.intersectionPoint);
                    lightintensity += intensityFactor * new Vector3(light.redIntensity, light.greenIntensity, light.blueIntensity) / (shadowraylength*shadowraylength);
                }
            }
            lightintensity /= lightSources.Count;
            return lightintensity;
        }

        //public Vector3 SecondaryRay(Intersection i, Ray ray)
        //{
        //    Vector3 R = ray.direction - 2 * CalcMethods.DotProduct(ray.direction, i.normal) * i.normal;
        //    Ray secondray = new Ray(i.intersectionPoint + i.normal * 0.01f, CalcMethods.Normalize(R));
        //    return Intersect(secondray);
        //}

        public void Draw2DRay(Ray ray, int color)
        {
            int x1 = (int)((ray.origin.X - debugXbottom) / (debugXtop - debugXbottom) * screenWidth + screenWidth);
            int y1 = screenHeight - (int)((ray.origin.Z - debugYbottom) / (debugYtop - debugYbottom) * screenHeight);
            int x2 = (int)(((ray.origin.X + ray.t * ray.direction.X) - debugXbottom) / (debugXtop - debugXbottom) * screenWidth + screenWidth);
            int y2 = screenHeight - (int)(((ray.origin.Z + ray.t * ray.direction.Z) - debugYbottom) / (debugYtop - debugYbottom) * screenHeight);

            if (x2 > 2 * screenWidth)
            {
                y2 -= (int)((x2 - (2 * screenWidth)) * ((float)(y2 - y1) / (float)(x2 - x1)));
                x2 = 2 * screenWidth - 1;
            }

            if (x2 < screenWidth)
            {
                y2 += (int)((screenWidth - x2) * ((float)(y2 - y1) / (float)(x2 - x1)));
                x2 = screenWidth;
            }

            if (y2 < 0)
            {
                x2 -= (int)((y2) * ((float)(x2 - x1) / (float)(y2 - y1)));
                y2 = 0;
            }

            if (y2 > screenHeight)
            {

                x2 += (int)((screenHeight - y2) * ((float)(x2 - x1) / (float)(y2 - y1)));
                y2 = screenHeight - 1;
            }
            Template.OpenTKApp.screen.Line(x1, y1, x2, y2, color);
        }
    }
}
