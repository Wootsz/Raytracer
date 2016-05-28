using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace template
{
    public class Light
    {
        public Vector3 location;
        public float blueIntensity;
        public float redIntensity;
        public float greenIntensity;

        /// <summary>
        /// Making a point-light source
        /// </summary>
        /// <param name="location">Position of the light</param>
        /// <param name="b">Intensity of the blue color</param>
        /// <param name="r">Intensity of the red color</param>
        /// <param name="g">Intensity of the green color</param>
        public Light(Vector3 location, float b, float r, float g)
        {
            this.location = location;
            blueIntensity = b;
            redIntensity = r;
            greenIntensity = g;
        }
    }
}
