using System;
using System.Collections;
using UnityEngine;

public class DoorUnlocker : MonoBehaviour
{
    [SerializeField] private HingeJoint _leftDoor;
    [SerializeField] private HingeJoint _rightDoor;
    [SerializeField] private BoxCollider _area;

    private Vector3 _zero = new Vector3(0f,0f,0f);

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            _area.enabled = false;
            other.GetComponent<Rigidbody>().velocity = _zero;
            OpenDoors();
        }
    }
    private void OpenDoors()
    {
        JointSpring _left = _leftDoor.spring;
        _left.targetPosition = -80f;
        _leftDoor.spring = _left;

        JointSpring _right = _rightDoor.spring;
        _right.targetPosition = 80f;
        _rightDoor.spring = _right;
    }
}
