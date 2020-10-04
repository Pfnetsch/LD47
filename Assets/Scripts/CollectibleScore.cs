using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectibleScore : MonoBehaviour
{
    public int MAX_SCORE = 10;
    
    // Start is called before the first frame update
    void Start()
    {
        GlobalInformation.saturnScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = "Score: " + GlobalInformation.saturnScore;

        if (GlobalInformation.saturnScore >= MAX_SCORE)
        {
            gameObject.GetComponent<TextMeshProUGUI>().text = "U win boiyo";
        }
    }
}
