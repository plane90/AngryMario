using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stage", menuName = "Create Stage Data", order = 0)]
public class Stage : ScriptableObject
{
    public string sceneName = "";
    public Controllable machine;
}
