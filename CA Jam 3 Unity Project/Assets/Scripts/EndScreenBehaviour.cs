using System.Collections;
using System.Collections.Generic;
using Services;
using TMPro;
using UnityEngine;

public class EndScreenBehaviour : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    
    // Start is called before the first frame update
    void Start()
    {
        string temp = "Your grade was: ";
        GameManager gm = ServiceLocator.Instance.Get<GameManager>();
        if (gm.grade <= 0.2)
        {
            temp += "F. Try better next time!";
        }
        else if (gm.grade <= 0.5)
        {
            temp += "D. Not great, give it another go!";
        }
        else if (gm.grade <= 0.7)
        {
            temp += "C. C's get degrees!";
        }
        else if (gm.grade <= 0.8)
        {
            temp += "B. Well done!";
        }
        else
        {
            temp += "A. Spectacular! You are truly a good employee!";
        }

        text.text = temp;
    }
}
