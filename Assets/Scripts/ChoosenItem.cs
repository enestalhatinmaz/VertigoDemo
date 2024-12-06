using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Wheel;

public class ChoosenItem : MonoBehaviour
{
    public SO_Slot item;
    public TextMeshProUGUI amountText;
    public int amount;

    private void Awake()
    {
        amountText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        SetValues();
    }

    public void SetValues()
    {
        transform.GetChild(0).GetComponent<Image>().sprite = item.Sprite;
        if (item is SO_Item so_item)
        {
            amount += so_item.DefaultAmount * (GameManager.Instance.Zone - 1);
            AnimateText(amountText, int.Parse(amountText.text), amount, 1f);
        }
    }

    private void AnimateText(TextMeshProUGUI text, int startValue, int endValue, float duration)
    {
        StartCoroutine(AnimateNumberCoroutine(text, startValue, endValue, duration));
    }

    private IEnumerator AnimateNumberCoroutine(TextMeshProUGUI amountText, int startValue, int endValue, float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            int currentValue = Mathf.RoundToInt(Mathf.Lerp(startValue, endValue, t));
            amountText.text = currentValue.ToString();

            yield return null;
        }

        amountText.text = endValue.ToString();
    }
}
