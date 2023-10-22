using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int lives = 3;
    private int score = 0;

    public TMP_Text lbl_Score;
    public TMP_Text lbl_Lives;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddScore()
    {
        score++;
        lbl_Score.text = "Score: " + score;
    }
    
    public void TakeDamage()
    {
        lives--;
        lbl_Lives.text = "Lives: " + lives;
    }
}
