using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace template
{
    public class Spotlight : Light
    {
        public Vector3 direction;

        public Spotlight(Vector3 location, float r, float g, float b, Vector3 direction)
        {
            this.location = location;
            this.redIntensity = r;
            this.greenIntensity = g;
            this.blueIntensity = b;
            this.direction = CalcMethods.Normalize(direction);
        }

    }
}
