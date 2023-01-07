using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class message : MonoBehaviour
{
    public List<task> tasks = new List<task>();

    // Start is called before the first frame update
    void Start()
    {
        if (tasks.Count != 0)
        {
            string warning = "";
            int i = 0;
            foreach (task t in tasks)
            {
                i++;
                warning += i + "." + t.name + " ";
            }

            Debug.LogError(warning);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public class task
{
    public string name = "default";
    public string text = "This is task you should do";
}
