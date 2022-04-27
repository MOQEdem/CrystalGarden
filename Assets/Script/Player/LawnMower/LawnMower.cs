using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LawnMower : MonoBehaviour
{
    [SerializeField] private ParticleSystem _cutEffect;
    [SerializeField] private GrassBin _bin;
    [SerializeField] private Backpack _backpack;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Bushes>(out Bushes bushes))
        {
            if (!bushes.IsCutOff)
            {
                bushes.Cut();
                _cutEffect.Play();
                _bin.PickUpGrass();
            }
        }

        if (other.TryGetComponent<Crystal>(out Crystal crystal))
        {
            crystal.Collect(_backpack.transform);
            _backpack.CollectCrystal();
        }
    }
}
