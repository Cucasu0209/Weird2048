using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace Simple2048
{
    public class UserInput : MonoBehaviour
    {
        public static event Action<UserInputType> OnSwipe;
        private List<Vector3> StartPositions = new List<Vector3>();
        public float d = 2;

        private void Update()
        {
            Vector2 startpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Input.touchCount > 0)
            {
                Touch t = Input.GetTouch(0);
                if (TouchPhase.Began == t.phase)
                {
                    SetStartPos(t.fingerId, Camera.main.ScreenToWorldPoint(t.position));
                }
                else
                {
                    Vector3 currentPos = Camera.main.ScreenToWorldPoint(t.position);
                    Vector3 startPos = StartPositions[t.fingerId];
                    Vector2 dir = (Vector2)(currentPos - startPos);
                    if (dir.magnitude > d)
                    {
                        OnSwipe?.Invoke(GetDirection((dir)));
                        SetStartPos(t.fingerId, currentPos);
                    }

                }
            }
        }

        private void SetStartPos(int id, Vector3 value)
        {
            if (StartPositions.Count <= id)
            {
                for (int i = StartPositions.Count; i <= id; i++)
                {
                    StartPositions.Add(Vector3.zero);
                }
            }
            StartPositions[id] = value;
        }

        private UserInputType GetDirection(Vector2 dir)
        {
            if (dir.magnitude > 2)
            {
                if (Math.Abs(dir.x) > Mathf.Abs(dir.y))
                {
                    if (dir.x > 0) return UserInputType.right;
                    else return UserInputType.left;
                }
                else
                {
                    if (dir.y > 0) return UserInputType.up;
                    else return UserInputType.down;
                }
            }
            return UserInputType.down;
        }
    }
    public enum UserInputType
    {
        up,
        down,
        left,
        right,

    }
}


