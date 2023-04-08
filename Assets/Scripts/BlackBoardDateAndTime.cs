using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlackBoardDateAndTime : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI blackBoardDateAndTime;

    void Update()
    {
        blackBoardDateAndTime.text = System.DateTime.Now.ToString("dd/MM/yyyy HH:mm");
    }
}
