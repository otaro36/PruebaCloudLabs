using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckStudent : MonoBehaviour
{
    public Transform passed;
    public Transform reprobate;
    private void Start()
    {
        GameManager.instance.LoadListStudentDragDrop();

    }
    // Update is called once per frame
    void Update()
    {

    }
}
