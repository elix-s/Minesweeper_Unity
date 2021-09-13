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
		
		#endregion
		
		#region Private Methods
		
		/// <summary>
		/// Настройка размеров игрового поля
		/// </summary>
		private void DropdownValueChanged(Dropdown change)
        {
            switch(change.value)
            {
                case 0:
					_coreGameplay.FieldSize = 5;
					_layoutGroup.constraintCount = 5;
                    break;
				case 1:
					_coreGameplay.FieldSize = 6;
					_layoutGroup.constraintCount = 6;
                    break;
				case 2:
					_coreGameplay.FieldSize = 7;
					_layoutGroup.constraintCount = 7;
                    break;
				case 3:
					_coreGameplay.FieldSize = 8;
					_layoutGroup.constraintCount = 8;
                    break;
				case 4:
					_coreGameplay.FieldSize = 9;
					_layoutGroup.constraintCount = 9;
                    break;
				case 5:
					_coreGameplay.FieldSize = 10;
					_layoutGroup.constraintCount = 10;
                    break;
                default:
                    _coreGameplay.FieldSize = 5;
					_layoutGroup.constraintCount = 5;
					break;
            }
        }
		
		#endregion
		
		#region MonoBehaviour
		
		void Start()
		{
			_fieldSize.onValueChanged.AddListener(delegate {
                DropdownValueChanged(_fieldSize);
            });	
		}
		
		#endregion
	}
}
