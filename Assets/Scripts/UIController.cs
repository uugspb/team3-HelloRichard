using UnityEngine;
using System.Collections;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class UIController : MonoBehaviour
{
    public void OnEnable()
    {
        Controller.LeftSwipeEvent += SelectLeft;
        Controller.RightSwipeEvent += SelectRight;
        Controller.TapEvent += Tap;
    }

    public void OnDisable()
    {
        Controller.LeftSwipeEvent -= SelectLeft;
        Controller.RightSwipeEvent -= SelectRight;
        Controller.TapEvent -= Tap;
    }


    private void SelectLeft()
    {
        GameObject current = EventSystem.current.currentSelectedGameObject;
        if (current != null)
        {
            Selectable left = current.GetComponent<Selectable>().FindSelectableOnLeft();
            if (left != null)
            {
                EventSystem.current.SetSelectedGameObject(left.gameObject);
            }
        }
    }

    private void SelectRight()
    {
        GameObject current = EventSystem.current.currentSelectedGameObject;
        if (current != null)
        {
            Selectable left = current.GetComponent<Selectable>().FindSelectableOnLeft();
            if (left != null)
            {
                EventSystem.current.SetSelectedGameObject(left.gameObject);
            }
        }
    }

    private void Tap()
    {
        GameObject current = EventSystem.current.currentSelectedGameObject;
        var pointer = new PointerEventData(EventSystem.current);
        if (current != null)
            ExecuteEvents.Execute(current, pointer, ExecuteEvents.submitHandler);
    }

    public void SelectPlanet()
    {
        Controller.targeting = !Controller.targeting;

        if (Controller.targeting)
        {
            foreach (var camera in GetComponentsInChildren<Camera>())
            {
                camera.DOKill();
                camera.DOFieldOfView(65, 0.5f);
            }
        }
        else
        {
            foreach (var camera in GetComponentsInChildren<Camera>())
            {
                camera.DOKill();
                camera.DOFieldOfView(90, 0.5f);
            }
        }

    }

    public void RotateSun()
    {
        Controller.EnableSunRotation = !Controller.EnableSunRotation;
    }

    public void Meteor()
    {

    }
}
