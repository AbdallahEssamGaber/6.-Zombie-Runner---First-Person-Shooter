using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] Canvas gameOverCanvas;
    [SerializeField] GameObject weapons;

    bool oneTimeBitch;
    private void Start()
    {
        oneTimeBitch = true;
        gameOverCanvas.enabled = false;
    }



    void Update()
    {
        if(gameObject.transform.position.y <= -1f && oneTimeBitch)
        {
            HandleDeath();
        }
    }

    public void HandleDeath()
    {
        oneTimeBitch = false;
        gameOverCanvas.enabled = true;
        weapons.SetActive(false);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

}
