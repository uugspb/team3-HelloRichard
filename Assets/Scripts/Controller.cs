using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Controller : MonoBehaviour 
{

	Planet focusedPlanet;

	// Use this for initialization
	void Start () {
	
	}
	

	void Update () 
	{
		Focus ();


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



					foreach (var camera in GetComponentsInChildren<Camera>())
					{
						camera.DOKill ();
						camera.DOFieldOfView (65, 0.5f);
					}
				}
			}
		} 
		else
		{
			if (focusedPlanet != null)
			{
				foreach (var camera in GetComponentsInChildren<Camera>())
				{
					camera.DOKill ();
					camera.DOFieldOfView (90, 0.5f);
				}

				focusedPlanet.Unfocus ();
				focusedPlanet = null;
			}
		}
	}

}
