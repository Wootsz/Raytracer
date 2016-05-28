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
        public int screenWidth = 512, screenHeight = 512;

        public Raytracer()
        {
            camera = new Camera();
            scene = new Scene();
            camera.position = Vector3.Zero;
        }

        public void Render()
        {
            Ray ray;
            //voor alle pixels
            for (int x = 0; x < screenWidth; x++)
            {
                for (int y = 0; y < screenHeight; y++)
                {
                    ray = new Ray(camera.position, new Vector3(x,y,camera.screenPlane.distance) - camera.position);
                    OpenTKApp.screen.Plot(x, y, (int)scene.Intersect(ray));
                }
            }
        }

    }
}
