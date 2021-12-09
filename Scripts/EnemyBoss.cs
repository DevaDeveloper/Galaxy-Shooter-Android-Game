using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;
    private float _canFire = 3f;
    private float _fireRate = -1f;
    private Player _player;
    [SerializeField]
    private int _lives = 20;

    [SerializeField]
    private GameObject _enemyBossWeapon;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if(_player == null)
        {
            Debug.Log("Player is NULL.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        if (Time.time > _canFire)
        {
            _fireRate = Random.Range(3.0f, 6.5f);
            _canFire = _fireRate + Time.time;
            Instantiate(_enemyBossWeapon, transform.position, Quaternion.identity);
        }
    }

    void CalculateMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        float randomX = Random.Range(-9.53f, 9.53f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if(player != null)
            {
                player.Damage();
            }
            MyDamage();
            _player.ExplosionSound();
        }
        if (other.tag == "Laser")
        {
            MyDamage();
            Destroy(other.gameObject);
            if(_player != null)
            {
                _player.AddScore(10);
            }
            
        }

    }
    void MyDamage()
    {
        _lives--;

        if(_lives < 1)
        {
            Destroy(this.gameObject);
        }
    }
}


