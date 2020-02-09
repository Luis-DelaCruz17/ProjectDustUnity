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
        //objeto de inputController

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

        private Vector2 newSpeed;
        private Vector2 mouseDelta;
        private float deltaT;

        //objeto de inputController
        public InputController _input; /* con _ para diferenciarlo de la clase */


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
            MoveControl();
            CameraControl();
            AnimControl();
        }

        private void PlayerControl()
        {
            _input.Update();

            //cambiar el Input por el _Input
            float deltaX = _input.CheckF("Horizontal");
            float deltaZ = _input.CheckF("Vertical");

            float mouseX = _input.CheckF("Mouse X");
            float mouseY = _input.CheckF("Mouse Y");

            moveDelta = new Vector2(deltaX, deltaZ);
            mouseDelta = new Vector2(mouseX, mouseY);

            deltaT = Time.deltaTime;

        }

        private void MoveControl()
        {
            Vector3 sp = rg.velocity;
            
            // Costados
            Vector3 side = speed * moveDelta.x *deltaT * tr.right;
            // Adelante
            Vector3 forward = speed * moveDelta.y * deltaT * tr.forward;

            Vector3 endSpeed = side + forward;

            // Asigna nueva velocidad al Rigidbody
            rg.velocity = endSpeed;
        }



        private void CameraControl()
        {
            
            
            // Calcular la cantidad de grados que el personaje debe girar
            // en cuestion sea derecha a izquierda o viceversa
            rotY += mouseDelta.y * deltaT * rotationSpeed;
            float xrot = mouseDelta.x * deltaT * rotationSpeed;
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
            anim.SetFloat("X", newSpeed.x);
            anim.SetFloat("Y", newSpeed.y);
        }
    }
}