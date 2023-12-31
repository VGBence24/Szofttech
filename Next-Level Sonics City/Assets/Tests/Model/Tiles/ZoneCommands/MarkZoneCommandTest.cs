﻿using Model.RoadGrids;
using Model.Tiles.Buildings;
using NUnit.Framework;

namespace Model.Tiles.ZoneCommands
{
	public class MarkZoneCommandTests
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
		public void Execute_ValidZoneTypeAndAdjacentRoad_CreatesZoneAndAddsToCity()
		{
			int x = 3;
			int y = 4;
			ZoneType zoneType = ZoneType.IndustrialZone;

			City.Instance.SetTile(new RoadTile(x, y-1));
			MarkZoneCommand command = new(x, y, zoneType);
			command.Execute();

			Assert.IsInstanceOf<IZoneBuilding>(City.Instance.GetTile(x, y));
			IZoneBuilding createdTile = (IZoneBuilding)City.Instance.GetTile(x, y);
			Assert.AreEqual(zoneType, createdTile.GetZoneType());
		}

		[Test]
		public void Execute_ValidZoneTypeAndNoAdjacentRoad_DoesNotCreateZone()
		{
			int x = 3;
			int y = 4;
			ZoneType zoneType = ZoneType.IndustrialZone;

			MarkZoneCommand command = new(x, y, zoneType);
			command.Execute();

			Assert.IsInstanceOf<IZoneBuilding>(City.Instance.GetTile(x, y));
			IZoneBuilding createdTile = (IZoneBuilding)City.Instance.GetTile(x, y);
			Assert.AreEqual(ZoneType.NoZone, createdTile.GetZoneType());
		}

		[Test]
		public void Execute_ValidNoZoneTypeAndExistingZoneType_RemovesZone()
		{
			int x = 3;
			int y = 4;
			ZoneType zoneType = ZoneType.NoZone;

			IRoadGridElement roadGridElement = new RoadTile(3, 3);
			City.Instance.SetTile(roadGridElement.GetTile());
			City.Instance.SetTile(new IndustrialBuildingTile(x, y, 123));

			MarkZoneCommand command = new(x, y, zoneType);
			command.Execute();

			Assert.IsInstanceOf<IZoneBuilding>(City.Instance.GetTile(x, y));
			IZoneBuilding createdTile = (IZoneBuilding)City.Instance.GetTile(x, y);
			Assert.AreEqual(zoneType, createdTile.GetZoneType());
		}

		[Test]
		public void Execute_ValidNoZoneTypeAndNoExistingZoneType_DoesNotCreateZone()
		{
			int x = 3;
			int y = 4;
			ZoneType zoneType = ZoneType.NoZone;

			MarkZoneCommand command = new(x, y, zoneType);
			command.Execute();

			Assert.IsInstanceOf<IZoneBuilding>(City.Instance.GetTile(x, y));
			IZoneBuilding createdTile = (IZoneBuilding)City.Instance.GetTile(x, y);
			Assert.AreEqual(zoneType, createdTile.GetZoneType());
		}
	}
}
