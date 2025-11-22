using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace vezba10 {
    public partial class Form2 : Form {

        public Student NoviStudent { get; set; }

        public Form2() {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e) {
            comboBox1.Items.Add("Menadzment");
            comboBox1.Items.Add("Informacione tehnologije");
            comboBox1.Items.Add("Elektrotehnicar Racunara");
            comboBox1.Items.Add("Telekomunikacije");
            comboBox1.Items.Add("Energetika");
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e) {

            NoviStudent = new Student {
                Ime = textBox1.Text,
                Prezime = textBox2.Text,
                BrojIndeksa = textBox3.Text,
                DatumRodjenja = dateTimePicker1.Value,
                Smer = comboBox1.Text,
            };

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e) {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
