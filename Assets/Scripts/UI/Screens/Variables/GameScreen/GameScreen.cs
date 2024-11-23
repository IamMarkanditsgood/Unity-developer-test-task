using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class GameScreen : BasicScreen
{
    [SerializeField] private CoinsTextManager _coinsTextManager;
    [SerializeField] private GameScreenViewManager _gameScreenViewManager;
    [SerializeField] private GameScreenButtonsManager _gameScreenButtonsManager;

    [SerializeField] private float _timeBeforeWordCheck = 1f;
    [SerializeField] private int _coinsReward;
    private List<LevelData> _levels = new List<LevelData>();
    private LevelData _currentLevel = new LevelData();
    private GamePlayManager _gamePlayManager = new GamePlayManager();

    private List<char> _levelWord = new List<char>();
    private int _hints = 10;
    private float _timer = 0;

    private void Start()
    {
        Subscribe();
        _coinsTextManager.Initialize();
    }

    private void OnDestroy()
    {
        UnSubscribe();
        _coinsTextManager.Cleanup();    
    }

    private void Subscribe()
    {
        _gameScreenButtonsManager.Subscribe();

        _gameScreenButtonsManager.OnHintPressed += OpenHintPopup;
        _gameScreenButtonsManager.OnFullErase += Erase;
        _gameScreenButtonsManager.OnLetterErase += EraseLastLetter;
        _gameScreenButtonsManager.OnLetterPressed += LetterPressed;
    }
    private void UnSubscribe()
    {
        _gameScreenButtonsManager.Unsubscribe();

        _gameScreenButtonsManager.OnHintPressed -= OpenHintPopup;
        _gameScreenButtonsManager.OnFullErase -= Erase;
        _gameScreenButtonsManager.OnLetterErase -= EraseLastLetter;
        _gameScreenButtonsManager.OnLetterPressed -= LetterPressed;
    }
    public override void Hide()
    {
        base.Hide();
        CleanScene();
    }

    public void StartLevel(int levelIndex)
    {
        _levels = SaveManager.LoadLevelList();
        _currentLevel = _levels[levelIndex];
        CleanScene();
        SetScene();
        StartCoroutine(TimerCounter());
    }

    private void SetScene()
    {
        _levelWord = _currentLevel.levelWord.ToList();
        _hints = ResourcesManager.Instance.GetResource(ResourceTypes.Hints);
        _timer = _currentLevel.levelTime;

        _gameScreenViewManager.SetText(_hints, _currentLevel);
        _gameScreenViewManager.UpdateTimer(_timer);
        _gameScreenViewManager.SetTypedWords(_currentLevel);

        _gameScreenButtonsManager.SetButtons(_levelWord);
    }
    private void CleanScene()
    {
        StopAllCoroutines();
        _gameScreenViewManager.CleanScene();
        _gameScreenButtonsManager.CleanScene();
        _gamePlayManager.Clean();
    }


    private void Erase()
    {
        StopAllCoroutines();
        _gamePlayManager.EraseWord();
        _gameScreenViewManager.EraseTypedWord();
        
    }
    private void EraseLastLetter()
    {
        _gamePlayManager.EraseLastLetter();
        _gameScreenViewManager.EraseLastLetter();
    }

    private void WordFound()
    {
        _levels = SaveManager.LoadLevelList();

        for(int i = 0; i < _levels.Count; i++)
        {
            if (_levels[i].levelWord == _currentLevel.levelWord)
            {
                _currentLevel = _levels[i];
            }
        }
        ResourcesManager.Instance.ModifyResource(ResourceTypes.Coins,_coinsReward);
        _gamePlayManager.Clean();
        GameEvents.WordFound(_levels);
        SetScene();
    }
    private void WordNotFound()
    {
        _gamePlayManager.Clean();
        SetScene();
    }

    private void LetterPressed(int letterIndex)
    {
        StopAllCoroutines();

        _gamePlayManager.TypeLetter(_levelWord[letterIndex]);
        _gameScreenViewManager.AddTypedLetter(_levelWord[letterIndex]);

        StartCoroutine(CheckWord());
    }

    private void OpenHintPopup()
    {
        UIEvents.OpenHint(_currentLevel);
    }

    private IEnumerator CheckWord()
    {
        yield return new WaitForSeconds(_timeBeforeWordCheck);
     
        if (_gamePlayManager.IsWordPresent(_currentLevel.levelWord))
        {
            WordFound();
        }
        else
        {
            WordNotFound();
        }
       
        StopAllCoroutines();
    }

    private IEnumerator TimerCounter()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);
            _timer++;
            // Do levelUpdater that will update all data of Levels
            _currentLevel.levelTime = _timer;
            for(int i = 0; i < _levels.Count; i++)
            {
                if (_levels[i].levelWord == _currentLevel.levelWord)
                {
                    _levels[i] = _currentLevel;
                }
            }
            SaveManager.UpdateLevelListSaves(_levels);
            _gameScreenViewManager.UpdateTimer(_timer);
        }
    }
}
