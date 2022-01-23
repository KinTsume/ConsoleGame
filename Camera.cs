using System.Collections.Generic;
namespace roberto
{
    public class Camera : Entity
    {
        private string[,]  camera;

        public Camera()
        {
            ShapeWidth = 20;
            ShapeHeight = 20;
            camera = new string[ShapeWidth, ShapeHeight];

            for(int i = 0; i < ShapeHeight; i++)
            {
                for(int j = 0; j < ShapeWidth; j++)
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
            for(int i = 0; i < ShapeHeight; i++)
            {
                for(int j = 0; j < ShapeWidth; j++)
                {
                    camera[i, j] = " ";
                }
            }

            //When rendering objects make sure to not change the pixel if the object pixel is empty ("")

            //render the static objects from the map
            var SEntityList = mapObject.GetEntityList();

            foreach (StaticEntity item in SEntityList)
            {
                var itemUpLeftVertex = item.GetVertices(0);
                var itemDownRightVertex = item.GetVertices(1);

                var cameraUpLeftVertex = this.GetVertices(0);
                var cameraDownRightVertex = this.GetVertices(1);
                
                bool DrawItem = false;

                if((itemUpLeftVertex[0] > cameraUpLeftVertex[0]) && (itemUpLeftVertex[1] < cameraUpLeftVertex[1]))
                {
                    if((itemUpLeftVertex[0] < cameraDownRightVertex[0]) && ((itemUpLeftVertex[1] > cameraDownRightVertex[1])))
                    {
                        DrawItem = true;
                    }
                } else if((itemDownRightVertex[0] > cameraUpLeftVertex[0]) && (itemDownRightVertex[1] < cameraUpLeftVertex[1]))
                {
                    if((itemDownRightVertex[0] < cameraDownRightVertex[0]) && ((itemDownRightVertex[1] > cameraDownRightVertex[1])))
                    {
                        DrawItem = true;
                    }
                }

                if(DrawItem)
                {
                    var shape = item.GetShapeString();
                    var indexToStartDrawing = new int[2]{(int)(itemUpLeftVertex[0] - cameraUpLeftVertex[0]), (int) (itemUpLeftVertex[1] - cameraUpLeftVertex[1])};
                    indexToStartDrawing[1] *= -1;

                    var itemDimensions = item.GetShapeDimensions();

                    var itemOffsetIndex = new int[2]{0, 0};

                    //This if is to prevent the camera to try render objects out of the camera X view
                    if(indexToStartDrawing[0] < 0)
                    {
                        itemDimensions[0] += indexToStartDrawing[0]; 
                        itemOffsetIndex[0] -= indexToStartDrawing[0];
                        indexToStartDrawing[0] = 0;                        
                    }

                    //This if is to prevent the camera to try render objects out of the camera Y view
                    if(indexToStartDrawing[1] < 0)
                    {
                        itemDimensions[1] += indexToStartDrawing[1]; 
                        itemOffsetIndex[1] -= indexToStartDrawing[1];
                        indexToStartDrawing[1] = 0;                        
                    }

                    for(int i = 0; i < itemDimensions[1]; i++)
                    {
                        if(i >= this.ShapeHeight)
                            break;

                        for(int j = 0; j < itemDimensions[0]; j++)
                        {   
                            if(j >= this.ShapeWidth)
                                break;

                            if(shape[i, j] == " ")
                                continue;

                            camera[indexToStartDrawing[1] + i, indexToStartDrawing[0] + j] = shape[itemOffsetIndex[1] + i, itemOffsetIndex[0] + j];
                        }
                    }

                }
            }

            //Transform the camera array in one string 
            var CamString = new string("");
            for(int i = 0; i < ShapeHeight; i++)
            {
                for(int j = 0; j < ShapeWidth; j++)
                {
                    CamString += camera[i ,j];
                }
                CamString += "\n";
            }

            return CamString;
        }
    }
}