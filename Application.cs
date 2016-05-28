using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace template
{
    public class Application
    {
        Raytracer raytracer;
        public Vector3 viewDirection;

        public Application()
        {
            raytracer = new Raytracer();
        }

        public void Update()
        {
            raytracer.Render();
        }

        public void HandleInput()
        {

        }
    }
}
