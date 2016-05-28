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
        }

        public void Update()
        {
            raytracer.Render();
            //HandleInput();
        }

        public void HandleInput()
        {
            var keyboard = OpenTK.Input.Keyboard.GetState();
            var mouse = OpenTK.Input.Mouse.GetState();

            mousePosition = new Vector2(mouse.X, mouse.Y);
            previousMousePosition = new Vector2(raytracer.screenWidth/2, raytracer.screenHeight/2);
            mouseDiff = mousePosition - previousMousePosition;
            OpenTK.Input.Mouse.SetPosition(previousMousePosition.X, previousMousePosition.Y);
            viewAngle += mouseDiff * 0.005f;
            if (viewAngle.Y > 1)
                viewAngle.Y = 1;
            if (viewAngle.Y < -1)
                viewAngle.Y = -1;
            viewVertex = new Vector3(   raytracer.camera.position.X + (float)(Math.Cos(viewAngle.X) * Math.Cos(viewAngle.Y)),
                                        raytracer.camera.position.Y + (float)Math.Sin(viewAngle.Y),
                                        raytracer.camera.position.Z + (float)(Math.Sin(viewAngle.X) * Math.Cos(viewAngle.Y)));
            
            if (keyboard[OpenTK.Input.Key.W])
            {
                raytracer.camera.position.X += velocity * (float)(Math.Cos(viewAngle.X) * Math.Cos(viewAngle.Y));
                raytracer.camera.position.Z += velocity * (float)(Math.Sin(viewAngle.X) * Math.Cos(viewAngle.Y));
            }
            if (keyboard[OpenTK.Input.Key.S]) 
            {
                raytracer.camera.position.X -= velocity * (float)(Math.Cos(viewAngle.X) * Math.Cos(viewAngle.Y));
                raytracer.camera.position.Z -= velocity * (float)(Math.Sin(viewAngle.X) * Math.Cos(viewAngle.Y));
            }
            if (keyboard[OpenTK.Input.Key.A]) 
            {
                raytracer.camera.position.X += velocity * (float)(Math.Sin(viewAngle.X) * Math.Cos(viewAngle.Y));
                raytracer.camera.position.Z -= velocity * (float)(Math.Cos(viewAngle.X) * Math.Cos(viewAngle.Y));
            }
            if (keyboard[OpenTK.Input.Key.D]) 
            {
                raytracer.camera.position.X -= velocity * (float)(Math.Sin(viewAngle.X) * Math.Cos(viewAngle.Y));
                raytracer.camera.position.Z += velocity * (float)(Math.Cos(viewAngle.X) * Math.Cos(viewAngle.Y));
            }
        }
    }
}
