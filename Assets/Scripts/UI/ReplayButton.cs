using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Minesweeper.UI
{
    /// <summary>
    /// События кнопки Replay в UI
    /// </summary>
    public class ReplayButton : MonoBehaviour
    {
        #region Public Methods
        
        public void HideButton()
        {
            gameObject.SetActive(false);
        }
        
        public void ShowButton()
        {
            gameObject.SetActive(true);
        }
        
        public void Replay()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene ().buildIndex);
        }
        
        #endregion
        
        #region MonoBehaviour
        
        void Start()
        {
            HideButton();
        }

        #endregion
    }
}
