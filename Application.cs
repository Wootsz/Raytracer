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
        Vector2 previousMousePosition, mouseDiff, mousePosition, viewAngle;
        Vector3 viewVertex;

        public Application()
        {
            raytracer = new Raytracer();
            velocity = 1f;
        }

        public void Update()
        {
            raytracer.Render();     
            //HandleMouse();
            HandleKeyBoard();
        }

        //public void HandleMouse()
        //{
        //    var mouse = OpenTK.Input.Mouse.GetState();

        //    mousePosition = new Vector2(mouse.X, mouse.Y);
        //    previousMousePosition = new Vector2(raytracer.screenWidth / 2, raytracer.screenHeight / 2);
        //    mouseDiff = mousePosition - previousMousePosition;
        //    OpenTK.Input.Mouse.SetPosition(previousMousePosition.X, previousMousePosition.Y);
        //    viewAngle += mouseDiff * 0.005f;
        //    if (viewAngle.Y > 1)
        //        viewAngle.Y = 1;
        //    if (viewAngle.Y < -1)
        //        viewAngle.Y = -1;
        //    viewVertex = new Vector3(raytracer.camera.position.X + (float)(Math.Cos(viewAngle.X) * Math.Cos(viewAngle.Y)),
        //                                raytracer.camera.position.Y + (float)Math.Sin(viewAngle.Y),
        //                                raytracer.camera.position.Z + (float)(Math.Sin(viewAngle.X) * Math.Cos(viewAngle.Y)));
        //}

        public void HandleKeyBoard()
        {
            var keyboard = OpenTK.Input.Keyboard.GetState();
            if (keyboard[OpenTK.Input.Key.D])
            {
                raytracer.camera.position.X += velocity * (float)(Math.Cos(raytracer.camera.direction.X) * Math.Cos(raytracer.camera.direction.Y));
                raytracer.camera.position.Z += velocity * (float)(Math.Sin(raytracer.camera.direction.X) * Math.Cos(raytracer.camera.direction.Y));
            }
            if (keyboard[OpenTK.Input.Key.A])
            {
                raytracer.camera.position.X -= velocity * (float)(Math.Cos(raytracer.camera.direction.X) * Math.Cos(raytracer.camera.direction.Y));
                raytracer.camera.position.Z -= velocity * (float)(Math.Sin(raytracer.camera.direction.X) * Math.Cos(raytracer.camera.direction.Y));
            }
            if (keyboard[OpenTK.Input.Key.S])
            {
                raytracer.camera.position.X += velocity * (float)(Math.Sin(raytracer.camera.direction.X) * Math.Cos(raytracer.camera.direction.Y));
                raytracer.camera.position.Z -= velocity * (float)(Math.Cos(raytracer.camera.direction.X) * Math.Cos(raytracer.camera.direction.Y));
            }
            if (keyboard[OpenTK.Input.Key.W])
            {
                raytracer.camera.position.X -= velocity * (float)(Math.Sin(raytracer.camera.direction.X) * Math.Cos(raytracer.camera.direction.Y));
                raytracer.camera.position.Z += velocity * (float)(Math.Cos(raytracer.camera.direction.X) * Math.Cos(raytracer.camera.direction.Y));
            }
            if (keyboard[OpenTK.Input.Key.Space])
                raytracer.camera.position.Y += velocity;
            if (keyboard[OpenTK.Input.Key.ShiftLeft])
                raytracer.camera.position.Y -= velocity;

            if (keyboard[OpenTK.Input.Key.Left]) { }
            if (keyboard[OpenTK.Input.Key.Right]) { }
            if (keyboard[OpenTK.Input.Key.Up]) { }
            if (keyboard[OpenTK.Input.Key.Down]) { }

        }
    }
}
