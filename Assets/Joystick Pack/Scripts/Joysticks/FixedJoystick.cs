using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class FixedJoystick : Joystick
{
     public override void OnPointerDown(PointerEventData eventData)
    {
        karakter.IsPointerDown = false;
        anakarakter.IsPointerDown = false;
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        karakter.IsPointerDown = true;
        anakarakter.IsPointerDown = true;
        base.OnPointerUp(eventData);
    }

    

}