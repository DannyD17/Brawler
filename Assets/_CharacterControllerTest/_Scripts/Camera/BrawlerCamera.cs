﻿using UnityEngine;

namespace Brawler
{
    public class BrawlerCamera : MonoBehaviour
    {

        [SerializeField]
        private Transform _targetTransform;

        [SerializeField]
        private float _distanceToTarget = 15.0f;

        [SerializeField]
        private float _followSpeed = 3.0f;

        public Transform targetTransform
        {
            get { return _targetTransform; }
            set { _targetTransform = value; }
        }

        public float distanceToTarget
        {
            get { return _distanceToTarget; }
            set { _distanceToTarget = Mathf.Max(0.0f, value); }
        }

        public float followSpeed
        {
            get { return _followSpeed; }
            set { _followSpeed = Mathf.Max(0.0f, value); }
        }

        private Vector3 cameraRelativePosition
        {
            get { return targetTransform.localPosition - transform.forward * distanceToTarget; }
        }

        public void OnValidate()
        {
            distanceToTarget = _distanceToTarget;
            followSpeed = _followSpeed;
        }

        public void Awake()
        {
            transform.position = cameraRelativePosition;
        }

        public void LateUpdate()
        {
            transform.position = Vector3.Lerp(transform.position, cameraRelativePosition, followSpeed * Time.deltaTime);
        }

    }
}