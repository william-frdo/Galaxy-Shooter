using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0F;

    // 0 = triple shot, 1 = speed boost, 2 = shields
    [SerializeField]
    private int powerupID;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            // Access the player
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                // Enable triple shot
                if (powerupID == 0)
                {
                    player.TripleShotPowerupOn();
                }

                // Enable speed boost
                if (powerupID == 1)
                {
                    player.SpeedBoostPowerupOn();
                }

                // Enable shields
                if (powerupID == 2)
                {
                    player.TripleShotPowerupOn();
                }
            }
            
            // Destroy ourself
            Destroy(this.gameObject);
        }
    }
}
