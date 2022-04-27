using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TransferredResource : MonoBehaviour
{
    public void Transfer(Vector3[] transferPath, float transferTime)
    {
        StartCoroutine(OrganizeMovement(transferPath, transferTime));
    }

    private IEnumerator OrganizeMovement(Vector3[] transferPath, float transferTime)
    {
        Destroy(gameObject, transferTime + 1f);

        transform.DOLocalPath(transferPath, transferTime, PathType.CatmullRom).SetOptions(false);
        yield return new WaitForSeconds(transferTime);

        gameObject.SetActive(false);
    }
}
