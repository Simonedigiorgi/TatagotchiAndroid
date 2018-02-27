using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private TextBox textBox;
    private PlayerController playerController;

    public GameObject hungerText;                                                           // Valore numerico della fame
    public GameObject happinessText;                                                        // Valore numerico della felicità
    public GameObject hygieneText;                                                          // Valore numerico dell'igiene

    public Text hungerStats;
    public Text happinessStats;
    public Text hygieneStats;

    public GameObject actionsCountText;

    public GameObject hungerBar;
    public GameObject happinessBar;
    public GameObject hygieneBar;
    public GameObject statsBar;

    public GameObject player;

    private bool isActive = true;

    void Start()
    {
        print(Time.realtimeSinceStartup);

        textBox = FindObjectOfType<TextBox>();
        playerController = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        // MOSTRA I VALORI NUMERICI DELLE NECESSITA'

        hungerText.GetComponent<Text>().text = player.GetComponent<PlayerController>()._hunger.ToString();
        happinessText.GetComponent<Text>().text = player.GetComponent<PlayerController>()._happiness.ToString();
        hygieneText.GetComponent<Text>().text = player.GetComponent<PlayerController>()._hygiene.ToString();
        actionsCountText.GetComponent<Text>().text = player.GetComponent<PlayerController>()._actionsCount.ToString();

        // MOSTRA IL BOX CON I TESTI (PANNELLO STATS)

        #region HUNGER
        if(playerController._hunger < 100 && playerController._hunger > 90)
        {
            hungerStats.text = "Testo 9"; 
        }
        else if (playerController._hunger < 90 && playerController._hunger > 80)
        {
            hungerStats.text = "Testo 8";
        }
        else if (playerController._hunger < 80 && playerController._hunger > 70)
        {
            hungerStats.text = "Testo 7";
        }
        else if (playerController._hunger < 70 && playerController._hunger > 60)
        {
            hungerStats.text = "Testo 6";
        }
        else if (playerController._hunger < 60 && playerController._hunger > 50)
        {
            hungerStats.text = "Testo 5";
        }
        else if (playerController._hunger < 50 && playerController._hunger > 40)
        {
            hungerStats.text = "Testo 4";
        }
        else if (playerController._hunger < 40 && playerController._hunger > 30)
        {
            hungerStats.text = "Testo 3";
        }
        else if (playerController._hunger < 30 && playerController._hunger > 20)
        {
            hungerStats.text = "Testo 2";
        }
        else if (playerController._hunger < 20 && playerController._hunger > 10)
        {
            hungerStats.text = "Testo 1";
        }
        else if (playerController._hunger < 10 && playerController._hunger > 0)
        {
            hungerStats.text = "Testo 0";
        }
        #endregion

        #region HAPPINESS
        if (playerController._happiness < 100 && playerController._happiness > 90)
        {
            happinessStats.text = "Testo 9";
        }
        else if (playerController._happiness < 90 && playerController._happiness > 80)
        {
            happinessStats.text = "Testo 8";
        }
        else if (playerController._happiness < 80 && playerController._happiness > 70)
        {
            happinessStats.text = "Testo 7";
        }
        else if (playerController._happiness < 70 && playerController._happiness > 60)
        {
            happinessStats.text = "Testo 6";
        }
        else if (playerController._happiness < 60 && playerController._happiness > 50)
        {
            happinessStats.text = "Testo 5";
        }
        else if (playerController._happiness < 50 && playerController._happiness > 40)
        {
            happinessStats.text = "Testo 4";
        }
        else if (playerController._happiness < 40 && playerController._happiness > 30)
        {
            happinessStats.text = "Testo 3";
        }
        else if (playerController._happiness < 30 && playerController._happiness > 20)
        {
            happinessStats.text = "Testo 2";
        }
        else if (playerController._happiness < 20 && playerController._happiness > 10)
        {
            happinessStats.text = "Testo 1";
        }
        else if (playerController._happiness < 10 && playerController._happiness > 0)
        {
            happinessStats.text = "Testo 0";
        }
        #endregion

        #region HYGIENE
        if (playerController._hygiene < 100 && playerController._hygiene > 90)
        {
            hygieneStats.text = "Testo 9";
        }
        else if (playerController._hygiene < 90 && playerController._hygiene > 80)
        {
            hygieneStats.text = "Testo 8";
        }
        else if (playerController._hygiene < 80 && playerController._hygiene > 70)
        {
            hygieneStats.text = "Testo 7";
        }
        else if (playerController._hygiene < 70 && playerController._hygiene > 60)
        {
            hygieneStats.text = "Testo 6";
        }
        else if (playerController._hygiene < 60 && playerController._hygiene > 50)
        {
            hygieneStats.text = "Testo 5";
        }
        else if (playerController._hygiene < 50 && playerController._hygiene > 40)
        {
            hygieneStats.text = "Testo 4";
        }
        else if (playerController._hygiene < 40 && playerController._hygiene > 30)
        {
            hygieneStats.text = "Testo 3";
        }
        else if (playerController._hygiene < 30 && playerController._hygiene > 20)
        {
            hygieneStats.text = "Testo 2";
        }
        else if (playerController._hygiene < 20 && playerController._hygiene > 10)
        {
            hygieneStats.text = "Testo 1";
        }
        else if (playerController._hygiene < 10 && playerController._hygiene > 0)
        {
            hygieneStats.text = "Testo 0";
        }
        #endregion
    }

    void FixedUpdate() { Screen.SetResolution(480, 800, true); }

    public void buttonBehaviour(int i)                                                                  // Onclick Buttons
    {
        switch (i)
        {
            case (0):
            default:
                hungerBar.SetActive(!hungerBar.activeInHierarchy);                                      // Toggle HungerBar 
                happinessBar.SetActive(false);                                                          // Chiudi il pannello Happiness
                hygieneBar.SetActive(false);                                                            // Chiudi il pannello Hygiene
                statsBar.SetActive(false);                                                              // Chiudi il pannello Stats                                                                  
                break;
            case (1):
                happinessBar.SetActive(!happinessBar.activeInHierarchy);                                // Toggle HappinessBar
                hungerBar.SetActive(false);                                                             // Chiudi il pannello Hunger
                hygieneBar.SetActive(false);                                                            // Chiudi il pannello Hygiene
                statsBar.SetActive(false);                                                              // Chiudi il pannello Stats   
                break;
            case (2):
                hygieneBar.SetActive(!hygieneBar.activeInHierarchy);                                    // Toggle HygieneBar                                      
                hungerBar.SetActive(false);                                                             // Chiudi il pannello Hunger
                happinessBar.SetActive(false);                                                          // Chiudi il pannello Happiness
                statsBar.SetActive(false);                                                              // Chiudi il pannello Stats   
                break;
            case (3):
                statsBar.SetActive(!statsBar.activeInHierarchy);                                        // Toggle StatsBar                                                                                            
                hungerBar.SetActive(false);                                                             // Chiudi il pannello Hunger
                happinessBar.SetActive(false);                                                          // Chiudi il pannello Happiness
                hygieneBar.SetActive(false);                                                            // Chiudi il pannello Hygiene
                break;
            case (4):
                                                           

                break;
        }
    }

    /*public void SelectFood(int i)
    {
        //player.GetComponent<Player>().UpdateHunger();
        //Toggle(foodPanel);                                                                          // Chiudi il pannello cibo
    }*/

    public void Toggle(GameObject g)
    {
        if (g.activeInHierarchy)
        {
            g.SetActive(false);
        }
    }
}
