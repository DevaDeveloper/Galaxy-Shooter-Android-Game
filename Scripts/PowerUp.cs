using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour

{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int PowerUpID;
    private AudioSource _powerUpSound;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        _powerUpSound = GameObject.Find("PowerUp_Sound").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        float randomX = Random.Range(-8f, 8f);

        if (transform.position.y < -6.5f)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                switch (PowerUpID)
                {
                    case 0:
                        player.TripleShotPowerUp();
                        _powerUpSound.Play();
                        break;
                    case 1:
                        player.SpeedPowerUp();
                        _powerUpSound.Play();
                        break;
                    case 2:
                        player.ShieldPowerUp();
                        _powerUpSound.Play();
                        break;
                    default:
                        Debug.Log("Default Value");
                        _powerUpSound.Play();
                        break;

                }

            }
                Destroy(this.gameObject);
            }
        }


    }
   




