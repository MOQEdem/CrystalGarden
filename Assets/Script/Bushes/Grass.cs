using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    [SerializeField] private float _oXScaleRandomizer;
    [SerializeField] private float _oYScaleRandomizer;
    [SerializeField] private float _oZScaleRandomizer;

    private void Awake()
    {
        transform.localScale += new Vector3(Random.Range(-_oXScaleRandomizer, _oXScaleRandomizer), Random.Range(-_oYScaleRandomizer, _oYScaleRandomizer), Random.Range(-_oZScaleRandomizer, _oZScaleRandomizer));
    }
}
