using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    [SerializeField] GameObject errorSound, highlightSound, approveSound, pickSound;


    public void SpawnApprove()
    {
        Instantiate(approveSound);
    }
    public void SpawnError()
    {
        Instantiate(errorSound);
    }
    public void SpawnHighlight()
    {
        Instantiate(highlightSound);
    }

    public void SpawnPick()
    {
        Instantiate(pickSound);
    }
}
