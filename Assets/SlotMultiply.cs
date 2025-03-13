using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SlotMultiply : MonoBehaviour
{
    BallStuff ballstuff;
    public float multiplier;
    private TextMeshPro m_TextMeshPro;

    
    // Start is called before the first frame update
    void Start()
    {
        ballstuff = FindObjectOfType<BallStuff>();
        TextMeshProUGUI textMeshPro = GetComponentInChildren<TextMeshProUGUI>();


        if (textMeshPro != null)
        {
            textMeshPro.text = multiplier.ToString() + "x";
        }
        else
        {
            Debug.LogError("nie dzia³a");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Circle"))
        {
            Ball ball = collision.GetComponent<Ball>();
            int winAmount = Mathf.RoundToInt(ball.betValue * multiplier);
            ballstuff.sCash += winAmount;
            Debug.Log("win " + winAmount);
            ballstuff.UpdateCashUI();
        }
    }

}
