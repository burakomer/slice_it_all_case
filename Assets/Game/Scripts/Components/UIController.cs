using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _gameplayUi;
    [SerializeField] private GameObject _finishUi;
    [SerializeField] private GameObject _gameOverUi;
    [Space]
    [SerializeField] private TextMeshProUGUI _levelTmp;

    private void Awake()
    {
        _gameplayUi.SetActive(false);
        _finishUi.SetActive(false);
        _gameOverUi.SetActive(false);
    }

    public void ShowGameplay(string levelText = null)
    {
        _gameplayUi.SetActive(true);
        if (levelText != null) _levelTmp.text = levelText;
    }

    public void ShowFinish(bool isGameOver)
    {
        _gameplayUi.SetActive(false);

        _finishUi.SetActive(!isGameOver);
        _gameOverUi.SetActive(isGameOver);
    }
}