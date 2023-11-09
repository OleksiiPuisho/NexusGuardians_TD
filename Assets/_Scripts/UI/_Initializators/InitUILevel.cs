using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MVP;

public class InitUILevel : MonoBehaviour
{
    [SerializeField] private Color _colorText;
    [SerializeField] private SelectedController _selectedController;
    [Space]
    //viewers
    [SerializeField] private PanelInfoLevelView _panelInfoLevelView;
    [SerializeField] private BuildingTowerPanelView _buildingTowerPanelView;

    //presenters
    private IPanelInfoLevelPresenter _panelInfoLevelPresenter;
    private BuildingTowerPanelPresenter _buildingTowerPanelPresenter;


    //models
    private IPanelInfoLevelModel _panelInfoLevelModel;

    //commands
    private TowerInfoCommand _towerInfoCommand;
    private CreateTowerCommand _createTowerCommand;

    private DestroyTowerCommand _destroyTowerCommand;

    private void Awake()
    {
        PreparePanelInfoLevel();
        PrepareBuildingTowerPanel();
    }

    private void PreparePanelInfoLevel()
    {
        _panelInfoLevelModel = new PanelInfoLevelModel();
        _towerInfoCommand = new TowerInfoCommand(_panelInfoLevelView, _colorText);
        _destroyTowerCommand = new DestroyTowerCommand(_selectedController);

        _panelInfoLevelPresenter = new PanelInfoLevelPresenter(_panelInfoLevelView, _panelInfoLevelModel, _towerInfoCommand, _destroyTowerCommand);
        _panelInfoLevelPresenter.Initialize();
    }

    private void PrepareBuildingTowerPanel()
    {
        _createTowerCommand = new CreateTowerCommand(_selectedController);

        _buildingTowerPanelPresenter = new BuildingTowerPanelPresenter(_buildingTowerPanelView, _createTowerCommand);
        _buildingTowerPanelPresenter.Initialize();
    }
}
