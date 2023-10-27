using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MVP;

public class MainMenuPresenter : IPresenter<MainMenuView>
{
    public MainMenuView View { get; private set; }

    public void Initialize()
    {
        View.InitPresenter(this);
    }
}
