using System;

namespace roberto // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var camera = new Camera();

            var map = new Map();

            var pos = new float[2] { 5, 5 };
            var shap = ShapeEnum.Triangle;
            var dim = new int[2] { 10, 10 };

            var entity = new StaticEntity(pos, shap, dim);
            map.AddToList(entity);

            pos = new float[2] { -5, -5 };
            shap = ShapeEnum.Rectangle;
            var entity2 = new StaticEntity(pos, shap, dim);
            map.AddToList(entity2);



            for (int i = 0; i < 20; i++)
            {
                string Pixels = camera.GetPixels(map);
                Console.Write(Pixels);
                /*var cameraPos = camera.GetPosition();
                var cameraNewPos = new float[2] { cameraPos[0] + 1, cameraPos[1] };
                camera.SetPosition(cameraNewPos);*/

                var EntityPos = entity.GetPosition();
                var newPosition = new float[2] { EntityPos[0] - 1, EntityPos[1] };
                entity.SetPosition(newPosition);

                EntityPos = entity2.GetPosition();
                newPosition = new float[2]{EntityPos[0]+2, EntityPos[1]+1};
                entity2.SetPosition(newPosition);

                System.Threading.Thread.Sleep(500);
            }

        }
    }
}