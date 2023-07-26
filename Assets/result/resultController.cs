using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;
using TMPro;

public class resultController : MonoBehaviour
{
    public GameObject text,scrollViewContent,btn;
    Text t;
    TextMeshProUGUI ranking;
    float timePassed;

    // Start is called before the first frame update
    void Start()
    {
        t = text.GetComponent<Text>();
        ranking = scrollViewContent.GetComponent<TextMeshProUGUI>();
        btn = GameObject.Find("restart");
        timePassed = PlayerPrefs.GetFloat("timePassed");  
        t.text = "時間：" + timeToText(timePassed);
        PlayerPrefs.SetFloat("timePassed",0.0f);

        //read file
        string filePath = Application.dataPath + @"/Ranking/Ranking.csv";        

        if (GameObject.Find("DEBUG"))  GameObject.Find("DEBUG").GetComponent<Text>().text=filePath;    

        List<float> fileRanking=new List<float>();        
        try
        {
            //ファイルをオープンする
            using (StreamReader sr = new StreamReader(filePath))
            {
                while (0 <= sr.Peek())
                {
                    fileRanking.Add(float.Parse(sr.ReadLine().Split(",")[1]));
                }
                sr.Close();
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
            if (GameObject.Find("DEBUG"))  GameObject.Find("DEBUG").GetComponent<Text>().text = "READING: " + ex.Message;
        }

        //add result and sort
        if(timePassed > 0) fileRanking.Add(timePassed);
        fileRanking = fileRanking.FindAll(a => a >= 1);
        fileRanking.Sort((a, b) => (int)(b*1000f) - (int)(a*1000f));
        int youIndex = fileRanking.FindLastIndex((a)=>a==timePassed);
        //show ranking        
        ranking.text = "";
        int index = 0;
        string tmpRk = "";
        foreach (var val in fileRanking)
        {
            index++;
            if ((index-1) == youIndex) ranking.text += "<color=\"red\">";
            ranking.text += index.ToString()+", "+ timeToText(val);
            if ((index - 1) == youIndex) ranking.text += " ← YOU</color>";

            ranking.text += "\n\n";
            tmpRk += index.ToString() + "," + val.ToString() + "\n";
        }        

        scrollViewContent.transform.position=new Vector3 (scrollViewContent.transform.position.x, 80*youIndex, scrollViewContent.transform.position.y);

        try
        {
            StreamWriter outStream = new System.IO.StreamWriter(filePath);
            outStream.Write(tmpRk);
            outStream.Close();
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
            if (GameObject.Find("DEBUG"))  GameObject.Find("DEBUG").GetComponent<Text>().text = "OUTPUT: "+ex.Message;
        }
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
    }

    public void onClickChangeScene() {
        btn.GetComponent<AudioSource>().Play();
        Invoke("load", 0.2f);
    }    
    void load()
    {
        SceneManager.LoadScene("Title");
    }

    private string timeToText(float timePassed) {
        string hour = ((int)timePassed / 60).ToString().PadLeft(2, '0');
        string second = ((int)timePassed % 60).ToString().PadLeft(2, '0');
        return hour + ":" + second;
    }   
}
