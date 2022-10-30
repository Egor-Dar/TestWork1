using System;
using System.Collections;
using System.Collections.Generic;
using NPC;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform camera;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float gravity;
    [SerializeField] private float smoothTime;
    [SerializeField] private float smoothVelocity;
    private IAnimation _animation;
    public Transform GetCamera() => camera;

    private void Awake()
    {
        _animation = new Animation();
        _animation.Initialize(animator);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 move = Vector3.zero;

        Vector3 rotateDirection = new Vector3(horizontal, 0f, vertical).normalized;
        if (rotateDirection.magnitude >= 0.05f)
        {
            float rotateAngle = Mathf.Atan2(rotateDirection.x, rotateDirection.z) * Mathf.Rad2Deg + camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotateAngle, ref smoothVelocity, smoothTime);
            transform.rotation = Quaternion.Euler(0,angle,0f);
            move = Quaternion.Euler(0f, rotateAngle, 0f) * Vector3.forward;
            _animation.Run();
        }
        else _animation.Idle();
        var moved = move.normalized;
        moved.y -= gravity * Time.deltaTime;
        characterController.Move(moved * (movementSpeed * Time.deltaTime));
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {

            _animation.Attack();
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 0.6f))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 0.6f, Color.yellow);
                if(hit.collider.transform.parent.TryGetComponent(out IInteractable interactable)) interactable.Damage();
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 0.6f, Color.white);
                Debug.Log("Did not Hit");
            }
        }
    }
}
