using UnityEngine;
using System.Collections;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public void OnEnable()
    {
        Controller.LeftSwipeEvent += SelectLeft;
        Controller.RightSwipeEvent += SelectRight;
    }

    public void OnDisable()
    {
        Controller.LeftSwipeEvent -= SelectLeft;
        Controller.RightSwipeEvent -= SelectRight;
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


}
