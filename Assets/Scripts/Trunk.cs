using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trunk : MonoBehaviour
{
    public static bool endGame = false;
    [SerializeField] private Transform[] _positions;
    [SerializeField] private GameObject endPanel;

    private int counter = 0;
    public void SetOnPos(Transform item)
    {
        item.position = _positions[counter].position;
        counter++;
        CheckEndGame();
    }

    private void CheckEndGame()
    {
        if (IsFull())
        {
            endPanel.SetActive(true);
            endGame = true;
        }
    }

    public bool IsFull()
    {
        return counter == 4;
    }

}
