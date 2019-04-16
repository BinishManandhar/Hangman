using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextManager : MonoBehaviour
{
    public GameObject gameObj;
    public InputField answer;
    public TextMeshPro textMeshPro;
    string ansText;


    void Start() {

    }

    public void SetGet() {
        if (answer.text != "answer")
        {
            BehaviourScript behaviourScript = gameObj.GetComponent<BehaviourScript>();
            //behaviourScript.MainWork(textMeshPro);
        }
    }
}
