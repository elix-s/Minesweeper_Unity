using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Minesweeper.UI;
using UnityEngine.EventSystems;

namespace Minesweeper.GameMechanics
{
	/// <summary>
	/// Класс логики ячеек игрового поля
	/// </summary>
	public class CellComponent : MonoBehaviour, IPointerClickHandler
	{
		#region Private Variables
		
		[SerializeField] private Image _backgound;
		[SerializeField] private Text _bombCountText;
		[SerializeField] private Image _bombImageComponent;
		[SerializeField] private Sprite _bombImage;
		[SerializeField] private Sprite _flagImage;
		private CoreGameplay _coreGameplay;
		private bool _setFlag;
		
		#endregion
		
		#region Properties
		
		public int NeighborBombs {get; set;}
		public int PositionX {get; set;}
		public int PositionY {get; set;}
		public bool IsBomb {get; private set;}
		public bool IsOpen {get; private set;}
		
		#endregion
		
		#region Public Methods
		
		public void SetBomb()
		{
			IsBomb = true;
		}
		
		/// <summary>
		/// Действия при открытии ячеек
		/// </summary>
		public void OpenCell()
		{
			IsOpen = true;
			_backgound.enabled = false;
			
			if(IsBomb)
			{
				_bombImageComponent.enabled = true;
				_bombImageComponent.sprite = _bombImage;
			}
			else
			{
				_bombCountText.text = NeighborBombs > 0 ? NeighborBombs.ToString() : "";
			}
		}
		
		public void OnCellPressed()
		{
			if(_setFlag == false)
				_coreGameplay.OnCellPressed(this);
		}

		/// <summary>
		/// Метод установки флага
		/// </summary>
		public void OnPointerClick(PointerEventData eventData)
		{
			if (eventData.button == PointerEventData.InputButton.Right)
				if(_setFlag == false)
				{
					_bombImageComponent.enabled = true;
					_bombImageComponent.sprite = _flagImage;
					_setFlag = true;
				}
				else
				{
					_bombImageComponent.enabled = false;
					_setFlag = false;
				}
		}
		
		#endregion
		
		#region MonoBehaviour
		
		void Start()
		{
			_coreGameplay = FindObjectOfType<CoreGameplay>();
			_bombCountText.text = "";
			_bombImageComponent.enabled = false;
			_setFlag = false;
		}
	
		#endregion
	}
}
