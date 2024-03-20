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
    public void Init()
    {
        GameObject gm = GameObject.Instantiate(new GameObject("GameManager"));
        gameManager = gm.AddComponent<GameManager>();
        gameManager.LoadInstancesItems();
        
        GameObject player = GameObject.Instantiate(new GameObject("Player"));
        inventaire = player.AddComponent<Inventory>();
        inventaire.InitInventaireTest(gameManager.FindItemsDictionary(1));
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [Test]
    public void oui() {
        Init();

        Assert.AreEqual(0, inventaire.CheckItemQuantity(0));
    }


    [Test]
    public void AddItemTest() {
        Init();

        inventaire.AddItem(gameManager.FindItemsDictionary(0),16);

        Assert.AreEqual(16, inventaire.CheckItemQuantity(0));
    }

    [Test] 
    public void RemoveItemTest() {
        Init();

        inventaire.AddItem(gameManager.FindItemsDictionary(0),2);

        int ret = inventaire.DropItemTest(0);

        Assert.AreEqual(1, inventaire.CheckItemQuantity(0));
        Assert.AreEqual(1, ret);
    }

    [Test] 
    public void RemoveItemToEmpty() {
        Init();

        inventaire.AddItem(gameManager.FindItemsDictionary(0),1);

        int ret = inventaire.DropItemTest(0);

        Assert.AreEqual(0, inventaire.CheckItemQuantity(0));
        Assert.AreEqual(1, ret);
    }

    [Test]
    public void SwapTest() {
        Init();

        inventaire.AddItem(gameManager.FindItemsDictionary(0),12);

        inventaire.SwapCases(0,5);

        Assert.AreEqual(2, inventaire.CheckItemId(5));
    }

    [Test]
    public void AddItemIfFull() {
        Init();

        inventaire.Fill(gameManager.FindItemsDictionary(0));

        int ret = inventaire.AddItem(gameManager.FindItemsDictionary(0),12);

        Assert.AreEqual(12, ret);
    }

    [Test]
    public void SwapCasesEmpty() {
        Init();

        inventaire.SwapCases(0,6);

        Assert.AreEqual(0, inventaire.CheckItemId(0));
        Assert.AreEqual(0, inventaire.CheckItemId(6));
    }

    [Test]
    public void RemoveItemEmpty() {
        Init();

        int ret = inventaire.DropItemTest(0);

        Assert.AreEqual(0, ret);
    }

    [Test]
    public void SwapTwoCasesItem() {
        Init();

        inventaire.AddItem(gameManager.FindItemsDictionary(0),15);
        inventaire.AddItem(gameManager.FindItemsDictionary(2),30);

        inventaire.SwapCases(0,1);

        Assert.AreEqual(1, inventaire.CheckItemId(0));
        Assert.AreEqual(2, inventaire.CheckItemId(1));
        
    }

    [Test]
    public void AddItemMaxCapacity() {
        Init();

        inventaire.AddItem(gameManager.FindItemsDictionary(0),64);
        inventaire.AddItem(gameManager.FindItemsDictionary(0),16);

        Assert.AreEqual(16, inventaire.CheckItemQuantity(1));
    }

}
