using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public enum GameState { Play, Pause, MainMenu }
    public GameState gameState;

    public GameObject pauzeCanvas;

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            gameState = GameState.MainMenu;
        }
        gameState = GameState.Play;
    }

    private void Update()
    {
        CheckState();

        if (Input.GetKeyDown(KeyCode.Backspace) && (FindObjectOfType<PhotoCapture>().cameraOn == false && FindObjectOfType<PhotoCapture>().viewingPhoto == false))
        {
            OnPause();
        }
    }

    void CheckState()
    {
        switch (gameState)
        {
            case GameState.Play: PlayBehaviour(); break;
            case GameState.Pause: PauseBehaviour(); break;
            case GameState.MainMenu: MainMenuBehaviour(); break;
        }
    }

    public void LoadScene(int i)
    {
        if (i == 0)
        {
            gameState = GameState.MainMenu;
        }
        gameState = GameState.Play;
        SceneManager.LoadScene(i);
    }

    void PlayBehaviour()
    {
        // Time.timeScale = 1f;
        FindObjectOfType<PlayerController>().canMove = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void MainMenuBehaviour()
    {
       // Time.timeScale = 1f;
        Cursor.visible = true;
    }

    void PauseBehaviour()
    {
        //Time.timeScale = 0f;
        FindObjectOfType<PlayerController>().canMove = false;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void EnableMenu(GameObject targetMenu)
    {
        targetMenu.SetActive(true);
    }

    public void DisableMenu(GameObject targetMenu)
    {
        targetMenu.SetActive(false);
    }

    public void OnPause()
    {
        pauzeCanvas.SetActive(true);
        gameState = GameState.Pause;
    }

    public void OnResume(GameObject canvas)
    {
        canvas.SetActive(false);
        gameState = GameState.Play;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
