using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{

    public Text Score1;
    public Text Score2;
    public Text Score3;
    public Text Score4;
    public Text Menssage;
    public Text Life;
    public Image ExtBar;
    public static GameControl Instance;

    public GameObject GameOverPanel;
    public GameObject NextLevelPanel;
    public GameObject PausePanel;
    public GameObject InputPanel;

    public int TotalScoreTrash;
    public int TotalScoreBat;
    public int TotalScoreAlternator;
    public int TotalScoreParts;

    public int LifeNum;
    public float ExtLevel = 1;
    public float ExtLevelMax = 100;
    public float AnimalDead = 0;

    public int MinScoreTrash;
    public int MinScoreBat;
    public int MinScoreAlternator;
    public int MinScoreParts;
    void Start()
    {
        //Iniciando variável de instancia
        Instance = this;
    }

    public void Update()
    {
        UpdateExtBar();
        UpdateLife();

        if (Player.Instance.LifeCount > 0)
        {
            LifeNum = Player.Instance.LifeCount / 10;
        }
        else
        {
            LifeNum = 0;
        }

        if (LifeNum == 0)
        {
            GameOver();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUIEnter();
        }
    }

    public void UpdateScore(int num)
    {
        if (Score1 != null)
        {
            if (num == 1)
            {
                Score1.text = TotalScoreTrash.ToString();
            }
        }

        if (Score2 != null)
        {
            if (num == 2)
            {
                Score2.text = TotalScoreBat.ToString();
            }
        }

        if (Score3 != null)
        {
            if (num == 3)
            {
                Score3.text = TotalScoreAlternator.ToString();
            }
        }

        if (Score4 != null)
        {
            if (num == 4)
            {
                Score4.text = TotalScoreParts.ToString();
            }
        }
    }

    public void UpdateMenssage(string Menssage_Text)
    {
        Menssage.text = Menssage_Text;
    }

    public void UpdateExtBar()
    {
        ExtBar.fillAmount = ExtLevel / ExtLevelMax;
    }

    public void UpdateLife()
    {
        Life.text = LifeNum.ToString();
    }

    public bool VerifAnimal()
    {

        if (AnimalDead == 1)
        {
            Menssage.text = "Nao Maltrate os animais";
        }

        if (AnimalDead == 2)
        {
            Menssage.text = "Voce sera demitido";
            UpdateLife();

        }

        if (AnimalDead == 3)
        {
            Menssage.text = "Voce foi demitido";
            return true;
        }

        return false;
    }

    public void GameOver()
    {
        GameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void NextLevel()
    {
        NextLevelPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void RestartGame(string LevelName)
    {
        SceneManager.LoadScene(LevelName);
        Time.timeScale = 1;
    }

    public void PauseUIEnter()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ControlUIEnter()
    {
        InputPanel.SetActive(true);
        PausePanel.SetActive(false);

    }

    public void PauseUIExit()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1;

    }

    public void ControlUIExit()
    {
        InputPanel.SetActive(false);
        Time.timeScale = 1;

    }

    public void ExitGame()
    {
        Debug.Log("Fechar jogo");
        Application.Quit();

    }

}
