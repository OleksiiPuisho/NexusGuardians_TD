using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ItemLinks
{
    public class TowerItem : MonoBehaviour
    {
        public Image Icon;
        public int Price;
        [SerializeField] private TMP_Text _priceText;

        private void Start()
        {
            _priceText.text = Price.ToString();
        }
    }
}
