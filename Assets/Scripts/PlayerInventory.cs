using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    public GameOverScript gameOverScript;
    public int NumberOfPapers {get; private set; }

    public UnityEvent<PlayerInventory> OnPaperCollected;

    public void PaperCollected()
    {
        NumberOfPapers++;
        if(NumberOfPapers == 3)
        {
            GameOver();
        }
        OnPaperCollected.Invoke(this);
    }

    public void GameOver()
    {
        gameOverScript.Setup();
    }
}
