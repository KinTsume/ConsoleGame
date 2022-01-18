using System;

namespace roberto // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var camera = new Camera();
            string Pixels = camera.GetPixels();
            Console.Write(Pixels);
            
            var pos = new float[2]{0,0};
            var shap = ShapeEnum.Triangle;
            var dim = new int[2]{10, 10};

            var entity = new StaticEntity(pos, shap, dim);
            Console.Write(entity);
        }
    }
}