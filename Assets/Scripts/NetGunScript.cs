using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NetGunScript : MonoBehaviour
{
    public GameObject _netProjectilePrefab;
    public Transform _netProjectileSpawnPoint;
    ObjectPool _netProjectilePool;

    public float shootCooldown;
    float shootTime;

    // Start is called before the first frame update
    void Start()
    {
        _netProjectilePool = new ObjectPool(_netProjectilePrefab, true, 5);

        shootTime = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        shootTime += Time.deltaTime;
    }

    public void OnShoot(InputValue value)
    {
        if (shootTime >= shootCooldown)
        {
            GameObject net = _netProjectilePool.GetObject();
            net.transform.position = _netProjectileSpawnPoint.position;
            net.GetComponent<Rigidbody>().velocity = Camera.main.transform.forward * 10;

            shootTime = 0f;
        }
    }
}
