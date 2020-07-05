using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    private bool _isParentNotNull;

    private void Start()
    {
        _isParentNotNull = transform.parent != null;
    }

    private void Update()
    {
        transform.Translate(Vector3.up * (_speed * Time.deltaTime));

        if (transform.position.y > 8)
        {
            Destroy(_isParentNotNull ? this.transform.parent.gameObject : this.gameObject);
        }
    }
}
