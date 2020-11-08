using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ejer4Tema3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string chDir="";
            if(textBox1.Text.StartsWith("%") && textBox1.Text.EndsWith("%"))
            {
                string enVar = textBox1.Text.Substring(1, textBox1.Text.Length - 2);
                try
                {
                    chDir = Environment.GetEnvironmentVariable(enVar);
                }
                catch (ArgumentNullException)
                {
                    label2.Text = "Variable de entorno no encontrada";
                }
                catch (SecurityException)
                {
                    label2.Text = "Acceso no autorizado";
                }
            }
            else
            {
                chDir = textBox1.Text;
            }
            if (ComprobarDirectorio(chDir))
            {
                bool valido = false;
                DirectoryInfo d = null;
                label2.Text = "";
                try
                {
                    valido = true;
                    d = new DirectoryInfo(Directory.GetCurrentDirectory());
                }
                catch (UnauthorizedAccessException)
                {
                    label2.Text = "Acceso no autorizado";
                    valido = false;
                }
                if (valido)
                {
                    textBox2.Text = "SUBDIRECTORIOS\r\n";
                    if (d.GetDirectories().Length > 0)
                    {
                        foreach (DirectoryInfo dir in d.GetDirectories())
                        {
                            textBox2.Text += dir.Name + "\r\n";
                        }
                    }
                    else
                    {
                        textBox2.Text += "No contiene subdirectorios";
                    }


                    textBox2.Text += "\r\n \r\nARCHIVOS\r\n";
                    if (d.GetFiles().Length > 0)
                    {
                        foreach (FileInfo arch in d.GetFiles())
                        {
                            textBox2.Text += arch.Name + "\r\n";
                        }
                    }
                    else
                    {
                        textBox2.Text += "No contiene archivos";
                    }
                }                                
            }            
        }

        private bool ComprobarDirectorio(string chDir)
        {
            try
            {
                Directory.SetCurrentDirectory(chDir);

            }
            catch (DirectoryNotFoundException)
            {
                label2.Text = "Directorio no encontrado";
                return false;
            }
            catch (SecurityException)
            {
                label2.Text = "No tienes el permiso requerido";
                return false;
            }
            catch (ArgumentException)
            {
                label2.Text = "Ruta no válida";
                return false;
            }
            catch (IOException ex)
            {
                label2.Text = "Error: " + ex.Message;
                return false;
            }
            return true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {            
            if(textBox1.TextLength == 0)
            {
                label2.Text = "";
            }
        }
    }
}
