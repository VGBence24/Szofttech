using Model.Tiles.Buildings;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Model
{
	public abstract class Tile
	{
		private uint _designID;
		public uint DesignID { get { return _designID; } protected set { _designID = value; DesignIDChangeEvent.Invoke(); } }
		public UnityEvent DesignIDChangeEvent = new();
		public Vector3 Coordinates { get; protected set; }
		public readonly UnityEvent OnTileChange = new ();
		public readonly UnityEvent OnTileDelete = new ();

		protected const int BUILD_PRICE = 10000;
		protected const int DESTROY_PRICE = 2000;
		

		public Tile(int x, int y, uint designID)
		{
			DesignID = designID;
			Coordinates = new Vector3(x, y, 0);
		}

		public virtual int GetBuildPrice()
		{
			return BUILD_PRICE;
		}

		public virtual int GetDestroyPrice()
		{
			return DESTROY_PRICE;
		}

		public virtual int GetMaintainanceCost()
		{
			return GetBuildPrice() / 10;
		}
	}
}