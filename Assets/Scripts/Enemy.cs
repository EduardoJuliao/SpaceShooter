using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 4.0f;
    void Update()
    {
        transform.Translate(Vector3.down * (_speed * Time.deltaTime));

        if (transform.position.y < Boundries.MinY - 2f)
        {
            var newX = Random.Range(Boundries.MinX, Boundries.MaxX);
            transform.position = new Vector3(newX, Boundries.MaxY + 2, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.transform.GetComponent<Player>();
            if (player != null)
                player.Damage(1);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Laser"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}