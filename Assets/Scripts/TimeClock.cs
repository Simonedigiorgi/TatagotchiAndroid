using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class TimeClock : MonoBehaviour
{
    private TextBox textBox;                                                            // Nome nella gerarchia: TextBox
    private PlayerController playerController;                                          // PLAYERCONTROLLER
    private GameManager gameManager;                                                    // GAMEMANAGER

    private bool isActive = true;                                                       // E' Attivo

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        gameManager = FindObjectOfType<GameManager>();
        textBox = FindObjectOfType<TextBox>();
    }

    void Update()
    {
        TimeSpan currentTime = DateTime.Now.TimeOfDay;                                  // Prendi l'ora del giorno
        GetComponent<Text>().text = currentTime.ToString();                             // Mostra l'orario a schermo

        #region EVENTI AD ORARI PRESTABILITI (UTILIZZA LA TEXTBOX)

        if (currentTime.Hours == 20 && currentTime.Minutes <= 40 && isActive == true)
        {
            textBox.ShowBar("E' quasi ora di cena :)");
            isActive = false;
        }
        else if(currentTime.Hours == 20 && currentTime.Minutes > 40)
        {
            isActive = true;
        }

        #endregion

        #region EVENTO (DORMI) // GLI ORARI NON SONO ANCORA SISTEMATI A DOVERE

        // ANIMAZIONE INIZIALE

        if (currentTime.Hours == 20 && currentTime.Minutes == 30 && currentTime.Seconds == 00)
        {
            playerController.GetComponent<Animator>().SetTrigger("Sleep");

            foreach (Button bt in gameManager.gameButtons)
            {
                bt.enabled = false;
            }

            foreach (Button bt in gameManager.objectButtons)
            {
                bt.enabled = false;
            }

        }

        // ANIMAZIONE DORMI (RIMANE PER TUTTE LE ORE STABILITE)

        else if((currentTime.Hours >= 20 && currentTime.Minutes >= 30 && currentTime.Seconds >= 00) && (currentTime.Hours < 21 && currentTime.Minutes < 00))
        {
            playerController.GetComponent<Animator>().SetTrigger("SleepIdle");

            foreach (Button bt in gameManager.gameButtons)
            {
                bt.enabled = false;
            }

            foreach (Button bt in gameManager.objectButtons)
            {
                bt.enabled = false;
            }
        }

        // ANIMAZIONE SVEGLIA

        else if (currentTime.Hours == 20 && currentTime.Minutes == 31 && currentTime.Seconds == 00)
        {
            playerController.GetComponent<Animator>().SetTrigger("WakeUp");

            foreach (Button bt in gameManager.gameButtons)
            {
                bt.enabled = true;
            }

            foreach (Button bt in gameManager.objectButtons)
            {
                bt.enabled = false;
            }
        }

        #endregion

    }
}
