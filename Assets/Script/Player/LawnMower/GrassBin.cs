using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GrassBin : MonoBehaviour
{
    [SerializeField] private StackOfGrass _stackOfGrassTemplate;
    [SerializeField] private Bin _spawPlace;
    [SerializeField] private float _timeToHoldStack;
    [SerializeField] private List<StackUpPoint> _topWaypoints;
    [SerializeField] private List<StackUpPoint> _backpackWaypoints;
    [SerializeField] private Backpack _backpack;
    [SerializeField] private int _oneBushesCost;
    [SerializeField] private Canvas _ui;

    private bool _isEmpty;
    private float _stackingTimeLeft;
    private StackOfGrass _currentStack;
    private int _harvestedGrass;

    public int HarvestedGrass => _harvestedGrass;

    public event UnityAction GrassHarvested;

    private void Awake()
    {
        _isEmpty = true;
    }

    public void PickUpGrass()
    {
        if (_isEmpty)
        {
            _harvestedGrass = _oneBushesCost;
            StartCoroutine(ShapeStackOfGrass());
        }
        else
        {
            _harvestedGrass += _oneBushesCost;
            _currentStack.AddGrassToStack();
            _stackingTimeLeft = _timeToHoldStack;
        }

        GrassHarvested?.Invoke();
    }

    private IEnumerator ShapeStackOfGrass()
    {
        _isEmpty = false;
        _ui.gameObject.SetActive(true);
        _stackingTimeLeft = _timeToHoldStack;

        _currentStack = Instantiate(_stackOfGrassTemplate, _spawPlace.transform.position, _spawPlace.transform.rotation, _spawPlace.transform);

        while (_stackingTimeLeft > 0 && _currentStack.CurrentScaleY < _currentStack.MaxScaleY)
        {
            _stackingTimeLeft -= Time.deltaTime;
            yield return null;
        }

        _currentStack.StackUp(GetStackUpWay());
        _backpack.HarvestGrass(_harvestedGrass);

        _ui.gameObject.SetActive(false);
        _isEmpty = true;
    }

    private List<StackUpPoint> GetStackUpWay()
    {
        List<StackUpPoint> waypoint = new List<StackUpPoint>();

        waypoint.Add(_topWaypoints[Random.Range(0, _topWaypoints.Count)]);
        waypoint.Add(_backpackWaypoints[Random.Range(0, _backpackWaypoints.Count)]);

        return waypoint;
    }
}
