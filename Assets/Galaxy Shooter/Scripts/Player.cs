using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool canTripleShot = false;
    public bool isSpeedBoostActive = false;

    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private float _fireRate = 0.25F;
    private float _canFire = 0.0F;
    [SerializeField]
    private float _speed = 6.0F;

    // Start is called before the first frame update
    void Start()
    {
        // current pos = new position
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        // If space key or Mouse button 0 be pressed, spawn laser at player position
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
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

        // if player on the y is greater than 0 set player position on the Y to 0
        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y < -4.2F)
        {
            transform.position = new Vector3(transform.position.x, -4.2F, 0);
        }

        // if player on the x is greater than 9.5, position on the X needs to be -9.5
        if (transform.position.x > 9.5F)
        {
            transform.position = new Vector3(-9.5F, transform.position.y, 0);
        }
        else if (transform.position.x < -9.5F)
        {
            transform.position = new Vector3(9.5F, transform.position.y, 0);
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
    
    public IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0F);
        canTripleShot = false;
    }

    public IEnumerator SpeedBoostDownRoutine()
    {
        yield return new WaitForSeconds(5.0F);
        isSpeedBoostActive = false;
    }
}
