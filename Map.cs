using System.Collections.Generic;

namespace roberto
{
    public class Map
    {
        private List<StaticEntity> SEntityList;

        public Map()
        {
            SEntityList = new List<StaticEntity>();
        }

        public List<StaticEntity> GetEntityList()
        {
            return SEntityList;
        }

        public void AddToList(StaticEntity entity)
        {
            SEntityList.Add(entity);
        }

        public void RemoveFromList()
        {

        }
    }
}