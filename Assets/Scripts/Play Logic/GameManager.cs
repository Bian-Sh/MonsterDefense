using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
     WaveManager waveManager;

    public void GameOver() 
    {
        waveManager.StopCreateEnemy();
        Time.timeScale = 0;
    }


    private void Reset()
    {
        waveManager = GetComponentInChildren<WaveManager>();
        UnityEditor.EditorUtility.SetDirty(this);
    }
}
