using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 4.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * (_speed * Time.deltaTime));

        if (transform.position.y < Boundries.MinY - 2f)
        {
            var newX = Random.Range(Boundries.MinX, Boundries.MaxX);
            transform.position = new Vector3(newX, Boundries.MaxY + 2, 0);
        }
    }
}
