using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace Simple2048
{
    public class GameManager : MonoBehaviour
    {
        private void OnEnable()
        {
            UserInput.OnSwipe += OnSwipe;
        }
        private void OnDisable()
        {
            UserInput.OnSwipe -= OnSwipe;
        }
        private void OnSwipe(UserInputType dir)
        {
            Debug.Log(dir.ToString());
        }
    }
}
