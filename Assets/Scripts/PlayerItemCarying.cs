using System;
using UnityEngine;

public class PlayerItemCarying : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float takeMaxDistance;
    [SerializeField] private float interactionCooldown;
    private Holdable _caryingItem;
    private float _timer;

    [Header("References")]
    [SerializeField] private Transform handTransform;
    [SerializeField] private Camera playerCamera;

    private void Update()
    {
        if (_timer >= 0)
        {
            _timer -= Time.deltaTime;
        }

        HandleObjectTake();
        if (_timer < 0)
        {
            HandleObjectPlace();
        }
    }

    private void TakeObject(GameObject target)
    {
        _caryingItem = target.GetComponent<Holdable>();
        target.transform.position = handTransform.position;
        target.transform.parent = handTransform;
    }

    private void PlaceObject(Vector3 placePosition)
    {
        _caryingItem.transform.parent = null;
        _caryingItem.transform.eulerAngles = Vector3.zero;
        _caryingItem.transform.position = placePosition;
        _caryingItem = null;
    }

    private void HandleObjectTake()
    {
        if (!_caryingItem)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hit, takeMaxDistance))
                {
                    if (hit.collider.TryGetComponent(out Holdable holdable))
                    {
                        holdable.OnTake();
                        _timer = interactionCooldown;
                        TakeObject(hit.collider.gameObject);
                    }
                }
            }
        }
    }

    private void HandleObjectPlace()
    {
        if (_caryingItem != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hit, takeMaxDistance))
                {
                    _caryingItem.OnPlace();
                    _timer = interactionCooldown;
                    PlaceObject(hit.point + new Vector3(0, _caryingItem.transform.localScale.y / 4, 0));
                }
            }
        }
    }
}
