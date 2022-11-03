using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "DataStudent")]
public class ScritableData : ScriptableObject
{
    [Serializable]
    public class DataStudent
    {
        public string nombre;
        public string apellido;
        public int codigoestudiante;
        public string correoinstitucional;
        public float notaFinal;
        public bool aprobo;
        public int edad;
    }
    [Serializable]
    public class ListDataStudent
    {
        public List<DataStudent> students = new List<DataStudent>();
    }
    public ListDataStudent listData;
}
