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
        public Vector3 position, direction, LTCorner, RTCorner, LBCorner;
        public float FOVfactor;

        public Camera(Vector3 position, Vector3 direction)
        {
            FOVfactor = 1f;
            this.position = position;
            this.direction = direction;
            SetCorners();
        }

        public void SetCorners()
        {
            direction = CalcMethods.Normalize(direction);
            Vector3 FOVdirection = new Vector3(direction.X * FOVfactor, direction.Y * FOVfactor, direction.Z * FOVfactor);
            LTCorner = CalcMethods.CrossProduct(CalcMethods.CrossProduct(FOVdirection, Vector3.UnitY), FOVdirection) + CalcMethods.CrossProduct(FOVdirection, Vector3.UnitY) + FOVdirection;
            RTCorner = CalcMethods.CrossProduct(CalcMethods.CrossProduct(FOVdirection, Vector3.UnitY), FOVdirection) + CalcMethods.CrossProduct(Vector3.UnitY, FOVdirection) + FOVdirection;
            LBCorner = CalcMethods.CrossProduct(CalcMethods.CrossProduct(Vector3.UnitY, FOVdirection), FOVdirection) + CalcMethods.CrossProduct(FOVdirection, Vector3.UnitY) + FOVdirection;
        }
    }
}
