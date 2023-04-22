using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

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
        public List<GameObject> entries = new();

        [SerializeField]
        public List<GameObject> stickies = new();

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

            AddSticky("A wild sticky has appeared!");

            yield return new WaitForSeconds(1);

            AddSticky("Another wild sticky has appeared!");

            yield return new WaitForSeconds(1);

            RemoveSticky();

            yield return new WaitForSeconds(1);

            RemoveSticky();
        }

        public void RemoveTask(GameObject task)
        {
            Destroy(task);
            entries.Remove(task);
        }

        public void RemoveSticky()
        {
            if(stickies.Any())
            {
                int i = stickies.Count - 1;
                Destroy(stickies[i]);
                stickies.RemoveAt(i);
            }
        }

        public void WriteTask(string text)
        {
            var entryObj = Instantiate(ClipboardTaskPrefab);
            var entryText = entryObj.GetComponentInChildren<TextMeshProUGUI>();

            entryObj.transform.SetParent(TaskEntryContainer, false);
            entryText.text = text;
            entryText.color = Color.black;
            entryObj.GetComponent<ClipboardTask>().TaskUI = this;

            entries.Add(entryObj);
        }

        public void AddSticky(string text)
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
            stickyText.font = fonts[randFont];
            stickyText.text = text;
            stickyText.color = Color.black;
            stickyObj.GetComponent<StickyNote>().TaskUI = this;

            stickies.Add(stickyObj);
        }
    #endregion
}
