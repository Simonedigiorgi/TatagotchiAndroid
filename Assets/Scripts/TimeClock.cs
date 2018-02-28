using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class TimeClock : MonoBehaviour
{

    public Text timeClock;                                                              // Nome nella gerarchia: TimeClock
    public TextBox textBox;                                                             // Nome nella gerarchia: TextBox

    public Button[] interactiveButtons = new Button[5];                                 // Bottoni interagibili (Si disattivano durante il sonno)

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

        // DORMI

        if (currentTime.Hours == 13 && currentTime.Minutes == 08 && currentTime.Seconds == 00)
        {
            playerController.GetComponent<Animator>().SetTrigger("Sleep");

            foreach (Button bt in interactiveButtons)
            {
                bt.enabled = false;
            }

        }
        else if((currentTime.Hours >= 13 && currentTime.Minutes >= 08 && currentTime.Seconds >= 00) && (currentTime.Hours < 14 /*&& currentTime.Minutes < 12*/))
        {
            playerController.GetComponent<Animator>().SetTrigger("SleepIdle");

            foreach (Button bt in interactiveButtons)
            {
                bt.enabled = false;
            }
        }
        else if (currentTime.Hours == 14 && currentTime.Minutes == 01 && currentTime.Seconds == 00)
        {
            playerController.GetComponent<Animator>().SetTrigger("WakeUp");

            foreach (Button bt in interactiveButtons)
            {
                bt.enabled = true;
            }
        }

    }
}
