using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Wheel;

namespace UI
{
    public class ChoosenUIManager : MonoBehaviour
    {
        [SerializeField] private GameObject wheelSlotsParent;
        [SerializeField] private SO_Slot choosedItemSO;
        [SerializeField] private GameObject collectedItemPrefab;
        [SerializeField] private GameObject collectedPanelContent;
        [SerializeField] private GameObject bombPanel;
        public static Action<bool> OnSpinCompleted; //true if bomb is selected

        private void OnEnable()
        {
            SpinManager.OnItemChoosed += UpdateChoosenUIList;
        }

        private void OnDisable()
        {
            SpinManager.OnItemChoosed -= UpdateChoosenUIList;
        }

        private void UpdateChoosenUIList(int choosenId)
        {
            choosedItemSO = wheelSlotsParent.transform.GetChild(choosenId).transform.GetChild(0).GetComponent<Slot>()._Slot;
            var choosedItem = wheelSlotsParent.transform.GetChild(choosenId).GetComponent<RectTransform>();
            var choosedItemText = choosedItem.GetChild(1).GetComponent<TextMeshProUGUI>();

            choosedItem.GetChild(0)
                .DOScale(new Vector3(0.5f, 0.5f, 0.5f), 1f)
                .OnComplete(() => { choosedItem.GetChild(0).DOScale(new Vector3(1f, 1f, 1f), 1f); });

            if (choosedItemSO is SO_Bomb)
            {
                OnSpinCompleted?.Invoke(true);
            }
            else
            {
                bool hasSameItem = false;
                OnSpinCompleted?.Invoke(false);
                foreach (Transform child in collectedPanelContent.transform)
                {
                    if (child != null && child.GetComponent<ChoosenItem>().item == choosedItemSO)
                    {
                        child.GetComponent<ChoosenItem>().SetValues();
                        hasSameItem = true;
                        break;
                    }
                }
                if (!hasSameItem)
                {
                    GameObject newItem = Instantiate(collectedItemPrefab, collectedPanelContent.transform);
                    newItem.GetComponent<ChoosenItem>().item = choosedItemSO;
                }
            }
        }
    }
}
