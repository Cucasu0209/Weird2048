using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
namespace Simple2048
{
    public class BoardManager : MonoBehaviour
    {
        // bảng bắt đầu từ góc dưới-trái bắt đầu từ (i,j)= (0,0) sang phải
        // nên index của 1 ô = (i * column + j);
        private List<int> boardData;
        private List<NumberPiece> boardDisplayed;
        bool[] boardMask;
        [Header("Initial Setup")]
        public int row;
        public int column;
        public float gap = 10;
        public Image Board;
        public Image pieceBGPrefab;

        [Header("Create Board")]
        public NumberPiece piecePrefab;
        public void Start()
        {
            InitializeBoard();
        }
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
            Shift(dir);
        }
        private void InitializeBoard()
        {
            boardData = new List<int>();
            for (int i = 0; i < row * column; i++)
            {
                boardData.Add(-1);
            }

            boardDisplayed = new List<NumberPiece>();
            //for (int i = 0; i < row * column; i++)
            //{
            //    boardDisplayed.Add(null);
            //}

            boardMask = new bool[row * column];
            CreateBoard(row, column, Board, pieceBGPrefab);

            CreateRandom(2);
            CreateRandom(2);
        }

        public void CreateBoard(int r, int c, Image board, Image prefab)
        {
            for (int i = 0; i < c; i++)
            {
                for (int j = 0; j < r; j++)
                {
                    NumberPiece newCell = GameObject.Instantiate(piecePrefab, board.transform);
                    newCell.gameObject.name = $"{i + 1}x{j + 1}";
                    newCell.SetValue(-1);
                    RectTransform piecerect = newCell.GetComponent<RectTransform>();
                    piecerect.anchorMax = Vector2.zero;
                    piecerect.anchorMin = Vector2.zero;

                    piecerect.sizeDelta = new Vector2(board.rectTransform.sizeDelta.x / c - gap, board.rectTransform.sizeDelta.y / r - gap);
                    piecerect.anchoredPosition = new Vector2(
                        board.rectTransform.sizeDelta.x / c * (i + 1 / 2f),
                        board.rectTransform.sizeDelta.y / r * (j + 1 / 2f));


                    boardDisplayed.Add(newCell);
                }
            }
        }
        public void DisplayBoard()
        {
            for (int i = 0; i < row * column; i++)
            {
                boardDisplayed[i].SetValue(boardData[i]);
            }
        }
        public NumberPiece CreatePiece(int index, int value)
        {
            NumberPiece newPiece = Instantiate(piecePrefab, Board.transform);
            RectTransform piecerect = newPiece.GetComponent<RectTransform>();
            piecerect.anchorMax = Vector2.zero;
            piecerect.anchorMin = Vector2.zero;
            piecerect.sizeDelta = new Vector2(Board.rectTransform.sizeDelta.x / column - gap, Board.rectTransform.sizeDelta.y / row - gap);
            piecerect.anchoredPosition = new Vector2(
                     Board.rectTransform.sizeDelta.x / column * (GetPos(index).y + 1 / 2f),
                     Board.rectTransform.sizeDelta.y / row * (GetPos(index).x + 1 / 2f));
            newPiece.SetValue(value);
            newPiece.transform.localScale = Vector3.zero;
            newPiece.transform.DOScale(1, 0.3f);
            return newPiece;
        }
        private void CreateRandom(int value)
        {
            List<int> emptyIndex = GetEmptySlotIndexes();
            if (emptyIndex.Count <= 0) return;
            int randomId = emptyIndex[Random.Range(0, emptyIndex.Count)];

            //boardDisplayed[randomId] = CreatePiece(randomId, value);
            boardData[randomId] = value;
            DisplayBoard();
        }
        private void Shift(UserInputType type)
        {
            //handle data
            for (int i = 0; i < row * column; i++)
            {
                boardMask[i] = false;
            }
            for (int i = 0; i < row * column; i++)
            {
                Shiftindex(i, type);
            }

            //handle display
            DisplayBoard();
            CreateRandom(2);
        }

        public void Shiftindex(int i, UserInputType type)
        {
            Debug.Log("1");
            if (boardData[i] < 0) return;
            Debug.Log("1");
            int indexInfront = GetInFrontIndex(i, type);
            if (i == indexInfront) boardMask[i] = true;
            else
            {
                if (boardData[indexInfront] < 0)
                {
                    boardData[indexInfront] = boardData[i];
                    boardData[i] = -1;
                }
                else if (boardMask[indexInfront] == false)
                {
                    Shiftindex(indexInfront, type);
                    indexInfront = GetInFrontIndex(i, type);
                    boardMask[indexInfront] = true;
                    if (boardMask[indexInfront] == false && boardData[i] == boardData[indexInfront])
                    {
                        //merge
                        boardData[indexInfront] += boardData[i];
                        boardData[i] = -1;
                    }
                    else if (indexInfront - ShiftDistance(type) != i)
                    {

                        boardData[indexInfront - ShiftDistance(type)] = boardData[i];
                        boardData[i] = -1;
                    }
                }
                else
                {
                    if (indexInfront - ShiftDistance(type) != i)
                    {

                        boardData[indexInfront - ShiftDistance(type)] = boardData[i];
                        boardData[i] = -1;
                    }
                }
            }



        }
        private int GetInFrontIndex(int i, UserInputType type)
        {
            for (int j = 0; j < Mathf.Max(row, column); j++)
            {
                if (((i + ShiftDistance(type) + column) % column == i % column + ShiftDistance(type) && boardData[i + ShiftDistance(type)] > 0)
                    || (i + ShiftDistance(type) >= 0 && i + ShiftDistance(type) < row * column && boardData[i + ShiftDistance(type)] > 0))
                {
                    i += ShiftDistance(type);
                }
                else break;
            }

            return i;
        }
        private int ShiftDistance(UserInputType type) => (type == UserInputType.up) ? column :
                          (type == UserInputType.down) ? -column :
                          (type == UserInputType.left) ? -1 :
                          (type == UserInputType.left) ? 1 : 0;

        #region BoardData Get
        private int GetIndex(int i, int j) => i * column + j;
        private Vector2 GetPos(int index) => Vector2.right * (index / column) + Vector2.up * (index % column);
        private List<int> GetEmptySlotIndexes()
        {
            List<int> result = new List<int>();
            for (int i = 0; i < row * column; i++)
            {
                if (boardData[i] < 0) result.Add(i);

            }
            return result;
        }
        #endregion
    }
}
