using Model.Simulation;
using Model.Tiles.Buildings.BuildingCommands;
using System;
using UnityEngine;

namespace Model.Tiles.Buildings
{
	public class BuildingManager
	{
		public delegate void BuildingBuiltOrDestroyedEventHandler(object sender, TileEventArgs e);
		public event BuildingBuiltOrDestroyedEventHandler BuildingBuilt;
		public event BuildingBuiltOrDestroyedEventHandler BuildingDestroyed;

		public BuildingManager() { }

		public void Build(Tile tile, TileType tileType, Rotation rotation)
		{
			if (tile is not EmptyTile)
			{
				return;
			}

			int x = (int)tile.Coordinates.x;
			int y = (int)tile.Coordinates.y;
			Tile oldTile = SimEngine.Instance.GetTile(x, y);

			BuildCommand bc = new (x, y, tileType, rotation);
			bc.Execute();
			OnBuildingBuilt(tile);
		}

		public void Destroy(Tile tile)
		{
			if (tile is EmptyTile)
			{
				return;
			}

			int x = (int)tile.Coordinates.x;
			int y = (int)tile.Coordinates.y;

			DestroyCommand dc = new (x, y);
			dc.Execute();
		}

		protected virtual void OnBuildingBuilt(Tile tile)
		{
			BuildingBuilt?.Invoke(this, new TileEventArgs(tile));
		}

		protected virtual void OnBuildingDestroyed(Tile tile)
		{
			BuildingDestroyed?.Invoke(this, new TileEventArgs(tile));
		}
	}
}