using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    
    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private GameObject _explosion;
    private SpawnManager _spawnManager;
    private Player _player;
    
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.down * _speed * Time.deltaTime);

        transform.Translate (Vector3.down * _speed * Time.deltaTime);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Destroy(this.gameObject, 0.25f);
            Instantiate(_explosion, transform.position, Quaternion.identity);
            Destroy(_explosion, 1.0f);
            
            Destroy(other.gameObject);
           _player.ExplosionSound();
            
        }
        if (other.tag == "Player")
        {
            Instantiate(_explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            _player.ExplosionSound();
        }

    }
    
        
    


}
