using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    GameObject menuPanel;

    [SerializeField]
    AudioMixer mixer;

    [SerializeField]
    GameObject resolutionsDropdown;

    Resolution[] resolutions;

    bool inMenu = false;
    bool keyPressed = false;
    float cooldown = 1f;

    // Start is called before the first frame update
    void Start()
    {
        PopulateResolutions();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!keyPressed && inMenu)
                Resume();
            else
                Pause();

            StartCoroutine(KeyPressCooldown());
        }
    }

    IEnumerator KeyPressCooldown()
    {
        keyPressed = true;
        yield return new WaitForSeconds(cooldown);
        keyPressed = false;
    }

    public void Resume()
    {
        inMenu = false;
        Time.timeScale = 1.0f;
        menuPanel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    public void Pause()
    {
        inMenu = true;
        Time.timeScale = 0f;
        menuPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void PopulateResolutions()
    {        
        resolutionsDropdown.GetComponent<TMP_Dropdown>().ClearOptions();

        List<string> options = new List<string>();

        resolutions = Screen.resolutions;

        int index = 0, current = 0;
        foreach (Resolution resolution in resolutions)
        {
            options.Add(resolution.width + "x" + resolution.height);

            if (resolution.width == Screen.currentResolution.width &&
                resolution.height == Screen.currentResolution.height)
                current = index;

            index += 1;
        }

        resolutionsDropdown.GetComponent<TMP_Dropdown>().AddOptions(options);
        resolutionsDropdown.GetComponent<TMP_Dropdown>().value = current;
        resolutionsDropdown.GetComponent<TMP_Dropdown>().RefreshShownValue();
    }


    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void SetVolume(float volume)
    {
        Debug.Log("volume: " + volume);
        mixer.SetFloat("volume", volume);
    }

    public void SetQuality(int index)
    {

        QualitySettings.SetQualityLevel(index);
    }

    public void ToggleFullScreen(bool fullScreen)
    {
        Screen.fullScreen = fullScreen;
    }

    public void SetResolution(int index)
    {
        Resolution res = resolutions[index];

        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }

    public void OpenSettings()
    {
        menuPanel.SetActive(true);
    }
}
