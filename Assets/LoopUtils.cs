using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopUtils : MonoBehaviour
{
    
    void Start()
    {
         IDictionary<int, bool> doneList = new Dictionary<int, bool>();
        doneList.Add(3, true);

        Debug.Log("3");
        Debug.Log(DoneAlready(doneList, 3));
        Debug.Log("2");
        Debug.Log(DoneAlready(doneList, 2));
    }

    bool DoneAlready(IDictionary<int, bool> theDictionary, int theItem)
    {
        bool tmpExists = false;
        theDictionary.TryGetValue(theItem, out tmpExists);
        return tmpExists;
    }

}
