using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5.0f;

    private Player _player;
    private Animator _enemy_Animator;
    [SerializeField]
    private GameObject _laserPreFab;
    private float _fireRate = 3.0f;
    private float _CanFire = -1.0f;
    
    

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("Player is NULL.");
        }
        _enemy_Animator = GetComponent<Animator>();
        if(_enemy_Animator == null)
        {
            Debug.Log("Animator is NULL!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Time.time > _CanFire)
        {
            _fireRate = Random.Range(3f, 6.5f);
            _CanFire = Time.time + _fireRate;
            Instantiate(_laserPreFab, transform.position, Quaternion.identity);
        }
    }
    void CalculateMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        float randomX = Random.Range(-9.53f, 9.53f);



        if (transform.position.y < -6f)
        {
            transform.position = new Vector3(randomX, 7.5f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }
            _speed = 0;
            Destroy(this.gameObject, 0.7f);
            _player.ExplosionSound();
            _enemy_Animator.SetTrigger("OnEnemyDeath");
        }

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);

            if (_player != null)
            {
                _player.AddScore(10);
            }

            
            
             _enemy_Animator.SetTrigger("OnEnemyDeath");
             Destroy(this.gameObject, 0.7f);
             _player.ExplosionSound();
             _speed = 0;
             Destroy(GetComponent<Collider2D>());

            
        }
    }

}
