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
    private PlayerMovement player;

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
        if (SavesManager.IsSavePresent(SavesManager.GetSaveFilePath(saveFileName)))
            LoadGameStateFromSaves();
        else
            InitializeNewGameState();

        if (SavesManager.IsSavePresent(SavesManager.GetSaveFilePath(blackBoardTextureFileName)))
            LoadBlackboardTextureFromSaves();
    }

    /// <summary>
    /// Delete all saves files
    /// </summary>
    [ContextMenu("Delete Saves")]
    public void DeleteSaves()
    {
        SavesManager.DeleteSaves(saveFileName);
        SavesManager.DeleteSaves(blackBoardTextureFileName);
    }

    /// <summary>
    /// Saves the current game state and blackboard texture to file
    /// </summary>
    public void Save()
    {
        currentGameState.playerPosition = player.transform.position;
        SaveDrinkPositions();
        SavesManager.SaveState(saveFileName, currentGameState);

        blackboardTexture2d = blackboardRenderTexture.ToTexture2D();
        SavesManager.SaveBlackboardTexture(blackBoardTextureFileName, blackboardTexture2d);
    }

    /// <summary>
    /// Load game state from saves and applies them in game
    /// </summary>
    private void LoadGameStateFromSaves()
    {
        currentGameState = SavesManager.LoadState(saveFileName);
        ApplySavesInGame();
    }

    /// <summary>
    /// Initializes a new game state for the current game session
    /// </summary>
    private void InitializeNewGameState()
    {
        currentGameState = new GameState(doors.Length, pcs.Length, tvs.Length);
        currentGameState.ambientVolume = 1f;
        currentGameState.musicVolume = 1f;
        currentGameState.effectsVolume = 1f;
    }

    /// <summary>
    /// Loads the blackboard texture from the save file
    /// </summary>
    private void LoadBlackboardTextureFromSaves()
    {
        blackboardTexture2d = blackboardRenderTexture.ToTexture2D();
        SavesManager.LoadBlackboardTexture(blackBoardTextureFileName, ref blackboardTexture2d);
        savedBlackboardTextureRenderer.material.mainTexture = blackboardTexture2d;
        savedBlackboardTextureRenderer.material.color = Color.white;
    }

    /// <summary>
    /// Notifies the game manager that a door state is changed and saves it in the current session game state
    /// </summary>
    public void SetDoorState(DoorInteraction door, bool state)
    {
        currentGameState.SetDoorState(System.Array.IndexOf<DoorInteraction>(doors, door), state);
    }

    /// <summary>
    /// Notifies the game manager that a PC state is changed and saves it in the current session game state
    /// </summary>
    public void SetPCState(ComputerInteraction pc, bool state)
    {
        currentGameState.SetPCState(System.Array.IndexOf<ComputerInteraction>(pcs, pc), state);
    }

    /// <summary>
    /// Notifies the game manager that a TV state is changed and saves it in the current session game state
    /// </summary>
    public void SetTVState(TVInteraction tv, bool state)
    {
        currentGameState.SetTVState(System.Array.IndexOf<TVInteraction>(tvs, tv), state);
    }

    /// <summary>
    /// Notifies the game manager that the microwave state is changed and saves it in the current session game state
    /// </summary>
    public void SetMicrowaveState(bool state)
    {
        currentGameState.microwaveState = state;
    }

    /// <summary>
    /// Notifies the game manager that the handwasher state is changed and saves it in the current session game state
    /// </summary>
    public void SetHandWasherState(bool state)
    {
        currentGameState.handWasherState = state;
    }

    /// <summary>
    /// Notifies the game manager that the burnt PC state is changed and saves it in the current session game state
    /// </summary>
    public void SetBurntPCState(bool burnt)
    {
        currentGameState.pcBurnt = burnt;
    }

    /// <summary>
    /// Notifies the game manager that the ambient volume is changed and saves it in the current session game state
    /// </summary>
    public void SetAmbientVolume(float ambientVolume)
    {
        currentGameState.ambientVolume = ambientVolume;
    }

    /// <summary>
    /// Notifies the game manager that the music volume is changed and saves it in the current session game state
    /// </summary>
    public void SetMusicVolume(float musicVolume)
    {
        currentGameState.musicVolume = musicVolume;
    }

    /// <summary>
    /// Notifies the game manager that the effects volume is changed and saves it in the current session game state
    /// </summary>
    public void SetEffectsVolume(float effectsVolume)
    {
        currentGameState.effectsVolume = effectsVolume;
    }

    /// <summary>
    /// Notifies the game manager that a drink has been spawned and saves it in the current drinks traking list
    /// </summary>
    public void AddDrink(Transform drinkTransform, DrinkInfo drinkInfo)
    {
        drinks.Add(drinkTransform, drinkInfo);
    }

    /// <summary>
    /// Clears the saved blackboard texture in order to disable it from rendering
    /// </summary>
    public void ClearSavedBlackboardTexture()
    {
        savedBlackboardTextureRenderer.material.color = Color.black;
    }

    /// <summary>
    /// Applies the loaded game state to the current game session
    /// </summary>
    private void ApplySavesInGame()
    {
        player.SetPlayerPosition(currentGameState.playerPosition);

        // Apply interactable objects states
        for (int i = 0; i < currentGameState.pcsState.Length; i++)
            pcs[i].SetPCState(currentGameState.pcsState[i]);

        for (int i = 0; i < currentGameState.doorsState.Length; i++)
            doors[i].SetDoorState(currentGameState.doorsState[i]);

        for (int i = 0; i < currentGameState.tvsState.Length; i++)
            tvs[i].SetTVState(currentGameState.tvsState[i]);

        microwave.SetState(currentGameState.microwaveState);
        handWasher.SetState(currentGameState.handWasherState);
        explosivePc.SetPCState(currentGameState.pcBurnt);

        // Spawn drinks in the saved positions with the saved rotations and colors
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

            // Adds the drink to the current traking list
            AddDrink(drink.transform, drinkInfo);
        }

        // Clear the drink info because new drinks can be added and old ones can change position
        // In the next save all drinks will be saved again
        ///<see cref="SaveDrinkPositions()">
        currentGameState.drinksInfo.Clear();

        // Apply volumes
        audioMixer.SetFloat("AmbientVolume", Mathf.Log10(currentGameState.ambientVolume) * 20);
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(currentGameState.musicVolume) * 20);
        audioMixer.SetFloat("EffectsVolume", Mathf.Log10(currentGameState.effectsVolume) * 20);

        pauseMenuManager.SetAmbientVolumeSliderValue(currentGameState.ambientVolume);
        pauseMenuManager.SetMusicVolumeSliderValue(currentGameState.musicVolume);
        pauseMenuManager.SetEffectsVolumeSliderValue(currentGameState.effectsVolume);

    }

    /// <summary>
    /// Takes current drinks positions and rotations and stores them in the current game state
    /// </summary>
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
