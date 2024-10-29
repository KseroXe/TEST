using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoalTextUI : MonoBehaviour
{
    [SerializeField] private TMP_Text tmp;
    [SerializeField] private string congratulationText;

    private void OnEnable() {
        ItemCounter.ItemCountUpdate += OnItemCountUpdate;
        ItemCounter.AllItemsCollected += OnAllItemsCollected;
    }

    private void OnDisable() {
        ItemCounter.ItemCountUpdate -= OnItemCountUpdate;
        ItemCounter.AllItemsCollected -= OnAllItemsCollected;
    }

    private void OnAllItemsCollected()
    {
        tmp.text = congratulationText;
    }

    private void OnItemCountUpdate(int newCount, int targetItemCount)
    {
        tmp.text = $"Items collected: {newCount}/{targetItemCount}";
    }

}
