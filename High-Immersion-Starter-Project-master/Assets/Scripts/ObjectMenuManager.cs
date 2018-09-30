using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMenuManager : MonoBehaviour {

    public List<GameObject> ObjectList;
    public List<GameObject> ObjectPrefab;
    public List<int> ObjectCount;
    public List<int> ObjectsLeft;
    public int currentObjectList;

    public static ObjectMenuManager instance;
	// Use this for initialization
	void Start () {
        
        instance = this;
        for (int i = 0; i < ObjectCount.Count; i++)
        {
            ObjectsLeft[i] = ObjectCount[i];
            SetText(ObjectList[i],i);
        }
	}

    public void MenuLeft()
    {
        ObjectList[currentObjectList].SetActive(false);
        currentObjectList--;
        if (currentObjectList < 0)
            currentObjectList = ObjectList.Count - 1;
        ObjectList[currentObjectList].SetActive(true);
    }
    public void MenuRight()
    {
        ObjectList[currentObjectList].SetActive(false);
        currentObjectList++;
        if (currentObjectList > ObjectList.Count - 1)
            currentObjectList = 0;
        ObjectList[currentObjectList].SetActive(true);
    }

    public void SpawnCurrentObject()
    {

        if (ObjectsLeft[currentObjectList] > 0)
        {
            Instantiate(ObjectPrefab[currentObjectList], ObjectList[currentObjectList].transform.position + new Vector3(0f, -0.1f, 0.5f), ObjectPrefab[currentObjectList].transform.rotation);
            ObjectsLeft[currentObjectList]--;    
            SetText(ObjectList[currentObjectList], currentObjectList);
        }       
    }

    public void Disable()
    {
        ObjectList[currentObjectList].SetActive(false);
    }

    public void SetText(GameObject ToSet,int num)
    {
        Debug.Log(ToSet.transform.GetChild(0).GetChild(0));
        ToSet.transform.GetChild(0).GetChild(1).GetComponent<UnityEngine.UI.Text>().text = ObjectsLeft[num] + "/" + ObjectCount[num];
        
    }
}
