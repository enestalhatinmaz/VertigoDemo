using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UI;
using UnityEngine;
using Wheel;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private int zone;
    [SerializeField] private int maxZone;

    [Header("Take Items")]
    [SerializeField] private RectTransform collectedPanel;
    [SerializeField] private GameObject takeItemsPanel;
    [SerializeField] private GameObject exitButtonGameObject;
    public int Zone { get { return zone; } }
    public int MaxZone { get { return maxZone; } }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        Application.targetFrameRate = 144;
    }

    private void Start()
    {
        DOTween.Clear();
    }

    private void OnEnable()
    {
        ChoosenUIManager.OnSpinCompleted += SetZone;
    }

    private void OnDisable()
    {
        ChoosenUIManager.OnSpinCompleted -= SetZone;
    }

    private void SetZone(bool isBomb)
    {
        if (!isBomb)
        {
            if(zone == maxZone)
                TakeAllItems();
            zone++;
        }
    }

    public int Level()
    {
        return Zone switch
        {
            < 20 => 1,
            < 40 => 2,
            < 60 => 3,
            _ => 1
        };
    }

    public void TakeAllItems()
    {
        exitButtonGameObject.SetActive(false);
        takeItemsPanel.SetActive(true);

        collectedPanel
            .DOSizeDelta(new Vector2(260, 260), 1)
            .OnComplete(() =>
            {
                collectedPanel.DOSizeDelta(new Vector2(1800, 400), 1);
            });


        collectedPanel
            .DOAnchorPos3D(new Vector3(190, 190, 0), 1)
            .OnComplete(() =>
            {
                collectedPanel.DOAnchorPos3D(new Vector2(0, 150), 1);
                collectedPanel.DOAnchorMin(new Vector2(0.5f, 0.5f), 1f);
                collectedPanel.DOAnchorMax(new Vector2(0.5f, 0.5f), 1f);
            });
    }
}
