using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace template
{
    public class Primitive
    {
        public Vector3 color;
        public float specularity;   //value between 0 and 1

        public Primitive()
        {
            
        }

        public virtual Intersection Intersect(Ray ray)
        {
            return null;
        }
    }
}
