using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class Numeros : MonoBehaviour
{
    private string filePath; // Ruta del archivo
    public Data data = new Data(); // Instancia de la clase Data
    public TextMeshProUGUI cantidadText; // Referencia al texto en la UI

    private void Awake()
    {
        // Configurar la ruta del archivo según la plataforma
        filePath = Path.Combine(Application.persistentDataPath, "data.json");
    }

    private void Start()
    {
        LoadData(); // Cargar datos al iniciar el juego
    }

    [System.Serializable]
    public class Data
    {
        public int cantidad;
    }

    public void SaveData()
    {
        string json = JsonUtility.ToJson(data); // Convertir los datos a JSON
        File.WriteAllText(filePath, json); // Guardar en el archivo
        Debug.Log("Datos guardados");
    }

    public void LoadData()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath); // Leer el archivo JSON
            data = JsonUtility.FromJson<Data>(json); // Deserializar el JSON
            Debug.Log("Datos cargados: " + data.cantidad);
            UpdateCantidadText();
        }
        else
        {
            Debug.Log("No se encontró el archivo");
        }
    }

    private void UpdateCantidadText()
    {
        if (cantidadText != null)
        {
            cantidadText.text = "Cantidad: " + data.cantidad;
        }
    }

    public void IncrementarCantidad()
    {
        data.cantidad++;
        UpdateCantidadText();
    }

    public void DecrementarCantidad()
    {
        if (data.cantidad > 0) // Evitar valores negativos
        {
            data.cantidad--;
            UpdateCantidadText();
        }
    }

}
