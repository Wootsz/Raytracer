using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace template
{
    public class CalcMethods
    {

        public static float DotProduct(Vector3 a, Vector3 b)
        {
            return (a.X * b.X + a.Y * b.Y + a.Z * b.Z);
        }

        public static Vector3 CrossProduct(Vector3 a, Vector3 b)
        {
            return new Vector3( a.Y * b.Z - a.Z * b.Y,
                                a.Z * b.X - a.X * b.Z,
                                a.X * b.Y - a.Y * b.X);
        }

    }
}
