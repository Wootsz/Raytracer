using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace template
{
    public class Camera
    {
        public Vector3 position;
        public Plane screenPlane;

        public Camera()
        {
            screenPlane = new Plane(new Vector3(1,0,1),1,0);
            
        }
    }
}
