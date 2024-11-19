using System;
using TMPro;
using UnityEngine;

[Serializable]
public class CoinsTextManager
{
    [SerializeField] private TMP_Text _coinsText;

    public void Initialize()
    {
        Subscribe();      
    }

    public void Cleanup()
    {
        Unsubscribe();
    }

    private void Subscribe()
    {
        ResourceEvents.OnResourceModified += ModifyResourceText;
    }

    private void Unsubscribe()
    {
        ResourceEvents.OnResourceModified -= ModifyResourceText;
    }

    public void SetCoinsText()
    {
        int coins = SaveManager.Resources.LoadResource(ResourceTypes.Coins);
        _coinsText.text = coins.ToString(); 
    }

    private void ModifyResourceText(ResourceTypes resource, int newAmount)
    {
        switch(resource)
        {
            case ResourceTypes.Coins:
                _coinsText.text = newAmount.ToString();
                break;
        }
    }
}