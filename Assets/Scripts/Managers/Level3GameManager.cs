using UnityEngine;

public class Level3GameManager : MonoBehaviour
{
    public static Level3GameManager Instance { get; private set; }

    [Header("Game State")]
    public float totalTime = 720f;
    private float remainingTime;
    private bool levelStarted = false;
    public bool lockdownActive = false;
    private bool gameEnded = false;

    [Header("Collectibles")]
    public int totalCoins = 20;
    private int coinsCollected = 0;
    public bool hasKeycard = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        remainingTime = totalTime;
        
        GameObject zombieSystem = GameObject.Find("Level3_ZombieSystem");
        if (zombieSystem != null)
        {
            foreach (Transform child in zombieSystem.transform) child.gameObject.SetActive(false);
        }
        
        if (Level3HUDController.Instance != null)
        {
            Level3HUDController.Instance.UpdateTimer(remainingTime);
            Level3HUDController.Instance.UpdateCoins(coinsCollected, totalCoins);
            Level3HUDController.Instance.UpdateObjective("Entra al hospital");
        }

        if (Level3AudioManager.Instance != null) {
            Level3AudioManager.Instance.PlayMusicStart();
        }
    }

    private void Update()
    {
        if (levelStarted && !gameEnded)
        {
            UpdateTimer();
        }
    }

    private void UpdateTimer()
    {
        remainingTime -= Time.deltaTime;
        if (Level3HUDController.Instance != null) Level3HUDController.Instance.UpdateTimer(remainingTime);

        if (remainingTime <= 0)
        {
            remainingTime = 0;
            LoseByTime();
        }
    }

    public void StartLevel()
    {
        if (levelStarted) return;
        levelStarted = true;
    }

    public void ActivateLockdown()
    {
        if (lockdownActive) return;
        
        StartLevel();
        lockdownActive = true;
        
        HospitalDoorController[] doors = FindObjectsOfType<HospitalDoorController>();
        foreach (var door in doors) door.CloseDoor();

        GameObject zombieSystem = GameObject.Find("Level3_ZombieSystem");
        if (zombieSystem != null)
        {
            foreach (Transform child in zombieSystem.transform) child.gameObject.SetActive(true);
        }

        KeycardSpawner spawner = FindObjectOfType<KeycardSpawner>();
        if (spawner != null) spawner.SpawnKeycard();
        
        if (Level3HUDController.Instance != null)
        {
            Level3HUDController.Instance.ShowMessage("¡CUARENTENA ACTIVADA!");
            Level3HUDController.Instance.UpdateObjective("Busca la tarjeta de acceso para salir");
        }

        if (Level3AudioManager.Instance != null) {
            Level3AudioManager.Instance.PlayAlarm();
            Level3AudioManager.Instance.PlayLockdownMusic();
        }
    }

    public void CollectCoin()
    {
        coinsCollected++;
        if (Level3HUDController.Instance != null) Level3HUDController.Instance.UpdateCoins(coinsCollected, totalCoins);
        if (Level3AudioManager.Instance != null) Level3AudioManager.Instance.PlayCoinSound();
    }

    public void CollectKeycard()
    {
        hasKeycard = true;
        if (Level3HUDController.Instance != null) Level3HUDController.Instance.ShowMessage("¡Tarjeta recogida! Diríjase a la puerta.");
        if (Level3AudioManager.Instance != null) {
            Level3AudioManager.Instance.PlayKeycardSound();
            Level3AudioManager.Instance.PlayAlarm();
        }
    }

    public void RegisterTotalCoins(int count)
    {
        totalCoins = count;
        if (Level3HUDController.Instance != null) Level3HUDController.Instance.UpdateCoins(coinsCollected, totalCoins);
    }

    public void TryExit()
    {
        if (hasKeycard) WinLevel();
        else if (Level3HUDController.Instance != null) Level3HUDController.Instance.ShowMessage("NECESITAS LA TARJETA DE ACCESO");
    }

    public void WinLevel()
    {
        if (gameEnded) return;
        gameEnded = true;
        
        int finalHealth = 0;
        PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
        if (playerHealth != null) finalHealth = playerHealth.currentHealth;

        if (Level3HUDController.Instance != null) Level3HUDController.Instance.ShowVictory(coinsCollected, totalCoins, finalHealth, totalTime - remainingTime);
        EndGame();
    }

    public void LoseByDeath()
    {
        if (gameEnded) return;
        gameEnded = true;
        if (Level3HUDController.Instance != null) Level3HUDController.Instance.ShowDefeat("HAS MUERTO", "Fuiste devorado por los zombies");
        EndGame();
    }

    public void LoseByTime()
    {
        if (gameEnded) return;
        gameEnded = true;
        if (Level3HUDController.Instance != null) Level3HUDController.Instance.ShowDefeat("TIEMPO AGOTADO", "El gas toxico lleno el hospital");
        EndGame();
    }

    private void EndGame()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}