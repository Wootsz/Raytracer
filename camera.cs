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
            LTCorner = CalcMethods.CrossProduct(CalcMethods.CrossProduct(direction, Vector3.UnitY), direction) + CalcMethods.CrossProduct(direction, Vector3.UnitY) + direction;
            RTCorner = CalcMethods.CrossProduct(CalcMethods.CrossProduct(direction, Vector3.UnitY), direction) + CalcMethods.CrossProduct(Vector3.UnitY, direction) + direction;
            LBCorner = CalcMethods.CrossProduct(CalcMethods.CrossProduct(Vector3.UnitY, direction), direction) + CalcMethods.CrossProduct(direction, Vector3.UnitY) + direction;
        }
    }
}
