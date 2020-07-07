using System;
using System.Collections;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 3.5f;
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _tripleLaser;
    [SerializeField] private float _fireRate = 0.5f;
    [SerializeField] private int _lives = 3;

    private SpawnManager _spawnManager;
    private float _nextFire = -1f;
    private readonly Vector3 _laserOffset = new Vector3(0, 1.05f, 0);
    private bool _isTripleShotEnable = false;
    private bool _isSpeedEnable = false;
    private bool _isShieldEnable = false;
    private GameObject _shield;
    private long _score;
    private UIManager _uiManager;

    // Start is called before the first frame update
    private void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _shield = transform.Find("Shield").gameObject;
        transform.position = new Vector3(0, 0, 0);
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_spawnManager == null)
        {
            Debug.Log("Spawn manager is null");
        }
        if (_uiManager == null)
        {
            Debug.Log("UI manager is null");
        }
    }

    // Update is called once per frame
    private void Update()
    {
        CalculateMovement();
        if (Input.GetKey(KeyCode.Space) && Time.time > _nextFire)
        {
            Shoot();
        }
    }

    private void CalculateMovement()
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

        if (transform.position.y <= Boundries.MinY +2)
        {
            playerTransform.position = new Vector3(transform.position.x, Boundries.MinY +2, 0);
        }
        else if (transform.position.y >= Boundries.MaxY-2)
        {
            playerTransform.position = new Vector3(transform.position.x, Boundries.MaxY -2, 0);
        }
    }

    private void Shoot()
    {
        _nextFire = Time.time + _fireRate;
        Instantiate(
            _isTripleShotEnable ? _tripleLaser : _laserPrefab, 
            transform.position + _laserOffset,
            Quaternion.identity);
    }

    public void Damage(int damageDealt)
    {
        if (_isShieldEnable)
        {
            _isShieldEnable = false;
            _shield.SetActive(false);
            return;
        }
        _lives -= damageDealt;

        if (_lives > 0) return;
        
        _spawnManager.OnPlayerDeath();
        Destroy(gameObject);
    }

    public void SpeedActive()
    {
        if (_isSpeedEnable)
            return;
        _isSpeedEnable = true;
        _speed *= 2;
        StartCoroutine(SpeedPowerUpTimer());
    }

    private IEnumerator SpeedPowerUpTimer()
    {
        yield return new WaitForSeconds(5);
        _isSpeedEnable = false;
        _speed /= 2;
    }
    
    public void ShieldActive()
    {
        _isShieldEnable = true;
        _shield.SetActive(true);
    }
    
    public void TripleShotActive()
    {
        _isTripleShotEnable = true;
        StartCoroutine(TripleLaserPowerUpTimer());
    }

    private IEnumerator TripleLaserPowerUpTimer()
    {
        yield return new WaitForSeconds(5);
        _isTripleShotEnable = false;
    }

    public void AddScore(int score)
    {
        _score += score;
        _uiManager.UpdateScoreText(_score);
    }
}