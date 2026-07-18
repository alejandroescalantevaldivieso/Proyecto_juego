using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class Level3HUDController : MonoBehaviour
{
    public static Level3HUDController Instance { get; private set; }

    [Header("UI References")]
    public TMP_Text healthText;
    public Image healthBarFill;
    public TMP_Text timerText;
    public TMP_Text coinsText;
    public TMP_Text keycardText;
    public TMP_Text objectiveText;
    public TMP_Text centerMessageText;
    
    [Header("Panels")]
    public GameObject victoryPanel;
    public GameObject defeatPanel;
    
    [Header("Victory Texts")]
    public TMP_Text victoryStatsText;
    
    [Header("Defeat Texts")]
    public TMP_Text defeatTitleText;
    public TMP_Text defeatSubtitleText;

    private Coroutine messageCoroutine;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (victoryPanel != null) victoryPanel.SetActive(false);
        if (defeatPanel != null) defeatPanel.SetActive(false);
        if (centerMessageText != null) centerMessageText.gameObject.SetActive(false);
        
        PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.OnHealthChanged += UpdateHealth;
            UpdateHealth(playerHealth.currentHealth, playerHealth.maxHealth);
        }
    }

    public void UpdateHealth(int current, int max)
    {
        if (healthText != null)
        {
            healthText.text = $"{current}/{max}";
        }
        
        if (healthBarFill != null)
        {
            float fillAmount = (float)current / max;
            healthBarFill.fillAmount = fillAmount;
            
            if (fillAmount > 0.5f)
            {
                healthBarFill.color = Color.Lerp(Color.yellow, Color.green, (fillAmount - 0.5f) * 2f);
            }
            else
            {
                healthBarFill.color = Color.Lerp(Color.red, Color.yellow, fillAmount * 2f);
            }
        }
    }

    public void UpdateTimer(float seconds)
    {
        if (timerText == null) return;
        
        int mins = Mathf.FloorToInt(seconds / 60);
        int secs = Mathf.FloorToInt(seconds % 60);
        
        timerText.text = string.Format("{0:00}:{1:00}", mins, secs);
        
        if (seconds < 60f)
        {
            timerText.color = Color.red;
        }
        else
        {
            timerText.color = Color.white;
        }
    }

    public void UpdateCoins(int collected, int total)
    {
        if (coinsText != null)
        {
            coinsText.text = $"MONEDAS: {collected} / {total}";
        }
    }

    public void UpdateKeycard(bool found)
    {
        if (keycardText != null)
        {
            if (found)
            {
                keycardText.text = "<color=#00FF00>TARJETA: ADQUIRIDA</color>";
            }
            else
            {
                keycardText.text = "<color=#FF0000>TARJETA: NO ENCONTRADA</color>";
            }
        }
    }

    public void UpdateObjective(string text)
    {
        if (objectiveText != null)
        {
            objectiveText.text = text;
        }
    }

    public void ShowMessage(string text)
    {
        if (centerMessageText == null) return;
        
        if (messageCoroutine != null)
        {
            StopCoroutine(messageCoroutine);
        }
        
        messageCoroutine = StartCoroutine(ShowMessageRoutine(text));
    }

    private IEnumerator ShowMessageRoutine(string text)
    {
        centerMessageText.text = text;
        centerMessageText.gameObject.SetActive(true);
        
        yield return new WaitForSeconds(3f);
        
        centerMessageText.gameObject.SetActive(false);
    }

    public void ShowVictory(int coins, int totalCoins, int health, float timeTaken)
    {
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true);
        }
        
        if (victoryStatsText != null)
        {
            int mins = Mathf.FloorToInt(timeTaken / 60);
            int secs = Mathf.FloorToInt(timeTaken % 60);
            string timeStr = string.Format("{0:00}:{1:00}", mins, secs);
            
            victoryStatsText.text = $"Vida Restante: {health}%\n" +
                                    $"Monedas: {coins}/{totalCoins}\n" +
                                    $"Tiempo: {timeStr}";
        }
    }

    public void ShowDefeat(string title, string subtitle)
    {
        if (defeatPanel != null)
        {
            defeatPanel.SetActive(true);
        }
        
        if (defeatTitleText != null)
        {
            defeatTitleText.text = title;
        }
        
        if (defeatSubtitleText != null)
        {
            defeatSubtitleText.text = subtitle;
        }
    }
}