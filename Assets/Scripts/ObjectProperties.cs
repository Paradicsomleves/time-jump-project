using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class ObjectProperties : MonoBehaviour
{
    public string objectName;
    public enum OfTypes { item, person }
    public OfTypes objectType;
    string[] overheadText = new string[] { "Press 'E' to pick up", "Press 'E' to talk" };

    public Vector3 textOffset;

    public string customOverheadText;

    private void Start()
    {
        if (objectName == "")
        {
            objectName = gameObject.name;
        }

        if (customOverheadText == "")
        {
            switch (objectType)
            {
                case OfTypes.item:
                    customOverheadText = overheadText[0]; break;
                case OfTypes.person:
                    customOverheadText = overheadText[1]; break;
                default:
                    break;
            }
        }
    }
}
