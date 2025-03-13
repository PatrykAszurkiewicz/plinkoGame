using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;


public class BallStuff : MonoBehaviour
{
    public int sCash = 500;
    public int currentBet = 0;
    public GameObject ball;
    public Transform spawnPoint;
    private bool canSpawn = true;

    public TMP_InputField inputField;
    public TMP_Text cashText;
    void Start()
    {
        UpdateCashUI();
        inputField.onValueChanged.AddListener(ChangeAmount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {

        if (currentBet > 0 && currentBet <= sCash && canSpawn)
        {
            sCash -= currentBet;
            UpdateCashUI();

            StartCoroutine(SpawnBalls());

            Vector3 randomOffset = new Vector3(Random.Range(-0.1f, 0.1f), 0, 0);
            GameObject newBall = Instantiate(ball, spawnPoint.position + randomOffset, Quaternion.identity);

            Ball ballScript = newBall.GetComponent<Ball>();
            if (ballScript != null)
            {
                ballScript.SetBet(currentBet);
            }
        }
    }
    private IEnumerator SpawnBalls()
    {
        canSpawn = false;
        yield return new WaitForSeconds(0.25f);
        canSpawn = true;
    }

    public void ChangeAmount(string input)
    {
        string filteredInput = "";

        foreach(char c in input)
        {
            if (char.IsDigit(c))
            {
                filteredInput += c;
            }
        }
        inputField.text = filteredInput;

        if (int.TryParse(filteredInput, out int amount))
        {
            currentBet = amount;
        }
        else
        {
            currentBet = 0;
        }
    }

    public void UpdateCashUI()
    {
        cashText.text = "" + sCash; 
    }

    public void Multx2()
    {
        currentBet *= 2;
        inputField.text = currentBet.ToString();
    }
    public void Divineby2()
    {
        currentBet /= 2;
        inputField.text = currentBet.ToString();
    }
    public void ResetCash()
    {
        sCash = 500;
        UpdateCashUI();
    }
}
