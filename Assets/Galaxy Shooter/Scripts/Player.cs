using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool canTripleShot = false;
    public bool isSpeedBoostActive = false;
    public bool shieldsActive = false;

    public int lives = 3;

    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _shildGameObject;
    [SerializeField]
    private float _fireRate = 0.05F;
    private float _canFire = 0.0F;
    [SerializeField]
    private float _speed = 6.0F;

    private UIManager _uiManager;
    private GameManager _gameManager;
    private SpawnManager _spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        // current pos = new position
        transform.position = new Vector3(0, 0, 0);

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_uiManager != null)
        {
            _uiManager.UpdateLives(lives);
        }

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (_spawnManager != null)
        {
            _spawnManager.StartSpawnRoutines();
        }        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        // If space key or Mouse button 0 be pressed, spawn laser at player position
        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (Time.time > _canFire)
        {
            if (canTripleShot)
            {
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.92F, 0), Quaternion.identity);
            }

            _canFire = Time.time + _fireRate;
        }
    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (isSpeedBoostActive)
        {
            transform.Translate(Vector3.right * _speed * 2.0F * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * _speed * 2.0F * verticalInput * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.right * _speed * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * _speed * verticalInput * Time.deltaTime);
        }

        // if player on the y is greater than 3.3 set player position on the Y to 3.3
        // if player on the y is lesser than -4.2 set player position on the Y to -4.2
        if (transform.position.y > 3.3)
        {
            transform.position = new Vector3(transform.position.x, 3.3F, 0);
        }
        else if (transform.position.y < -4.2)
        {
            transform.position = new Vector3(transform.position.x, -4.2F, 0);
        }

        // if player on the x is greater than 8.25 set player position on the X to 8.25
        // if player on the x is lesser than -8.28 set player position on the X to -8.28
        if (transform.position.x > 8.25)
        {
            transform.position = new Vector3(8.25F, transform.position.y, 0);
        }
        else if (transform.position.x < -8.25)
        {
            transform.position = new Vector3(-8.25F, transform.position.y, 0);
        }
    }

    public void Damage()
    {
        if (shieldsActive)
        {
            shieldsActive = false;
            _shildGameObject.SetActive(false);
            return;
        }

        lives--;
        _uiManager.UpdateLives(lives);

        if (lives < 1)
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);           
            Destroy(this.gameObject);
            _gameManager.gameOver = true;            
            _uiManager.ShowTitleScreen();
        }
    }

    public void TripleShotPowerupOn()
    {
        canTripleShot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    public void SpeedBoostPowerupOn()
    {
        isSpeedBoostActive = true;
        StartCoroutine(SpeedBoostDownRoutine());
    }

    public void EnableShields()
    {
        shieldsActive = true;
        _shildGameObject.SetActive(true);
        StartCoroutine(DisableShields());
    }
    
    public IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(15.0F);
        canTripleShot = false;
    }

    public IEnumerator SpeedBoostDownRoutine()
    {
        yield return new WaitForSeconds(5.0F);
        isSpeedBoostActive = false;
    }

    public IEnumerator DisableShields()
    {
        yield return new WaitForSeconds(20.0F);
        _shildGameObject.SetActive(false);
        shieldsActive = false;
    }
}
