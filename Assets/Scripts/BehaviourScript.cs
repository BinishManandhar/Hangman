using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using System;

public class BehaviourScript : MonoBehaviour
{
    public GameObject head;
    public GameObject body;
    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject leftLeg;
    public GameObject rightLeg;
    public TextEditor textEditor;
    public InputField answer;
    public TextMeshPro textMeshPro;
    public Button button;
    public GameObject noose;
    public Animator animator;
    int steps = 0;

    public ObjectStateBehaviours objectStateBehaviours;

    public enum ButtonText {
      Check,TryAgain
    };
    public const string LAYER_NAME = "Foreground(2)";
    public const string DEFAULT_LAYER_NAME = "Default";
    public const string token = "bLdZR7iTdzIDyL7DRvNosgtGGKGrozpGTgFZUTD+wn3z7/xApYeuy0lG2BpkjoklUhwbo8hzjcSFqBq9NSED9Q==";


    GameObject[] players;
    GameObject[] sortedPlayers;
    int i=0;
    //Vector2 pos;
    //float speedY = -1f;
    
        
    //Called even if the script is not activated (Usually for initialization purpose)
    private void Awake()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        sortedPlayers = new GameObject[players.Length];
        foreach (GameObject player in players)
        {
            if (player.gameObject.name == "Head")
            {
                sortedPlayers[0] = player;
            }
            else if (player.gameObject.name == "Body") {
                sortedPlayers[1] = player;
            }
            else if (player.gameObject.name == "LeftHand") {
                sortedPlayers[2] = player;
            }
            else if (player.gameObject.name == "RightHand") {
                sortedPlayers[3] = player;
            }
            else if (player.gameObject.name == "LeftLeg") {
                sortedPlayers[4] = player;
            }
            else if (player.gameObject.name == "RightLeg") {
                sortedPlayers[5] = player;
            }

            player.gameObject.SetActive(false);
            
        }

        animator = GetComponent<Animator>();

    }

    // Start is called before the first frame update
    void Start()
    {
        //pos = transform.position;
        //Destroy(gameObject, 3f);
        objectStateBehaviours = animator.GetBehaviour<ObjectStateBehaviours>();
        objectStateBehaviours.behaviourScript = this;

        noose = GameObject.FindGameObjectWithTag("Noose");
        noose.GetComponent<Animator>().SetTrigger("NooseTrigger");
        Test();
        i = 0;
        StartCoroutine(GetRequest("http://192.168.0.114:8000/siterank/google.com/1"));
    }

    // Update is called once per frame
    void Update()
    {
        //pos.y -= 0.1f;
        //transform.Translate(0,speedY * Time.deltaTime,0);
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //InvokeRepeating("inst", 1f, 1f);

        }
    }


    public void MainWork() {
        AfterInst();
    }

    void AfterInst() {
        steps++;
        if (i == sortedPlayers.Length-1)
        {
            GameObject.FindGameObjectWithTag("Person").GetComponent<Animator>().SetTrigger("HangingTrigger");
            GameObject.FindGameObjectWithTag("Person").GetComponent<SpriteRenderer>().sortingLayerName = LAYER_NAME;
            noose.SetActive(false);
            noose.SetActive(true);
            noose.GetComponent<Animator>().SetTrigger("NooseTrigger");
            GameObj();
            sortedPlayers[i].gameObject.SetActive(true);
            textMeshPro.SetText("DEAD");
            answer.text = "Sorry";
            i++;
        }
        else
        {
            GameObj();
            Debug.Log("Steps: " + steps);
            sortedPlayers[i].gameObject.SetActive(true);
            i++;
        }
    }

    public void SetGet(Text buttonText)
    {
        if (buttonText.text.Equals(ButtonText.Check.ToString()))
        {
            if (!answer.text.Equals("Binish"))
            {
                MainWork();
            }
            if (i == sortedPlayers.Length)
            {
                buttonText.text = "Try Again";
            }
        }
        else
        {
            i = 0;
            steps = 0;
            Awake();
            textMeshPro.SetText("Hangman");
            answer.text = "";
            buttonText.text = "Check";
            GameObj();
            GameObject.FindGameObjectWithTag("Person").GetComponent<SpriteRenderer>().sortingLayerName = DEFAULT_LAYER_NAME;
        }
    }

    IEnumerator GetRequest(string uri)
    {
        UnityWebRequest www = UnityWebRequest.Get(uri);
        www.SetRequestHeader("content-type","application/json");
        www.SetRequestHeader("topnepalsec",token);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log("Error: " + www.error);
        }
        else
        {
            string receivedData = www.downloadHandler.text;
            
            ProcessJSON(www.downloadHandler.text);
            Debug.Log(www.downloadHandler.text);
        }
    }

    private void ProcessJSON(string jsonString)
    {
        QACollectionScript qACollectionScript = new QACollectionScript();
        qACollectionScript = JsonUtility.FromJson<QACollectionScript>(jsonString);
        Debug.Log(qACollectionScript.data[0].Rank.Global);
    }

    public void Test()
    {
        textMeshPro.SetText("What is my name?");
        Text[] option1 = GameObject.Find("option(1)").GetComponentsInChildren<Text>();
        Text[] option2 = GameObject.Find("option(2)").GetComponentsInChildren<Text>();
        Text[] option3 = GameObject.Find("option(3)").GetComponentsInChildren<Text>();
        Text[] option4 = GameObject.Find("option(4)").GetComponentsInChildren<Text>();
        option1[1].text = "Tinish";
        option2[1].text = "Finish";
        option3[1].text = "Binish";
        option4[1].text = "Ginish";
        GameObject.FindGameObjectWithTag("InputCanvas").GetComponent<Canvas>().enabled = false;
    }

    private void GameObj()
    {
        GameObject.FindGameObjectWithTag("Person").GetComponent<Animator>().SetInteger("steps", steps);
        GameObject.FindGameObjectWithTag("Bird").GetComponent<Animator>().SetInteger("BirdMove", steps);
        GameObject.FindGameObjectWithTag("Ladder").GetComponent<Animator>().SetInteger("LadderMove", steps);
        GameObject.FindGameObjectWithTag("Noose").GetComponent<Animator>().SetInteger("NooseRestart", steps);
    }

    public void AnswersFromOptions(Text answerText) {
        if (!answerText.text.Equals("Binish"))
        {
            MainWork();
        }
        else {
            textMeshPro.text = "You are a Winner";
        }
    }
}
