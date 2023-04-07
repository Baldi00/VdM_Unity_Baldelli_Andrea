using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;
    public static GameManager Instance { get => _instance; }

    [SerializeField]
    private string saveFileName;

    [SerializeField]
    private ComputerInteraction[] pcs;
    [SerializeField]
    private DoorInteraction[] doors;
    [SerializeField]
    private TVInteraction[] tvs;
    [SerializeField]
    private ComputerInteraction[] burntPcs;

    private GameState currentGameState;

    void Awake()
    {
        if (_instance == null)
            _instance = this;
        else if (_instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        if (SavesManager.IsSavePresent(SavesManager.GetSaveFilePath(saveFileName)))
        {
            currentGameState = SavesManager.Load(saveFileName);
            ApplySavesInGame();
        }
        else
            currentGameState = new GameState(doors.Length, pcs.Length, tvs.Length, burntPcs.Length);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            //drink positions
            SavesManager.Save(saveFileName, currentGameState);
        }
    }

    [ContextMenu("Delete Saves")]
    public void DeleteSaves()
    {
        SavesManager.DeleteSaves(saveFileName);
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

    public void SetBurntPCStateState(ComputerInteraction pc, bool state)
    {
        currentGameState.SetBurntPCState(System.Array.IndexOf<ComputerInteraction>(burntPcs, pc), state);
    }

    private void ApplySavesInGame()
    {
        for (int i = 0; i < currentGameState.pcsState.Length; i++)
            pcs[i].SetPCState(currentGameState.pcsState[i]);

        for (int i = 0; i < currentGameState.doorsState.Length; i++)
            doors[i].SetDoorState(currentGameState.doorsState[i]);

        for (int i = 0; i < currentGameState.tvsState.Length; i++)
            tvs[i].SetTVState(currentGameState.tvsState[i]);

        //for (int i = 0; i < currentGameState.PCsState.Length; i++)
        //    pcs[i].SetPCState(currentGameState.PCsState[i]);
    }
}
