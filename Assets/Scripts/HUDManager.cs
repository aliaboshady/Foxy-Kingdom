using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    Text cherriesText;
    string textConst = "Cherries: ";

    void Start()
    {
        cherriesText = transform.Find("Cherries").GetComponent<Text>();
        UpdateCherries(0);
    }

    public void UpdateCherries(int cherriesTotal)
    {
        cherriesText.text = textConst + cherriesTotal;
    }
}
