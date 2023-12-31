﻿using NUnit.Framework;

namespace Model.Tiles.Buildings
{
	public class BuildingManagerTests
	{
		[SetUp]
		public void Setup()
		{
			City.Reset();
			for (int i = 0; i < City.Instance.GetSize(); i++)
			{
				for (int j = 0; j < City.Instance.GetSize(); j++)
				{
					City.Instance.SetTile(new EmptyTile(i, j));
				}
			}
		}

		[Test]
		public void Build_ValidEmptyTile_BuildsBuildingAndRaisesBuildingBuiltEvent()
		{
			Tile tile = new EmptyTile(3, 4);
			TileType tileType = TileType.PoliceDepartment;
			Rotation rotation = Rotation.Zero;

			BuildingManager manager = BuildingManager.Instance;
			bool eventRaised = false;
			manager.BuildingBuilt += (sender, e) => eventRaised = true;

			manager.Build(tile, tileType, rotation);

			Tile createdTile = City.Instance.GetTile(3, 4);
			Assert.NotNull(createdTile);
			Assert.AreEqual(tileType, createdTile.GetTileType());
			Assert.True(eventRaised);
		}

		[Test]
		public void Build_InvalidNonEmptyTile_DoesNotBuildBuildingAndDoesNotRaiseBuildingBuiltEvent()
		{
			int x = 3;
			int y = 4;
			City.Instance.SetTile(new PoliceDepartmentBuildingTile(x, y, 123, Rotation.Zero));
			TileType tileType = TileType.PoliceDepartment;
			Rotation rotation = Rotation.Zero;

			BuildingManager manager = BuildingManager.Instance;
			bool eventRaised = false;
			manager.BuildingBuilt += (sender, e) => eventRaised = true;

			manager.Build(City.Instance.GetTile(x, y), tileType, rotation);

			Tile createdTile = City.Instance.GetTile(3, 4);
			Assert.IsInstanceOf<PoliceDepartmentBuildingTile>(createdTile);
			Assert.False(eventRaised);
		}
	}
}
