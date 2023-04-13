using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskEntry : MonoBehaviour
{
    #region Refs
        private TextMeshProUGUI taskText;
        private TaskUI taskUI;
    #endregion

    #region Accessors
        public TaskUI TaskUI { set { taskUI = value; } }
    #endregion

    #region Built-In Methods
        void Start()
        {
            taskText = GetComponentInChildren<TextMeshProUGUI>();
        }
    #endregion

    #region Custom Methods
        public void DestroyFeedback()
        {
            taskUI.RemoveTask(this.gameObject);
        }
    #endregion
}
