using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadTheGame : MonoBehaviour
{
  public void LoadGame()
  {
    SceneManager.LoadSceneAsync("Game");
  }
}
