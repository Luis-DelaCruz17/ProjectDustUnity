using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Para que sea multiplataforma
namespace project1
{
    [System.Serializable]
    public class InputController
    {
        public List<InputKey> keys = new List<InputKey>();

        private Dictionary<string, InputKey> mappedKeys = new Dictionary<string, InputKey>();

        bool initialized = false;
        private void Initialize()
        {
            if (initialized)
                return;
            foreach (InputKey key in keys)
            {
                // si el mappedKeys no lo encuentra
                if (!mappedKeys.ContainsKey(key.Name))
                {
                    // lo añade
                    mappedKeys.Add(key.Name, key);
                }

            }
        }

        public bool Check(string name)
        {
            // si el mappedKeys no contiene name
            if (!mappedKeys.ContainsKey(name))
                return false;
            // si lo contiene devuelve CHECK
            return mappedKeys[name].Check();

        }

        public float CheckF(string name)
        {
            // si el mappedKeys no contiene name
            if (!mappedKeys.ContainsKey(name))
                return 0f;

            // si lo contiene devuelve CHECKF
            return mappedKeys[name].CheckF();
        }
        public void Update()
        {
            Initialize();
            foreach (InputKey key in keys)
                key.Update();
        }
    }
    public enum InputType
    {
        down,
        up,
        release,
        pressed
    }

    [System.Serializable]
    public class InputKey
    {
        public string Name;
        public InputType _Type;
        public float Sensibility = 0.1f;

        private float act;
        private float prev;

        public bool Check()
        {
            switch (_Type)
            {
                // si estamos presionando el boton
                case InputType.down: return act > Sensibility;  // igual a presionando el boton
                case InputType.up: return act < Sensibility;    // igual a dejar de presionar el boton
                case InputType.release: return act < prev;      // igual a dejar de presionar el boton
                case InputType.pressed: return act > prev;      // igual a dejar de presionar el boton
                default: return false;
            }
        }

        public float CheckF()
        {
            return act;
        }
        public bool Update()
        {
            prev = act;
            act = Input.GetAxis(Name);
        }
    }
}