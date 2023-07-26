using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject text, kumaGenerator;
    public bool GameOver;
    public float timePassed;
    public bool pause;
    float gameOverTimer;   

    Text t;
    // Start is called before the first frame update    
    void Start()
    {
        GameOver = false;
        pause = false;
        t =text.GetComponent<Text>();
        gameOverTimer = 0;
        kumaGenerator = GameObject.Find("kumaGenerator");        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
            Application.Quit();
        }
        if (!GameOver && !pause)
        {
            //DEBUG用チートコード
            if (Input.GetKeyDown(KeyCode.I)) timePassed += 60.0f;

            timePassed += Time.deltaTime;
            string minutes = ((int)timePassed / 60).ToString().PadLeft(2, '0');
            string second = ((int)timePassed % 60).ToString().PadLeft(2, '0');
            t.text = "時間：" + minutes + ":" + second;
            if (kumaGenerator != null) {
                kumaGenerator.GetComponent<KumaGenerator>().genHP = 10 + ((int)timePassed / 60);
                float tmp = 1f;
                for (int i = 0; i < ((int)timePassed / 60 / 2); i++) {
                    tmp *= 0.9f;
                }
                kumaGenerator.GetComponent<KumaGenerator>().genOffset = tmp;
            }
        }
        else if(GameOver) {
            gameOverTimer += Time.deltaTime;
            PlayerPrefs.SetFloat("timePassed", timePassed);
            if (gameOverTimer >= 2.0f) SceneManager.LoadScene("result");
        }
        
    }
}
