using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public ScritableData scritableData;
    public GameObject panelListStudent;
    public GameObject panelPopUp;
    public GameObject panelNewStudent;
    public Transform panelListContainer;
    public Transform popCheckInst;
    public Button buttonCheck;
    public Button buttonNewStuden;
    public GameObject studentIcon;
    public Transform neutralZone;
    public GameObject panelNeutral;
    public GameObject panelPased;
    public GameObject panelReprobate;
    public GameObject firstStep;
    public GameObject secondStep;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
        ReadToJson();

    }
    public void LoadListStudent(int id)
    {
        GameObject panelStudent = Instantiate(panelListStudent,panelListContainer);
        InfoStudiante go = panelStudent.GetComponent<InfoStudiante>();
        go.codStudent.text = scritableData.listData.students[id].codigoestudiante.ToString();
        go.nameStudent.text = scritableData.listData.students[id].nombre;
        go.lastnameStudent.text = scritableData.listData.students[id].apellido;
        go.emailStudent.text = scritableData.listData.students[id].correoinstitucional;
        go.finalNote.text = scritableData.listData.students[id].notaFinal.ToString();
        go.idStudent=id;
        go.passed.isOn = scritableData.listData.students[id].aprobo;
        go.buttonDelete.onClick.AddListener(()=>DeleteStudent(id));
    }
    public void LoadListStudentDragDrop()
    {
        int passed=0;
        int reprobate=0;
        for (int i = 0; i < scritableData.listData.students.Count; i++)
        {
            GameObject panelStudent = Instantiate(studentIcon, neutralZone);
            DragDrop go = panelStudent.GetComponent<DragDrop>();
            go.nameStudent.text = scritableData.listData.students[i].nombre;
            go.finalNote = scritableData.listData.students[i].notaFinal;
            go.passed = scritableData.listData.students[i].aprobo;
            go.id = i;

            if (scritableData.listData.students[i].aprobo)
            {
                passed++;
            }
            else
            {
                reprobate++;
            }

        }
        panelPased.GetComponent<ItemSlot>().allStudent= scritableData.listData.students.Count;
        panelReprobate.GetComponent<ItemSlot>().allStudent= scritableData.listData.students.Count;
        panelPased.GetComponent<ItemSlot>().passed = passed;
        panelPased.GetComponent<ItemSlot>().reprobate = reprobate;
        panelReprobate.GetComponent<ItemSlot>().passed = passed;
        panelReprobate.GetComponent<ItemSlot>().reprobate = reprobate;
    }
    public void SaveToJson()
    {
        JsonManager.SaveToJson<ScritableData.DataStudent>(scritableData.listData.students, "/estudiantes.json");
        ReadToJson();
    }
    public void ReadToJson()
    {
        scritableData.listData.students = JsonManager.ReadJson<ScritableData.DataStudent>("/estudiantes.json");
        for (int i = 0; i < panelListContainer.childCount; i++)
        {
            Destroy(panelListContainer.GetChild(i).gameObject);
        }
        for (int i = 0; i < scritableData.listData.students.Count; i++)
        {
            LoadListStudent(i);
        }
    }
    public void CheckPassed()
    {
        SaveToJson();
        int faults = 0;
        for (int i = 0; i < scritableData.listData.students.Count; i++)
        {
            if (scritableData.listData.students[i].notaFinal<3 && scritableData.listData.students[i].aprobo==true)
            {
                faults++;
                InstanciaPopUp(i, " y no le alcanza para aprobar Por favor Desmarcalo Y vuelve a verificar",true);
                Debug.Log("aaaaaaa");
            }
            else if (scritableData.listData.students[i].notaFinal >= 3 && scritableData.listData.students[i].aprobo == false)
            {
                faults++;
                InstanciaPopUp(i," y le alcanza para aprobar Por favor marcalo Y vuelve a verificar",true);
            }
        }
        if (faults==0)
        {
            InstanciaPopUpCongratulaciones("Toda la lista ha sido validada y no se encontraron errores");
        }

    }
    public void InstanciaPopUp(int id, string mensaje,bool act)
    {
        GameObject popUp = Instantiate(panelPopUp, popCheckInst);
        popUp.transform.GetChild(0).GetComponent<TMP_Text>().text = "Advertencia";
        popUp.transform.GetChild(0).GetComponent<TMP_Text>().color = Color.red;
        popUp.transform.GetChild(1).GetComponent<TMP_Text>().text = "El estudiante " + scritableData.listData.students[id].nombre + " tiene una nota de " + scritableData.listData.students[id].notaFinal + mensaje;
        popUp.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => buttonCheck.interactable = true);
        popUp.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => Destroy(popUp.gameObject));
        popUp.SetActive(act);
    }
    public void InstanciaPopUpCongratulaciones(string mensaje)
    {
        GameObject popUp = Instantiate(panelPopUp, popCheckInst);
        popUp.transform.GetChild(0).GetComponent<TMP_Text>().text = "Felicitaciones";
        popUp.transform.GetChild(0).GetComponent<TMP_Text>().color = Color.green;
        popUp.transform.GetChild(1).GetComponent<TMP_Text>().text = mensaje;
        popUp.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => buttonCheck.interactable = true);
        popUp.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => Destroy(popUp.gameObject));
        popUp.transform.GetChild(3).gameObject.SetActive(true);
        popUp.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => firstStep.SetActive(false));
        popUp.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => secondStep.SetActive(true));
        popUp.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => Destroy(popUp.gameObject));
    }

    public void CheckAprobateDragDrop()
    {
        if (popCheckInst.childCount==0) 
        {
            GameObject popUp = Instantiate(panelPopUp, popCheckInst);
            popUp.transform.GetChild(0).GetComponent<TMP_Text>().text = "Felicitaciones";
            popUp.transform.GetChild(0).GetComponent<TMP_Text>().color = Color.green;
            popUp.transform.GetChild(1).GetComponent<TMP_Text>().text = "Todos los studiantes quedaron bien ubicados";
            popUp.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => buttonCheck.interactable = true);
            popUp.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => Destroy(popUp.gameObject));
            popUp.transform.GetChild(3).gameObject.SetActive(true);
            popUp.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene("SampleScene"));
        }
    }
    public void ActivatePopUp()
    {
        for (int i = 0; i < popCheckInst.childCount; i++)
        {
            popCheckInst.transform.GetChild(i).gameObject.SetActive(true);
        }
    }
    public void DeleteStudent(int id)
    {
        scritableData.listData.students.RemoveAt(id);
        SaveToJson();
    }
    public void NewStudent(string nameStudent,string lastNameStudent, int cod,string email,float finalNote,bool passed,int edad )
    {
        ScritableData.DataStudent student = new ScritableData.DataStudent();
        student.nombre=nameStudent;
        student.apellido = lastNameStudent;
        student.codigoestudiante = cod;
        student.correoinstitucional= email;
        student.notaFinal=finalNote;
        student.aprobo= passed;
        student.edad= edad;
        scritableData.listData.students.Add(student);
        SaveToJson();
    }
    public void InstanciaNewStuden()
    {
        GameObject popUp = Instantiate(panelNewStudent, popCheckInst);
        SaveInfo popUpSave=popUp.transform.GetChild(1).GetComponent<SaveInfo>();
        popUpSave.button.onClick.AddListener(() => NewStudent(popUpSave.nameStudnet.text, popUpSave.lastNameStudnet.text, int.Parse(popUpSave.code.text), popUpSave.emailStudnet.text, float.Parse(popUpSave.finalNoteStudnet.text), popUpSave.passed.isOn, int.Parse(popUpSave.edad.text)));
        popUpSave.button.onClick.AddListener(() => buttonNewStuden.interactable = true);
        popUpSave.button.onClick.AddListener(() => Destroy(popUp));

    }

}
