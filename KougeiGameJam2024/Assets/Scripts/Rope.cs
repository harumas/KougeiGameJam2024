using System;
using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField] private float initialLength;
    [SerializeField] private float min;
    [SerializeField] private float max;

    [Space(20)]

    [Header("動きの制限関係")]
    [Tooltip("制限を始める距離")]
    [Range(0f, 1f)]
    [SerializeField]private float LimitStartLength;
    [Tooltip("減衰の方法")]
    [SerializeField]private AnimationCurve LimitCurve;

    [Space(20)]
    
    [Header("ReadOnly")]
    [SerializeField] private float realLength;
    [SerializeField] private float normalizedLength;

    private void Start()
    {
        Length = initialLength;
    }

    public float GetMaxLength()
    {
        return max;
    }

    public float GetRopeDecayRate()
    {
        if(LimitStartLength >= normalizedLength){
            return 1;
        }else{
            return LimitCurve.Evaluate(normalizedLength / 1);
        }
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
            Debug.Log(normalizedLength);
            realLength = Mathf.Lerp(min, max, normalizedLength);
        }
    }
}