using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace template
{
    public class Application
    {
        Raytracer raytracer;

        public Application()
        {
            raytracer = new Raytracer();
        }

        public void Update()
        {
            raytracer.Render();
        }
    }
}
