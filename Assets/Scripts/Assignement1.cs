using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assignement1 : MonoBehaviour
{
    private bool question1Done;
    private bool question2Done;
    // Start is called before the first frame update
    void Start()
    {
        question1Done = false;
        question2Done = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!question1Done)
            Question1();

        if (!question2Done)
            Question2("abcd");
    }

    void Question1()
    {
        for(int i = 1; i <= 100; i++)
        {
            string toDisplay = i.ToString();
            int toDelete = 1;

            if (i >= 10)
                toDelete++;

            if (i == 100)
                toDelete++;

            if(i%3 == 0)
            {
                toDisplay = "Fizz";
                toDisplay += new string(' ', toDelete);
            }

            if(i%5 == 0)
            {
                toDisplay = toDisplay.Remove(toDisplay.Length - toDelete, toDelete);
                toDisplay += "Buzz";
            }

            Debug.Log(toDisplay);
        }

        question1Done = true;
    }

    void Question2(string entry)
    {
        if(entry == "" || entry == null)
        {
            question2Done = true;
            return;
        }

        if(entry.Length == 1)
        {
            Debug.Log(entry);
            question2Done = true;
            return;
        }

        string temp = "";
        string result = "";

        for(int i = 0; i < entry.Length; i++)
        {
            temp = entry;
            result = "";

            result += entry[i];

            temp = entry.Remove(i, 1);

            Question2Recursive(temp, result);
        }

        question2Done = true;
    }

    void Question2Recursive(string entry, string result)
    {
        if (entry.Length == 1)
        {
            result += entry;
            Debug.Log(result);
            return;
        }

        string temp = "";
        string tempResult = "";

        for (int i = 0; i < entry.Length; i++)
        {
            tempResult = result;
            temp = entry;
            tempResult += entry[i];
            temp = entry.Remove(i, 1);
            Question2Recursive(temp, tempResult);
        }
    }
}
