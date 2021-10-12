using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Minesweeper.GameMechanics;
using UnityEngine.SceneManagement;

namespace Minesweeper.UI
{
	/// <summary>
	/// Класс выбора количества ячеек на игровом поле
	/// </summary>
	public class FieldSizeDropdown : MonoBehaviour
	{
		#region Private Variables
		
		[SerializeField] private Dropdown _fieldSize; 
		[SerializeField] private CoreGameplay _coreGameplay;
		[SerializeField] private GridLayoutGroup _layoutGroup;
		private int _counter;
		
		#endregion
		
		#region Private Methods
		
		/// <summary>
		/// Настройка размеров поля
		/// </summary>
		private void DropdownValueChanged(Dropdown change)
        {
            switch(change.value)
            {
                case 0:
					_counter = 5;
                    break;
				case 1:
					_counter = 6;
                    break;
				case 2:
					_counter = 7;
                    break;
				case 3:
					_counter = 8;
                    break;
				case 4:
					_counter = 9;
                    break;
				case 5:
					_counter = 10;
                    break;
                default:
                    _counter = 5;
					break;
            }

			_coreGameplay.FieldSize = _counter;
			_layoutGroup.constraintCount = _counter;
        }
		
		#endregion
		
		#region MonoBehaviour
		
		void Start()
		{
			_fieldSize.onValueChanged.AddListener(delegate { DropdownValueChanged(_fieldSize); });	
		}
		
		#endregion
	}
}
