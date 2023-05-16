using Model.Persons;
using Model.Simulation;
using Model.Tiles.Buildings.BuildingCommands;
using System;
using System.Collections.Generic;
using Model.RoadGrids;

namespace Model.Tiles.Buildings
{
	public class Industrial : Building, IWorkplace, IZoneBuilding
	{
		public ZoneBuildingLevel Level { get; private set; }
		private readonly List<Worker> _workers = new();
		private int _workersLimit = 0;

		public Industrial(int x, int y, uint designID) : base(x, y, designID, Rotation.TwoSeventy) //TODO rotation
		{
			Level = 0;
		}

		public override TileType GetTileType() { throw new InvalidOperationException(); }

		public void RegisterWorkplace(RoadGrid roadGrid)
		{
			roadGrid?.AddWorkplace(this);
		}

		public void UnregisterWorkplace(RoadGrid roadGrid)
		{
			roadGrid?.RemoveWorkplace(this);
		}

		public void LevelUp()
		{
			if (Level == ZoneBuildingLevel.THREE) { return; }
			++Level;
			_workersLimit += 5;
		}

		public bool Employ(Worker worker)
		{
			if (_workers.Count < _workersLimit)
			{
				_workers.Add(worker);
				return true;
			}

			return false;
		}

		public bool Unemploy(Worker worker)
		{
			return _workers.Remove(worker);
		}

		public List<Worker> GetWorkers()
		{
			return _workers;
		}

		public int GetWorkersCount()
		{
			return _workers.Count;
		}

		public int GetWorkersLimit()
		{
			return _workersLimit;
		}
		public Tile GetTile() { return this; }

		public override int GetBuildPrice() //TODO implementik logic
		{
			return BUILD_PRICE;
		}

		public override int GetDestroyPrice()
		{
			return DESTROY_PRICE;
		}

		public override int GetMaintainanceCost()
		{
			return GetBuildPrice() / 10;
		}
	}
}