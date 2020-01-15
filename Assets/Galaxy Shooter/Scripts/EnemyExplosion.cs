using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplosion : MonoBehaviour
{
    private float _speed = 5.0F;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 4F);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }
}
