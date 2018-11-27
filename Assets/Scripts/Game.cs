using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {

    [SerializeField]
    private Image[] gridImages;//images in grid

    //decides which player is playing
    public int player = 0;//0 for x & 1 for o
    public Image playerRefImage;
    public Sprite X, O;
    [SerializeField]
    private Text playerRefText;
    private string Xtext,Otext;

    public int[] result;//contains data of current play state-

	// Use this for initialization
	void Start () {
        result = new int[9];
        //initalize grid with black images & result with -1
        for (int i = 0; i < gridImages.Length; i++)
        {
            gridImages[i].color = Color.black;
            result[i] = -1;
        }
        //player=0 so X turn
        playerRefImage.sprite = X;
        //set UI text
        Xtext = "It is X's Turn.";
        Otext = "It is O's Turn.";
        playerRefText.text = Xtext;
        
	}

    private void Update()
    {
        refreshResult();
        if (verifyEndGame())
        {
            GameObject.Find("Canvas").GetComponent<GraphicRaycaster>().enabled = false;
            if (player == 0)
            {
                playerRefText.text = "O won";
            }
            else
            {
                playerRefText.text = "X won";
            }
            StartCoroutine("End");
        }
        if (boardFull())
        {
            playerRefText.text = "Draw";
            StartCoroutine("End");
        }
    }

    //function to change turn
    public void changeTurn()
    {
        if (player == 0)
        {
            player = 1;
            playerRefImage.sprite = O;
            playerRefText.text = Otext;
        }
        else
        {
            player = 0;
            playerRefImage.sprite = X;
            playerRefText.text = Xtext;
        }
    }

    private void refreshResult()
    {
        for(int i = 0; i < gridImages.Length; i++)
        {
            if (gridImages[i].sprite == O && gridImages[i].color!=Color.black)
            {
                result[i] = 1;
            }
            else if(gridImages[i].sprite == X && gridImages[i].color != Color.black)
            {
                result[i] = 0;
            }
        }
    }

    private bool verifyEndGame()
    {
        if(completeLine(0,1,2)|| completeLine(3, 4, 5) || completeLine(6, 7, 8)||completeLine(0,3,6)|| completeLine(1, 4, 7)|| completeLine(2, 5, 8)|| completeLine(0, 4, 8)|| completeLine(2, 4, 6))
        {
            return true;
        }
        return false;
    }

    private bool completeLine(int i, int j, int k)
    {
        if (result[i] == result[j] && result[i] == result[k] && result[i]!=-1)
            return true;
        return false;
    }

    IEnumerator End()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private bool boardFull()
    {
        for(int i = 0; i < result.Length; i++)
        {
            if (result[i] == -1)
                return false;
        }
        return true;
    }
}
