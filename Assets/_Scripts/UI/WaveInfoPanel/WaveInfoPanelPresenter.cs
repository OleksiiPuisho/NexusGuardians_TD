using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVP
{
    public interface IWaveInfoPanelPresenter : IPresenter<IWaveInfoPanelView>
    {

    }
    public class WaveInfoPanelPresenter : IWaveInfoPanelPresenter
    {
        public IWaveInfoPanelView View { get; private set; }

        public void Initialize()
        {
            View.InitPresenter(this);
        }
    }
}
