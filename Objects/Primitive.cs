using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace template
{
    public class Primitive
    {
        private float color;

        public Primitive()
        {
            
        }

        protected virtual Intersection Intersect(Ray ray)
        {
            return null;
        }
    }
}
