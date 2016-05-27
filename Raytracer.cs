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
        public Surface screen;
        public int screenWidth = 512, screenHeight = 512;

        public Raytracer(Surface screen)
        {
            this.screen = screen;
            camera = new Camera();
            scene = new Scene();
        }

        public void Render()
        {
            //Ray ray;
            //voor alle pixels
            for (int x = 0; x < screenWidth; x++)
            {
                for (int y = 0; y < screenHeight; y++)
                {
                    //ray = new Ray();
                    //ray.position = new Vector3(camera.position)
                    float color = 0; //=rayintersection.color;
                    screen.Plot(x, y, (int)color);
                }
            }
        }

    }
}
