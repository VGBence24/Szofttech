using System.Collections.Generic;
using UnityEngine;

public interface IClickable
{
	/// <summary>
	/// Called when mouse clicked on the object
	/// </summary>
	/// <param name="isLeftMouseButton">True if left click and false if right click</param>
	/// <param name="location">Position of the mouse</param>
	void OnClick(bool isLeftMouseButton, Vector3 location);

	void OnSecondClick(List<IClickable> clicked);

	/// <summary>
	/// Called once a drag started
	/// </summary>
	/// <param name="isLeftMouseButton">True if left click and false if right click</param>
	/// <param name="location">Position of the mouse</param>
	void OnDragStart(bool isLeftMouseButton, Vector3 location);
	
	/// <summary>
	/// Called during mouse moving with unreleased mouse button
	/// </summary>
	/// <param name="isLeftMouseButton">True if left click and false if right click</param>
	/// <param name="direction">Move delta since the last call</param>
	/// <returns>Virtual mouse should be moved</returns>
	bool OnDrag(bool isLeftMouseButton, Vector3 direction);

	/// <summary>
	/// Called once a drag finished
	/// </summary>
	/// <param name="isLeftMouseButton">True if left click and false if right click</param>
	void OnDragEnd(bool isLeftMouseButton);
}
