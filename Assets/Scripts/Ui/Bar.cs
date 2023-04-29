using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public abstract class Bar : MonoBehaviour
{
   [SerializeField] private Slider _slider;

   private void Start()
   {
      Subscribe(UpdateSlider);
   }

   private void OnDisable()
   {
      Describe(UpdateSlider);
   }

   protected abstract void Subscribe(Action<int, int> action);

   protected abstract void Describe(Action<int, int> action);

   private void UpdateSlider(int total, int Cap)
   {
      _slider.value = (float)total / Cap;
   }
}
