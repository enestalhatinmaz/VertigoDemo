using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ZoneCounterManager : MonoBehaviour
    {
        [SerializeField] private GameObject zonePrefab;
        [SerializeField] private RectTransform contentTransform;

        private float contentCurrentXPosition = 0f;

        private void OnEnable()
        {
            ChoosenUIManager.OnSpinCompleted += UpdateZone;
        }

        private void OnDisable()
        {
            ChoosenUIManager.OnSpinCompleted -= UpdateZone;
        }

        private void Start()
        {
            for (int i = 1; i < GameManager.Instance.MaxZone + 1; i++)
            {
                GameObject zoneObj = Instantiate(zonePrefab, contentTransform);
                zoneObj.GetComponentInChildren<TextMeshProUGUI>().text = i.ToString();
                if (i % 30 == 0)
                {
                    zoneObj.GetComponentInChildren<TextMeshProUGUI>().color = Color.yellow;
                }
                else if (i % 5 == 0)
                {
                    zoneObj.GetComponentInChildren<TextMeshProUGUI>().color = Color.green;
                }
            }

            contentTransform.anchoredPosition = new Vector3((GameManager.Instance.Zone - 1) * -90, 0, 0);
        }
        private void UpdateZone(bool isBomb)
        {
            if (!isBomb && GameManager.Instance.Zone != GameManager.Instance.MaxZone)
            {
                int currentZone = GameManager.Instance.Zone;
                contentCurrentXPosition = ((currentZone - 1) * -90) - 90f;

                contentTransform
                    .DOAnchorPosX(contentCurrentXPosition, 1f)
                            .SetEase(Ease.OutCubic);
            }
        }
    }
}
