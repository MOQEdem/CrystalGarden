using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PreBuilding : MonoBehaviour
{
    [SerializeField] protected int Cost;
    [SerializeField] protected bool IsActive;
    [SerializeField] protected float DestroyDelay;
    [SerializeField] protected float FrameDestroyDelay;
    [SerializeField] protected UIPreBuilding UI;

    [SerializeField] private Building _building;
    [SerializeField] private Building _buildingTemplate;
    [SerializeField] private float _buildingSpeed;
    [SerializeField] private PreBuilding _nextBuilding;
    [SerializeField] private GameObject _structure;
    [SerializeField] private Transform _pointerTarget;

    private Vector3 _buildingScale;

    public int CostLeft => Cost;
    public Transform PointerTarget => _pointerTarget;

    public event UnityAction Builded;

    private void Awake()
    {
        _buildingScale = _building.transform.localScale;
        _building.transform.localScale = new Vector3(0, 0, 0);


        if (IsActive == false)
        {
            UI.gameObject.SetActive(false);
        }
    }

    public void Activate()
    {
        IsActive = true;
        UI.gameObject.SetActive(true);
    }

    protected void Build()
    {
        Builded?.Invoke();
        Destroy(_structure, FrameDestroyDelay);
        StartCoroutine(PlayBuildingAnimation());

        if (_nextBuilding != null)
        {
            _nextBuilding.Activate();
        }

        if (_buildingTemplate != null)
        {
            Destroy(_buildingTemplate.gameObject);
        }

        Destroy(this.gameObject, DestroyDelay);
    }

    private IEnumerator PlayBuildingAnimation()
    {
        while (_building.transform.localScale != _buildingScale)
        {
            _building.transform.localScale = Vector3.MoveTowards(_building.transform.localScale, _buildingScale, _buildingSpeed * Time.deltaTime);
            yield return null;
        }

    }
}
