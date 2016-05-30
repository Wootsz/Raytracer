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
        public int screenWidth, screenHeight, debugXtop, debugXbottom, debugYtop, debugYbottom;

        public Raytracer()
        {
            camera = new Camera(Vector3.Zero, new Vector3(0, 0, 1));
            scene = new Scene();
            camera.position = Vector3.Zero;
            screenHeight = Template.OpenTKApp.screen.height;
            screenWidth = Template.OpenTKApp.screen.width / 2;
            debugXbottom = -100;
            debugXtop = 100;
            debugYbottom = -10;
            debugYtop = 100;

        }

        public void Render()
        {
            Ray ray;
            //voor alle pixels
            for (int x = 0; x < screenWidth; x++)
            {
                for (int y = 0; y < screenHeight; y++)
                {
                    ray = new Ray(camera.position, CalcMethods.Normalize(new Vector3(camera.LTCorner + (x * (camera.RTCorner - camera.LTCorner) / screenWidth) + y * (camera.LBCorner - camera.LTCorner) / screenHeight)));
                    ray.t = 1000;
                    Vector3 color = scene.Intersect(ray);
                    OpenTKApp.screen.Plot(x, y, CreateColor((int)color.X, (int)color.Y, (int)color.Z));
                    if (y == screenHeight / 2 && x % 10 == 0)
                        Draw2DRay(ray);
                    
                }
            }
        }

        int CreateColor(int red, int green, int blue)
        {
            return (red << 16) + (green << 8) + blue;
        }

        public void Draw2DRay(Ray ray)
        {
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
                    y2 = screenHeight - 1;
                }
                OpenTKApp.screen.Line(x1, y1, x2, y2, 16711680);
            }
        }
    }
}
