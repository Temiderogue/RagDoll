using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public bool isCanShoot = false;

    [SerializeField] private float _speed;
    [SerializeField] private Rigidbody[] _body;
    [SerializeField] private Animator _animator;
    [SerializeField] private Animator _cameraAnim;
    [SerializeField] private TextMeshProUGUI _tapToPlay;
    [SerializeField] private TextMeshProUGUI _levelComplete;
    [SerializeField] private TextMeshProUGUI _restart;
    [SerializeField] private Image _panel;
    [SerializeField] private Gun _gun;
    [SerializeField] private Collider _playerCol,_bulletCol;
    
    private void Awake()
    {
        _levelComplete.enabled = false;
        _restart.enabled = false;
        _gun.enabled = false;
        for (int i = 0; i < _body.Length; i++)
        {
            _body[i].isKinematic = true;
        }
    }

    private void Update()
    {
        if (isCanShoot == true && _gun.isOnCoolDown == false && Input.GetMouseButtonDown(0))
        {
            _gun.StartCoroutine("ThrowBall") ;
        }
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * _speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FinishLine")
        {
            _levelComplete.enabled = true;
            makePhysic();
            EndLevel();
        }
        else if (other.tag == "Door")
        {
            Death();
        }
    }

    public void StartGame()
    {
        _animator.SetBool("isRunning", true);
        _cameraAnim.SetBool("isGameStarted", true);
        _speed = 0.1f;
        _tapToPlay.enabled = false;
        _panel.enabled = false;
        _gun.enabled = true;
        isCanShoot = true;
    }

    public void EndLevel()
    {
        isCanShoot = false;
        _speed = 0f;
        _animator.SetBool("isRunning", false);
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(0);
    }
    
    private void makePhysic()
    {
        _animator.enabled = false;
        for (int i = 0; i < _body.Length; i++)
        {
            _body[i].isKinematic = false;
        }
    }

    private void Death()
    {
        EndLevel();
        makePhysic();
        _restart.enabled = true;
    }
}
