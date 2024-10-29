using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Holdable : MonoBehaviour
{
  private Rigidbody _rb;

  private void Awake(){
    _rb = GetComponent<Rigidbody>();
  }

  public void OnTake(){
    _rb.constraints = RigidbodyConstraints.FreezeAll;
  }

  public void OnPlace(){
    _rb.constraints = RigidbodyConstraints.None;
  }
}
