using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    float startingTime = 0;
    float counterTime;
    [SerializeField] Text counterTimeText;
    // Start is called before the first frame update
    void Start()
    {
        counterTime = startingTime;
        
    }

    // Update is called once per frame
    void Update()
    {
        counterTime += 1 * Time.deltaTime;
        counterTimeText.text = counterTime.ToString("00");
    }
}
