using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyExplosionPrefab;

    private float _speed = 5.0F;
    private UIManager _uiManager;

    // Start is called before the first frame update
    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Move down
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        // When off the screen on the botton
        // respawn back on top with a new x position between the bounds of the screen
        if (transform.position.y < -6.5)
        {
            float randomX = Random.Range(-6.5F, 6.5F);
            transform.position = new Vector3(randomX, 6.5F, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            if (other.transform.parent != null)
            {
                Destroy(other.transform.parent.gameObject);
            }
            
            if (_uiManager != null)
            {
                _uiManager.UpdateScore();
            }

            Destroy(other.gameObject);
            Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        else if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();

                if (player.lives >= 1 && _uiManager != null)
                {
                    _uiManager.UpdateScore();
                }
            }
            Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);            
        }
    }
}
