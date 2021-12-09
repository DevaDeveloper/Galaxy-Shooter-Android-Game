using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 0.15f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    [SerializeField]
    private bool _isTripleShotActive = false;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private bool _isSpeedPowerUpActive = false;
    [SerializeField]
    private float _speedPowerUp = 8.5f;
    [SerializeField]
    private bool _isShieldActive = false;
    [SerializeField]
    private GameObject Visualizer;
    [SerializeField]
    private GameObject _rightEngine;
    [SerializeField]
    private GameObject _leftEngine;
    

    [SerializeField]
    public int _score;

    private UIManager _UIManager;
    [SerializeField]
    private AudioSource _laserAudio;
    [SerializeField]
    private AudioSource _explosionAudio;


    



    // Start is called before the first frame update
    void Start()
    {
        _explosionAudio = GameObject.Find("Explosion_Sound").GetComponent<AudioSource>();
        _laserAudio = GameObject.Find("Laser_Audio").GetComponent<AudioSource>();

        _UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL");
        }
        if (_UIManager == null)
        {
            Debug.LogError("UIManager is null.");
        }
        if (_laserAudio == null)
        {
            Debug.Log("Audio Source is NULL.");
        }
        //take the current position = new position ( 0, 0, 0)
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        FireLaser();


    }

    void CalculateMovement()
    {
        if (_isSpeedPowerUpActive == true)
        {
            _speed = 8.5f;
        }
        else
        {
            _speed = 5.0f;
        }
        float horizInput = CrossPlatformInputManager.GetAxis("Horizontal"); // Input.GetAxis("Horizontal");
        float vertiInput = CrossPlatformInputManager.GetAxis("Vertical"); // Input.GetAxis("Vertical");

        
            transform.Translate(Vector3.right * horizInput * _speed * Time.deltaTime);
            transform.Translate(Vector3.up * vertiInput * _speed * Time.deltaTime);
       

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);


        if (transform.position.x > 11)
        {
            transform.position = new Vector3(-11, transform.position.y, 0);
        }
        else if (transform.position.x < -11)
        {
            transform.position = new Vector3(11, transform.position.y, 0);
        }
    }
    void FireLaser()
    {

        if (Input.GetKeyDown(KeyCode.Space) || CrossPlatformInputManager.GetButtonDown("Fire") && Time.time > _canFire)
        {
            _canFire = Time.time + _fireRate;
            if (_isTripleShotActive == true)
            {
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
            }
            
            _laserAudio.Play();
        }
    }

    public void Damage()
    {
        if (_isShieldActive == true)
        {
           
            _lives++;
            _lives--;
            
        }
        else
        {
            Visualizer.SetActive(false);

            _lives--;

            if (_lives == 2)
            {
                _rightEngine.SetActive(true);
            }
             else
            {
                _leftEngine.SetActive(true);
            }

            _UIManager.UpdateLives(_lives);

            if (_lives < 1)
            {
                
                _UIManager.CheckBestScore();
                _spawnManager.OnPlayerDeath();
                Destroy(this.gameObject);
                ExplosionSound();
            }
        }
        
    }
    public void TripleShotPowerUp()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }

    public void SpeedPowerUp()
    {
        _isSpeedPowerUpActive = true;
        StartCoroutine(SpeedBoostDownRoutine());
    }

    IEnumerator SpeedBoostDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedPowerUpActive = false;
    }

    public void ShieldPowerUp()
    {
        _isShieldActive = true;
        Visualizer.SetActive(true);
        StartCoroutine(ShieldDownRoutine());
    }

    IEnumerator ShieldDownRoutine()
    {
        yield return new WaitForSeconds(6.0f);
        _isShieldActive = false;
        Visualizer.SetActive(false);
        
    }
    public void AddScore(int points)
    {
        _score += points;
        _UIManager.UpdateScore();
    }
    public void ExplosionSound()
    {
        _explosionAudio.Play();
    }
    
}



