using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class NetGunScript : MonoBehaviour
{
    public GameObject _netProjectilePrefab;
    public Transform _netProjectileSpawnPoint;
    ObjectPool _netProjectilePool;
    Rigidbody _humanRigidbody;

    public float shootCooldown;
    public float velocityInfluence;
    float shootTime;

    Slider netProgressBar;

    // Start is called before the first frame update
    void Start()
    {
        _netProjectilePool = new ObjectPool(_netProjectilePrefab, true, 5);
        _humanRigidbody = GetComponent<Rigidbody>();
        shootTime = 3f;
        netProgressBar = GameObject.FindGameObjectWithTag("NetProgressBar").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        shootTime += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        netProgressBar.value = Mathf.Min(shootTime / 5, 1);
    }

    public void OnShoot(InputValue value)
    {
        if (shootTime >= shootCooldown)
        {
            Vector3 humanVelocity = _humanRigidbody.velocity;

            GameObject net = _netProjectilePool.GetObject();
            net.transform.position = _netProjectileSpawnPoint.position;
            net.GetComponent<Rigidbody>().velocity = (transform.forward * 120) + (humanVelocity * velocityInfluence);

            shootTime = 0f;
        }
    }
}
