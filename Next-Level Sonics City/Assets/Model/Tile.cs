using Model.RoadGrids;
using Model.Tiles;
using System;
using UnityEngine;

namespace Model
{
	public abstract class Tile
	{
		private uint _designID;
		public uint DesignID
		{
			get { return _designID; }
			protected set
			{
				if (value != _designID) { OnDesignIDChange?.Invoke(this, EventArgs.Empty); }
				_designID = value;
			}
		}

		public Vector3 Coordinates { get; protected set; }

		public event EventHandler<Tile> OnTileDelete;
		private void TileDeleteInvoke() => OnTileDelete?.Invoke(this, this);
		public event EventHandler<Tile> OnTileChange;
		protected void TileChangeInvoke() => OnTileChange?.Invoke(this, this);
		public event EventHandler OnDesignIDChange;

		/// <summary>
		/// Constructor for Tile
		/// </summary>
		/// <param name="x">X coordinate for tile</param>
		/// <param name="y">Y coordinate for tile</param>
		/// <param name="designID">designID for the tile</param>
		public Tile(int x, int y, uint designID)
		{
			if (x < 0 || y < 0) { throw new System.ArgumentException("Coordinates must be positive"); }
			if (x >= City.Instance.GetSize() || y >= City.Instance.GetSize()) { throw new System.ArgumentException("Coordinates must be less than city size"); }

			DesignID = designID;
			Coordinates = new Vector3(x, y, 0);
		}

		/// <summary>
		/// Returns the type of the tile
		/// </summary>
		/// <returns>Type of the tile</returns>
		public abstract TileType GetTileType();

		/// <summary>
		/// Returns if the tile can be built on location
		/// </summary>
		/// <returns>True if there is empty tile</returns>
		public virtual bool CanBuild()
		{
			return City.Instance.GetTile(Coordinates) is EmptyTile;
		}

		/// <summary>
		/// Returns if the tile can be destroyed
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public void UpdateCoordinates(int x, int y)
		{
			if (_isFinalized) { throw new InvalidOperationException(); }

			Coordinates = new Vector3(x, y, 0);
		}

		protected bool _isFinalized = false;

		/// <summary>
		/// <para>MUST BE CONTAINS ONLY <code>this.Finalizing()</code> AND ALL THE OTHER LOGIC MUST BE IMPLEMENTED IN THAT</para>
		/// <para>Finalizes the tile</para>
		/// </summary>
		public abstract void FinalizeTile();

		/// <summary>
		/// <para>MUST BE STARTED WITH <code>base.Finalizing()</code></para>
		/// <para>Do the actual finalization</para>
		/// </summary>
		protected void Finalizing()
		{
			if (_isFinalized) return;
			_isFinalized = true;

			if (this is IRoadGridElement roadGridElement)
			{
				roadGridElement.RegisterRoadGridElement();
			}
		}

		/// <summary>
		/// <para>MUST BE CONTAINS ONLY <code>this.Deleting()</code> AND ALL THE OTHER LOGIC MUST BE IMPLEMENTED IN THAT WHICH MUST BE START WITH <code>base.Deleting()</code></para>
		/// <para>Delete the tile</para>
		/// </summary>
		public abstract void DeleteTile();

		/// <summary>
		/// <para>MUST BE STARTED WITH <code>base.Deleting()</code></para>
		/// <para>Do the deletion administration</para>
		/// </summary>
		protected void Deleting()
		{
			if (!_isFinalized) { throw new InvalidOperationException(); }

			if (this is IRoadGridElement roadGridElement)
			{
				roadGridElement.UnregisterRoadGridElement();
			}

			TileDeleteInvoke();
		}

		/// <summary>
		/// Returns the price of building this tile
		/// </summary>
		public abstract int BuildPrice { get; }

		/// <summary>
		/// Returns the price of destroying this tile
		/// </summary>
		public abstract int DestroyIncome { get; }

		/// <summary>
		/// Returns the price of maintaining this tile
		/// </summary>
		public virtual int MaintainanceCost { get; } = 0;

		/// <summary>
		/// Returns the tile transparency for the effects
		/// </summary>
		public virtual float Transparency { get; } = 0.75f;
	}
}