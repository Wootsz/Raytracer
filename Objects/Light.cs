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
        /// <param name="b">Intensity of the red color - value between 0 and 1</param>
        /// <param name="r">Intensity of the green color - value between 0 and 1</param>
        /// <param name="g">Intensity of the blue color - value between 0 and 1</param>
        public Light(Vector3 location,  float r, float g, float b)
        {
            this.location = location;
            redIntensity = r;
            greenIntensity = g;
            blueIntensity = b;
        }
    }
}
