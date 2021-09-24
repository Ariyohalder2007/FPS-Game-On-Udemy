using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 8f;
    private float lifeTimer;
    public float lifeDuration=2f;
    bool moving;

    private void Start()
    {
        lifeTimer = lifeDuration;
        moving = true;
    }

    private void OnEnable()
    {
        moving = true;
        lifeTimer = lifeDuration;
    }

    private void Update()
    {
        if (moving)
        {
        transform.position += transform.forward * speed * Time.deltaTime;
        }
        
        lifeTimer -= Time.deltaTime;
        if (lifeTimer<=0)
        {
            gameObject.SetActive(false);
            moving = false;

        }
    }
}
