using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BindingButtonUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private GameInput.Binding _binding;
    [SerializeField] private int _height = 50;

    private Button _button;
    private RectTransform _rectTransform;

    public GameInput.Binding Binding => _binding;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _rectTransform = GetComponent<RectTransform>();
    }

    public void UpdateText()
    {
        var text = GameInput.Instance.GetBindingText(_binding).ToUpper();

        if (text == "ESCAPE")
        {
            text = "ESC";
        }

        _text.text = text;

        UpdateWidthByText();
    }

    public void AddListener(UnityEngine.Events.UnityAction action)
    {
        if (_button != null)
        {
            _button.onClick.AddListener(action);
        }
    }

    public void UpdateWidthByText()
    {
        if (_text.text == "SPACE")
        {
            _rectTransform.sizeDelta = new Vector2(200, _height);
        }
        else if (_text.text.Length == 3)
        {
            _rectTransform.sizeDelta = new Vector2(75, _height);
        }
        else if ((_text.text.Length == 1))
        {
            _rectTransform.sizeDelta = new Vector2(_height, _height);
        }
        else
        {
            var width = _text.preferredWidth + 40;
            _rectTransform.sizeDelta = new Vector2(width, _height);
        }
    }
}
