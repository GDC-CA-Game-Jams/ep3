using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;

public class TaskUI : MonoBehaviour
{
    #region Refs
        public static TaskUI Instance { get; private set; }

        [SerializeField]
        private int MaxEntries = 3;

        [SerializeField]
        private Transform TaskEntryContainer;

        [SerializeField]
        private Transform StickySpawn;

        [SerializeField]
        private GameObject ClipboardTaskPrefab;

        [SerializeField]
        private GameObject StickyNotePrefab;

        [SerializeField]
        private Sprite CheckBoxEmpty;

        [SerializeField]
        private Sprite CheckBoxTicked;

        public Dictionary<TaskSO, GameObject> entries = new();

        public Stack<(TaskSO, GameObject)> stickies = new();

        [SerializeField]
        private List<TMP_FontAsset> fonts = new();
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
    #endregion

    #region Custom Methods
        public void RemoveTask(TaskSO task)
        {
            Destroy(entries[task]);
            entries.Remove(task);
        }

        public void CompleteTask(TaskSO task)
        {
            var img = entries[task].GetComponentInChildren<Image>();
            img.sprite = CheckBoxTicked;
            entries.Remove(task);
        }

        public void RemoveSticky()
        {
            if(stickies.Any())
            {
                Destroy(stickies.Peek().Item2);
                stickies.Pop();
            }
        }

        public void WriteTask(TaskSO task)
        {
            var entryObj = Instantiate(ClipboardTaskPrefab);
            var entryText = entryObj.GetComponentInChildren<TextMeshProUGUI>();
            var entryImg = entryObj.GetComponentInChildren<Image>();

            entryObj.transform.SetParent(TaskEntryContainer, false);
            entryObj.GetComponent<ClipboardTask>().TaskUI = this;

            entryText.text = task.GenerateUIText();
            entryText.color = Color.black;

            entryImg.sprite = CheckBoxEmpty;

            entries.Add(task, entryObj);
        }

        public void AddSticky(TaskSO task)
        {
            var stickyObj = Instantiate(StickyNotePrefab);
            var stickyText = stickyObj.GetComponentInChildren<TextMeshProUGUI>();

            float randRotation = Random.Range(-30.0f, 30.0f);
            float randX = Random.Range(-30.0f, 30.0f);
            float randY = Random.Range(-30.0f, 30.0f);
            int randFont = Random.Range(0, fonts.Count() - 1);
            
            stickyObj.transform.localPosition = new Vector3(randX, randY, 0.0f);
            stickyObj.transform.Rotate(0.0f, 0.0f, randRotation, Space.Self);
            stickyObj.transform.SetParent(StickySpawn, false);
            stickyObj.GetComponent<StickyNote>().TaskUI = this;

            stickyText.font = fonts[randFont];
            stickyText.text = task.GenerateUIText();
            stickyText.color = Color.black;

            stickies.Push((task, stickyObj));
        }

        public void UpdateTask(TaskSO task)
        {
            if (!entries.ContainsKey(task))
            {
                return;
            }

            entries[task].GetComponentInChildren<TextMeshProUGUI>().text = task.GenerateUIText();
        }

        public void UpdateSticky(TaskSO task)
        {
            if (!stickies.Any())
            {
                return;
            }

            stickies.Peek().Item2.GetComponentInChildren<TextMeshProUGUI>().text = task.GenerateUIText();
        }

        internal void ClearAll()
        {
            for(int i = 0; i < stickies.Count(); i++)
            {
                RemoveSticky();
            }

            Dictionary<TaskSO, GameObject> temp = entries;
            for (int i = 0; i < temp.Count; ++i)
            {
                RemoveTask(temp.ElementAt(i).Key);
            }
        }
    #endregion
}
