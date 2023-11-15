using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;
using Helpers.Events;

namespace MVP
{
    public interface IWaveInfoPanelView : IView<IWaveInfoPanelPresenter>
    {

    }
    public class WaveInfoPanelView : BaseView, IWaveInfoPanelView
    {
        public IWaveInfoPanelPresenter Presenter { get; private set; }

        public void InitPresenter(IWaveInfoPanelPresenter presenter)
        {
            Presenter = presenter;
        }
    }
}
