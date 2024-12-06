using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;
using UI;

namespace Wheel
{
    public class WheelManager : MonoBehaviour
    {
        private enum WheelType
        {
            Bronze,
            Silver,
            Gold
        }
        [Header("Arrays")]

        [SerializeField] private GameObject[] slots;
        [SerializeField] private SO_Item[] allItems;
        [SerializeField] private SO_Bomb bomb;

        private int zone;

        private WheelType _wheelType;
        [Header("Wheel")]

        [SerializeField] private GameObject wheelGameObject;
        [SerializeField] private GameObject indicatorGameObject;
        [SerializeField] private Sprite bronzeWheel;
        [SerializeField] private Sprite bronzeIndicator;
        [SerializeField] private Sprite silverWheel;
        [SerializeField] private Sprite silverIndicator;
        [SerializeField] private Sprite goldWheel;
        [SerializeField] private Sprite goldIndicator;

        private void OnEnable()
        {
            ButtonManager.OnSpinButtonClicked += SetSlots;
        }

        private void OnDisable()
        {
            ButtonManager.OnSpinButtonClicked -= SetSlots;
        }

        private void Start()
        {
            SetSlots();
        }

        private void SetSlots()
        {
            SetWheel();
            List<SO_Item> choosableItems = CreateChoosableItems();
            foreach (var slot in slots)
            {
                int randomIndex = Random.Range(0, choosableItems.Count);
                slot.GetComponent<Slot>()._Slot = choosableItems[randomIndex];
                slot.GetComponent<Slot>().SetSlot();
                choosableItems.RemoveAt(randomIndex);
            }

            if (_wheelType == WheelType.Bronze)
            {
                int randomBombSlot = Random.Range(0, 8);
                slots[randomBombSlot].GetComponent<Slot>()._Slot = bomb;
                slots[randomBombSlot].GetComponent<Slot>().SetSlot();
            }
        }

        private List<SO_Item> CreateChoosableItems()
        {
            List<SO_Item> choosableItems = new List<SO_Item>();
            choosableItems.Clear();
            foreach (var item in allItems)
            {
                if (_wheelType == WheelType.Gold)
                {
                    if (item.Level == 4)
                        choosableItems.Add(item);
                }
                else if (GameManager.Instance.Level() >= item.Level)
                {
                    choosableItems.Add(item);
                }
            }
            return choosableItems;
        }

        private void SetWheel()
        {
            if (GameManager.Instance.Zone % 30 == 0)
            {
                _wheelType = WheelType.Gold;
            }
            else if (GameManager.Instance.Zone % 5 == 0)
            {
                _wheelType = WheelType.Silver;
            }
            else
            {
                _wheelType = WheelType.Bronze;
            }


            switch (_wheelType)
            {
                case WheelType.Bronze:
                    wheelGameObject.GetComponent<Image>().sprite = bronzeWheel;
                    indicatorGameObject.GetComponent<Image>().sprite = bronzeIndicator;
                    break;
                case WheelType.Silver:
                    wheelGameObject.GetComponent<Image>().sprite = silverWheel;
                    indicatorGameObject.GetComponent<Image>().sprite = silverIndicator;
                    break;
                case WheelType.Gold:
                    wheelGameObject.GetComponent<Image>().sprite = goldWheel;
                    indicatorGameObject.GetComponent<Image>().sprite = goldIndicator;
                    break;
            }
        }
    }
}
