using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Vector3 aim;
    public GameObject bulletPrefab;
    public float force;
    private Camera cam;
    private GameObject barBG, bar,hpBarBG,hpBar,expBar;
    private float holdTime, maxHoldTime;
    public GameObject explode;

    GameController gc;

    float hp;

    int maxExp, curExp, curLevel;
    int fireRtLv, powerLv;
    float fireRtMutipler, powerMutipler;

    public TextMeshProUGUI expText;
    public Text level;
    public GameObject levelUpGUI;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        maxHoldTime = 1f;
        barBG = GameObject.Find("barBG");
        bar = GameObject.Find("bar");
        hpBarBG = GameObject.Find("hpBarBG");
        hpBar = GameObject.Find("hpBar");
        expText = GameObject.Find("expText").GetComponent<TextMeshProUGUI>();
        level = GameObject.Find("Level").GetComponent<Text>();
        levelUpGUI = GameObject.Find("LevelUp");

        levelUpGUI.SetActive(false);
        bar.SetActive(false);
        barBG.SetActive(false);

        hp = 100.0f;

        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        fireRtLv = 0;
        powerLv = 0;

        maxExp = 40;
        curExp = 0;
        curLevel = fireRtLv+ powerLv+1;
        expBar = GameObject.Find("expBar");

        expBar.transform.localScale = new Vector3((float)curExp / (float)maxExp, 1, 1);
        expText.text = curExp + " / " + maxExp;
    }

    // Update is called once per frame
    void Update()
    {        
        if (hp <= 0)
        {
            gc.GameOver = true;
            Destroy(gameObject);
        }

        if (!gc.GameOver && !gc.pause)
        {
            //status
            hpBar.transform.localScale = new Vector3(2.02f * (hp / 100.0f), 1.03f, 1);
            curLevel = fireRtLv + powerLv + 1;
            fireRtMutipler = 1f - (float)fireRtLv / 10f;  
            level.text = "レベル：" + curLevel + "　パワー："+ (10f+ powerLv)+ "　チャージ時間：" + maxHoldTime+"秒";
            maxHoldTime = 1f * fireRtMutipler;
            //End Status

            //Fire
            Vector3 mousePos = Input.mousePosition;
            aim = cam.ScreenToWorldPoint(Input.mousePosition);

            //DEBUG用チートコード
            if (Input.GetKeyDown(KeyCode.K)) {
                levelUp();
            }


            if (Input.GetKey(KeyCode.Mouse0))
            {
                bar.SetActive(true);
                barBG.SetActive(true);
                bar.transform.localScale = new Vector3(2.02f * (holdTime / maxHoldTime ) , 1.03f, 1);
                holdTime += Time.deltaTime;
                if (holdTime > maxHoldTime) { holdTime = maxHoldTime; }
            }
            else if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                int power = Mathf.Max(Mathf.RoundToInt((10.0f + powerLv) * (holdTime / maxHoldTime) ), 1);
                bullet.GetComponent<shooController>().power = power;

                GetComponent<AudioSource>().Play();

                holdTime = 0;
                bar.SetActive(false);
                barBG.SetActive(false);
            }
            //End Fire
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("kumas")) {
            
        }        
    }

    public void damage(float dmg) {
        hp -= dmg;
        var ex = Instantiate(explode, transform.position, Quaternion.identity);
        ex.transform.localScale = new Vector3(5, 5, 1);       
    }

    public void addExp() {
        curExp++;
        UpdateExpBar();
        if (curExp >= maxExp) levelUp();
    }

    public void levelUp() {
        gc.pause = true;
        curExp = 0;
        maxExp = (int)((float)maxExp*1.1f);
        UpdateExpBar();
        levelUpGUI.SetActive(true); 
        Time.timeScale = 0;
    }

    public void fireRtUp() {
        fireRtLv++;
        levelUpGUI.SetActive(false);
        gc.pause = false;
        Time.timeScale = 1;
    }
    public void powerUp() {
        powerLv++;
        levelUpGUI.SetActive(false);
        gc.pause = false;
        Time.timeScale = 1;
    }

    private void UpdateExpBar()
    {
        expBar.transform.localScale = new Vector3((float)curExp / (float)maxExp, 1, 1);
        expText.text = curExp + " / " + maxExp;
    }

}
