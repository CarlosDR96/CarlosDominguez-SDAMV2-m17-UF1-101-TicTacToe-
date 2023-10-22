using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isCubeTurn = true;
    public bool win = false;
    public TextMeshProUGUI label;
    public Cell[] cells;
    public GameObject restartButton;
    public GameObject backToMenuButton;
    // Start is called before the first frame update
    public AudioClip clipWin;
    public AudioClip clipDraw;
    public int flag;
    private bool waitingForAIMove = false;

    void Start()
    {
        ChangeTurn();
        restartButton.SetActive(false);
        backToMenuButton.SetActive(false);
        flag = PlayerPrefs.GetInt("AI", 1);
        Debug.Log("FLAG: "+ flag );
    }

    // Suponiendo que las cells se disponen de la siguiente forma:
    // 0 | 1 | 2
    // 3 | 4 | 5
    // 6 | 7 | 8

    public void CheckWinner()
    {
        bool isDraw = true;

        // Revisa las filas
        for (int i = 0; i < 9; i += 3)
        {
            if (cells[i].status != 0 && cells[i].status == cells[i + 1].status && cells[i + 1].status == cells[i + 2].status)
            {
                DeclareWinner(cells[i].status);
                win = true;
                return;
            }
            if (cells[i].status == 0 || cells[i + 1].status == 0 || cells[i + 2].status == 0) isDraw = false;
        }

        // Revisa las columnas
        for (int i = 0; i < 3; i++)
        {
            if (cells[i].status != 0 && cells[i].status == cells[i + 3].status && cells[i + 3].status == cells[i + 6].status)
            {
                DeclareWinner(cells[i].status);
                win = true;
                return;
            }
        }

        // Revisa las diagonales
        if (cells[0].status != 0 && cells[0].status == cells[4].status && cells[4].status == cells[8].status)
        {
            DeclareWinner(cells[0].status);
            win = true;
            return;
        }

        if (cells[2].status != 0 && cells[2].status == cells[4].status && cells[4].status == cells[6].status)
        {
            DeclareWinner(cells[2].status);
            win = true;
            return;
        }

        // Si todas las celdas están llenas y no hay ganador, entonces es un empate.
        if (isDraw)
        {
            label.text = "It's a draw!";
            win = true;
            SetupGameFinished(false);
            
        }
    }

    public void ChangeTurn()
    {
        isCubeTurn = !isCubeTurn;
        if (isCubeTurn)
        {
            if(flag == 1)
            {
                label.text = "Cube's turn (Player)";
            }
            else
            {
                label.text = "Cube's turn (Player1)";
            }
               
        }
        else
        {
            if(flag == 1)
            {
                label.text = "sphere's turn (IA)";
            }
            else
            {
                label.text = "Sphere's turn (Player2)";
            }
               
        }
    }

    void DeclareWinner(int status)
    {
        if (status == 1)
        {
            label.text = "Sphere is the winner";   
        }
        else
        {
            label.text = "Cube is the winner";   
        }

        SetupGameFinished(true);
        
    }



    // Update is called once per frame
    void Update()
    {
        if (!isCubeTurn && flag == 1 && !waitingForAIMove)
        {
            // Llama a la función MakeDelayedMove después de 5 segundos.
            waitingForAIMove= true;
            Invoke("MakeDelayedMove", 2f);
        }
    }

    void MakeDelayedMove()
    {
        if (!isCubeTurn && flag == 1 && !win)
        {
            // Suponiendo que has definido una función GetRandomCellIndex() que devuelve un índice de celda al azar.
            int randomCellIndex = GetRandomCellIndex();

            // Asegúrate de que el índice esté dentro del rango válido.
            if (randomCellIndex >= 0 && randomCellIndex < cells.Length)
            {
                // Simula un clic en la celda al azar.
                cells[randomCellIndex].OnMouseDown();
                if (cells[randomCellIndex].status != 0) { 
                    waitingForAIMove= false;
                }
                
            }
        }
    }

    int GetRandomCellIndex()
    {
        // Devuelve un índice de celda al azar.
        return Random.Range(0, cells.Length);
    }

    public void RestartGame()
    {
        Debug.Log("RESTART");
        SceneManager.LoadScene(1);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void SetupGameFinished(bool winner)
    {
        restartButton.SetActive(true);
        backToMenuButton.SetActive(true);
        if (winner)
        {
            GetComponent<AudioSource>().PlayOneShot(clipWin);
        }
        else
        {
            GetComponent<AudioSource>().PlayOneShot(clipDraw);    
        }
        
    }
}
