using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public static SettingsMenu instance;
    Resolution[] resolutions;

    #region PUBLIC UI ELEMENTS
    // AUDIO
    public Slider masterSlider;
    public Slider sFXSlider;
    public Slider musicSlider;

    // VIDEO
    public Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;
    public Dropdown graphicsDropdown;
    #endregion

    #region PRIVATE GAMEOBJECTS
    private AudioManager audioManager;
    private GameObject audioMenu;
    private GameObject settingMenu;
    private GameObject pauseMenu;
    private GameObject videoMenu;
    #endregion

    #region PRIVATE FIELDS
    // AUDIO
    private static float masterVolume = 0.5f;
    private static float sFXVolume = 0.5f;
    private static float musicVolume = 0.5f;

    // VIDEO
    private static bool fullscreen = true;
    private static int graphicsQuality = 3;
    #endregion

    private void Awake()
    {
        Cursor.visible = true;

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audioMenu = transform.Find("AudioMenu").gameObject;
        settingMenu = transform.Find("SettingsMenu").gameObject;
        pauseMenu = transform.Find("PauseMenu").gameObject;
        videoMenu = transform.Find("VideoMenu").gameObject;

        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width
                && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    private void Start()
    {
        // AUDIO
        masterSlider.value = masterVolume;
        sFXSlider.value = sFXVolume;
        musicSlider.value = musicVolume;

        //VIDEO
        fullscreenToggle.isOn = fullscreen;
        graphicsDropdown.value = graphicsQuality;
    }

    public void SetMasterVolume(float volume)
    {
        foreach (Sound sound in audioManager.sounds)
        {
            if (sound.volume != 0)
            {
                sound.volume = volume;
                if (sound.name == "MageAttack" || sound.name == "ArcherAttack")
                {
                    sound.volume /= 2;
                }
            }
        }

        audioManager.SetSounds();

        audioMenu.transform.Find("MenuPanel").Find("MasterVolume").Find("VolumeText").gameObject.GetComponent<Text>().text = (volume * 100).ToString();
        masterVolume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        foreach (Sound sound in audioManager.sounds)
        {
            if (sound.name != "Theme")
            {
                sound.volume = volume;
                if (sound.name == "MageAttack" || sound.name == "ArcherAttack")
                {
                    sound.volume /= 2;
                }
            }
        }

        audioManager.SetSounds();

        audioMenu.transform.Find("MenuPanel").Find("SFXVolume").Find("VolumeText").gameObject.GetComponent<Text>().text = (volume * 100).ToString();
        sFXVolume = volume;
    }

    public void SetMusicVolume(float volume)
    {
        foreach (Sound sound in audioManager.sounds)
        {
            if (sound.name == "Theme") sound.volume = volume;
        }

        audioManager.SetSounds();

        audioMenu.transform.Find("MenuPanel").Find("MusicVolume").Find("VolumeText").gameObject.GetComponent<Text>().text = (volume * 100).ToString();
        musicVolume = volume;
    }

    public void SetResolution (int resolutionInt)
    {
        Resolution resolution = resolutions[resolutionInt];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;

        fullscreen = isFullscreen;
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);

        graphicsQuality = qualityIndex;
    }

    public void CloseAudioMenu()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        audioMenu.SetActive(false);
    }

    public void OpenAudioMenu()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        audioMenu.SetActive(true);
    }
    public void CloseSettingsMenu()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        settingMenu.SetActive(false);
    }

    public void OpenSettingsMenu()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        settingMenu.SetActive(true);
    }

    public void CloseVideoMenu()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        videoMenu.SetActive(false);
    }

    public void OpenVideoMenu()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        videoMenu.SetActive(true);
    }

    public void Resume()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        Cursor.visible = false;
        pauseMenu.SetActive(false);
        GameMaster.ResumeGame();
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().SetSettingsOpen(false);
    }
}
