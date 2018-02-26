using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TextBox : MonoBehaviour {

	void Start () {
        transform.DOScale(new Vector2(0, 0), 0);
    }

    public void ShowBar(string s)
    {
        transform.GetChild(0).GetComponent<Text>().text = s;
        StartCoroutine(TextBoxRoutine());
    }

    public IEnumerator TextBoxRoutine()
    {

        yield return new WaitForSeconds(3);
        transform.DOScale(new Vector2(1, 1), 0.3f);
        yield return new WaitForSeconds(5);
        transform.DOScale(new Vector2(0, 0), 0.3f);
    }

}
