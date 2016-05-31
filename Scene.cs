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
        Triangle triangle1;
        Light light1, light2;
        Spotlight spotlight1;
        public int screenWidth, screenHeight, debugXtop, debugXbottom, debugYtop, debugYbottom;
        int count = 0, count2 = 0;

        public Scene()
        {
            primitives = new List<Primitive>();
            lightSources = new List<Light>();

            sphere1 = new Sphere(new Vector3(20, 0, 30), 5, new Vector3(255, 255, 255), 1f);
            sphere2 = new Sphere(new Vector3(0, 0, 30), 5, new Vector3(100, 255, 100), .5f);
            sphere3 = new Sphere(new Vector3(-20, 0, 30), 5, new Vector3(100, 100, 255), .1f);
            plane1 = new Plane(new Vector3(0, 1, 0), 10, new Vector3(255, 255, 255), .2f);
            triangle1 = new Triangle(new Vector3(255, 255, 255), new Vector3(15, 10, 20), new Vector3(10, 0, 20), new Vector3(20, 0, 20), .1f);

            light1 = new Light(new Vector3(20, 10, -10), 255,255,255);
            light2 = new Light(new Vector3(-20, 5, 20), 255, 255, 255);
            spotlight1 = new Spotlight(new Vector3(), 255, 255, 255, new Vector3(0, 0, 30));

            primitives.Add(sphere1);
            primitives.Add(sphere2);
            primitives.Add(sphere3);
            primitives.Add(plane1);

            //primitives.Add(triangle1);

            //lightSources.Add(light1);
            //lightSources.Add(light2);
            lightSources.Add(spotlight1);

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
        public Vector3 Intersect(Ray ray, int recursionTimes)
        {
            //Calculate the smallest intersection with a primitive
            Intersection smallest = IntersectScene(ray);
            //If there were no intersections, return Vector3.Zero, which corresponds to a black color
            if (smallest == null)
                return Vector3.Zero;
            //If there IS an intersection with an object
            Vector3 c = smallest.nearestPrimitive.Color(smallest.intersectionPoint);
            if (smallest.nearestPrimitive is Sphere)
                c *= -1;
            Vector3 N = smallest.normal;
            Vector3 I = smallest.intersectionPoint;
            float s = smallest.nearestPrimitive.specularity;
            if (recursionTimes > 0 && s != 0)
            { 
                count2++;
                recursionTimes--;
                Vector3 R = ray.direction - (2 * CalcMethods.DotProduct(ray.direction, N) * N);
                Ray secondray = new Ray(smallest.intersectionPoint + N * 0.001f, CalcMethods.Normalize(R));
                secondray.t = 1000;
                Vector3 result = (1 - s) * DirectIllumination(smallest.intersectionPoint, smallest.normal, ray.draw2D) * c +
                    s * Intersect(secondray, recursionTimes);
                if (ray.draw2D)
                {
                    Draw2DRay(secondray, 255255);
                }
                return result;
            }
            else
                return DirectIllumination(I, N, ray.draw2D) * c;
        }

        public Vector3 Colorcap(Vector3 c)
        {
            return new Vector3(Math.Min(1, c.X), Math.Min(1, c.Y), Math.Min(1, c.Z));
        }

        public Intersection IntersectScene(Ray ray)
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
            return smallest;
        }

        public Vector3 DirectIllumination(Vector3 I, Vector3 N, bool draw2D)
        {            
            //Make a Vector3 lightintensity and set it to 0, in case there were no lights reachable
            Vector3 lightIntensity = Vector3.Zero;
            foreach (Light light in lightSources)
            {
                Vector3 L = light.location - I;
                float distance = CalcMethods.VectorLength(L);
                L *= (1.0f / distance);
                if (IsVisible(I, L, distance, draw2D, light))
                {
                    float attenuation = 1 / (distance * distance);
                    lightIntensity += new Vector3(light.redIntensity, light.greenIntensity, light.blueIntensity) *
                        Math.Min(1, CalcMethods.DotProduct(N, L)) * Math.Min(1, attenuation);
                }
            }
            //return the intensity and color of the light, and cap it to 255, otherwise it'll be weird
            return new Vector3(Math.Min(255, lightIntensity.X), Math.Min(255, lightIntensity.Y), Math.Min(255, lightIntensity.Z));
        }

        /// <summary>
        /// Method checking whether something is visible
        /// </summary>
        public bool IsVisible(Vector3 I, Vector3 L, float distance, bool draw2D, Light light)
        {
            //Make a shadow ray: origin is the intersectionpoint + small offset, direction is the light source
            Ray shadowray = new Ray(I + 0.001f * L, L);
            shadowray.t = distance;
            foreach (Primitive p in primitives)
            {
                Intersection intersect = p.Intersect(shadowray);
                if (intersect != null){ //&& intersectie is dichterbij de lamp als de originele intersectie
                    if (CalcMethods.VectorLength(intersect.intersectionPoint - ((L*distance)+I)) < distance){
                        if (light is Spotlight)
                        {
                            Spotlight s = light as Spotlight;
                            if (Math.Acos(CalcMethods.DotProduct(s.direction, L) / (CalcMethods.VectorLength(L) * CalcMethods.VectorLength(s.direction))) > 30)
                                return false;
                        }
                        else { return false; }
                    }
                }
            }
            if (draw2D)
                Draw2DRay(shadowray, 101111);

            return true;
        }

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

            if ((x1 < screenWidth || x2 < screenWidth))
                return;
            Template.OpenTKApp.screen.Line(x1, y1, x2, y2, color);
        }

        public void Draw2DCircle(Vector3 center, float r)
        {
            for (float t = 0; t < Math.PI * 2; t += (float)Math.PI / 50)
            {
                int x1 = (int)((center.X - debugXbottom + Math.Cos(t) * r) / (debugXtop - debugXbottom) * screenWidth + screenWidth);
                int y1 = screenHeight - (int)((center.Z - debugYbottom + Math.Sin(t) * r) / (debugYtop - debugYbottom) * screenHeight);
                int x2 = (int)(((center.X + (Math.Cos(t + (float)Math.PI / 50) * r)) - debugXbottom) / (debugXtop - debugXbottom) * screenWidth + screenWidth);
                int y2 = screenHeight - (int)(((center.Z + (Math.Sin(t + (float)Math.PI / 50) * r)) - debugYbottom) / (debugYtop - debugYbottom) * screenHeight);
                Template.OpenTKApp.screen.Line(x1, y1, x2, y2, 255);
            }
        }

    }
}
