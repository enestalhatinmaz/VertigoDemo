using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UI;

namespace Wheel
{
    public class SpinManager : MonoBehaviour
    {
        [SerializeField] private Transform wheel;
        [SerializeField] private int currentSlot;
        
        public static Action<int> OnItemChoosed;

        private void OnEnable()
        {
            ButtonManager.OnSpinButtonClicked += SpinButtonClicked;
        }

        private void OnDisable()
        {
            ButtonManager.OnSpinButtonClicked -= SpinButtonClicked;
        }

        private void SpinButtonClicked()
        {
            int randomSlot = UnityEngine.Random.Range(0, 8);
            SpinWheel(randomSlot);
        }

        public void SpinWheel(int targetSlot)
        {
            float targetAngle = targetSlot * 45;
            float totalRotation = targetAngle + (3 * 360);

            wheel
                .DORotate(new Vector3(0, 0, totalRotation), 6f, RotateMode.FastBeyond360)
                .OnComplete(() =>
                {
                    currentSlot = targetSlot;                   
                    OnItemChoosed?.Invoke(currentSlot);
                });
        }
    }
}
