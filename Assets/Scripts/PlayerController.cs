using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator anim;

    private TextBox textBox;

    [SerializeField] private int hunger;
    [SerializeField] private int happiness;

    [SerializeField] private int clickCount;

    private int foodValue;

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

        /*if (hunger < 50 && hunger > 10)
        {
            textBox.ShowBar();                                                                          // Mostra la TextBox
        }
        else
        {
            textBox.HideBar();
        }*/
    }

    public void updateStatus()
    {
        // STATUS

        if (!PlayerPrefs.HasKey("hunger"))
        {
            hunger = 100;
            PlayerPrefs.SetInt("hunger", hunger);
        }
        else
        {
            hunger = PlayerPrefs.GetInt("hunger");
        }

        if (!PlayerPrefs.HasKey("happiness"))
        {
            happiness = 100;
            PlayerPrefs.SetInt("happiness", happiness);
        }
        else
        {
            happiness = PlayerPrefs.GetInt("happiness");
        }

        if (!PlayerPrefs.HasKey("restoreClickCount"))
        {
            clickCount = 0;
            PlayerPrefs.SetInt("restoreClickCount", clickCount);
        }
        else
        {
            clickCount = PlayerPrefs.GetInt("restoreClickCount");
        }

        if (!PlayerPrefs.HasKey("then"))
        {
            PlayerPrefs.SetString("then", getStringTime());
        }

        // STATISTICHE

        TimeSpan ts = getTimeSpan();

        // Diminuisce la fame con il passare del tempo

        hunger -= (int)(ts.TotalHours * 5);                                                             // Sottrae ogni ora

        if (hunger < 50 && hunger > 10)
        {
            textBox.ShowBar("Sei molto affamata");
        }

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

        // RESETTA IL CLICKCOUNT A ZERO (DOPO 3 MINUTI) OSSERVARLO PER CREARE METODI COME DORMIRE O ALTRI EVENTI DI ATTESA

        clickCount -= (int)(ts.TotalMinutes * 1);
        if (clickCount < 0)
        {
            clickCount = 0;
            Debug.Log("puoi di nuovo cliccare");
        }

        // SALVA OGNI 30 SEC

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

    private void updateServer()
    {
        // Update server
    }

    // SALVA PARTITA

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

    // GET & SET

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

    public int _restoreClickCount
    {
        get { return clickCount; }
        set { clickCount = value; }
    }

    // AGGIORNA FAME

    public void UpdateHunger()
    {
        hunger += foodValue;                                                                        // Valore del cibo

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
            StartCoroutine(StartAnimation("Eat_0", 5, 5));                                              // Nome Animazione, Tempo di attesa, Valore dell'oggetto
            UpdateHunger();
        }
    }

    public void CoffeeValue()                                                                           // Valore del caffè (CIBO)
    {
        if(isActive == true)
        {
            StartCoroutine(StartAnimation("Eat_1", 5, 2));                                                  // Nome Animazione, Tempo di attesa, Valore dell'oggetto
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

    // SALVA IL GIOCATORE

    public void SavePlayer()
    {
        if (!serverTime)
        {
            updateDevice();
            PlayerPrefs.SetInt("hunger", hunger);
            PlayerPrefs.SetInt("happiness", happiness);
            PlayerPrefs.SetInt("restoreClickCount", clickCount);
        }
    }

    public IEnumerator StartAnimation(string name, float time, int value)
    {
        if(hunger < 100)
        {
            // NOME ANIMAZIONE
            // TEMPO DI ATTESA
            // VALORE DELL'OGGETTO

            foodValue = value;

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

    public void UpdateTextBox()
    {

    }

}
