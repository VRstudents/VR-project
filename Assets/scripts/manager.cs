using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class manager : MonoBehaviour {

    public Sprite[] cardFace;
    public Sprite cardBack;
    public GameObject[] cards;
    public Text matchText;

    private bool _init = false;
    private int _matches = 5;

    void Update()
    {
        if (!_init)
        {
            initializeCards();

        }
        if (Input.GetMouseButtonUp(0))
            checkCards();
    }

    void initializeCards()
    {
        for (int id = 0; id < 2; id++)
        {
            for (int i = 1; i < 6; i++)
            {
                bool test = false;
                int choice = 0;
                while (!test)
                {
                    choice = Random.Range(0, cards.Length);
                    test = !(cards[choice].GetComponent<card>().initialized);
                }
                cards[choice].GetComponent<card>().cardValue = i;
                cards[choice].GetComponent<card>().initialized = true;
            }
        }

        foreach (GameObject c in cards)
            c.GetComponent<card>().setupGraphics();

        if (!_init)
            _init = true;
    }

    public Sprite getCardBack()
    {
        return cardBack;
    }
    public Sprite getCardFace(int i)
    {
        return cardFace[i - 1];
    }

    void checkCards()
    {
        List<int> c = new List<int>();

        for (int i = 0; i < cards.Length; i++)
        {
            if (cards[i].GetComponent<card>().state == 1)
                c.Add(i);
        }

        if (c.Count == 2)
            cardComparison(c);
    }

    void cardComparison(List<int> c)
    {
        card.DO_NOT = true;

        int x = 0;

        if (cards[c[0]].GetComponent<card>().cardValue == cards[c[1]].GetComponent<card>().cardValue)
        {
            x = 2;
            _matches--;
            matchText.text = "Number of Matches: " + _matches;
            if (_matches == 0)
            SceneManager.LoadScene("Room");
        }
        for (int i = 0; i < c.Count; i++)
        {
            cards[c[i]].GetComponent<card>().state = x;
            cards[c[i]].GetComponent<card>().falseCheck();
        }
    }
}﻿


