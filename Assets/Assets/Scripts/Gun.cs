using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.UIElements;

public class Gun : MonoBehaviour
{
    public bool isOnCoolDown = false;

    [SerializeField] private Rigidbody _bullet;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private Rig _rig;
    [SerializeField] private float _rigSpeed;
    Vector3 _gunPos, _hitPlace;
    public IEnumerator ThrowBall()
    {
        _gunPos = transform.position;

        Ray _ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit _hit;

        if (Physics.Raycast(_ray,out _hit))
        {
            _hitPlace = _hit.point;
        }

        Vector3 _direction = _hitPlace - _gunPos;
        _direction = _direction.normalized;
        var _ball = Instantiate(_bullet, _gunPos, Quaternion.identity);
        _ball.AddForce(_direction * _bulletSpeed, ForceMode.Impulse);

        StartCoroutine(HandUp());
        yield return new WaitForSeconds(0.4f);
        StartCoroutine(HandDown());
        isOnCoolDown = true;

        yield return new WaitForSeconds(0.5f);
        isOnCoolDown = false;
    }

    public IEnumerator HandUp()
    {
        while (_rig.weight < 1f)
        {
            _rig.weight += 0.05f;
            yield return null;
        }
    }
    public IEnumerator HandDown()
    {
        while (_rig.weight > 0f)
        {
            _rig.weight -= 0.05f;
            yield return null;
        }
    }
}
