using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    bool _isOpened;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource audioSource;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _isOpened = false;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (_isOpened)
        {
            return;
        }
        
        if (other.gameObject.CompareTag("Player"))
        {
            Open();
            _isOpened = true;
        }
    }

    void Open()
    {
        animator.Play("OpenDoor");
        audioSource.Play();
    }
}
