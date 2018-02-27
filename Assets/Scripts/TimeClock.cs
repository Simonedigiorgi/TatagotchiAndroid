using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class TimeClock : MonoBehaviour
{

    public Text timeClock;                                                              // Nome nella gerarchia: TimeClock
    public TextBox textBox;                                                             // Nome nella gerarchia: TextBox

    private PlayerController playerController;

    private bool isActive = true;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
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

        /*if (currentTime.Hours == 23 && currentTime.Minutes <= 30 && isActive == true)
        {
            textBox.ShowBar("E' quasi ora di andare a letto");
            isActive = false;
        }
        else if (currentTime.Hours == 23 && currentTime.Minutes > 30)
        {
            isActive = true;
        }*/

        // DORMI

        if (currentTime.Hours == 22 && currentTime.Minutes <= 56 && isActive == true)
        {
            //textBox.ShowBar("Compi azione dormi");
            StartCoroutine(playerController.SleepAnimation("Sleep", "Dormo"));                                    // Nome Animazione, Tempo di attesa, Valore dell'oggetto
            isActive = false;
        }
        else if (currentTime.Hours == 22 && currentTime.Minutes > 56)
        {
            isActive = true;
        }

        // SVEGLIATI

        if (currentTime.Hours == 22 && currentTime.Minutes <= 57 && isActive == true)
        {
            //textBox.ShowBar("Compi azione dormi");
            StartCoroutine(playerController.WakeUpAnimation("WakeUp", "Mi sveglio"));                                    // Nome Animazione, Tempo di attesa, Valore dell'oggetto
            isActive = false;
        }
        else if (currentTime.Hours == 22 && currentTime.Minutes > 57)                                       // Risolvere il bug!!!!!!!!!!!!!!!!!
        {
            isActive = true;
        }

    }
}
