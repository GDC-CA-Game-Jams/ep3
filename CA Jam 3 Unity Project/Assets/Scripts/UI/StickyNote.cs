using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;

public class StickyNote : MonoBehaviour
{
    #region Refs
        private TextMeshProUGUI stickyText;
        private TaskUI taskUI;
    #endregion

    #region Accessors
        public TaskUI TaskUI { set { taskUI = value; } }
    #endregion

    #region Built-In Methods
        void Start()
        {
            stickyText = GetComponentInChildren<TextMeshProUGUI>();
        }
    #endregion

    #region Custom Methods
        public void DestroySticky()
        {
            taskUI.RemoveSticky();
        }
    #endregion
}
