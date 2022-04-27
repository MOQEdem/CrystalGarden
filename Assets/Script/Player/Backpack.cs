using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Backpack : MonoBehaviour
{
    [SerializeField] private TransferredResource _grassTemplate;
    [SerializeField] private TransferredResource _crystalTemplate;
    [SerializeField] private int _resourceDivider;
    [SerializeField] private Transform _topTransferPoint;
    [SerializeField] private float _transferTime;
    [SerializeField] private float _transferDelay;

    private int _harvestedGrass;
    private int _collectedCrystal;

    public int HarvestedGrass => _harvestedGrass;
    public int CollectedCrystal => _collectedCrystal;

    public event UnityAction CrystalAmountChanged;

    private void Awake()
    {
        _collectedCrystal = 0;
        _harvestedGrass = 0;
    }

    public void HarvestGrass(int harvestedGrass)
    {
        _harvestedGrass += harvestedGrass;
    }

    public void CollectCrystal()
    {
        _collectedCrystal++;
        CrystalAmountChanged?.Invoke();
    }

    public int PayGrassPrice(int grassDemand, Transform receiverPosition)
    {
        var givenGrass = Mathf.Clamp(grassDemand, 0, _harvestedGrass);
        _harvestedGrass -= givenGrass;
        TransferResourses(_grassTemplate, givenGrass, receiverPosition);
        return givenGrass;
    }

    public int PayCrystalPrice(int crystalDemand, Transform receiverPosition)
    {
        var givenCrystal = Mathf.Clamp(crystalDemand, 0, _collectedCrystal);
        _collectedCrystal -= givenCrystal;
        TransferResourses(_crystalTemplate, givenCrystal, receiverPosition);
        CrystalAmountChanged?.Invoke();
        return givenCrystal;
    }

    private void TransferResourses(TransferredResource resoursType, int resourseAmount, Transform finalPosition)
    {
        Vector3[] transferPath = new Vector3[2];
        transferPath[0] = _topTransferPoint.position;
        transferPath[1] = finalPosition.position;

        int iterationCount = resourseAmount / _resourceDivider;

        StartCoroutine(AnimateTransfer(resoursType, transferPath, iterationCount));
    }

    private IEnumerator AnimateTransfer(TransferredResource resoursType, Vector3[] transferPath, int iterationCount)
    {

        while (iterationCount > 0)
        {
            var delay = new WaitForSeconds(_transferDelay);

            TransferredResource transferPiece = Instantiate(resoursType, transform.position, Quaternion.identity);

            transferPiece.Transfer(transferPath, _transferTime);

            iterationCount--;

            yield return delay;
        }
    }
}
