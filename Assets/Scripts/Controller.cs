using UnityEngine;
using System.Collections;
using DG.Tweening;

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

		if(focusedPlanet != null)
			Targeting ();

	}

	void Targeting()
	{
		if (Input.touches.Length > 0)
		{
			if (Input.GetTouch (0).phase == TouchPhase.Ended)
			{
				if (targeting)
				{
					targeting = false;


					foreach (var camera in GetComponentsInChildren<Camera>())
					{
						camera.DOKill ();
						camera.DOFieldOfView (90, 0.5f);
					}
				} else
				{
					targeting = true;

					foreach (var camera in GetComponentsInChildren<Camera>())
					{
						camera.DOKill ();
						camera.DOFieldOfView (65, 0.5f);
					}

				}

				isCameraLocked = false;

			} else if (Input.GetTouch (0).phase == TouchPhase.Ended)
			{
				isCameraLocked = true;
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
