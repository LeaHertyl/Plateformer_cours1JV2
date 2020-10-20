using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Collectibles : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Score;
    private int ScoreValue;

    // Start is called before the first frame update
    void Start()
    {
        ScoreValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Score.text = "Score : " + ScoreValue;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("FlyCollectible"))
        {
            Debug.Log("youpi");
            Destroy(other.gameObject);
        }
        
    }
}
