using System;
using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField] private float initialLength;
    [SerializeField] private float min;
    [SerializeField] private float max;
    
    [Header("ReadOnly")]
    [SerializeField] private float realLength;
    [SerializeField] private float normalizedLength;

    private void Start()
    {
        Length = initialLength;
    }

    public float Length
    {
        get
        {
            //0 ~ 1で長さを返す
            return Mathf.InverseLerp(min, max, realLength);
        }
        
        set
        {
            normalizedLength = Mathf.Clamp01(value);
            realLength = Mathf.Lerp(min, max, normalizedLength);
        }
    }
}