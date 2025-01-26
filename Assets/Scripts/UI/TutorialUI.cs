using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour {

    [SerializeField] private List<BindingButtonUI> _bindingButtons;

    [SerializeField] private TextMeshProUGUI keyMoveUpText;
    [SerializeField] private TextMeshProUGUI keyMoveDownText;
    [SerializeField] private TextMeshProUGUI keyMoveLeftText;
    [SerializeField] private TextMeshProUGUI keyMoveRightText;
    [SerializeField] private TextMeshProUGUI keyInteractText;
    [SerializeField] private TextMeshProUGUI keyInteractAlternateText;
    [SerializeField] private TextMeshProUGUI keyPauseText;

    private void Start() {
        GameInput.Instance.OnBindingRebound += GameInput_OnBindingRebound;
        KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;
        UpdateVisual();

        Show();
    }

    private void KitchenGameManager_OnStateChanged(object sender, EventArgs e)
    {
        if (KitchenGameManager.Instance.IsCountdownToStartActive())
        {
            Hide();
        }
    }

    private void GameInput_OnBindingRebound(object sender, System.EventArgs e) {
        UpdateVisual();
    }

    private void UpdateVisual() {
        //keyMoveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        //keyMoveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        //keyMoveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        //keyMoveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        //keyInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        //keyInteractAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate);
        //keyPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);

        foreach (var button in _bindingButtons)
        {
            button.UpdateText();
        }
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
