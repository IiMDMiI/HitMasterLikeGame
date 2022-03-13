using UnityEngine;
using System.Linq;

public class Player : MonoBehaviour
{
    [SerializeField] private BattleArea[] _battleAreas;
    [SerializeField] private MovementController _movementController;
    [SerializeField] private int _currentBattleAreaIndex = 0;

    private void Start()
    {
        _battleAreas = FindObjectsOfType<BattleArea>();
        _battleAreas = _battleAreas.OrderBy(n => n.gameObject.name).ToArray();

        foreach (var battleArea in _battleAreas)
            battleArea.OnAreaCleared += OnAreaClearedHandler;

        _movementController.OnStop += OnStopHandler;
        TryGoToNextBattleArea();
    }
    private void OnDestroy()
    {
        if (_battleAreas.Length > 0)
        {
            foreach (var battleArea in _battleAreas)
                battleArea.OnAreaCleared -= OnAreaClearedHandler;
        }

        _movementController.OnStop -= OnStopHandler;
    }
    
    private void OnStopHandler()
    {
        TryGoToNextBattleArea();
    }

    private void OnAreaClearedHandler(BattleArea battleArea)
    {
        battleArea.OnAreaCleared -= OnAreaClearedHandler;
        TryGoToNextBattleArea();
    }

    private void TryGoToNextBattleArea()
    {
        if (_battleAreas[_currentBattleAreaIndex].IsCleared == false)
            return;

        _currentBattleAreaIndex += 1;


        if (_currentBattleAreaIndex >= _battleAreas.Length)
        {
            SceneSwitcher.Inctance.SwitchScene("Level1");
            return;
        }

        _movementController.StartMoving(_battleAreas[_currentBattleAreaIndex].transform);
    }



}
