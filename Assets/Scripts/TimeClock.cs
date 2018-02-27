using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class TimeClock : MonoBehaviour
{

    public Text timeClock;                                                              // Nome nella gerarchia: TimeClock
    public TextBox textBox;                                                             // Nome nella gerarchia: TextBox

    private bool isActive = true;

    void Start()
    {

    }

    void Update()
    {
        TimeSpan currentTime = DateTime.Now.TimeOfDay;
        timeClock.text = currentTime.ToString();

        // EVENTI AD ORARI PRESTABILITI

        if (currentTime.Hours == 19 && currentTime.Minutes <= 30 && isActive == true)
        {
            textBox.ShowBar("E' quasi ora di cena :)");
            isActive = false;
        }
        else if(currentTime.Hours == 19 && currentTime.Minutes > 30)
        {
            isActive = true;
        }

        if (currentTime.Hours == 23 && currentTime.Minutes <= 30 && isActive == true)
        {
            textBox.ShowBar("E' quasi ora di andare a letto");
            isActive = false;
        }
        else if (currentTime.Hours == 23 && currentTime.Minutes > 30)
        {
            isActive = true;
        }



    }
}
