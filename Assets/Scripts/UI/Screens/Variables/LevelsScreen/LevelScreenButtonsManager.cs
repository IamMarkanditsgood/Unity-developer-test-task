using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelScreenButtonsManager
{
    private List<GameObject> _levelButtons = new List<GameObject>();

    public void Init(List<GameObject> drawers)
    {
        _levelButtons = drawers;
        Subscribe();
    }

    public void Destruct()
    {
        Unsubscribe();
    }

    private void Subscribe()
    {
        for (int i = 0; i < _levelButtons.Count; i++)
        {
            int index = i;
            if (_levelButtons[index].GetComponent<Button>())
            {
                _levelButtons[index].GetComponent<Button>().onClick.AddListener(() => LevelButtonPressed(index));
            }
        }
    }

    private void Unsubscribe()
    {
        for (int i = 0; i < _levelButtons.Count; i++)
        {
            int index = i;
            if (_levelButtons[index].GetComponent<Button>())
            {
                _levelButtons[index].GetComponent<Button>().onClick.RemoveListener(() => LevelButtonPressed(index));
            }
        }
    }

    private void LevelButtonPressed(int levelIndex)
    {
        UIEvents.PlayLevel(levelIndex);
    }
}