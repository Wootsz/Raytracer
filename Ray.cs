using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace template
{
    public class Ray
    {
        public Vector3 origin, direction;
        public float t;
        public bool draw2D;

        public Ray(Vector3 origin, Vector3 direction)
        {
            this.origin = origin;
            this.direction = direction;
            //position = origin + new Vector3(0.001f, 0, 0.001f); //origin + small offset
        }

        public void Update()
        {

        }
    }
}
