using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{

    // ========== UI ELEMENTS ==========
    [Header("UI Text Elements")]
    public Text Score1;          // Pontuação Lixo
    public Text Score2;          // Pontuação Baterias
    public Text Score3;          // Pontuação Alternadores
    public Text Score4;          // Pontuação Peças
    public Text Menssage;        // Mensagens do jogo
    public Text Life;            // Vida do jogador
    public Image ExtBar;         // Barra de extintor

    [Header("UI Panels")]
    public GameObject GameOverPanel;     // Painel Game Over
    public GameObject NextLevelPanel;    // Painel Próximo Nível
    public GameObject PausePanel;        // Painel Pausa
    public GameObject InputPanel;        // Painel Controles

    // ========== GAME STATS ==========
    [Header("Pontuações Atuais")]
    public int TotalScoreTrash = 0;
    public int TotalScoreBat = 0;
    public int TotalScoreAlternator = 0;
    public int TotalScoreParts = 0;

    [Header("Vida e Extintor")]
    public int LifeNum = 5;              // Vida atual (exibida)
    public float ExtLevel = 100f;        // Nível atual do extintor
    public float ExtLevelMax = 100f;     // Nível máximo do extintor

    [Header("Controle de Animais")]
    public float AnimalDead = 0f;        // Contador de animais mortos

    [Header("Pontuações Mínimas")]
    public int MinScoreTrash = 0;
    public int MinScoreBat = 0;
    public int MinScoreAlternator = 0;
    public int MinScoreParts = 0;

    // ========== VARIÁVEIS DE CONTROLE ==========
    public static GameControl Instance;
    private bool isGameOver = false;
    private bool isPaused = false;
    private string originalMessage = "Mensagens";

    // ========== INICIALIZAÇÃO ==========
    void Awake()
    {
        // Singleton pattern - apenas uma instância
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        // Garante que o jogo está rodando
        Time.timeScale = 1f;
        isGameOver = false;
        isPaused = false;

        // Inicializa valores
        LifeNum = 5;  // Começa com 5 vidas
        ExtLevel = 100f;

        // Configura painéis
        if (GameOverPanel != null) GameOverPanel.SetActive(false);
        if (NextLevelPanel != null) NextLevelPanel.SetActive(false);
        if (PausePanel != null) PausePanel.SetActive(false);
        if (InputPanel != null) InputPanel.SetActive(false);

        // Atualiza UI inicial
        UpdateAllUI();

        // Mensagem inicial
        if (Menssage != null)
        {
            Menssage.text = originalMessage;
        }
    }

    // ========== UPDATE PRINCIPAL ==========
    void Update()
    {
        // Se está em game over ou pausado, não atualiza lógica do jogo
        if (isGameOver || isPaused) return;

        // Atualiza UI constantemente
        UpdateExtBar();
        UpdateLifeDisplay();

        // Verifica estado do jogador
        CheckPlayerStatus();

        // Controle de pausa com ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    // ========== MÉTODOS DE ATUALIZAÇÃO DE UI ==========
    public void UpdateScore(int scoreType)
    {
        switch (scoreType)
        {
            case 1:  // Lixo
                if (Score1 != null) Score1.text = TotalScoreTrash.ToString();
                break;
            case 2:  // Baterias
                if (Score2 != null) Score2.text = TotalScoreBat.ToString();
                break;
            case 3:  // Alternadores
                if (Score3 != null) Score3.text = TotalScoreAlternator.ToString();
                break;
            case 4:  // Peças
                if (Score4 != null) Score4.text = TotalScoreParts.ToString();
                break;
        }
    }

    public void UpdateMenssage(string newMessage)
    {
        if (Menssage != null)
        {
            Menssage.text = newMessage;
        }
    }

    private void UpdateExtBar()
    {
        if (ExtBar != null)
        {
            float fillAmount = Mathf.Clamp(ExtLevel / ExtLevelMax, 0f, 1f);
            ExtBar.fillAmount = fillAmount;
        }
    }

    private void UpdateLifeDisplay()
    {
        if (Life != null)
        {
            Life.text = LifeNum.ToString();
        }
    }

    private void UpdateAllUI()
    {
        UpdateScore(1);
        UpdateScore(2);
        UpdateScore(3);
        UpdateScore(4);
        UpdateExtBar();
        UpdateLifeDisplay();
    }

    // ========== VERIFICAÇÃO DE ESTADO DO JOGO ==========
    private void CheckPlayerStatus()
    {
        // Se não existe Player, game over
        if (Player.Instance == null)
        {
            StartCoroutine(TriggerGameOver());
            return;
        }

        // Atualiza LifeNum baseado na vida real do Player
        if (Player.Instance.LifeCount > 0)
        {
            LifeNum = Mathf.CeilToInt(Player.Instance.LifeCount / 10f);
        }
        else
        {
            LifeNum = 0;
        }

        // Se acabou a vida, game over
        if (LifeNum <= 0 && !isGameOver)
        {
            StartCoroutine(TriggerGameOver());
        }
    }

    // ========== SISTEMA DE GAME OVER ==========
    private IEnumerator TriggerGameOver()
    {
        if (isGameOver) yield break;

        isGameOver = true;

        // Pequeno delay para animações
        yield return new WaitForSeconds(0.8f);

        // Ativa painel de Game Over
        if (GameOverPanel != null)
        {
            GameOverPanel.SetActive(true);
            Time.timeScale = 0f;  // Pausa o jogo
        }
        else
        {
            // Fallback: se não tem painel, recarrega a cena
            Debug.LogWarning("GameOverPanel não encontrado. Reiniciando nível...");
            RestartGame(SceneManager.GetActiveScene().name);
        }
    }

    public void GameOver()
    {
        // Método público para forçar game over
        if (!isGameOver)
        {
            StartCoroutine(TriggerGameOver());
        }
    }

    // ========== SISTEMA DE PAUSA ==========
    private void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;
            if (PausePanel != null) PausePanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            if (PausePanel != null) PausePanel.SetActive(false);
            if (InputPanel != null) InputPanel.SetActive(false);
        }
    }

    // ========== MÉTODOS PÚBLICOS PARA UI ==========
    public void RestartGame(string levelName)
    {
        // IMPORTANTE: Restaura o time scale antes de trocar de cena
        Time.timeScale = 1f;
        SceneManager.LoadScene(levelName);
    }

    public void NextLevel()
    {
        if (NextLevelPanel != null)
        {
            NextLevelPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void PauseUIEnter()
    {
        if (!isGameOver && PausePanel != null)
        {
            isPaused = true;
            Time.timeScale = 0f;
            PausePanel.SetActive(true);
        }
    }

    public void ControlUIEnter()
    {
        if (PausePanel != null && InputPanel != null)
        {
            PausePanel.SetActive(false);
            InputPanel.SetActive(true);
        }
    }

    public void PauseUIExit()
    {
        if (PausePanel != null)
        {
            isPaused = false;
            Time.timeScale = 1f;
            PausePanel.SetActive(false);
            if (InputPanel != null) InputPanel.SetActive(false);
        }
    }

    public void ControlUIExit()
    {
        if (InputPanel != null)
        {
            InputPanel.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
        }
    }

    // ========== SISTEMA DE ANIMAIS ==========
    public bool VerifAnimal()
    {
        if (AnimalDead == 1f)
        {
            UpdateMenssage("Não Maltrate os animais");
            return false;
        }

        if (AnimalDead == 2f)
        {
            UpdateMenssage("Você será demitido");
            // Penalidade de vida
            if (Player.Instance != null)
            {
                Player.Instance.LifeCount = Mathf.Max(0, Player.Instance.LifeCount - 20);
            }
            return false;
        }

        if (AnimalDead >= 3f)
        {
            UpdateMenssage("Você foi demitido");
            StartCoroutine(TriggerGameOver());
            return true;
        }

        return false;
    }

    // ========== UTILITÁRIOS ==========
    public void AddExtLevel(float amount)
    {
        ExtLevel = Mathf.Clamp(ExtLevel + amount, 0f, ExtLevelMax);
        UpdateExtBar();
    }

    public void ReduceExtLevel(float amount)
    {
        ExtLevel = Mathf.Clamp(ExtLevel - amount, 0f, ExtLevelMax);
        UpdateExtBar();
    }

    public void AddLife(int amount)
    {
        LifeNum += amount;
        UpdateLifeDisplay();
    }

    public void ResetMessage()
    {
        if (Menssage != null)
        {
            Menssage.text = originalMessage;
        }
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    // ========== GETTERS PARA OUTROS SCRIPTS ==========
    public bool IsGameOver()
    {
        return isGameOver;
    }

    public bool IsPaused()
    {
        return isPaused;
    }
}