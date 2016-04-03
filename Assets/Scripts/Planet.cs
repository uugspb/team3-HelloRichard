using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.EventSystems;

public class Planet : MonoBehaviour
{
    public GameObject DefaultSelector;

    bool isFocused = false;

    public CanvasGroup canvas;

    public bool IsFocused
    {
        get
        {
            return isFocused;
        }
    }

    public void Start()
    {
        canvas.alpha = 0;
        canvas.interactable = false;
    }

    public void Focus()
    {
        isFocused = true;
        transform.DOKill();
        transform.DOScale(1.1f * Vector3.one, 0.5f);

        canvas.DOKill();
        canvas.DOFade(1f, 0.5f);
        canvas.interactable = true;

        EventSystem.current.SetSelectedGameObject(DefaultSelector);
    }

    public void Unfocus()
    {
        isFocused = false;
        transform.DOKill();
        transform.DOScale(1.0f * Vector3.one, 0.5f);

        canvas.DOKill();
        canvas.DOFade(0f, 0.5f);
        canvas.interactable = false;
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

	Quaternion initialSubEuler = Quaternion.identity;
	Vector3 initialEuler = Vector3.zero;

	void Update () 
	{

		if (IsFocused && Controller.targeting)
		{
			if (initialSubEuler == Vector3.zero)
			{
				initialSubEuler = InputRemoute.InputPacket.Gyroscope.Attitude.eulerAngles;
				initialEuler = transform.eulerAngles;
			}

			transform.eulerAngles = initialEuler + InputRemoute.InputPacket.Gyroscope.Attitude.eulerAngles - initialSubEuler;
		}
		else if(initialSubEuler != Vector3.zero)
			initialSubEuler = Vector3.zero;

//		if (IsFocused && Controller.targeting)
//		{
//			if (initialSubEuler == Quaternion.identity)
//			{
//				initialSubEuler = InputRemoute.InputPacket.Gyroscope.Attitude;
//				initialEuler = transform.eulerAngles;
//			}
//
//			transform.localRotation = Quaternion.Inverse(InputRemoute.InputPacket.Gyroscope.Attitude) * initialSubEuler;
//		}
//		else if(initialSubEuler != Quaternion.identity)
//			initialSubEuler = Quaternion.identity;
	}

}

/*
 * if (IsFocused && Controller.IsTargeting)
		{
			if (initialSubEuler == Vector3.zero)
			{
				initialSubEuler = InputRemoute.InputPacket.Gyroscope.Attitude.eulerAngles;
				initialEuler = transform.eulerAngles;
			}

			transform.eulerAngles = initialEuler + InputRemoute.InputPacket.Gyroscope.Attitude.eulerAngles - initialSubEuler;
		}
		else if(initialSubEuler != Vector3.zero)
			initialSubEuler = Vector3.zero;
			*/
