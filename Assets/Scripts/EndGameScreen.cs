using UnityEngine;
using System.Collections;

public class EndGameScreen : MonoBehaviour
{
    public static EndGameScreen Instance = null;

    private void Awake()
    {
        Debug.Log("End game screen awake");
        Instance = this;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        Debug.Log("End game screen destroy");
        Instance = null;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnRestartButton()
    {
        Application.LoadLevel(0);
        Time.timeScale = 1;
    }

    public static void ShowEndGameScreen()
    {
        Time.timeScale = 0;
        Instance.gameObject.SetActive(true);
    }
}
