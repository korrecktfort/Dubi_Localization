using Dubi.Localization;
using Dubi.SingletonSpace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationManager : Singleton<LocalizationManager>
{
    [SerializeField] CurrentLanguageValue currentLanguage = new CurrentLanguageValue();
    List<SetLocalization> setLocalizations = new List<SetLocalization>();
    public int CurrentLanguage => this.currentLanguage.Value;

    protected override void OnAwake()
    {
        this.currentLanguage.InjectLanguageObject(Resources.Load<CurrentLanguageObject>("Current Language Object"));
    }

    private void OnEnable()
    {
        this.currentLanguage.RegisterCallback(UpdateLocalization);
    }

    private void OnDisable()
    {
        this.currentLanguage.DeregisterCallback(UpdateLocalization);
    }

    void UpdateLocalization(int index)
    {
        if(Quitting)
            return;

        foreach(SetLocalization setLocalization in setLocalizations)
        {
            setLocalization.SetLocalization(index);
        }
    }

    public void Register(SetLocalization setLocalization)
    {
        if (Quitting)
            return;

        if (this.setLocalizations.Contains(setLocalization))
            return;

        setLocalization.SetLocalization(this.currentLanguage.Value);
        this.setLocalizations.Add(setLocalization);        
    }

    public void Deregister(SetLocalization setLocalization)
    {
        if (Quitting)
            return;

        if (!this.setLocalizations.Contains(setLocalization))
            return;

        this.setLocalizations.Remove(setLocalization);
    }
}
