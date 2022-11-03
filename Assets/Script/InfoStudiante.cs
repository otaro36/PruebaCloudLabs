using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoStudiante : MonoBehaviour
{
    public TMP_Text codStudent;
    public TMP_Text nameStudent;
    public TMP_Text lastnameStudent;
    public TMP_Text emailStudent;
    public TMP_Text finalNote;
    public Toggle passed;
    public int idStudent;
    public Button buttonDelete;
    
    public void UpdatePassed()
    {
        GameManager.instance.scritableData.listData.students[idStudent].aprobo = passed.isOn;
    }
}
