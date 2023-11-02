using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerSystems
{
    public interface ISearchSystem
    {
        void SetSearchingType(SearchingType searchingType);
        SearchingType GetSearchingType();
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
        private SearchingType _searchingType;

        private Vector3 _turretPosition;
        private Vector3 _basePosition;

        private float _radiusTower;

        public SearchTowerSystem(Vector3 towerPosition, Vector3 basePosition, float radiusTower)
        {
            _turretPosition = towerPosition;
            _basePosition = basePosition;
            _radiusTower = radiusTower;
        }

        public Transform GetTarget(List<Enemy> enemiesList) => SearchTargetHandler(enemiesList);

        public void SetSearchingType(SearchingType searchingType) => _searchingType = searchingType;
        public SearchingType GetSearchingType() => _searchingType;

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
                var distance = (enemy.transform.position - _turretPosition).magnitude;
                if(distance <= _radiusTower)
                {
                    var direction = enemy.transform.position - _turretPosition;
                    if (Physics.Raycast(_turretPosition, direction, out RaycastHit hit))
                    {
                        if (hit.collider.gameObject.GetComponent<Enemy>())
                        {
                            if (enemyNearesToTower == null)
                                enemyNearesToTower = enemy;
                            else
                            {
                                var distanceLast = (enemyNearesToTower.transform.position - _turretPosition).magnitude;
                                if (distance < distanceLast)
                                    enemyNearesToTower = enemy;
                            }
                        }
                    }
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
                var distance = (enemy.transform.position - _basePosition).magnitude;
                if (distance <= _radiusTower)
                {
                    var direction = enemy.transform.position - _turretPosition;
                    if (Physics.Raycast(_turretPosition, direction, out RaycastHit hit))
                    {
                        if (hit.collider.gameObject.GetComponent<Enemy>())
                        {
                            if (enemyNearesToBase == null)
                            {
                                enemyNearesToBase = enemy;
                            }
                            else
                            {
                                var distanceLast = (enemyNearesToBase.transform.position - _basePosition).magnitude;
                                if (distance < distanceLast)
                                    enemyNearesToBase = enemy;
                            }
                        }
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
                var distance = (enemy.transform.position - _turretPosition).magnitude;
                if (distance <= _radiusTower)
                {
                    var direction = enemy.transform.position - _turretPosition;
                    if (Physics.Raycast(_turretPosition, direction, out RaycastHit hit))
                    {
                        if (hit.collider.gameObject.GetComponent<Enemy>())
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
                }
            }
            if (enemyMinHP != null)
                return enemyMinHP.transform;
            else
                return null;
        }

    }
}