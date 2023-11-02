using Command;
using UnityEngine;

namespace MVP
{
    public class TowerInfoCommand : ICommand<TowerData>
    {
        private IPanelInfoLevelView _panelInfoLevelView;
        private Color _colorText;

        public TowerInfoCommand(IPanelInfoLevelView panelInfoLevelView, Color colorText)
        {
            _panelInfoLevelView = panelInfoLevelView;
            _colorText = colorText;
        }
        public void Execute(TowerData data)
        {
            _panelInfoLevelView.UpdateTowerInfo(GetTowerInfo(data));
        }

        private TowerInfo GetTowerInfo(TowerData towerData)
        {
            var result = new TowerInfo()
            {
                Name = towerData.Name,
                BulletType = towerData.BulletType,
                SerchingType = towerData.SearchingType,

                AttackType = string.Format("Type: <color=#{1}> {0} </color>", GetAttackType(towerData.AttackType), ColorUtility.ToHtmlStringRGB(_colorText)),
                Radius = string.Format("Radius: <color=#{1}> {0}m </color>", towerData.Radius, ColorUtility.ToHtmlStringRGB(_colorText)),
                SpeedRotate = string.Format("Speed rotation: <color=#{1}> {0} </color>", towerData.SpeedRotate, ColorUtility.ToHtmlStringRGB(_colorText)),
                Damage = string.Format("Damage: <color=#{2}> {0}-{1} </color>", towerData.Damage.x, towerData.Damage.y, ColorUtility.ToHtmlStringRGB(_colorText)),
                ReloadingSpeed = string.Format("Reloading speed: <color=#{1}>{0}s </color>", towerData.ReloadingSpeed, ColorUtility.ToHtmlStringRGB(_colorText)),
                FiringSpread = string.Format("Firing spread: <color=#{1}>{0}° </color>", towerData.FiringSpread, ColorUtility.ToHtmlStringRGB(_colorText)),

            };
            return result;
        }

        private string GetAttackType(AttackType attackType)
        {
            string result;
            switch (attackType)
            {
                case global::AttackType.Ground:
                    result =  "Ground";
                    break;
                case global::AttackType.Air:
                    result = "Air";
                    break;
                case global::AttackType.GroundAir:
                    result = "Ground - Air";
                    break;
                default:
                    result =  "None";
                    break;
            }
            return result;
        }
    }
}