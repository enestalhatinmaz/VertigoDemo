using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Wheel
{
    public class Slot : MonoBehaviour
    {
        public SO_Slot _Slot;
        private TextMeshProUGUI amountText;
        public int amount;

        private void Awake()
        {
            amountText = transform.parent.GetComponentInChildren<TextMeshProUGUI>();    
        }

        public void SetSlot()
        {
            GetComponent<Image>().sprite = _Slot.Sprite;
            if (_Slot is SO_Item so_item)
            {
                amount = so_item.DefaultAmount * GameManager.Instance.Zone;
                amountText.text = amount.ToString();
            }
            else 
            {
                amountText.text = "";
            }
        }
    }
}
