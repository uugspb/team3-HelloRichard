using UnityEngine;
using System.Collections;
using DG.Tweening;
using System.Collections.Generic;

public class Controller : MonoBehaviour 
{
	public delegate void SwipeDelegate();

	static public event SwipeDelegate LeftSwipeEvent;
	static public event SwipeDelegate RightSwipeEvent;

	static bool isCameraLocked = false;

	static public bool IsCameraLocked
	{
		get
		{
			return isCameraLocked;
		}
	}

	Planet focusedPlanet;
	static bool targeting = false;

	public static bool IsTargeting
	{
		get
		{
			return targeting;
		}
	}

	// Use this for initialization
	void Start () {
	
	}

	void OnRightSwipe()
	{
		if (RightSwipeEvent != null)
			RightSwipeEvent ();

	}

	void OnLeftSwipe()
	{
		if (LeftSwipeEvent != null)
			LeftSwipeEvent ();

	}

//	string msg = "none";
//
//	void OnGUI()
//	{
//		GUILayout.Box (msg);
//	}
	

	void Update () 
	{
		if(!targeting)
			Focus ();

		Targeting ();

	}

	bool rightPressed = false, leftPressed = false;

	Vector2 beginTouch = Vector2.zero;
	Vector2 endTouch = Vector2.zero;

	int minPixelCountForSwipe = 5;

	void Targeting()
	{
		bool left = false, right = false;

		if (InputRemoute.InputPacket.Touches.CountTouches > 0)
		{
			foreach (TouchRemoute touch in InputRemoute.InputPacket.Touches.Touches)
			{
				if (touch.Position.x > Screen.width / 2)
				{
					right = true;

					if (!isCameraLocked)
					{
						beginTouch = touch.Position;
					} 
					else
					{
						endTouch = touch.Position;
					}
				} 
				else if (touch.Position.x <= Screen.width / 2)
				{
					left = true;
				}
			}
		}

		if (right)
		{
			if (!isCameraLocked)
			{
				isCameraLocked = true;

			}
		} 
		else
		{
			if (isCameraLocked)
			{
				isCameraLocked = false;

				if (endTouch.x > beginTouch.x && endTouch.x - beginTouch.x > minPixelCountForSwipe)
					OnRightSwipe ();
				else if (endTouch.x < beginTouch.x && beginTouch.x - endTouch.x > minPixelCountForSwipe)
					OnLeftSwipe ();
			}
		}

		if (focusedPlanet != null)
		{
			if (left)
			{
				if (!leftPressed)
				{
					if (!targeting)
					{
						targeting = true;

						foreach (var camera in GetComponentsInChildren<Camera>())
						{
							camera.DOKill ();
							camera.DOFieldOfView (65, 0.5f);
						}
					} else
					{
						targeting = false;


						foreach (var camera in GetComponentsInChildren<Camera>())
						{
							camera.DOKill ();
							camera.DOFieldOfView (90, 0.5f);
						}
					}
				}
				leftPressed = true;
			} else
			{
				leftPressed = false;
			}
		}


	}

	void Focus()
	{
		RaycastHit hit;

		if (Physics.Raycast (transform.position, transform.forward, out hit))
		{
			Planet planet = hit.transform.GetComponent<Planet> ();

			if (planet != null)
			{
				focusedPlanet = planet;

				if (!planet.IsFocused)
				{
					planet.Focus ();

				}
			}
		} 
		else
		{
			if (focusedPlanet != null)
			{
				

				focusedPlanet.Unfocus ();
				focusedPlanet = null;
			}
		}
	}

}
