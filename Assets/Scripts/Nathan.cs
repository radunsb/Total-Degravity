using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nathan : MonoBehaviour
{

    public Transform _playerTransform;
    float nathanSpeed = 0.2f;
    public AudioClip _bsSoundEffect;
    public AudioSource _as;
    bool startedBS = false;
    public GameObject bs;
    public AudioClip boof;

    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, _playerTransform.position) > 15)
        {
            transform.position = Vector3.MoveTowards(transform.position, _playerTransform.position, nathanSpeed);
            nathanSpeed += 0.001f;
        }
        else if(!startedBS)
        {
            startedBS = true;
            StartCoroutine(blueScreen());
        }
    }
    IEnumerator blueScreen()
    {
        yield return new WaitForSeconds(1);
        bs.SetActive(true);
        AudioSource _musicSource = GameObject.FindGameObjectWithTag("MusicSource").GetComponent<AudioSource>();
        _musicSource.PlayOneShot(boof);
        for (int i = 0; i < 20; i++)
        {
            _as.PlayOneShot(_bsSoundEffect);
            yield return new WaitForSeconds(0.2f);
        }
        Application.Quit();
    }
}
