using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Move_Position : MonoBehaviour
{
    public float speed;
    private Vector3 pos;
    private bool isStop = false;

    void Start()
    {
        pos = transform.position;
        transform.rotation = new Quaternion(0, 180, 0, transform.rotation.w);
    }

    void Update()
    {
        if (!isStop)
        {
            pos.x += Time.deltaTime * speed; // speed�͈ړ����x
            transform.position = pos;

            if (pos.x > 55) // �I�_�i���R�ɕύX�\�j
            {
                transform.rotation = new Quaternion(0,0,0, transform.rotation.w); 
                isStop = true;
            }
        }
        else
        {
            pos.x -= Time.deltaTime * speed;
            transform.position = pos;

            if (pos.x < -55) // �n�_�i���R�ɕύX�\�j
            {
                transform.rotation = new Quaternion(0, 180, 0, transform.rotation.w);
                isStop = false;
            }
        }
    }
}