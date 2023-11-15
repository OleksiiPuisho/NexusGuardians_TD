using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace MVP
{
    public interface IMainInfoPanelPresenter : IPresenter<IMainInfoPanelView>
    {
        void UpdateEnergyText(int energyAmmount);
    }
    public class MainInfoPanelPresenter : IMainInfoPanelPresenter
    {
        public MainInfoPanelPresenter(IMainInfoPanelView view)
        {
            View = view;
        }

        public IMainInfoPanelView View { get; private set; }

        public void Initialize()
        {
            View.InitPresenter(this);
        }

        public void UpdateEnergyText(int energyAmmount)
        {
            string energyText = energyAmmount.ToString();
            View.SetEnergyText(energyText);
        }
    }
}
