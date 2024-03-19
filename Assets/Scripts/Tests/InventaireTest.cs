using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class InventaireTest
{
    GameManager gameManager;
    Inventory inventaire;

    // A Test behaves as an ordinary method
    [Test]
    public void Init()
    {
        GameObject gm = GameObject.Instantiate(new GameObject("GameManager"));
        gameManager = gm.AddComponent<GameManager>();
        gameManager.LoadInstancesItems();
        
        GameObject player = GameObject.Instantiate(new GameObject("Player"));
        inventaire = player.AddComponent<Inventory>();
        inventaire.InitInventaire();
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [Test]
    public void AddItemTest() {
        Init();

        inventaire.AddItem(gameManager.FindItemsDictionary(0),16);

        Debug.Assert(inventaire.CheckItemQuantity(0) == 16);
    }

    [Test] 
    public void RemoveItemTest() {
        Init();

        inventaire.AddItem(gameManager.FindItemsDictionary(0),1);

        inventaire.DropItemTest(0);

        Debug.Assert(inventaire.CheckItemId(0) != 0);
    }

    [Test]
    public void SwapTest() {
        Init();

        inventaire.AddItem(gameManager.FindItemsDictionary(0),12);

        inventaire.SwapCases(0,5);

        Debug.Assert(inventaire.CheckItemId(5) == 0);
    }

    [Test]
    public void AddItemIfFull() {
        Init();

        inventaire.Fill(gameManager.FindItemsDictionary(0));

        int ret = inventaire.AddItem(gameManager.FindItemsDictionary(0),12);

        Debug.Assert(ret == 12);
    }
}
