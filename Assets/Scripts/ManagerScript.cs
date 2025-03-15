using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ManagerScript : MonoBehaviour
{
    public GameObject humanPrefab;
    public PlayerInputManager inputManager;
    public Text p1Text;
    public Text p2Text;
    bool playerOneIn = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onFirstPlayerJoin()
    {
        if (playerOneIn)
        {
            onSecondPlayerJoin();
        }
        else
        {
            inputManager.playerPrefab = humanPrefab;
            p1Text.gameObject.SetActive(false);
            StartCoroutine(waitToSayPlayerIn());
        }
    }

    public void onSecondPlayerJoin()
    {
        p2Text.gameObject.SetActive(false);
    }

    IEnumerator waitToSayPlayerIn()
    {
        yield return new WaitForSeconds(1);
        playerOneIn = true;
    }
}
