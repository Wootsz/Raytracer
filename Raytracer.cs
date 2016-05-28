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
        public int screenWidth, screenHeight;

        public Raytracer()
        {
            camera = new Camera(Vector3.Zero, new Vector3(0, 0, 1));
            scene = new Scene();
            camera.position = Vector3.Zero;
            screenHeight = Template.OpenTKApp.screen.height;
            screenWidth = Template.OpenTKApp.screen.width / 2;
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
                    ray.t = 10000;
                    OpenTKApp.screen.Plot(x, y, (int)scene.Intersect(ray));
                }
            }
            Console.WriteLine("Camera Position: " + camera.position);
            Debug();
        }

        public void Debug()
        {
            Ray ray;

            for (int i = 0; i<100; i++)
            {
                OpenTKApp.screen.Plot((int)(OpenTKApp.screen.width * 3 / 4 + i - 50), (int)(screenHeight * 3 / 4 - camera.position.Z), 0xffffff);
                ray = new Ray(new Vector3 (OpenTKApp.screen.width*3/4, screenHeight*3/4, 0), CalcMethods.Normalize(new Vector3(0,0,0)));
                //OpenTKApp.screen.Plot();
            }
        }
    }
}
