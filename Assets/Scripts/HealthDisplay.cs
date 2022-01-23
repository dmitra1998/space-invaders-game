using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{

    Text healthText;
    Player pHealth;


    // Start is called before the first frame update
    void Start()
    {
        healthText = GetComponent<Text>();
        pHealth = FindObjectOfType<Player>();
        healthText.text = pHealth.Health().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = pHealth.Health().ToString();
    }
}
