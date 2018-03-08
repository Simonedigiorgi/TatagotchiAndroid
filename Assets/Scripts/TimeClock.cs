using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;
using UnityEngine.UI;

public class TimeClock : MonoBehaviour
{
    private TextBox textBox;                                                            // Nome nella gerarchia: TextBox
    private PlayerController playerController;                                          // PLAYERCONTROLLER
    private GameManager gameManager;                                                    // GAMEMANAGER

    public Image sleepImage;
    public Image fadeImage;

    private bool isActive = true;                                                       // E' Attivo

    void Start()
    {
        fadeImage.enabled = true;
        StartCoroutine(StartFade());

        //sleepImage.enabled = true;
        sleepImage.DOFade(0, 0);
        playerController = FindObjectOfType<PlayerController>();
        gameManager = FindObjectOfType<GameManager>();
        textBox = FindObjectOfType<TextBox>();
    }

    void Update()
    {
        TimeSpan currentTime = DateTime.Now.TimeOfDay;                                  // Prendi l'ora del giorno
        GetComponent<Text>().text = currentTime.ToString();                             // Mostra l'orario a schermo

        #region EVENTI AD ORARI PRESTABILITI (UTILIZZA LA TEXTBOX)

        if (currentTime.Hours == 20 && currentTime.Minutes <= 40 && isActive == true && fadeImage.enabled == false)
        {
            textBox.ShowBar("E' quasi ora di cena :)");
            isActive = false;
        }
        else if(currentTime.Hours == 20 && currentTime.Minutes > 40)
        {
            isActive = true;
        }

        // PROVA 

        if (currentTime.Hours == 17 && currentTime.Minutes <= 40 && isActive == true && fadeImage.enabled == false)
        {
            textBox.ShowBar("Testo di prova");
            isActive = false;
        }
        else if (currentTime.Hours == 20 && currentTime.Minutes > 40)
        {
            isActive = true;
        }

        #endregion

        #region EVENTO (DORMI) // GLI ORARI NON SONO ANCORA SISTEMATI A DOVERE

        // ANIMAZIONE INIZIALE

        if (currentTime.Hours == 20 && currentTime.Minutes == 18 && currentTime.Seconds == 00)
        {
            playerController.GetComponent<Animator>().SetTrigger("Sleep");
            sleepImage.enabled = true;
            sleepImage.DOFade(1, 1);

            foreach (Button bt in gameManager.gameButtons)
            {
                bt.enabled = false;
            }

            foreach (Button bt in gameManager.objectButtons)
            {
                bt.enabled = false;
            }

            gameManager.hungerBar.SetActive(false);                                                             // Chiudi il pannello Hunger
            gameManager.happinessBar.SetActive(false);                                                          // Chiudi il pannello Happiness
            gameManager.hygieneBar.SetActive(false);                                                            // Chiudi il pannello Hygiene
            gameManager.statsBar.SetActive(false);                                                              // Chiudi il pannello Stats 

        }

        // ANIMAZIONE DORMI (RIMANE PER TUTTE LE ORE STABILITE)

        if((currentTime.Hours >= 20 && currentTime.Minutes > 18 /*&& currentTime.Seconds >= 00*/) && (currentTime.Hours < 08 && currentTime.Minutes < 00))
        {
            playerController.GetComponent<Animator>().SetTrigger("SleepIdle");
            sleepImage.enabled = true;
            sleepImage.DOFade(1, 0);

            foreach (Button bt in gameManager.gameButtons)
            {
                bt.enabled = false;
            }

            foreach (Button bt in gameManager.objectButtons)
            {
                bt.enabled = false;
            }

            playerController.isNecessitiesActive = false;
        }
        else
        {
            playerController.isNecessitiesActive = true;
        }

        // ANIMAZIONE SVEGLIA

        if (currentTime.Hours == 20 && currentTime.Minutes == 19 && currentTime.Seconds == 00)
        {
            playerController.GetComponent<Animator>().SetTrigger("WakeUp");
            sleepImage.DOFade(0, 1);
            sleepImage.enabled = false;

            foreach (Button bt in gameManager.gameButtons)
            {
                bt.enabled = true;
            }

            foreach (Button bt in gameManager.objectButtons)
            {
                bt.enabled = true;
            }
        }

        #endregion

    }

    public IEnumerator StartFade()
    {
        yield return new WaitForSeconds(2);

        fadeImage.DOFade(0, 1);
        yield return new WaitForSeconds(0.8f);
        fadeImage.enabled = false;
        // disattiva immagine fade
    }
}
