using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SendMailObject : MonoBehaviour
{
    public UnityAction mouseDownAction;
    void Start()
    {

    }

    void Update()
    {

    }

    private void OnMouseDown()
    {
        mouseDownAction?.Invoke();
    }
}
