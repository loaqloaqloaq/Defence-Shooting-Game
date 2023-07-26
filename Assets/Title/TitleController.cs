using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleController : MonoBehaviour
{
    public GameObject Title;
    public GameObject startBtn;
    SpriteRenderer t;
    Image i;
    float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        Title = GameObject.Find("title"); 
        t = Title.GetComponent<SpriteRenderer>();
        Title.transform.localScale = Vector3.zero;
        t.color = new Color(1, 1, 1, 0);

        startBtn = GameObject.Find("startBtn");
        i = startBtn.GetComponent<Image>();
        i.color = new Color(1, 1, 1, 0);

        speed = 0.5f;        

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
        if (t.color.a < 1)
        {
            t.color = t.color + new Color(0, 0, 0, 1f / speed) * Time.deltaTime;
            Title.transform.localScale += new Vector3(1f / speed, 1f / speed,0) * Time.deltaTime;
            Title.transform.Rotate(0, 0, 720f / speed * Time.deltaTime);
        }
        else if (i.color.a < 1)
        {
            Title.transform.rotation = Quaternion.Euler(0, 0, 0);
            i.color = i.color + new Color(0, 0, 0, 1f / speed) * Time.deltaTime;
        }        
    }

    public void ChangeScene() {
        startBtn.GetComponent<AudioSource>().Play();
        Invoke("loadMain", 0.2f);
    }
    void loadMain() {
        SceneManager.LoadScene("main");
    }
}
