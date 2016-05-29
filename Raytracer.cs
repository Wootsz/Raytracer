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
                    ray.t = 100;
                    Vector3 color = scene.Intersect(ray);
                    OpenTKApp.screen.Plot(x, y, CreateColor((int)color.X, (int)color.Y, (int)color.Z));
                    if (y == screenHeight / 2 && x % 5 == 0 && ray.t != 1000)
                    {
                        int x1 = (int)((ray.origin.X - debugXbottom) / (debugXtop - debugXbottom) * screenWidth + screenWidth);
                        int y1 = screenHeight - (int)((ray.origin.Z - debugYbottom) / (debugYtop - debugYbottom) * screenHeight);
                        int x2 = (int)(((ray.origin.X + ray.t * ray.direction.X) - debugXbottom) / (debugXtop - debugXbottom) * screenWidth + screenWidth);
                        int y2 = screenHeight - (int)(((ray.origin.Z + ray.t * ray.direction.Z) - debugYbottom) / (debugYtop - debugYbottom) * screenHeight);

                        OpenTKApp.screen.Line(x1, y1, x2, y2, 16776961);
                    }
                }
            }
        }

        int CreateColor(int red, int green, int blue)
        {
            return (red << 16) + (green << 8) + blue;
        }

        //public void Debug()
        //{
        //    Ray ray;
        //    OpenTKApp.screen.Line(camera.LTCorner.X,camera.RTCorner, );
        //    for (int i = 0; i < 100; i++)
        //    {
        //        ray = new Ray(new Vector3 (OpenTKApp.screen.width*3/4 - camera.position.Z, screenHeight*3/4, 0), CalcMethods.Normalize(new Vector3(i,1,0)));
        //        OpenTKApp.screen.Line((int)(ray.origin.X + OpenTKApp.screen.width * 3 / 4), (int)(ray.origin.Z + screenHeight * 3 / 4), 1, 1, 0xffffff);
        //    }
        //}
    }
}
