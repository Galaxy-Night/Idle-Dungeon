using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private GameObject menu;
    [SerializeField]
    private GameObject openButton;

    public void OnMenuOpen()
    {
        openButton.SetActive(false);
        menu.SetActive(true);
        GetComponent<Game>().Pause();
    }

    public void OnMenuClose() {
        openButton.SetActive(true);
        menu.SetActive(false);
        GetComponent<Game>().Unpause();
    }
    public void OnLeaveClick() {
        SceneManager.LoadScene("LeaveDungeon");
	}
}
