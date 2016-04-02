using UnityEngine;
using System.Collections;

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
