using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private PlayerSettingsAsset _playerSettings;

    [Header("Dependencies")]
    [SerializeField] private GameObject _modelContainer;

    [Header("Movement")]
    [SerializeField] private float _movementSpeed = 1f;
    [SerializeField] private float _movementDuration = 1.2f;
    [SerializeField] private Ease _movementEase = Ease.OutSine;

    [Header("Jump")]
    [SerializeField] private float _jumpPower = 5f;

    [Header("Rotation")]
    [SerializeField] private float _rotateDuration = 0.7f;
    [SerializeField] private Ease _rotateEase = Ease.OutExpo;

    // Components
    private Rigidbody _rb;

    // Fields
    private Vector3 _originalRotation;

    // States
    private float _currentMovementSpeed = 0;
    private Tweener _movementTween;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.isKinematic = true;

        var rotation = transform.rotation.eulerAngles;
        _originalRotation = new Vector3(rotation.x, rotation.y, 0);

        // Select a random player model
        foreach (Transform child in _modelContainer.transform) Destroy(child.gameObject);
        Instantiate(_playerSettings.models[Random.Range(0, _playerSettings.models.Count)], transform);
    }

    private void FixedUpdate()
    {
        if (_currentMovementSpeed > 0f)
        {
            _rb.MovePosition(_rb.position + new Vector3(-_currentMovementSpeed, 0, 0) * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var otherGO = other.gameObject;
        
        if (otherGO.CompareTag(GameTags.Cuttable))
        {
            otherGO.GetComponent<ICuttable>().Cut(transform.position);
        }
        else if (otherGO.CompareTag(GameTags.Ground))
        {
            if (_rb.velocity.y > 0) return;
            _movementTween?.Kill();
            transform.DOKill();

            _currentMovementSpeed = 0f;
            _rb.isKinematic = true;
        }
    }

    public void OnInputTapped()
    {
        _currentMovementSpeed = _movementSpeed;
        _movementTween?.Kill();
        _movementTween = DOTween.To(
            getter: () => _currentMovementSpeed,
            setter: (value) => _currentMovementSpeed = value,
            endValue: 0f,
            duration: _movementDuration
        )
        .SetEase(_movementEase);

        transform.DOKill();
        transform
        .DORotate(
            endValue: _originalRotation + new Vector3(0, 0, -135),
            duration: _rotateDuration,
            mode: RotateMode.FastBeyond360
        )
        .SetEase(_rotateEase);
        // .SetLoops(-1);

        _rb.velocity = new Vector3(0, _jumpPower, 0);
        _rb.isKinematic = false;
    }
}
