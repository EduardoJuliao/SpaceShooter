using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] private float _speed = 3;
    [SerializeField] private PowerUpType _powerUpType;

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
        if (!other.CompareTag("Player")) return;
        
        var player = other.GetComponent<Player>();
        switch (_powerUpType)
        {
            case PowerUpType.Shield:
                player.ShieldActive();
                break;
            case PowerUpType.Speed:
                player.SpeedActive();
                break;
            case PowerUpType.TripleShot:
                player.TripleShotActive();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        Destroy(this.gameObject);
    }
}
