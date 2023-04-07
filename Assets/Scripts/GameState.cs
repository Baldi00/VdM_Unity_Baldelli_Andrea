using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameState
{
	public bool[] doorsState;
    public bool[] pcsState;
    public bool[] tvsState;
    public bool[] burntPCsState;
    public bool microwaveState;
    public bool handWasherState;
    public List<DrinkInfo> drinksInfo;

    public GameState(int doorsCount, int PCsCount, int TVsCount, int burntPCsCount)
    {
        doorsState = new bool[doorsCount];
        pcsState = new bool[PCsCount];
        tvsState = new bool[TVsCount];
        burntPCsState = new bool[burntPCsCount];

        drinksInfo = new List<DrinkInfo>();
    }

    public void SetPCState(int index, bool state)
    {
        if (index >= 0 && index < pcsState.Length)
            pcsState[index] = state;
    }

    public void SetDoorState(int index, bool state)
    {
        if (index >= 0 && index < doorsState.Length)
            doorsState[index] = state;
    }

    public void SetTVState(int index, bool state)
    {
        if (index >= 0 && index < tvsState.Length)
            tvsState[index] = state;
    }

    public void SetBurntPCState(int index, bool state)
    {
        if (index >= 0 && index < burntPCsState.Length)
            burntPCsState[index] = state;
    }

    public void AddDrink(DrinkInfo drinkInfo)
    {
        drinksInfo.Add(drinkInfo);
    }
}
