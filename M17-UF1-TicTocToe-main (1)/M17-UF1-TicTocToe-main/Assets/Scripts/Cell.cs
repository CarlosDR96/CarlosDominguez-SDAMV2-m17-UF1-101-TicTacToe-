using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public int status; //0: none, 1:sphere, 2:cube
    public GameManager gameManger;
    public GameObject sphere;
    public GameObject cube;
    // Start is called before the first frame update
    void Start()
    {
        sphere.SetActive(false);
        cube.SetActive(false);
        status = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnMouseDown()
    {
        if (cube.activeSelf==true ||  sphere.activeSelf == true)
        {
            //nos vamos si cubo y sphere activos
            Debug.Log("Eips que la cel·la ja està clicada");
            return;
        }
        
        if (gameManger.isCubeTurn == true)
        {
            status = 2;
            cube.SetActive(true);
            sphere.SetActive(false);
            gameManger.ChangeTurn();
        }
        else
        {
            status = 1;
            sphere.SetActive(true);    
            cube.SetActive(false);
            gameManger.ChangeTurn();
        }
        
        gameManger.CheckWinner();
    }
}
