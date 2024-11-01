using TMPro;
using UnityEngine;

public class ScoreViewerUI : MonoBehaviour
{
    [SerializeField] private ScoreCounter _counter;
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;

    private void Awake()
    {
        _textMeshProUGUI.text = $"Score: {_counter.DefaultScore}";
    }

    private void OnEnable()
    {
        _counter.ScoreChanged += OnValueChanged;
    }

    private void OnDisable()
    {
        _counter.ScoreChanged -= OnValueChanged;
    }

    private void OnValueChanged()
    {
        _textMeshProUGUI.text = $"Score: {_counter.Score}";
    }
}