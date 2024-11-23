using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameScreen : BasicScreen
{
    [SerializeField] private CoinsTextManager _coinsTextManager;
    [SerializeField] private GameScreenViewManager _gameScreenViewManager;
    [SerializeField] private GameScreenButtonsManager _gameScreenButtonsManager;

    [SerializeField] private float _timeBeforeWordCheck = 1f;
    [SerializeField] private int _coinsReward;

    private GamePlayManager _gamePlayManager = new GamePlayManager();
    private LevelData _currentLevel = new LevelData();
    
    private Coroutine _prevCheckWordCoroutine;
    private List<char> _levelWord = new List<char>();
    private int _currentLevelIndex;
    private int _hints = 10;
    private float _timer = 0;

    private const int _timerSecond = 1;

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

        ResourceEvents.OnResourceModified += _gameScreenViewManager.ModifyHintsText;
    }

    private void UnSubscribe()
    {
        _gameScreenButtonsManager.Unsubscribe();

        _gameScreenButtonsManager.OnHintPressed -= OpenHintPopup;
        _gameScreenButtonsManager.OnFullErase -= Erase;
        _gameScreenButtonsManager.OnLetterErase -= EraseLastLetter;
        _gameScreenButtonsManager.OnLetterPressed -= LetterPressed;

        ResourceEvents.OnResourceModified -= _gameScreenViewManager.ModifyHintsText;
    }

    public override void Show()
    {
        _coinsTextManager.SetCoinsText();
        base.Show();
    }

    public override void Hide()
    {
        base.Hide();
        CleanScene();
    }

    public void StartLevel(int levelIndex)
    {
        _currentLevelIndex = levelIndex;
        
        List<LevelData> _levels = SaveManager.LoadLevelList();
        _currentLevel = _levels[_currentLevelIndex];

        CleanScene();
        SetScene();

        if (_currentLevel.levelWords.Count > _currentLevel.foundWords.Count)
        {
            StartCoroutine(TimerCounter());
        }
    }

    private void CleanScene()
    {
        StopAllCoroutines();

        _gameScreenViewManager.CleanScene();
        _gameScreenButtonsManager.CleanScene();
        _gamePlayManager.Clean();
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

    private void Erase()
    {
        StopSomeCoroutine(_prevCheckWordCoroutine);
        _gamePlayManager.EraseWord();
        _gameScreenViewManager.EraseTypedWord();
        
    }

    private void EraseLastLetter()
    {
        _gamePlayManager.EraseLastLetter();
        _gameScreenViewManager.EraseLastLetter();
    }

    private void OpenHintPopup()
    {
        UIEvents.OpenHint(_currentLevel);
    }

    private void LetterPressed(int letterIndex)
    {
        StopSomeCoroutine(_prevCheckWordCoroutine);

        _gamePlayManager.TypeLetter(_levelWord[letterIndex]);
        _gameScreenViewManager.AddTypedLetter(_levelWord[letterIndex]);

        _prevCheckWordCoroutine = StartCoroutine(CheckWord());
    }

    private void WordFound()
    {
        _currentLevel = LevelDataManager.GetCurrentLevelData(_currentLevel.levelWord);
        ResourcesManager.Instance.ModifyResource(ResourceTypes.Coins,_coinsReward);

        _gamePlayManager.Clean();

        GameEvents.WordFound(LevelDataManager.Levels);

        CheckLevelProgress();
    }

    private void WordNotFound()
    {
        _gamePlayManager.Clean();
        SetScene();
    }

    private void CheckLevelProgress()
    {
        
        if (_currentLevel.levelWords.Count == _currentLevel.foundWords.Count)
        {
            StopCoroutine(TimerCounter());
            int nextLevel = _currentLevelIndex + 1;
            StartLevel(nextLevel);
        }
        else
        {
            SetScene();
        }
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
    }

    private IEnumerator TimerCounter()
    {
        while(true)
        {
            yield return new WaitForSeconds(_timerSecond);

            _timer++;
            LevelDataManager.UpdateTime(_currentLevel, _timer);

            _gameScreenViewManager.UpdateTimer(_timer);
        }
    }

    private void StopSomeCoroutine(Coroutine coroutine)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
    }
}