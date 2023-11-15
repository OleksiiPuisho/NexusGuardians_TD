using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MVP;

public class InitUILevel : MonoBehaviour
{
    [SerializeField] private Color _colorText;
    [SerializeField] private SelectedController _selectedController;
    [SerializeField] private TowerObserver _towerObserver;
    [Space]
    //viewers
    [SerializeField] private PanelInfoLevelView _panelInfoLevelView;
    [SerializeField] private BuildingTowerPanelView _buildingTowerPanelView;
    [SerializeField] private MainInfoPanelView _mainInfoPanelView;

    //presenters
    private IPanelInfoLevelPresenter _panelInfoLevelPresenter;
    private BuildingTowerPanelPresenter _buildingTowerPanelPresenter;
    private MainInfoPanelPresenter _mainInfoPanelPresenter;


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
        PrepareMainInfoPanel();
    }

    private void PreparePanelInfoLevel()
    {
        _panelInfoLevelModel = new PanelInfoLevelModel();
        _towerInfoCommand = new TowerInfoCommand(_panelInfoLevelView, _colorText);
        _destroyTowerCommand = new DestroyTowerCommand(_selectedController);

        _panelInfoLevelPresenter = new PanelInfoLevelPresenter(_panelInfoLevelView, _panelInfoLevelModel, _towerInfoCommand, _destroyTowerCommand, _towerObserver);
        _panelInfoLevelPresenter.Initialize();
    }

    private void PrepareBuildingTowerPanel()
    {
        _createTowerCommand = new CreateTowerCommand(_selectedController);

        _buildingTowerPanelPresenter = new BuildingTowerPanelPresenter(_buildingTowerPanelView, _createTowerCommand);
        _buildingTowerPanelPresenter.Initialize();
    }

    private void PrepareMainInfoPanel()
    {
        _mainInfoPanelPresenter = new(_mainInfoPanelView);
        _mainInfoPanelPresenter.Initialize();
    }
}
