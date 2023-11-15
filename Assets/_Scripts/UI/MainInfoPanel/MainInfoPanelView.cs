using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;
using Helpers.Events;
using TMPro;

namespace MVP
{
    public interface IMainInfoPanelView : IView<IMainInfoPanelPresenter>
    {
        void SetEnergyText(string text);
    }
    public class MainInfoPanelView : BaseView, IMainInfoPanelView
    {
        [SerializeField] private TMP_Text _energyText;
        [SerializeField] private TMP_Text _goldText;
        public IMainInfoPanelPresenter Presenter { get; private set; }

        public void InitPresenter(IMainInfoPanelPresenter presenter)
        {
            Presenter = presenter;
        }

        public void SetEnergyText(string text) => _energyText.text = text;

        protected override void OnAwake()
        {
            base.OnAwake();
            EventAggregator.Subscribe<EnergyUpdateUIEvent>(UpdateEnergyText);
        }

        private void UpdateEnergyText(object sender, EnergyUpdateUIEvent eventData) => Presenter.UpdateEnergyText(eventData.MoneyAmmount);

        protected override void OnDestroyInner()
        {
            base.OnDestroyInner();
            EventAggregator.Unsubscribe<EnergyUpdateUIEvent>(UpdateEnergyText);
        }
    }
}
