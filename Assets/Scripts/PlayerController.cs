using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator anim;

    private TextBox textBox;

    public Text[] numbers;

    [SerializeField] private int hunger;
    [SerializeField] private int happiness;
    [SerializeField] private int hygiene;

    // Popup

    [SerializeField] private int poo;
    public Button pooButton;

    [SerializeField] private int clickCount;
    [SerializeField] private int actionsCount;                                          // Quante azioni puoi fare

    private int foodValue;
    private int hygieneValue;

    private int pooValue;

    private bool serverTime;

    public bool isActive;

    void Start()
    {
        isActive = true;
        anim = GetComponent<Animator>();
        textBox = FindObjectOfType<TextBox>();

        //PlayerPrefs.SetString("then", "02/24/2018 21:00:00");
        updateStatus();
    }

    void Update()
    {

        //updateStatus();

        if (Input.GetMouseButtonDown(0) && isActive == true)
        {
            Vector2 v = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(v), Vector2.zero);

            if (hit == true)
            {
                if (hit.transform.gameObject.tag == "Player")
                {
                    clickCount++;                                                                           // aumenta conta dei click

                    SavePlayer();                                                                       // Salva il Player

                    if (clickCount > 3)
                    {
                        clickCount = 3;
                        UpdateHappiness(0);
                    }
                    else
                    {
                        UpdateHappiness(1);
                    }
                }
            }
        }

        if (actionsCount < 0)
        {
            actionsCount = 0;
        }
    }

    public void updateStatus()
    {
        // STATUS

        // FAME

        if (!PlayerPrefs.HasKey("hunger"))
        {
            hunger = 100;
            PlayerPrefs.SetInt("hunger", hunger);
        }
        else
        {
            hunger = PlayerPrefs.GetInt("hunger");
        }

        // FELICITA'

        if (!PlayerPrefs.HasKey("happiness"))
        {
            happiness = 100;
            PlayerPrefs.SetInt("happiness", happiness);
        }
        else
        {
            happiness = PlayerPrefs.GetInt("happiness");
        }

        // IGENE

        if (!PlayerPrefs.HasKey("hygiene"))
        {
            hygiene = 100;
            PlayerPrefs.SetInt("hygiene", hygiene);
        }
        else
        {
            hygiene = PlayerPrefs.GetInt("hygiene");
        }

        // CACCA (Ogni TOT ore attiva l'oggetto in scena)

        if (!PlayerPrefs.HasKey("poo"))
        {
            poo = 4;
            PlayerPrefs.SetInt("poo", poo);
        }
        else
        {
            poo = PlayerPrefs.GetInt("poo");
        }

        // CONTA DEI CLICK

        if (!PlayerPrefs.HasKey("clickCount"))
        {
            clickCount = 0;
            PlayerPrefs.SetInt("clickCount", clickCount);
        }
        else
        {
            clickCount = PlayerPrefs.GetInt("clickCount");
        }

        // CONTA DELLE AZIONI 

        if (!PlayerPrefs.HasKey("actionsCount"))
        {
            actionsCount = 5;
            PlayerPrefs.SetInt("actionsCount", actionsCount);
        }
        else
        {
            actionsCount = PlayerPrefs.GetInt("actionsCount");
        }

        // PRENDI L'ORA CORRENTE

        if (!PlayerPrefs.HasKey("then"))
        {
            PlayerPrefs.SetString("then", getStringTime());
        }

        // STATISTICHE

        TimeSpan ts = getTimeSpan();

        // Diminuisce la fame con il passare del tempo

        hunger -= (int)(ts.TotalHours * 5);                                                             // Sottrae ogni ora

        if (hunger < 0)
        {
            hunger = 0;
            Debug.Log("fine partita");
        }

        // Diminuisce la felicità con il passare del tempo

        happiness -= (int)(ts.TotalMinutes * 1);                                                             // Sottrae ogni ora
        if (happiness < 0)
        {
            happiness = 0;
            Debug.Log("sei infelice");
        }

        // Diminuisce la felicità con il passare del tempo

        hygiene -= (int)(ts.TotalMinutes * 1);                                                             // Sottrae ogni ora
        if (hygiene < 0)
        {
            hygiene = 0;
            Debug.Log("stai puzzando");
        }

        // Diminuisce il tempo di apparizione della CACCA

        poo -= (int)(ts.TotalMinutes * 1);                                                             // Sottrae ogni ora
        if (poo < 0)
        {
            poo = 0;
            pooButton.gameObject.SetActive(true);
            Debug.Log("C'è la cacca");
        }

        // RESETTA IL CLICKCOUNT A ZERO (DOPO 3 MINUTI) OSSERVARLO PER CREARE METODI COME DORMIRE O ALTRI EVENTI DI ATTESA

        clickCount -= (int)(ts.TotalMinutes * 1);
        if (clickCount < 0)
        {
            clickCount = 0;
            Debug.Log("puoi di nuovo cliccare");
        }

        // LE TUE AZIONI DISPONIBILI

        actionsCount += (int)(ts.TotalSeconds * 1);
        if (actionsCount > 5)
        {
            actionsCount = 5;
        }

        // RITORNA OGNI 30 SECONDI IL TEMPO ATTUALE 

        if (serverTime)
        {
            updateServer();
        }
        else
        {
            InvokeRepeating("updateDevice", 0f, 30f);
        }

        /*if (!serverTime)
        {
            InvokeRepeating("updateDevice", 0f, 30f);
        }*/

    }

    #region UPDATE DEVICE (ONLY DEVICE)
    private void updateServer()
    {
        // Update server
    }

    private void updateDevice()
    {
        PlayerPrefs.SetString("then", getStringTime());
    }

    TimeSpan getTimeSpan()
    {
        if (serverTime)
        {
            return new TimeSpan();
        }
        else
        {
            return DateTime.Now - Convert.ToDateTime(PlayerPrefs.GetString("then"));
        }
    }

    private string getStringTime()
    {
        DateTime now = DateTime.Now;
        return now.Month + "/" + now.Day + "/" + now.Year + " " + now.Hour + ":" + now.Minute + ":" + now.Second;
    }
    #endregion

    #region GET & SET
    public int _hunger
    {
        get { return hunger; }
        set { hunger = value; }
    }

    public int _happiness
    {
        get { return happiness; }
        set { happiness = value; }
    }

    public int _hygiene
    {
        get { return hygiene; }
        set { hygiene = value; }
    }

    public int _poo
    {
        get { return poo; }
        set { poo = value; }
    }

    public int _clickCount
    {
        get { return clickCount; }
        set { clickCount = value; }
    }

    public int _actionsCount
    {
        get { return actionsCount; }
        set { actionsCount = value; }
    }
    #endregion

    // AGGIORNA FAME

    public void UpdateHunger()
    {
        hunger += foodValue;                                                                        // Valore del cibo

        // Animazione del numero

        if(hunger < 100)
        {
            numbers[0].GetComponent<Animator>().SetTrigger("ScaleNumbers");
            numbers[3].GetComponent<Animator>().SetTrigger("ScaleNumbers");
        }

        if (hunger > 100)
        {
            hunger = 100;
            
        }

        SavePlayer();
    }

    // VALORI DEL CIBO

    public void AppleValue()                                                                            // Valore della mela (CIBO)
    {
        if(isActive == true)
        {
            StartCoroutine(HungerAnimation("Eat_0", 1.2f, 5));                                              // Nome Animazione, Tempo di attesa, Valore dell'oggetto
            UpdateHunger();
        }
    }

    public void CoffeeValue()                                                                           // Valore del caffè (CIBO)
    {
        if(isActive == true)
        {
            StartCoroutine(HungerAnimation("Eat_1", 1.2f, 5));                                                  // Nome Animazione, Tempo di attesa, Valore dell'oggetto
            UpdateHunger();
        }
    }

    // AGGIORNA FELICITA'

    public void UpdateHappiness(int i)
    {
        happiness += i;

        if (happiness > 100)
        {
            happiness = 100;
        }

        SavePlayer();
    }

    // AGGIORNA SALUTE (Hygiene)

    public void UpdateHygiene()
    {
        hygiene += hygieneValue;

        if (hygiene < 100)
        {
            numbers[2].GetComponent<Animator>().SetTrigger("ScaleNumbers");
            numbers[3].GetComponent<Animator>().SetTrigger("ScaleNumbers");
        }

        if (hygiene > 100)
        {
            hygiene = 100;
        }

        SavePlayer();
    }

    // AGGIORNA CACCA (POO)

    public void UpdatePoo()
    {
        poo = 4;                                                                                    // Resetta (poo)                                                                                                    
        pooButton.gameObject.SetActive(false);                                                      // Disattiva il Tasto (Poo)
        happiness += 10;                                                                            // Aumenta la felicità

        SavePlayer();                                                                               // SALVA IL GIOCATORE
    }

    // VALORI DELLA SALUTE (Hygiene)

    public void HygieneValue_0()                                                                     
    {
        if (isActive == true)
        {
            StartCoroutine(HygieneAnimation("Eat_0", 1.2f, 5));                                      // Nome Animazione, Tempo di attesa, Valore dell'oggetto
            UpdateHygiene();                                                                         // Aggiorna la Salute
        }
    }

    public void HygieneValue_1()
    {
        if (isActive == true)
        {
            StartCoroutine(HygieneAnimation("Eat_1", 1.2f, 5));                                      // Nome Animazione, Tempo di attesa, Valore dell'oggetto
            UpdateHygiene();                                                                         // Aggiorna la Salute
        }
    }

    // SALVA IL GIOCATORE

    public void SavePlayer()
    {
        if (!serverTime)
        {
            updateDevice();
            PlayerPrefs.SetInt("hunger", hunger);
            PlayerPrefs.SetInt("happiness", happiness);
            PlayerPrefs.SetInt("hygiene", hygiene);

            PlayerPrefs.SetInt("poo", poo);

            PlayerPrefs.SetInt("clickCount", clickCount);
            PlayerPrefs.SetInt("actionsCount", actionsCount);
        }
    }

    public IEnumerator HungerAnimation(string name, float time, int value)
    {
        if(hunger < 100 && actionsCount <= 5 && actionsCount > 0)
        {
            // NOME ANIMAZIONE
            // TEMPO DI ATTESA
            // VALORE DELL'OGGETTO

            foodValue = value;
            actionsCount -= 1;

            anim.SetTrigger(name);

            isActive = false;
            yield return new WaitForSeconds(time);
            isActive = true;
        }
        else
        {
            anim.SetTrigger("No");
            isActive = false;
            yield return new WaitForSeconds(time);
            isActive = true;
        }

        
    }

    public IEnumerator HygieneAnimation(string name, float time, int value)
    {
        if (hygiene < 100 && actionsCount <= 5 && actionsCount > 0)
        {
            // NOME ANIMAZIONE
            // TEMPO DI ATTESA
            // VALORE DELL'OGGETTO

            hygieneValue = value;
            actionsCount -= 1;

            anim.SetTrigger(name);

            isActive = false;
            yield return new WaitForSeconds(time);
            isActive = true;
        }
        else
        {
            anim.SetTrigger("No");
            isActive = false;
            yield return new WaitForSeconds(time);
            isActive = true;
        }
    }
}
