using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestApplication
{
    public partial class LSystem : Form
    {
        private string axiom = null;
        
        StringBuilder stringBuilder = new StringBuilder();
        
        Dictionary<char, string> rules = new Dictionary<char, string>();
        
        private int count = 0;
        public LSystem()
        {
            InitializeComponent();
            displayGenerate.BackColor = Color.BlueViolet;

            rules.Add('A', "ABA");
            rules.Add('B', "BBB");
            rules.Add('F', "F+F−F−F+F");

        }
        private void generateButton_Click_1(object sender, EventArgs e)
        {
            Generate(int.Parse(lSystemPrompt.Text));
        }
        private void button2_Click(object sender, EventArgs e)
        {
            axiom = "A";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            axiom = "F";
        }
        
        private void LSystem_Load(object sender, EventArgs e)
        {
            
        }

        private void Generate(int num) {

            stringBuilder.Clear();

            for (int i = 0; i < axiom.Length; i++)
            {
                char axiomConverted = axiom[i];
                 
                switch(axiomConverted)
                {
                    case 'A':
                        stringBuilder.Append(rules['A']);
                    break;

                    case 'B':
                        stringBuilder.Append(rules['B']);
                    break;

                    case 'F':
                        stringBuilder.Append(rules['F']);
                    break;

                    default:
                        stringBuilder.Append("");
                    break;
                }
                
            }
            
            axiom = stringBuilder.ToString();
            count++;
            displayGenerate.AppendText(String.Format("Generation : " + count + " => " + axiom + "\n"));
            
            if (num >= 1)
            {
                Generate(num - 1);
            }
            
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            var showForm = new Form2();
            showForm.Show();
            Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            displayGenerate.Clear();
            
        }

    }
}
