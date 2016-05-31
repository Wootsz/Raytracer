using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template;
using OpenTK;

namespace template
{
    public class Raytracer
    {
        public Scene scene;
        public Camera camera;
        public static int recursionTimes;

        public Raytracer()
        {
            camera = new Camera(Vector3.Zero, new Vector3(0, 0, 1));
            scene = new Scene();
            camera.position = Vector3.Zero;
            recursionTimes = 2;
        }

        public void Render()
        {
            Ray ray;
            //voor alle pixels
            for (int x = 0; x < scene.screenWidth; x++)
            {
                for (int y = 0; y < scene.screenHeight; y++)
                {
                    ray = new Ray(camera.position, CalcMethods.Normalize(new Vector3(camera.LTCorner + (x * (camera.RTCorner - camera.LTCorner) / scene.screenWidth) + y * (camera.LBCorner - camera.LTCorner) / scene.screenHeight)));
                    if (y == scene.screenHeight / 2 && x % 50 == 0)
                    {
                        ray.draw2D = true;
                    }
                    ray.t = 1000;
                    Vector3 color = scene.Intersect(ray, recursionTimes);
                    if(ray.draw2D)
                        scene.Draw2DRay(ray, 16711680);
                    
                    OpenTKApp.screen.Plot(x, y, CreateColor((int)color.X, (int)color.Y, (int)color.Z));
                }
            }
            foreach (Primitive p in scene.primitives)
            {
                if (p is Sphere)
                {
                    Sphere s = p as Sphere;
                    scene.Draw2DCircle(s.position, s.radius);
                }
            }
        }

        int CreateColor(int red, int green, int blue)
        {
            return (Math.Min(255,red) << 16) + (Math.Min(255, green) << 8) + Math.Min(255, blue);
        }

        
    }
}
