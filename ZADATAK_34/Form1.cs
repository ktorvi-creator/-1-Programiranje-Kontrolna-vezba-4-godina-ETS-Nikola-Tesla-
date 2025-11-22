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
    public partial class Form1 : Form {

        private List<Student> studenti = new List<Student>();

        public Form1() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            Form2 frm = new Form2();
            if (frm.ShowDialog() == DialogResult.OK) {
                Student s = frm.NoviStudent;
                studenti.Add(s);
                listBox1.Items.Add(s);
            }
        }

        private void button3_Click(object sender, EventArgs e) {
            if(listBox1.SelectedItem != null) {
                studenti.RemoveAt(listBox1.SelectedIndex);
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            }
        }

        private void button2_Click(object sender, EventArgs e) {
            studenti.Clear();
            listBox1.Items.Clear();
        }
    }
}
