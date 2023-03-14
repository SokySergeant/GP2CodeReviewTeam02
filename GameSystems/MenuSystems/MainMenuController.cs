using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public GameObject ButtonPanel;
    public GameObject SettingsPanel;



    public void OpenSettingsPanel()
    {
        ButtonPanel.SetActive(false);
        SettingsPanel.SetActive(true);
        SettingsPanel.GetComponentInChildren<Button>().Select();
    }

    public void CloseSettingsPanel()
    {
        ButtonPanel.SetActive(true);
        SettingsPanel.SetActive(false);
        ButtonPanel.GetComponentInChildren<Button>().Select();
    }

    public void ExitGame(){
        Application.Quit();
    }

}

