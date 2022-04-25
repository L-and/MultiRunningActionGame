using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    public GameObject player;
    public Text moveDistanceText;

    private PlayerMove pm;

    void Start()
    {
        pm = player.GetComponent<PlayerMove>();
    }

    void Update()
    {
        moveDistanceText.text = pm.moveDistance.ToString();
    }
}
