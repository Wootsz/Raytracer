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

        public Scene()
        {
            //add primitives and light sources
        }

        public bool Intersect()
        {
            return false;
        }
    }
}
