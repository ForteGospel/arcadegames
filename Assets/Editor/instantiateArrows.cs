using UnityEngine;
using UnityEditor;

public class Example
{
    [MenuItem("Examples/Instantiate Selected")]
    static void InstantiatePrefab()
    {
        GameObject parent = GameObject.Find("NoteParent");
        for (int i = 0; i < 300; i++)
        {         
            GameObject go = (GameObject)PrefabUtility.InstantiatePrefab(Selection.activeObject as GameObject);
            go.name = "note# " + (i + 1);
            go.transform.parent = parent.transform;
            go.GetComponent<noteController>().arrow = GetKeyCode(Random.Range(0, 4));
            go.transform.position = new Vector3(getXPos(go.GetComponent<noteController>().arrow), -2 + i * -1, 0);

            go.transform.rotation = Quaternion.Euler(0, 0, getZRot(go.GetComponent<noteController>().arrow));
        }
    }

    static float getXPos(KeyCode keyCode)
    {
        float pos = -3.5f;

        switch (keyCode)
        {
            case KeyCode.DownArrow:
                pos = -2.5f;
                break;
            case KeyCode.UpArrow:
                pos = -1.5f;
                break;
            case KeyCode.RightArrow:
                pos = -0.5f;
                break;
        }
        return pos;
    }

    static KeyCode GetKeyCode(int code)
    {
        KeyCode key = KeyCode.LeftArrow;

        switch (code)
        {
            case 1:
                key = KeyCode.DownArrow;
                break;
            case 2:
                key = KeyCode.UpArrow;
                break;
            case 3:
                key = KeyCode.RightArrow;
                break;
        }

        return key;
    }

    static float getZRot(KeyCode keyCode)
    {
        float rot = 0f;

        switch (keyCode)
        {
            case KeyCode.DownArrow:
                rot = 90f;
                break;
            case KeyCode.UpArrow:
                rot = -90f;
                break;
            case KeyCode.RightArrow:
                rot = 180f;
                break;
        }
        return rot;
    }
}