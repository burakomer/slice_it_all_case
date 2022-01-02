using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerController _player;
    [SerializeField] private GameplayInputController _input;
    [SerializeField] private UIController _ui;
    [Space]
    [SerializeField] private Transform _levelSpawnPoint;
    private Transform _playerStartPoint;
    private PlayerTriggerController _finishLine;
    private List<PlayerTriggerController> _gameOverTriggers;

    [Header("Settings")]
    [SerializeField] private GameSettingsAsset _gameSettings;

    private int _currentLevelIndex;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        _currentLevelIndex = PlayerPrefs.GetInt(SaveKeys.CurrentLevel, 0);
    }

    private void Start()
    {
        // Choose a level and instantiate it.
        var currentLevelPrefab = _gameSettings.levels.GetElementLooping(_currentLevelIndex);
        Instantiate(currentLevelPrefab, _levelSpawnPoint.position, Quaternion.identity);

        var playerTriggers = GameObject.FindObjectsOfType<PlayerTriggerController>();
        _finishLine = playerTriggers.Where((trigger) => trigger.gameObject.CompareTag(GameTags.Finish)).First();
        _gameOverTriggers = playerTriggers.Where((trigger) => trigger.gameObject.CompareTag(GameTags.GameOver)).ToList();

        _finishLine.Triggered += () => StartCoroutine(FinishLevel(isGameOver: false));
        foreach (var gameOverTrigger in _gameOverTriggers)
        {
            gameOverTrigger.Triggered += () => StartCoroutine(FinishLevel(isGameOver: true));
        }

        // Place player on start point
        _playerStartPoint = GameObject.FindGameObjectWithTag(GameTags.PlayerStartPoint).transform;
        _player.transform.position = _playerStartPoint.transform.position;

        // Set up touch input.
        _input.Tapped += _player.OnInputTapped;

        // Set up UI.
        _ui.ShowGameplay(levelText: $"Level {_currentLevelIndex + 1}");
    }

    private IEnumerator FinishLevel(bool isGameOver)
    {
        _input.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        _ui.ShowFinish(isGameOver);

        if (isGameOver) yield break;
        PlayerPrefs.SetInt(SaveKeys.CurrentLevel, _currentLevelIndex + 1);
    }
}
