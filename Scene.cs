﻿using System;
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

            sphere1 = new Sphere(new Vector3(20, 0, 30), 5, new Vector3(200, 0, 0), 0);
            primitives.Add(sphere1);

            sphere2 = new Sphere(new Vector3(0, 0, 30), 5, new Vector3(0, 200, 0), 0);
            primitives.Add(sphere2);

            sphere3 = new Sphere(new Vector3(-20, 0, 30), 5, new Vector3(0, 0, 200), 0);
            primitives.Add(sphere3);

            plane1 = new Plane(new Vector3(0, 1, 0), 10, new Vector3(255, 255, 255), 0);
            primitives.Add(plane1);

            light1 = new Light(new Vector3(20, 10, -10), 500,500,500);
            lightSources.Add(light1);

            //light2 = new Light(new Vector3(-20, 10, -10), 500, 500, 500);
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
        public Vector3 Intersect(Ray ray, int recursionTimes)
        {
            //Calculate the smallest intersection with a primitive
            Intersection smallest = IntersectScene(ray);
            //If there were no intersections, return Vector3.Zero, which corresponds to a black color
            if (smallest == null)
                return Vector3.Zero;
            //If there IS an intersection with an object
            Vector3 c = smallest.nearestPrimitive.Color(smallest.intersectionPoint);
            Vector3 N = smallest.normal;
            Vector3 I = smallest.intersectionPoint;
            float s = smallest.nearestPrimitive.specularity;
            if (recursionTimes > 0 && s != 0)
            { 
                recursionTimes--;
                Vector3 R = ray.direction - 2 * CalcMethods.DotProduct(ray.direction, N) * N;
                Ray secondray = new Ray(smallest.intersectionPoint + N * 0.001f, CalcMethods.Normalize(R));
                secondray.t = 100;
                //Draw2DRay(secondray, 255255);
                if (s == 1)
                    return Intersect(secondray, recursionTimes) * c;
                else
                    return (s - 1) * DirectIllumination(smallest.intersectionPoint, smallest.normal) * c +
                        s * Intersect(secondray, recursionTimes) * c;
            }
            else
                return DirectIllumination(I, N) * c;
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

        public Vector3 DirectIllumination(Vector3 I, Vector3 N)
        {            
            //Make a Vector3 lightintensity and set it to 0, in case there were no lights reachable
            Vector3 lightIntensity = Vector3.Zero;
            foreach (Light light in lightSources)
            {
                Vector3 L = light.location - I;
                float distance = CalcMethods.VectorLength(L);
                L *= (1.0f / distance);
                if (!IsVisible(I, L, distance))
                    break;
                float attenuation = 1 / (distance * distance);
                lightIntensity += new Vector3(light.redIntensity, light.greenIntensity, light.blueIntensity) * 
                    CalcMethods.DotProduct(N, L) * attenuation;
            }
            return lightIntensity;
        }

        /// <summary>
        /// Method checking whether something is visible
        /// </summary>
        public bool IsVisible(Vector3 I, Vector3 L, float distance)
        {
            //Make a shadow ray: origin is the intersectionpoint + small offset, direction is the light source
            Ray shadowray = new Ray(I + 0.001f * L, L);
            shadowray.t = distance;
            foreach (Primitive p in primitives)
            {
                Intersection intersect = p.Intersect(shadowray);
                if (intersect != null) //&& intersectie is dichterbij de lamp als de originele intersectie
                    if (CalcMethods.VectorLength(intersect.intersectionPoint - ((L*distance)+I)) < distance)
                        return false;
            }
            //add 1 to the counter for each (pair of) shadow ray(s)
            if (I.Y == 0 && count % 10 == 0)
                Draw2DRay(shadowray, 101111);
            count++;

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
            Template.OpenTKApp.screen.Line(x1, y1, x2, y2, color);
        }
    }
}
