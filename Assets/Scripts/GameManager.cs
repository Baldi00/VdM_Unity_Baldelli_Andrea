using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[DisallowMultipleComponent]
public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;
    public static GameManager Instance { get => _instance; }

    [SerializeField]
    private string saveFileName;
    [SerializeField]
    private string blackBoardTextureFileName;

    [SerializeField]
    private Transform player;

    [SerializeField]
    private ComputerInteraction[] pcs;
    [SerializeField]
    private DoorInteraction[] doors;
    [SerializeField]
    private TVInteraction[] tvs;
    [SerializeField]
    private MicrowaveInteraction microwave;
    [SerializeField]
    private HandWasherInteraction handWasher;
    [SerializeField]
    private ExplosiveComputerInteraction explosivePc;

    [SerializeField]
    private GameObject standardDrinkPrefab;
    [SerializeField]
    private GameObject magicDrinkPrefab;

    [SerializeField]
    private RenderTexture blackboardRenderTexture;
    [SerializeField]
    private Renderer savedBlackboardTextureRenderer;

    [SerializeField]
    private AudioMixer audioMixer;
    [SerializeField]
    private PauseMenuManager pauseMenuManager;

    private GameState currentGameState;
    private Dictionary<Transform, DrinkInfo> drinks;
    private Texture2D blackboardTexture2d;

    public bool IsPaused;

    void Awake()
    {
        if (_instance == null)
            _instance = this;
        else if (_instance != this)
            Destroy(gameObject);

        drinks = new Dictionary<Transform, DrinkInfo>();
    }

    void Start()
    {
        LoadSavesIfPresent();
    }

    [ContextMenu("Delete Saves")]
    public void DeleteSaves()
    {
        SavesManager.DeleteSaves(saveFileName);
        SavesManager.DeleteSaves(blackBoardTextureFileName);
    }

    public void Save()
    {
        currentGameState.playerPosition = player.position;
        SaveDrinkPositions();
        SavesManager.SaveState(saveFileName, currentGameState);

        blackboardTexture2d = blackboardRenderTexture.ToTexture2D();
        SavesManager.SaveBlackboardTexture(blackBoardTextureFileName, blackboardTexture2d);
    }

    public void LoadSavesIfPresent()
    {
        if (SavesManager.IsSavePresent(SavesManager.GetSaveFilePath(saveFileName)))
        {
            currentGameState = SavesManager.LoadState(saveFileName);
            ApplySavesInGame();
        }
        else
        {
            currentGameState = new GameState(doors.Length, pcs.Length, tvs.Length);
            currentGameState.ambientVolume = 1f;
            currentGameState.musicVolume = 1f;
            currentGameState.effectsVolume = 1f;
        }

        if (SavesManager.IsSavePresent(SavesManager.GetSaveFilePath(blackBoardTextureFileName)))
        {
            blackboardTexture2d = blackboardRenderTexture.ToTexture2D();
            SavesManager.LoadBlackboardTexture(blackBoardTextureFileName, ref blackboardTexture2d);
            savedBlackboardTextureRenderer.material.mainTexture = blackboardTexture2d;
            savedBlackboardTextureRenderer.material.color = Color.white;
        }
    }

    public void SetDoorState(DoorInteraction door, bool state)
    {
        currentGameState.SetDoorState(System.Array.IndexOf<DoorInteraction>(doors, door), state);
    }

    public void SetPCState(ComputerInteraction pc, bool state)
    {
        currentGameState.SetPCState(System.Array.IndexOf<ComputerInteraction>(pcs, pc), state);
    }

    public void SetTVState(TVInteraction tv, bool state)
    {
        currentGameState.SetTVState(System.Array.IndexOf<TVInteraction>(tvs, tv), state);
    }

    public void SetMicrowaveState(bool state)
    {
        currentGameState.microwaveState = state;
    }

    public void SetHandWasherState(bool state)
    {
        currentGameState.handWasherState = state;
    }

    public void SetAmbientVolume(float ambientVolume)
    {
        currentGameState.ambientVolume = ambientVolume;
    }

    public void SetMusicVolume(float musicVolume)
    {
        currentGameState.musicVolume = musicVolume;
    }

    public void SetEffectsVolume(float effectsVolume)
    {
        currentGameState.effectsVolume = effectsVolume;
    }

    public void AddDrink(Transform drinkTransform, DrinkInfo drinkInfo)
    {
        drinks.Add(drinkTransform, drinkInfo);
    }

    public void ClearSavedBlackboardTexture()
    {
        savedBlackboardTextureRenderer.material.color = Color.black;
    }

    public void SetBurntPCState(bool burnt)
    {
        currentGameState.pcBurnt = burnt;
    }

    private void ApplySavesInGame()
    {
        player.position = currentGameState.playerPosition;

        for (int i = 0; i < currentGameState.pcsState.Length; i++)
            pcs[i].SetPCState(currentGameState.pcsState[i]);

        for (int i = 0; i < currentGameState.doorsState.Length; i++)
            doors[i].SetDoorState(currentGameState.doorsState[i]);

        for (int i = 0; i < currentGameState.tvsState.Length; i++)
            tvs[i].SetTVState(currentGameState.tvsState[i]);

        microwave.SetState(currentGameState.microwaveState);
        handWasher.SetState(currentGameState.handWasherState);

        foreach(DrinkInfo drinkInfo in currentGameState.drinksInfo)
        {
            GameObject drink;
            if(drinkInfo.isMagic)
                drink = Instantiate(magicDrinkPrefab, drinkInfo.position, drinkInfo.rotation);
            else
                drink = Instantiate(standardDrinkPrefab, drinkInfo.position, drinkInfo.rotation);

            drink.GetComponent<Renderer>().material.color = drinkInfo.color;

            if (drinkInfo.isMagic)
                drink.GetComponent<Light>().color = drinkInfo.color;

            AddDrink(drink.transform, drinkInfo);
        }

        audioMixer.SetFloat("AmbientVolume", Mathf.Log10(currentGameState.ambientVolume) * 20);
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(currentGameState.musicVolume) * 20);
        audioMixer.SetFloat("EffectsVolume", Mathf.Log10(currentGameState.effectsVolume) * 20);

        pauseMenuManager.SetAmbientVolumeSliderValue(currentGameState.ambientVolume);
        pauseMenuManager.SetMusicVolumeSliderValue(currentGameState.musicVolume);
        pauseMenuManager.SetEffectsVolumeSliderValue(currentGameState.effectsVolume);

        explosivePc.SetPCState(currentGameState.pcBurnt);
    }

    private void SaveDrinkPositions()
    {
        foreach(KeyValuePair<Transform, DrinkInfo> drink in drinks)
        {
            DrinkInfo drinkInfo = drink.Value;
            drinkInfo.position = drink.Key.position;
            drinkInfo.rotation = drink.Key.rotation;
            currentGameState.AddDrink(drinkInfo);
        }
    }
}
