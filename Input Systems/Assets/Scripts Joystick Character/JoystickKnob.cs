using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickKnob : MonoBehaviour
{
   public RectTransform joystickObj;
   public RectTransform Knob;

   private void Awake()
   {
      joystickObj = GetComponent<RectTransform>();
   }
}
