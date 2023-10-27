using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MVP;

[RequireComponent(typeof(UIDocument))]
public class MainMenuView : BaseView, IView<MainMenuPresenter>
{
    public MainMenuPresenter Presenter { get; private set; }

    public void InitPresenter(MainMenuPresenter presenter)
    {
        Presenter = presenter;
    }

    protected override void OnAwake()
    {
        base.OnAwake();

    }
}
