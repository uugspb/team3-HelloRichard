using UnityEngine;
using System.Collections;
using DG.Tweening;


public class Planet : MonoBehaviour {

	bool isFocused = false;

	public bool IsFocused
	{
		get
		{
			return isFocused;
		}
	}

	public void Focus()
	{
		isFocused = true;
        transform.DOKill();
        transform.DOScale(1.1f * Vector3.one, 0.5f);
    }

	public void Unfocus()
	{
		isFocused = false;
        transform.DOKill();
        transform.DOScale(1.0f * Vector3.one, 0.5f);
    }

    static string GetCoordinate(int index)
	{
		switch(index)
		{
		case 0:
			return "x"; break;
		case 1:
			return "y"; break;
		case 2:
			return "z"; break;
		default:
			return "w"; break;
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
