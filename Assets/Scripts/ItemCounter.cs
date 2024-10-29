using System;
using UnityEngine;


[RequireComponent(typeof(Collider))]
public class ItemCounter : MonoBehaviour
{
    // Events
    public static Action<int, int> ItemCountUpdate;
    public static Action AllItemsCollected;

    [SerializeField] private int targetItemCount;
    private int _itemCounter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Holdable>(out _))
        {
            _itemCounter++;
            if (_itemCounter >= targetItemCount)
            {
                AllItemsCollected?.Invoke();
            }
            else
            {
                ItemCountUpdate?.Invoke(_itemCounter, targetItemCount);
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<Holdable>(out _))
        {
            _itemCounter--;
            ItemCountUpdate?.Invoke(_itemCounter, targetItemCount);
        }
    }
}
