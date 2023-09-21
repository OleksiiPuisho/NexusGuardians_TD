using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerSystems
{
    public interface ISearchSystem
    {
        void SetSearchingType(SearchingType searchingType);
        Transform GetTarget(List<Enemy> enemiesList);
    }

    public enum SearchingType
    {
        NearestToTower = 0,
        NearestToBase = 1,
        MinimumHealth = 2
    }

    public class SearchTowerSystem : ISearchSystem
    {
        private SearchingType _searchingType = SearchingType.NearestToTower;

        private Vector3 _towerPosition;
        private Vector3 _basePosition;

        private float _radiusTower;

        public SearchTowerSystem(Vector3 towerPosition, Vector3 basePosition, float radiusTower)
        {
            _towerPosition = towerPosition;
            _basePosition = basePosition;
            _radiusTower = radiusTower;
        }

        public Transform GetTarget(List<Enemy> enemiesList) => SearchTargetHandler(enemiesList);

        public void SetSearchingType(SearchingType searchingType) => _searchingType = searchingType;

        private Transform SearchTargetHandler(List<Enemy> enemiesList)
        {
            switch (_searchingType)
            {
                case SearchingType.NearestToTower:
                    return NearestToTowerHandler(enemiesList);

                case SearchingType.NearestToBase:
                    return NearestToBaseHandler(enemiesList);

                case SearchingType.MinimumHealth:
                    return MinimumHealthHandler(enemiesList);

                default:
                    return null;
            }
        }
        private Transform NearestToTowerHandler(List<Enemy> enemiesList)
        {
            Enemy enemyNearesToTower = null;
            foreach (var enemy in enemiesList)
            {
                if(Vector3.Distance(_towerPosition, enemy.transform.position) <= _radiusTower)
                {
                    if (enemyNearesToTower == null)
                        enemyNearesToTower = enemy;
                    else if (Vector3.Distance(_towerPosition, enemy.transform.position) < Vector3.Distance(_towerPosition, enemyNearesToTower.transform.position))
                        enemyNearesToTower = enemy;
                }
            }
            if (enemyNearesToTower != null)
                return enemyNearesToTower.transform;
            else
                return null;
        }
        private Transform NearestToBaseHandler(List<Enemy> enemiesList)
        {
            Enemy enemyNearesToBase = null;
            foreach (var enemy in enemiesList)
            {
                if (Vector3.Distance(_towerPosition, enemy.transform.position) <= _radiusTower)
                {
                    if (enemyNearesToBase == null)
                    {
                        enemyNearesToBase = enemy;
                    }
                    else if (Vector3.Distance(_basePosition, enemy.transform.position) < Vector3.Distance(_basePosition, enemyNearesToBase.transform.position))
                    {
                        enemyNearesToBase = enemy;
                    }
                }
            }
            if (enemyNearesToBase != null)
                return enemyNearesToBase.transform;
            else
                return null;
        }
        private Transform MinimumHealthHandler(List<Enemy> enemiesList)
        {
            Enemy enemyMinHP = null;
            foreach (var enemy in enemiesList)
            {
                if (Vector3.Distance(_towerPosition, enemy.transform.position) <= _radiusTower)
                {
                    if (enemyMinHP == null)
                    {
                        enemyMinHP = enemy;
                    }
                    else if (enemy.Health < enemyMinHP.Health)
                    {
                        enemyMinHP = enemy;
                    }
                }
            }
            if (enemyMinHP != null)
                return enemyMinHP.transform;
            else
                return null;
        }
    }
}