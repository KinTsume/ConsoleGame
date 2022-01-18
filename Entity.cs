namespace roberto
{
    public abstract class Entity
    {
        public Guid id { get;  set;}
        protected float PositionX { get; set; }
        protected float PositionY { get; set; }
        protected ShapeEnum Shape { get; set; }
        protected int ShapeWidth { get; set; }
        protected int ShapeHeight { get; set; }

        public Guid GetId()
        {
            return id;
        }

        public float[] GetPosition()
        {
            var Pos = new float[2]{PositionX, PositionY};
            return Pos;
        }

        public void SetPosition(float[] position)
        {
            if(position.Length != 2)
            {
                throw new ArgumentException("Array length must be 2");
            }

            PositionX = position[0];
            PositionY = position[1];
        }

        public int[] GetShapeDimensions()
        {
            var dim = new int[2]{ShapeWidth, ShapeHeight};
            return dim;
        }

        public void SetShapeDimensions(int[] dimensions)
        {
            if(dimensions.Length != 2)
            {
                throw new ArgumentException("Array length must be 2");
            }

            ShapeWidth = dimensions[0];
            ShapeHeight = dimensions[1];
        }

        public string[,] GetShapeString()
        {
            var shapeString = new string[ShapeHeight, ShapeWidth];

            switch(Shape)
            {
                case ShapeEnum.Rectangle:
                    for(int i = 0; i < ShapeHeight; i++)
                    {
                        for(int j = 0; j < ShapeWidth; j++)
                        {
                            shapeString[i, j] = "O";
                        }
                    }
                    break;

                case ShapeEnum.Elipse:
                    for(int i = 0; i < ShapeHeight; i++)
                    {
                        for(int j = 0; j < ShapeWidth; j++)
                        {
                            var pos = GetPosition();
                            var shapeDim = GetShapeDimensions();
                            var UpLeftVertex = new float[2]{pos[0] - shapeDim[0] / 2, pos[1] - shapeDim[1] / 2};
                            var testPoint = new float[2]{UpLeftVertex[0] + j, UpLeftVertex[1] + i};

                            if(GeometryCalculation.IsInsideElipse(pos, shapeDim, testPoint))                            
                                shapeString[i, j] = "O";
                            else 
                                shapeString[i, j] = " ";                    
                        }
                    }
                    break;

                case ShapeEnum.Triangle:
                    for(int i = 0; i < ShapeHeight; i++)
                    {
                        for(int j = 0; j < ShapeWidth; j++)
                        {
                            var pos = GetPosition();
                            var shapeDim = GetShapeDimensions();
                            var UpLeftVertex = new float[2]{pos[0] - shapeDim[0] / 2, pos[1] + shapeDim[1] / 2};
                            var testPoint = new float[2]{UpLeftVertex[0] + j, UpLeftVertex[1] - i};

                            var testA = testPoint[0];
                            var testB = testPoint[1];
                            
                            if(GeometryCalculation.IsInsideTriangle(pos, shapeDim, testPoint, 1))                            
                                shapeString[i, j] = "T";
                            else 
                                shapeString[i, j] = " ";                    
                        }
                    }
                    break;
                case ShapeEnum.Custom:
                    break;
                default:
                    break;
            }
            return shapeString;
        }

        public override string ToString()
        {
            var shapeString = GetShapeString();
            var stringToReturn = "";
            for(int i = 0; i < ShapeHeight; i++)
            {
                for(int j = 0; j < ShapeWidth; j++)
                {
                    stringToReturn += shapeString[i,j];
                }
                stringToReturn += "\n";
            }
            return stringToReturn;
        }
    }
}