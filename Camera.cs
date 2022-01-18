namespace roberto
{
    public class Camera : Entity
    {
        private string[,]  camera;
        public int cameraWidth { get; set; }
        public int cameraHeight { get; set; }

        public Camera()
        {
            cameraWidth = 10;
            cameraHeight = 10;
            camera = new string[cameraWidth, cameraHeight];

            for(int i = 0; i < cameraHeight; i++)
            {
                for(int j = 0; j < cameraWidth; j++)
                {
                    camera[i, j] = "A";
                }
            }
        }

        public string GetPixels()
        {
            //When rendering objects make sure to not change the pixel if the object pixel is empty ("")
            var CamString = new string("");
            for(int i = 0; i < cameraHeight; i++)
            {
                for(int j = 0; j < cameraWidth; j++)
                {
                    CamString += camera[i ,j];
                }
                CamString += "\n";
            }
            return CamString;
        }
    }
}