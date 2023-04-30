using Model.Tiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace View.GUI.Footer.ZoneMarkerPanel
{
	public class ResidentialZone : MonoBehaviour, IClickable
	{
		public void OnClick(bool isLeftMouseButton, Vector3 location)
		{
			TileManager.Instance.MarkZone(ZoneType.ResidentialZone);
		}

		public bool OnDrag(bool isLeftMouseButton, Vector3 direction) { return true; }

		public void OnDragEnd(bool isLeftMouseButton) { }

		public void OnDragStart(bool isLeftMouseButton, Vector3 location) { }

		public void OnHover(Vector3 location) { }

		public void OnHoverEnd() { }

		public void OnHoverStart(Vector3 location) { }

		public void OnScroll(float delta) { }

		public void OnSecondClick(List<IClickable> clicked) { }

		// Start is called before the first frame update
		void Start()
		{
			
		}

		// Update is called once per frame
		void Update()
		{
			
		}
	}
}
