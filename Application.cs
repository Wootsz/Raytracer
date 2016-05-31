using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace template
{
    public class Application
    {
        Raytracer raytracer;
        public Vector3 viewDirection;
        float velocity;

        public Application()
        {
            raytracer = new Raytracer();
            velocity = 2f;
        }

        public void Update()
        {
            raytracer.Render();     
            HandleKeyBoard();
        }

        public void HandleKeyBoard()
        {
            var keyboard = OpenTK.Input.Keyboard.GetState();
            
            if (keyboard[OpenTK.Input.Key.Space])
                raytracer.camera.position.Y += velocity;
            if (keyboard[OpenTK.Input.Key.ShiftLeft])
                raytracer.camera.position.Y -= velocity;

            if (keyboard[OpenTK.Input.Key.Right])
            {
                raytracer.camera.direction = CalcMethods.Normalize(raytracer.camera.direction - CalcMethods.CrossProduct(raytracer.camera.direction, Vector3.UnitY) * .05f);
                raytracer.camera.SetCorners();
            }
            if (keyboard[OpenTK.Input.Key.Left])
            {
                raytracer.camera.direction = CalcMethods.Normalize(raytracer.camera.direction + CalcMethods.CrossProduct(raytracer.camera.direction, Vector3.UnitY) *.05f);
                raytracer.camera.SetCorners();
            }
            if (keyboard[OpenTK.Input.Key.Up])
            {
                raytracer.camera.direction = CalcMethods.Normalize(raytracer.camera.direction + CalcMethods.CrossProduct(raytracer.camera.direction, Vector3.UnitX) * .05f);
                raytracer.camera.SetCorners();
            }
            if (keyboard[OpenTK.Input.Key.Down])
            {
                raytracer.camera.direction = CalcMethods.Normalize(raytracer.camera.direction - CalcMethods.CrossProduct(raytracer.camera.direction, Vector3.UnitX) * .05f);
                raytracer.camera.SetCorners();
            }
            if (keyboard[OpenTK.Input.Key.D])
            {
                raytracer.camera.position.X += velocity * (float)(Math.Cos(raytracer.camera.direction.X) * Math.Cos(raytracer.camera.direction.Z));
                raytracer.camera.position.Z += velocity * (float)(Math.Sin(raytracer.camera.direction.X) * Math.Cos(raytracer.camera.direction.Z));
            }
            if (keyboard[OpenTK.Input.Key.A])
            {
                raytracer.camera.position.X -= velocity * (float)(Math.Cos(raytracer.camera.direction.X) * Math.Cos(raytracer.camera.direction.Z));
                raytracer.camera.position.Z -= velocity * (float)(Math.Sin(raytracer.camera.direction.X) * Math.Cos(raytracer.camera.direction.Z));
            }
            if (keyboard[OpenTK.Input.Key.S])
            {
                raytracer.camera.position.X += velocity * (float)(Math.Sin(raytracer.camera.direction.X) * Math.Cos(raytracer.camera.direction.Z));
                raytracer.camera.position.Z -= velocity * (float)(Math.Cos(raytracer.camera.direction.X) * Math.Cos(raytracer.camera.direction.Z));
            }
            if (keyboard[OpenTK.Input.Key.W])
            {
                raytracer.camera.position.X -= velocity * (float)(Math.Sin(raytracer.camera.direction.X) * Math.Cos(raytracer.camera.direction.Z));
                raytracer.camera.position.Z += velocity * (float)(Math.Cos(raytracer.camera.direction.X) * Math.Cos(raytracer.camera.direction.Z));
            }
        }
    }
}
