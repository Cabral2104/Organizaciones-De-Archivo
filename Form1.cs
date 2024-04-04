using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Organizaciones_De_Archivo
{
    public partial class Form1 : Form
    {
        // Rutas de archivos
        private string centralizadoFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "organizacionarchivo", "datos.txt");
        private string descentralizadoFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "organizacionarchivo", "Descentralizado");

        public Form1()
        {
            InitializeComponent();
            // Crear el directorio para los archivos descentralizados si no existe
            if (!Directory.Exists(descentralizadoFolder))
            {
                Directory.CreateDirectory(descentralizadoFolder);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Obtener datos del formulario
            string nombre = txtNombre.Text;
            int edad = (int)numEdad.Value;
            string correo = txtCorreo.Text;

            // Guardar datos en los diferentes tipos de organizaciones de archivo
            GuardarCentralizado(nombre, edad, correo);
            GuardarDescentralizado(nombre, edad, correo);
            GuardarMixto(nombre, edad, correo);

            // Limpiar campos después de guardar
            LimpiarCampos();
        }
        // Método para guardar en archivo centralizado
        private void GuardarCentralizado(string nombre, int edad, string correo)
        {
            using (StreamWriter sw = File.AppendText(centralizadoFile))
            {
                sw.WriteLine($"{nombre},{edad},{correo}");
            }
        }
        // Método para guardar en archivos descentralizados
        private void GuardarDescentralizado(string nombre, int edad, string correo)
        {
            string filePath = Path.Combine(descentralizadoFolder, $"{nombre}.txt");
            using (StreamWriter sw = File.CreateText(filePath))
            {
                sw.WriteLine($"Nombre: {nombre}");
                sw.WriteLine($"Edad: {edad}");
                sw.WriteLine($"Correo: {correo}");
            }
        }
        // Método para guardar en una combinación de archivos centralizados y descentralizados
        private void GuardarMixto(string nombre, int edad, string correo)
        {
            // Por ejemplo, guardar en archivos centralizados si la edad es menor o igual a 30
            // y en archivos descentralizados si la edad es mayor a 30
            if (edad <= 30)
            {
                GuardarCentralizado(nombre, edad, correo);
            }
            else
            {
                GuardarDescentralizado(nombre, edad, correo);
            }
        }
        // Método para limpiar campos del formulario
        private void LimpiarCampos()
        {
            txtNombre.Clear();
            numEdad.Value = 0;
            txtCorreo.Clear();
        }
    }
}
