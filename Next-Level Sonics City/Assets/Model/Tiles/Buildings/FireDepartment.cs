using Model.Simulation;
using Model.Tiles.Buildings.BuildingCommands;
using System;
using System.Collections.Generic;

namespace Model.Tiles.Buildings
{
	public class FireDepartment : Building, IWorkplace
	{
		private readonly List<Person> _workers = new();
		private int _workersLimit = 10;

		public FireDepartment(int x, int y, uint designID, Rotation rotation) : base(x, y, designID, rotation)
		{

		}

		public bool Employ(Person person)
		{
			if (_workers.Count < _workersLimit)
			{
				_workers.Add(person);
				return true;
			}

			return false;
		}

		public bool Unemploy(Person person)
		{
			if (_workers.Count > 0)
			{
				_workers.Remove(person);
				return true;
			}

			return false;
		}

		public List<Person> GetWorkers()
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

		internal override bool IsExpandable()
		{
			return true;
		}

		internal override bool CanExpand()
		{
			int x1 = (int)Coordinates.x;
			int y1 = (int)Coordinates.y;
			int x2 = (int)Coordinates.x;
			int y2 = (int)Coordinates.y;


			switch (Rotation)
			{
				case Rotation.Zero:
					x1 += 1; y1 += 1;
					break;
				case Rotation.Ninety:
					x1 += -1; y1 += 1;
					break;
				case Rotation.OneEighty:
					x1 += -1; y1 += 1;
					break;
				case Rotation.TwoSeventy:
					x1 += 1; y1 += -1;
					break;
			}

			int minX = Math.Min(x1, x2);
			int maxX = Math.Max(x1, x2);
			int minY = Math.Min(y1, y2);
			int maxY = Math.Max(y1, y2);

			for (int i = minX; i < maxX; ++i)
			{
				for (int j = minY; j < maxY; ++j)
				{
					if (SimEngine.Instance.GetTile(i, j) is not EmptyTile)
					{
						return false;
					}
				}
			}

			return true;
		}

		internal override void Expand()
		{
			int x1 = (int)Coordinates.x;
			int y1 = (int)Coordinates.y;
			int x2 = (int)Coordinates.x;
			int y2 = (int)Coordinates.y;

			switch (Rotation)
			{
				case Rotation.Zero:
					x1 += 1; y1 += 1;
					break;
				case Rotation.Ninety:
					x1 += -1; y1 += 1;
					break;
				case Rotation.OneEighty:
					x1 += -1; y1 += 1;
					break;
				case Rotation.TwoSeventy:
					x1 += 1; y1 += -1;
					break;
			}

			int minX = Math.Min(x1, x2);
			int maxX = Math.Max(x1, x2);
			int minY = Math.Min(y1, y2);
			int maxY = Math.Max(y1, y2);

			for (int i = minX; i < maxX; ++i)
			{
				for (int j = minY; j < maxY; ++j)
				{
					if (i == (int)Coordinates.x && j == (int)Coordinates.y) { continue; }
					Tile oldTile = SimEngine.Instance.GetTile(i, j);
					ExpandCommand ec = new(i, j, this);
					ec.Execute();

					MainThreadDispatcher.Instance.Enqueue(() =>
					{
						oldTile.OnTileDelete.Invoke();
					});
				}
			}
		}
	}
}