using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public int whoseTurn;
    public int turnCount;
    int turns;
    public GameObject[] turnIcons;
    public Button[] spaces;
    public Sprite[] playerIcons;
    int[] markedSpaces = new int[9];
    public GameObject[] winningLines;
    public Text winningText;
    public GameObject winnerPanel;
    int xScore;
    int oScore;
    public Text[] ScoreText;
    public AudioSource audioScourceObject;
    public Button changeTurn;
  
	// Use this for initialization
	void Start () {

        turns = 0;
        GameStart();
	}
	
    void GameStart()
    {
        if (turns%2==0)
        {
            whoseTurn = 0;
        }
        else
        {
            whoseTurn = 1;
        }

        turnCount = 0;
        turns++;

        if (whoseTurn == 0)
        {
            turnIcons[0].SetActive(true);
            turnIcons[1].SetActive(false);
        }
        else
        {
            turnIcons[0].SetActive(false);
            turnIcons[1].SetActive(true);
        }
        for (int i = 0; i < spaces.Length; i++)
        {
            spaces[i].interactable=true;
            spaces[i].GetComponent<Image>().sprite = null;
        }

        for(int i = 0; i < markedSpaces.Length; i++)
        {
            markedSpaces[i] = -101;
        }

        for(int i = 0; i < winningLines.Length; i++)
        {
            winningLines[i].SetActive(false);
        }

        winnerPanel.SetActive(false);

        changeTurn.gameObject.SetActive(true);
    }
	// Update is called once per frame
	void Update () {

	}

    public void displayPlayerIcon(int number)
    {
        spaces[number].image.sprite = playerIcons[whoseTurn];
        spaces[number].interactable = false;

        markedSpaces[number] = whoseTurn + 1;
        turnCount++;

        if (turnCount == 1)
        {
            changeTurn.gameObject.SetActive(false);
        }

        if (turnCount > 4)
        {
            bool hasWinner = winnerCheck();

            if (turnCount == 9 && hasWinner == false)
            {
                Cat();
            }
        }

        if (whoseTurn == 0)
        {
            whoseTurn = 1;
            turnIcons[1].SetActive(true);
            turnIcons[0].SetActive(false);
        }
        else
        {
            whoseTurn = 0;
            turnIcons[0].SetActive(true);
            turnIcons[1].SetActive(false);
        }
    }

    bool winnerCheck()
    {
        int[] ways = new int[8];

        ways[0] = markedSpaces[0] + markedSpaces[1] + markedSpaces[2];
        ways[1] = markedSpaces[3] + markedSpaces[4] + markedSpaces[5];
        ways[2] = markedSpaces[6] + markedSpaces[7] + markedSpaces[8];
        ways[3] = markedSpaces[0] + markedSpaces[3] + markedSpaces[6];
        ways[4] = markedSpaces[1] + markedSpaces[4] + markedSpaces[7];
        ways[5] = markedSpaces[2] + markedSpaces[5] + markedSpaces[8];
        ways[6] = markedSpaces[0] + markedSpaces[4] + markedSpaces[8];
        ways[7] = markedSpaces[2] + markedSpaces[4] + markedSpaces[6];

        for (int i = 0; i < ways.Length; i++)
        {
            if (ways[i] == 3 || ways[i] == 6)
            {
                displayWinner(i);
                return true;
            }
        }
        return false;
    }

    void displayWinner(int lineIndex)
    {
        winnerPanel.SetActive(true);

        if (whoseTurn == 0)
        {
            xScore++;
            winningText.text="Player X wins!";
            ScoreText[0].text = xScore.ToString();
        }
        else
        {
            oScore++;
            winningText.text = "Player O wins!";
            ScoreText[1].text = oScore.ToString();
        }

        winningLines[lineIndex].SetActive(true);

    }

    public void rematch()
    {
        GameStart();
    }

    public void ResetMatch()
    {
        oScore = 0;
        xScore = 0;
        ScoreText[0].text = xScore.ToString();
        ScoreText[1].text = oScore.ToString();
        turns = 0;
        GameStart();
    }

    void Cat()
    {
        winningText.text = "Draw";
        winnerPanel.SetActive(true);
    }

    public void playAudio()
    {
        audioScourceObject.Play();
    }

    public void ChangeTurn()
    {
        if (whoseTurn == 0)
        {
            whoseTurn = 1;
            turnIcons[0].SetActive(false);
            turnIcons[1].SetActive(true);
        }
        else
        {
            whoseTurn = 0;
            turnIcons[0].SetActive(true);
            turnIcons[1].SetActive(false);
        }
    }
}
