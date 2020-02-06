using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace project1
{
    public class CharController : MonoBehaviour
    {
        Transform tr;
        Rigidbody rg;
        Animator anim;

        public Transform CameraShoulder;
        public Transform CameraHolder;
        private Transform Cam;

        private float rotY = 0f;
        private float rotX = 0f;

        public float speed = 6;
        public float rotationSpeed = 25;
        public float minAngle = -70;
        public float maxAngle = 90;
        public float cameraSpeed = 24;

        void Start()
        {
            tr = this.transform;
            rg = GetComponent<Rigidbody>();
            anim = GetComponentInChildren<Animator>();
            Cam = Camera.main.transform;
        }

        
        void FixedUpdate()
        {
            PlayerControl();
            CameraControl();
            AnimControl();
        }

        private void PlayerControl()
        {
            Vector3 sp = rg.velocity;

            float deltaX = Input.GetAxis("Horizontal");
            float deltaZ = Input.GetAxis("Vertical");
            float deltaT = Time.deltaTime;

            // Costados
            Vector3 side = speed * deltaX * deltaT * tr.right;
            // Adelante
            Vector3 forward = speed * deltaZ * deltaT * tr.forward;

            Vector3 endSpeed = side + forward;

            // Asigna nueva velocidad al Rigidbody
            rg.velocity = endSpeed;
        }
        private void CameraControl()
        {
            // Para que el personaje gire con la camara es necesario
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            float deltaT = Time.deltaTime;

            // Calcular la cantidad de grados que el personaje debe girar
            // en cuestion sea derecha a izquierda o viceversa
            rotY += mouseY * deltaT * rotationSpeed;
            float xrot = mouseX * deltaT * rotationSpeed;
            // rotar al personaje
            tr.Rotate(0, xrot, 0);
            // poner limites al girar la camara
            rotY = Mathf.Clamp(rotY, minAngle, maxAngle);
            //rotar el cameraShoulder
            Quaternion localRotation = Quaternion.Euler(-rotY, 0, 0);
            CameraShoulder.localRotation = localRotation;

            Cam.position = Vector3.Lerp(Cam.position, CameraHolder.position,
                cameraSpeed * deltaT);
            Cam.rotation = Quaternion.Lerp(Cam.rotation, CameraHolder.rotation,
                cameraSpeed * deltaT);

        }

        private void AnimControl()
        {
            
        }
    }
}