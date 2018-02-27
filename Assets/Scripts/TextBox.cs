﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TextBox : MonoBehaviour {

	void Start () {
        transform.DOScale(new Vector2(0, 0), 0);                                                        // Scala iniziale (0)
    }

    public void ShowBar(string s)                                                                       // Mostra la TextBox (stringa gestita dal GameManager)
    {
        transform.GetChild(0).GetComponent<Text>().text = s;                                            // Prendi il testo come figlio (0)                                               
        StartCoroutine(TextBoxRoutine());                                                               // Inizializza Coroutine
    }

    public IEnumerator TextBoxRoutine()
    {
        yield return new WaitForSeconds(1);                                                             // Testo di attesa per la comparsa
        transform.DOScale(new Vector2(1, 1), 0.3f);                                                     // Scala (1,1)                                                         
        yield return new WaitForSeconds(5);                                                             // Tempo di attesa prima della scomparsa
        transform.DOScale(new Vector2(0, 0), 0.3f);                                                     // Scala (0)
    }
}
