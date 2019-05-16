using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRockPosition : MonoBehaviour
{
    public static SetRockPosition Instance;

    private void Start()
    {
        if (Instance == null) Instance = this;
        else if (Instance != null) Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
    }

    public void SetPosition(GameObject rockTr)
    {
        rockTr.transform.position = this.transform.position;

        this.gameObject.GetComponentInChildren<ObjectHold>().SetRock(rockTr);
    }
}
