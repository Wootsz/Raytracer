using System;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK;                           //!
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace template
{
    public class Plane : Primitive
    {
        public Vector3 normal;          //normal
        public Vector3 distance;        //distance to the origin
    }
}
