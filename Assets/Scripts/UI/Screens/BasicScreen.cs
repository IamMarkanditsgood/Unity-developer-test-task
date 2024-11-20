using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class BasicScreen : MonoBehaviour
{
    [SerializeField] private GameObject _screenView;

    public virtual void Show()
    {
        _screenView.SetActive(true);
    }

    public virtual void Hide() 
    {
        _screenView.SetActive(false);
    }
}