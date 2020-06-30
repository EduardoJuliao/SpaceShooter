using UnityEditor.UIElements;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 3.5f;
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private float _fireRate = 0.5f;

    private float _nextFire = -1f;
    private readonly Vector3 _laserOffset = new Vector3(0, 0.8f, 0);

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        if (Input.GetKeyUp(KeyCode.Space) && Time.time > _nextFire)
        {
            Shoot();
        }
    }

    void CalculateMovement()
    {
        // Time.deltaTime converts to real human time
        // Equivalent of incorporating real time

        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");
        var playerTransform = transform;

        var direction = new Vector3(horizontalInput, verticalInput, 0);

        playerTransform.Translate(direction * (_speed * Time.deltaTime));

        if (transform.position.x >= 9)
        {
            playerTransform.position = new Vector3(9, transform.position.y, 0);
        }
        else if (transform.position.x <= -9)
        {
            playerTransform.position = new Vector3(-9, transform.position.y, 0);
        }

        if (transform.position.y <= -3.8)
        {
            playerTransform.position = new Vector3(transform.position.x, -3.8f, 0);
        }
        else if (transform.position.y >= 5.5)
        {
            playerTransform.position = new Vector3(transform.position.x, 5.5f, 0);
        }
    }

    void Shoot()
    {
        _nextFire = Time.time + _fireRate;
        Instantiate(_laserPrefab, transform.position + _laserOffset, Quaternion.identity);
    }
}