using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Minesweeper.UI;

namespace Minesweeper.GameMechanics
{
    /// <summary>
    /// Основной класс игры, инициализация игрового поля, бомб, логика нажатия на ячейку
    /// </summary>
    public class CoreGameplay : MonoBehaviour
    {
        #region Private Variables
        
        [SerializeField] private CellComponent _cellPrefab;
        [SerializeField, Tooltip("Кол-во бомб")] private int _bombCount;
        [SerializeField] private ReplayButton _replayButton;
        [SerializeField] private GameObject _playButton;
        [SerializeField, Tooltip("Размер поля")] private GameObject _dropdownFieldSize;
        private CellComponent[,] _grid;

        /// <summary>
        /// счетчик ячеек, которые нужно открыть
        /// </summary>
        private int _clickableCells;

        /// <summary>
        /// массив всех ячеек игрового поля
        /// </summary>
        private CellComponent[] _allCells;

        /// <summary>
        /// счетчик открытых ячеек
        /// </summary>
        private int _openCells;
        
        #endregion
        
        #region Public Variables
        
        /// <summary>
        /// размер поля
        /// </summary>	
        public int FieldSize;
        
        #endregion
    
        #region Private Methods

        /// <summary>
        /// Стартовая инициализация ячеек
        /// </summary>	
        private void GridInitialize()
        {
            _grid = new CellComponent[FieldSize, FieldSize];
            
            for(int i = 0; i < FieldSize; i++)
                for(int j = 0; j <FieldSize; j++)
                {
                    _grid[i, j] = Instantiate(_cellPrefab, transform);
                    
                    //задаем положение ячейки по осям x и y
                    _grid[i, j].PositionX = i;
                    _grid[i, j].PositionY = j;
                }		
        }
        
        /// <summary>
        /// Стартовая инициализация бомб
        /// </summary>
        private void BombsInitialize()
        {
            int count = 0;
            
            while(count < _bombCount)
            {
                int position = Random.Range(0, FieldSize * FieldSize);
                int x = position / FieldSize;
                int y = position % FieldSize;
                
                if(_grid[x, y].IsBomb == false)
                {
                    _grid[x, y].SetBomb();
                    count++;
                }
            }
        }
        
        /// <summary>
        /// Считаем количество занятых соседних ячеек у каждой ячейки
        /// </summary>
        private void NeighboursBombInitialize()
        {
            for(int i = 0; i < FieldSize; i++)
                for(int j = 0; j <FieldSize; j++)
                    _grid[i, j].NeighborBombs = GetNeighborsBombCount(i, j);
        }
        
        private int GetNeighborsBombCount(int x, int y)
        {
            int count = 0;
            
            for(int i = -1; i <= 1; i++)
                for(int j = -1; j <= 1; j++)
                {
                    if(x + i < 0 || x + i >= FieldSize || y + j < 0 || y + j >= FieldSize)
                        continue;
                        
                    if(_grid[x + i, y + j].IsBomb)
                        count++;
                }
            
            return count;
        }
        
        /// <summary>
        /// Открываем все ячейки при проигрыше
        /// </summary>
        private void OpenAllCells()
        {
            for(int i = 0; i < FieldSize; i++)
                for(int j = 0; j <FieldSize; j++)
                    _grid[i, j].OpenCell();		
        }
        
        /// <summary>
        /// Открываем свободные соседния ячейки, алгоритм - поиск по ширине
        /// </summary>
        private void OpenFreeCell(CellComponent cell)
        {
            List<CellComponent> emptyCells = new List<CellComponent>();
            emptyCells.Add(cell);
            
            while(emptyCells.Count > 0)
            {
                CellComponent firstCell = emptyCells[0];
                emptyCells.RemoveAt(0);
                
                firstCell.OpenCell();
                if(firstCell.NeighborBombs > 0)
                    continue;
                
                for(int i = -1; i <= 1; i++)
                    for(int j = -1; j <= 1; j++)
                    {
                        int x = firstCell.PositionX + i;
                        int y = firstCell.PositionY + j;
                        
                        if(x < 0 || x >= FieldSize || y < 0 || y >= FieldSize)
                            continue;
                        
                        if(_grid[x, y].IsBomb == false && _grid[x, y].IsOpen == false)
                            emptyCells.Add(_grid[x, y]);
                    }		
            }
        }
        
        /// <summary>
        /// Подсчет ячеек, которые нужно открыть для победы
        /// </summary>
        private void ClickableCellsCounter()
        {
            _clickableCells = FieldSize * FieldSize - _bombCount;
        }
        
        /// <summary>
        /// Подсчет открытых ячеек
        /// </summary>
        private void OpenCellCounter()
        {
            int openCells = 0;
            foreach(var cell in _allCells)
            {
                if(cell.GetComponent<CellComponent>().IsOpen == true)
                {
                    openCells++;
                    Debug.Log(openCells);
                }
            }
            
            if(openCells == _clickableCells)
            {
                Debug.Log("Win");
            }
        }
        
        #endregion
        
        #region Public Methods
        
        /// <summary>
        /// Обрабатываем клик по ячейке
        /// </summary>
        public void OnCellPressed(CellComponent cell)
        {
            if(cell.IsBomb)
            {
                OpenAllCells();
                _replayButton.ShowButton();
                return;
            }
            
            if(cell.NeighborBombs == 0)
            {
                OpenFreeCell(cell);
            }

            cell.OpenCell();
            OpenCellCounter();
        }
        
        /// <summary>
        /// Запуск игры
        /// </summary>
        public void Play()
        {
            GridInitialize();
            BombsInitialize();
            NeighboursBombInitialize();
            ClickableCellsCounter();
            _allCells = FindObjectsOfType<CellComponent>();
            _playButton.SetActive(false);
            _dropdownFieldSize.SetActive(false);
        }
        
        #endregion
    }
}