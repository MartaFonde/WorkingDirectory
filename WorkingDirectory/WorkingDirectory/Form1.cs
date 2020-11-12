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
            DirectoryInfo d = null;
            if (textBox1.Text.StartsWith("%") && textBox1.Text.EndsWith("%"))
            {
                string enVar = textBox1.Text.Substring(1, textBox1.Text.Length - 2);
                try
                {
                    chDir = Environment.GetEnvironmentVariable(enVar);
                }
                catch (ArgumentNullException)
                {
                    label2.Text = "Environment variable not found";
                }
                catch (SecurityException)
                {
                    label2.Text = "Access unauthorized";
                }
            }
            else
            {
                chDir = textBox1.Text;
            }
            if (ComprobarDirectorio(chDir))
            {
                bool valido = false;                
                label2.Text = "";
                try
                {
                    valido = true;
                    d = new DirectoryInfo(Directory.GetCurrentDirectory());
                }
                catch (UnauthorizedAccessException)
                {
                    label2.Text = "Access unauthorized";
                    valido = false;
                }
                if (valido)
                {
                    textBox2.Text = "CURRENT DIRECTORY:   " + d.FullName+ "\r\n\r\n";
                    textBox2.Text += "SUBDIRECTORIES\r\n";
                    if (d.GetDirectories().Length > 0)
                    {
                        foreach (DirectoryInfo dir in d.GetDirectories())
                        {
                            textBox2.Text += dir.Name + "\r\n";
                        }
                    }
                    else
                    {
                        textBox2.Text += "Not found";
                    }


                    textBox2.Text += "\r\n \r\nFILES\r\n";
                    if (d.GetFiles().Length > 0)
                    {
                        foreach (FileInfo arch in d.GetFiles())
                        {
                            textBox2.Text += arch.Name + "\r\n";
                        }
                    }
                    else
                    {
                        textBox2.Text += "Not found";
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
                label2.Text = "Directory not found";
                return false;
            }
            catch (SecurityException)
            {
                label2.Text = "Access denied";
                return false;
            }
            catch (ArgumentException)
            {
                label2.Text = "Paht invalid";
                return false;
            }
            catch (IOException ex)
            {
                label2.Text = "Error: it is a file";
                return false;
            }
            return true;
        }
    }
}
