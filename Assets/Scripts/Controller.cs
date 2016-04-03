﻿using UnityEngine;
using System.Collections;
using DG.Tweening;
using System.Collections.Generic;

public class Controller : MonoBehaviour 
{
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
	

	void Update () 
	{
		if(!targeting)
			Focus ();

		Targeting ();

	}

	bool rightPressed = false, leftPressed = false;

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
				} 
				else if (touch.Position.x <= Screen.width / 2)
				{
					left = true;
				}
			}
		}

		if (left)
		{
			if (!isCameraLocked)
			{
				//leftPressed = true;
				isCameraLocked = true;
			}
		} 
		else
		{
			if (isCameraLocked)
			{
				//leftPressed = false;
				isCameraLocked = false;
			}
		}

		if (focusedPlanet != null)
		{
			if (right)
			{
				if (!rightPressed)
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
				rightPressed = true;
			} else
			{
				rightPressed = false;
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
