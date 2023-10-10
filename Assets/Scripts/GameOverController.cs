using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverController : MonoBehaviour
{

    [SerializeField]
    TextMeshProUGUI nameText;

    [SerializeField]
    TextMeshProUGUI prizeText;

    public void Retry()
    {
        LevelManager.Instance.FirstScene();
    }

    void Start()
    {
        nameText.text = StateManager.Instance.getName();
        prizeText.text = StateManager.Instance.getPrize();
    }

}
