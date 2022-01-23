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
           
            var pos = new float[2]{0,0};
            var shap = ShapeEnum.Rectangle;
            var dim = new int[2]{10, 10};

            var entity = new StaticEntity(pos, shap, dim);
            //Console.Write(entity);
            map.AddToList(entity);

            for(int i = 0; i < 20; i++)
            {
                string Pixels = camera.GetPixels(map);
                Console.Write(Pixels);

                var posstionman = new float[2]{i, 0f};
                camera.SetPosition(posstionman);
                System.Threading.Thread.Sleep(500);
            }
            
        }
    }
}