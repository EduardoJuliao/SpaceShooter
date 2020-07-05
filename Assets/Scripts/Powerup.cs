using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] private float _speed = 3;

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(Vector3.down * (_speed * Time.deltaTime));
        
        if (transform.position.y < Boundries.MinY)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<Player>();
            player.TripleShotActive();
            Destroy(this.gameObject);
        }
    }
}
