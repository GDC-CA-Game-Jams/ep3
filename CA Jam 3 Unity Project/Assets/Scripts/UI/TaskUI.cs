using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskUI : MonoBehaviour
{
    #region Refs
        public static TaskUI Instance { get; private set; }

        [SerializeField]
        private int MaxEntries = 3;

        [SerializeField]
        private Transform TaskEntryContainer;

        [SerializeField]
        private GameObject TaskEntryPrefab;

        [SerializeField]
        private List<GameObject> entries = new();
    #endregion

    #region Accessors

    #endregion

    #region Built-In Methods
        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(this);
            else
                Instance = this;
        }

        private void Start()
        {
            WriteTask("This is a task!");
            StartCoroutine(TestTasks());
        }
    #endregion

    #region Custom Methods
        private IEnumerator TestTasks()
        {
            yield return new WaitForSeconds(1);

            WriteTask("This is a new task!");

            yield return new WaitForSeconds(1);

            WriteTask("This is what happens when you write a long task!");

            yield return new WaitForSeconds(1);

            WriteTask("This is another new task!");

            yield return new WaitForSeconds(1);

            RemoveTask(entries[2]);

            yield return new WaitForSeconds(1);

            WriteTask("This is another new task!");
        }

        public void RemoveTask(GameObject entry)
        {
            Destroy(entry);
            entries.Remove(entry);
        }

        public void WriteTask(string value)
        {
            var entryObj = Instantiate(TaskEntryPrefab);
            var entryText = entryObj.GetComponentInChildren<TextMeshProUGUI>();

            entryObj.transform.SetParent(TaskEntryContainer, false);
            entryText.text = value;
            entryText.color = Color.black;
            entryObj.GetComponent<TaskEntry>().TaskUI = this;

            entries.Add(entryObj);
        }
    #endregion
}
