using Model.Persons;
using Model.Simulation;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Tiles.Buildings
{
    public class WorkplaceSubTile : Building, IWorkplace
    {
        private IWorkplace _baseWorkplace;

        public WorkplaceSubTile(int x, int y, uint designID, IWorkplace baseBuilding) : base(x, y, designID)
		{
            _baseWorkplace = baseBuilding;
        }

		public void ParentDestroyedEventHandler(object sender, EventArgs e)
		{
			SimEngine.Instance.BuildingManager.Destroy(this);
		}

        public Vector2 GetRealCoordinates()
        {
            return this.Coordinates;
        }

        public Vector2 GetBaseCoordinates()
        {
            return ((Building)_baseWorkplace).Coordinates;
		}

		public bool Employ(Person person)
		{
			return _baseWorkplace.Employ(person);
		}

		public bool Unemploy(Person person)
		{
			return _baseWorkplace.Unemploy(person);
		}

		public List<Person> GetWorkers()
		{
			return _baseWorkplace.GetWorkers();
		}

		public int GetWorkersCount()
		{
			return _baseWorkplace.GetWorkersCount();
		}

		public int GetWorkersLimit()
		{
			return _baseWorkplace.GetWorkersLimit();
		}
	}
}
