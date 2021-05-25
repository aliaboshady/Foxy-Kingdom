using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    Text cherriesText;

    void Start()
    {
        cherriesText = transform.Find("Cherry Count").GetComponent<Text>();
        UpdateCherries(0);
    }

    public void UpdateCherries(int cherriesTotal)
    {
        cherriesText.text = cherriesTotal.ToString();
    }
}
