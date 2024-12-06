using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Wheel;
using DG.Tweening;
using UnityEngine.SceneManagement;

namespace UI
{

    public class ButtonManager : MonoBehaviour
    {
        [Header("Main Game Buttons")]
        [SerializeField] private Button spinButton;
        [SerializeField] private Button exitButton;

        [Header("Bomb Panel Buttons")]
        [SerializeField] private Button giveUpButton;
        [SerializeField] private Button reviveButton;

        [Header("Exit Panel Buttons")]
        [SerializeField] private Button collectRewardsButton;
        [SerializeField] private Button goBackButton;

        [Header("Take Items Panel Buttons")]
        [SerializeField] private Button continueButton;

        [Header("Panels")]
        [SerializeField] private GameObject bombPanel;
        [SerializeField] private GameObject exitPanel;

        public static Action OnSpinButtonClicked;

        private void OnEnable()
        {
            ChoosenUIManager.OnSpinCompleted += OnSpinCompleted;
        }

        private void OnDisable()
        {
            ChoosenUIManager.OnSpinCompleted -= OnSpinCompleted;
        }

        private void Start()
        {
            SetupButton(spinButton, SpinButton);
            SetupButton(giveUpButton, GiveUpButton);
            SetupButton(reviveButton, RevieveButton);
            SetupButton(exitButton, ExitButton);
            SetupButton(collectRewardsButton, CollectRewardsButton);
            SetupButton(goBackButton, GoBackButton);
            SetupButton(continueButton, ContinueButton);
        }

        private void OnDestroy()
        {
            RemoveButtonListeners(spinButton, SpinButton);
            RemoveButtonListeners(giveUpButton, GiveUpButton);
            RemoveButtonListeners(reviveButton, RevieveButton);
            RemoveButtonListeners(exitButton, ExitButton);
            RemoveButtonListeners(collectRewardsButton, CollectRewardsButton);
            RemoveButtonListeners(goBackButton, GoBackButton);
            RemoveButtonListeners(continueButton, ContinueButton);
        }

        private void SpinButton()
        {
            OnSpinButtonClicked?.Invoke();
            spinButton.interactable = false;
            spinButton.GetComponent<Image>().DOFade(0f, 2f);
            exitButton.interactable = false;
        }

        private void OnSpinCompleted(bool isBomb)
        {
            if (!isBomb)
            {
                spinButton.GetComponent<Image>().DOFade(1, 2f)
                    .OnComplete(() => spinButton.interactable = true);

                exitButton.interactable = true;
            }
            else
            {
                bombPanel.SetActive(true);
                reviveButton.interactable = false;
                giveUpButton.interactable = false;
                bombPanel.transform
                    .DOScale(Vector3.one, 2f).SetEase(Ease.OutBack)
                    .OnComplete(() =>
                    {
                        reviveButton.interactable = true;
                        giveUpButton.interactable = true;
                    });
            }
        }

        private void GiveUpButton()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void ContinueButton()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void RevieveButton()
        {
            reviveButton.interactable = false;
            giveUpButton.interactable = false;

            bombPanel.transform
                .DOScale(Vector3.zero, 2f).SetEase(Ease.InBack)
                .OnComplete(() =>
                {
                    bombPanel.SetActive(false);
                    spinButton.GetComponent<Image>().DOFade(1, 2f)
                    .OnComplete(() =>
                    {
                        spinButton.interactable = true;
                        exitButton.interactable = true;
                        reviveButton.interactable = true;
                        giveUpButton.interactable = true;
                    });
                });
        }

        private void ExitButton()
        {
            exitPanel.SetActive(true);
        }

        private void CollectRewardsButton()
        {
            exitPanel.SetActive(false);
            spinButton.gameObject.SetActive(false);

            GameManager.Instance.TakeAllItems();
        }

        private void GoBackButton()
        {
            exitPanel.SetActive(false);
        }

        private void OnValidate()
        {
            spinButton = FindButton("ui_wheel_button");
            giveUpButton = FindButton("ui_bomb_panel_button_giveup");
            reviveButton = FindButton("ui_bomb_panel_button_revive");
            exitButton = FindButton("ui_collected_panel_exitButton");
            collectRewardsButton = FindButton("ui_exit_panel_button_collect_rewards");
            goBackButton = FindButton("ui_exit_panel_button_go_back");
            continueButton = FindButton("ui_take_items_continue_button");
        }

        private void SetupButton(Button button, Action onClickAction)
        {
            if (button != null)
            {
                button.onClick.AddListener(() => onClickAction?.Invoke());
            }
        }

        private void RemoveButtonListeners(Button button, Action onClickAction)
        {
            if (button != null)
            {
                button.onClick.RemoveListener(() => onClickAction?.Invoke());
            }
        }

        private Button FindButton(string name)
        {
            GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>(true);

            foreach (GameObject obj in allObjects)
            {
                if (obj.name == name)
                {
                    Button button = obj.GetComponent<Button>();
                    if (button != null)
                    {
                        return button;
                    }
                }
            }
            return null;
        }
    }
}
