using System;

namespace roberto
{
    public static class GeometryCalculation
    {
        public static bool IsInsideElipse(float[] centerPosition, int[] dimensions, float[] pointToTest)
        {
            //Elipse equation: (x + c)^2 / a^2 + (y + d)^2 / b ^2 = 1
            if ((centerPosition.Length != 2) || (dimensions.Length != 2) || (pointToTest.Length != 2))
            {
                throw new ArgumentException("Array length must be 2");
            }

            var x = pointToTest[0];
            var y = pointToTest[1];
            var a = dimensions[0] / 2;
            var b = dimensions[1] / 2;
            var c = centerPosition[0];
            var d = centerPosition[1];

            //If the result of the elipse equation is: 
            //result < 1 --> the point is inside the elipse
            //result = 1 --> the point is part of the elipse
            //result > 1 --> the point is outside the elipse
            var result = Math.Pow((x + c), 2) / Math.Pow(a, 2) + Math.Pow((y + d), 2) / Math.Pow(b, 2);
            if (result <= 1)
                return true;
            else
                return false;
        }

        public static bool IsInsideTriangle(float[] centerPosition, int[] dimensions, float[] pointToTest, int side)
        {
            var x = pointToTest[0];
            var y = pointToTest[1];
            var a = dimensions[0] / 2;
            var b = dimensions[1] / 2;
            var c = centerPosition[0];
            var d = centerPosition[1];

            float[] point1 = new float[2];
            float[] point2 = new float[2];
            float[] point3 = new float[2];
            switch (side)
            {
                //Triangle is pointing up
                case 0:
                    point1 = new float[2] { c, d + b };
                    point2 = new float[2] { c - a, d - b };
                    point3 = new float[2] { c + a, d - b };
                    break;

                //Triangle is pointing right
                case 1:
                    point1 = new float[2] { c + a, d };
                    point2 = new float[2] { c - a, d + b };
                    point3 = new float[2] { c - a, d - b };
                    break;

                //Triangle is pointing down
                case 2:
                    point1 = new float[2] { c, d - b };
                    point2 = new float[2] { c - a, d + b };
                    point3 = new float[2] { c + a, d + b };
                    break;

                //Triangle is pointing left
                case 3:
                    point1 = new float[2] { c - a, d };
                    point2 = new float[2] { c + a, d + b };
                    point3 = new float[2] { c + a, d - b };
                    break;

                default:
                    break;
            }

            var line12 = new float[2] { point1[0] - point2[0], point1[1] - point2[1] };
            //Line equation: ax + by + c = 0
            //To get the equation from 2 points:
            //a = (y1 - y2)
            //b = (x2 - x1)
            //c = x1 * y2 - x2 * y1
            var linea = point1[1] - point2[1];
            var lineb = point2[0] - point1[0];
            var linec = point1[0] * point2[1] - point1[1] * point2[0];

            var result1 = linea * point3[0] + lineb * point3[1] + linec;
            var result2 = linea * x + lineb * y + linec;

            //Here I care only about the signal of the results so I'm making they 2 have a value 1 but preserving the signal
            //if one result is -10 it'll be -1; if 24 --> 1; if -387 --> -1
            var result1Abs = 0f;
            var result2Abs = 0f;

            if (Math.Abs(result1) != 0)
            {
                result1Abs = result1 / (Math.Abs(result1));
            }

            if (Math.Abs(result2) != 0)
            {
                result2Abs = result2 / (Math.Abs(result2));
            }

            if (result2 == 0)
                return true;

            if (result1Abs != result2Abs)
                return false;

            //Calculation for point1 and point3
            var line13 = new float[2] { point1[0] - point3[0], point1[1] - point3[1] };

            linea = point1[1] - point3[1];
            lineb = point3[0] - point1[0];
            linec = point1[0] * point3[1] - point1[1] * point3[0];

            result1 = linea * point2[0] + lineb * point2[1] + linec;
            result2 = linea * x + lineb * y + linec;

            result1Abs = result1 / (Math.Abs(result1));
            result2Abs = result2 / (Math.Abs(result2));


            if (result2 == 0)
                return true;

            if (result1Abs != result2Abs)
                return false;

            //Calculation for point2 and point3
            var line23 = new float[2] { point2[0] - point3[0], point2[1] - point3[1] };

            linea = point2[1] - point3[1];
            lineb = point3[0] - point2[0];
            linec = point2[0] * point3[1] - point2[1] * point3[0];

            result1 = linea * point1[0] + lineb * point1[1] + linec;
            result2 = linea * x + lineb * y + linec;

            result1Abs = result1 / (Math.Abs(result1));
            result2Abs = result2 / (Math.Abs(result2));

            if (result2 == 0)
                return true;

            if (result1Abs != result2Abs)
                return false;

            return true;

        }
    }
}