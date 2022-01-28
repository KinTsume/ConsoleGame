using System.Collections.Generic;
using System;
namespace roberto
{
    public class Camera : Entity
    {
        private string[,] camera;

        public Camera()
        {
            ShapeWidth = 20;
            ShapeHeight = 20;
            camera = new string[ShapeWidth, ShapeHeight];

            for (int i = 0; i < ShapeHeight; i++)
            {
                for (int j = 0; j < ShapeWidth; j++)
                {
                    camera[i, j] = " ";
                }
            }

            PositionX = 0;
            PositionY = 0;
        }

        public string GetPixels(Map mapObject)
        {
            //Empty the camera before rendering
            for (int i = 0; i < ShapeHeight; i++)
            {
                for (int j = 0; j < ShapeWidth; j++)
                {
                    camera[i, j] = " ";
                }
            }

            //When rendering objects make sure to not change the pixel if the object pixel is empty ("")

            //render the static objects from the map
            var SEntityList = mapObject.GetEntityList();

            foreach (StaticEntity item in SEntityList)
            {
                bool DrawItem = false;

                var itemCenter = item.GetPosition();
                var itemDimension = item.GetShapeDimensions();

                var cameraCenter = this.GetPosition();
                var cameraDimension = this.GetShapeDimensions();

                var centerDistance = new float[] {Math.Abs(itemCenter[0] - cameraCenter[0]), Math.Abs(itemCenter[1] - cameraCenter[1])};

                if ((centerDistance[0] <= (itemDimension[0]/2 + cameraDimension[0]/2)) && (centerDistance[1] <= (itemDimension[1] / 2 + cameraDimension[1] / 2)))
                {
                    DrawItem = true;
                }
                
                //The info about the index of the vertices is at Entity.GetVertices()
                var itemVertex1 = item.GetVertices(0);
                var cameraVertex1 = this.GetVertices(0);


                /*
                var itemVertex1 = item.GetVertices(0);
                var itemVertex2 = item.GetVertices(1);
                var itemVertex3 = item.GetVertices(2);
                var itemVertex4 = item.GetVertices(3);
                         
                var cameraVertex1 = this.GetVertices(0);
                var cameraVertex2 = this.GetVertices(1);
                var cameraVertex3 = this.GetVertices(2);
                var cameraVertex4 = this.GetVertices(3);

                if ((itemVertex1[0] > cameraVertex1[0]) && (itemVertex1[1] < cameraVertex1[1]))
                {
                    if ((itemVertex1[0] < cameraVertex2[0]) && ((itemVertex1[1] > cameraVertex2[1])))
                    {
                        DrawItem = true;
                    }
                }
                else if ((itemVertex2[0] > cameraVertex1[0]) && (itemVertex2[1] < cameraVertex1[1]))
                {
                    if ((itemVertex2[0] < cameraVertex2[0]) && ((itemVertex2[1] > cameraVertex2[1])))
                    {
                        DrawItem = true;
                    }
                }
                else if ((itemVertex2[0] > cameraVertex1[0]) && (itemVertex2[1] < cameraVertex1[1]))
                {
                    if ((itemVertex2[0] < cameraVertex2[0]) && ((itemVertex2[1] > cameraVertex2[1])))
                    {
                        DrawItem = true;
                    }
                }
                else if ((itemVertex2[0] > cameraVertex1[0]) && (itemVertex2[1] < cameraVertex1[1]))
                {
                    if ((itemVertex2[0] < cameraVertex2[0]) && ((itemVertex2[1] > cameraVertex2[1])))
                    {
                        DrawItem = true;
                    }
                }*/

                if (DrawItem)
                {
                    var shape = item.GetShapeString();
                    var indexToStartDrawing = new int[2] { (int)(itemVertex1[0] - cameraVertex1[0]), (int)(itemVertex1[1] - cameraVertex1[1]) };
                    indexToStartDrawing[1] *= -1;

                    var itemDimensions = item.GetShapeDimensions();

                    var itemOffsetIndex = new int[2] { 0, 0 };

                    //This if is to prevent the camera to try render objects out of the camera X view
                    if (indexToStartDrawing[0] < 0)
                    {
                        itemDimensions[0] += indexToStartDrawing[0];
                        itemOffsetIndex[0] -= indexToStartDrawing[0];
                        indexToStartDrawing[0] = 0;
                    }

                    //This if is to prevent the camera to try render objects out of the camera Y view
                    if (indexToStartDrawing[1] < 0)
                    {
                        itemDimensions[1] += indexToStartDrawing[1];
                        itemOffsetIndex[1] -= indexToStartDrawing[1];
                        indexToStartDrawing[1] = 0;
                    }

                    for (int i = 0; i < itemDimensions[1]; i++)
                    {
                        if (indexToStartDrawing[1] + i >= this.ShapeHeight)
                            break;

                        for (int j = 0; j < itemDimensions[0]; j++)
                        {
                            if (indexToStartDrawing[0] + j >= this.ShapeWidth)
                                break;

                            if (shape[i, j] == " ")
                                continue;

                            camera[indexToStartDrawing[1] + i, indexToStartDrawing[0] + j] = shape[itemOffsetIndex[1] + i, itemOffsetIndex[0] + j];
                        }
                    }

                }
            }

            //Transform the camera array in one string 
            var CamString = new string("");
            for (int i = 0; i < ShapeHeight; i++)
            {
                for (int j = 0; j < ShapeWidth; j++)
                {
                    CamString += camera[i, j];
                }
                CamString += "\n";
            }

            return CamString;
        }
    }
}