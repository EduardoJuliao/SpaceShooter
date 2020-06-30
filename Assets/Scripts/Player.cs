using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 3.5f;
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private float _fireRate = 0.5f;
    [SerializeField] private int _lives = 3;

    private SpawnManager _spawnManager;
    private float _nextFire = -1f;
    private readonly Vector3 _laserOffset = new Vector3(0, 0.8f, 0);

    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        if (Input.GetKey(KeyCode.Space) && Time.time > _nextFire)
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

        if (transform.position.x >= Boundries.MaxX)
        {
            playerTransform.position = new Vector3(Boundries.MaxX, transform.position.y, 0);
        }
        else if (transform.position.x <= Boundries.MinX)
        {
            playerTransform.position = new Vector3(Boundries.MinX, transform.position.y, 0);
        }

        if (transform.position.y <= Boundries.MinY)
        {
            playerTransform.position = new Vector3(transform.position.x, Boundries.MinY, 0);
        }
        else if (transform.position.y >= Boundries.MaxY)
        {
            playerTransform.position = new Vector3(transform.position.x, Boundries.MaxY, 0);
        }
    }

    void Shoot()
    {
        _nextFire = Time.time + _fireRate;
        Instantiate(_laserPrefab, transform.position + _laserOffset, Quaternion.identity);
    }

    public void Damage(int damageDealt)
    {
        _lives -= damageDealt;

        if (_lives <= 0)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(gameObject);
        }
    }
}