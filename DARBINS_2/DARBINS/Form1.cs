using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DARBINS
{
    public partial class Form1 : Form
    {
        public bool Cilveka_gajiens = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void SpeletejaPunkti_TextChanged(object sender, EventArgs e)
        {

        }

        private void Exit(object sender, EventArgs e)
        {
            Close();
        }

        private void CHANGE(object sender, EventArgs e)
        {
            if (Cilveka_gajiens)
            {
                try
                {
                    string userInput = Indeks.Text;
                    string[] numbers = userInput.Split(';');
                    int num1 = Convert.ToInt32(numbers[0]);
                    int num2 = Convert.ToInt32(numbers[1]);
                    CilvekaCheckAdjacent(num1, num2);
                }
                catch
                {
                    MessageBox.Show("Notikusi kļūda ievadītājā skaitļu pāru indeksu virknē!",
                    "Kļūda", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }
            }
            else
            {
                SpelesKoks Sakne = new SpelesKoks();
                Sakne.SpelesStavoklis = Result.Text;
                Sakne.Limenis = 0;
                Sakne.CilvekaPunkti = Convert.ToInt32(SpeletejaPunkti.Text);
                Sakne.DatoraPunkti = Convert.ToInt32(DatoraPunkti.Text);
                KonstrueSpelesKoku(Sakne);
            }

        }
        private void KonstrueSpelesKoku(SpelesKoks mezgls) 
        {
            string stavoklis = mezgls.SpelesStavoklis;

            for (int i=0;i < stavoklis.Length-2;i++)
            {
                SpelesKoks berns = new SpelesKoks();
                berns.Limenis = 0;
                berns.SpelesStavoklis = stavoklis.Remove(i, i + 1);
                string pair = stavoklis.Substring(i, i+1);

                if (pair == "00")
                {
                    berns.CilvekaPunkti = mezgls.CilvekaPunkti+1;                                        
                }
                mezgls.PievienoBernu(berns);
                //Result.Text = Result.Text.Remove(num1, 2);
            }
            //SpelesKoks Sakne = new SpelesKoks();

            //Result.Text
        }
        private void CilvekaCheckAdjacent(int num1, int num2)
        {
            if (Math.Abs(num1 - num2) == 1)
            {
                CilvekaGajiens(num1, num2);
            }
            else
            {
                MessageBox.Show("Ievadītie indeksi nav blakusesošie",
                "Kļūda", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
        }
        private void CilvekaGajiens(int num1, int num2)
        {
            string pair = Result.Text.Substring(num1, 2); // Get the pair of numbers at the adjacent indices

            if (pair == "00")
            {
                SpeletejaPunkti.Text = (Convert.ToInt32(SpeletejaPunkti.Text) + 1).ToString(); // Add 1 to the player's score
                Result.Text = Result.Text.Remove(num1, 2); // Remove the pair from the sequence
            }
            else if (pair == "01")
            {
                DatoraPunkti.Text = (Convert.ToInt32(DatoraPunkti.Text) + 1).ToString(); // Add 1 to the opponent's score
                Result.Text = Result.Text.Remove(num1, 2); // Remove the pair from the sequence
            }
            else if (pair == "10")
            {
                SpeletejaPunkti.Text = (Convert.ToInt32(SpeletejaPunkti.Text) + 1).ToString(); // Add 1 to the player's score
                DatoraPunkti.Text = (Convert.ToInt32(DatoraPunkti.Text) - 1).ToString(); // Subtract 1 from the opponent's score
                Result.Text = Result.Text.Remove(num1, 2); // Remove the pair from the sequence
            }
            else if (pair == "11")
            {
                SpeletejaPunkti.Text = (Convert.ToInt32(SpeletejaPunkti.Text) + 1).ToString(); // Add 1 to the player's score
                Result.Text = Result.Text.Remove(num1,2); // Remove the pair from the sequence
            }

            Cilveka_gajiens = false;
            Indeks.ReadOnly = true;
            button1.Text = "Datora gājiens";
            Indeks.Text = "";
        }



        private void ENTER(object sender, EventArgs e)
        {
            try
            {
                if (!izvele_1.Checked && !izvele_2.Checked)
                {
                    MessageBox.Show("Netika izvēlēts, kurš uzsāk spēli!",
                    "Kļūda", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                    return;
                }

                int size = Convert.ToInt32(SkaitluVirkne.Text);
                SpeletejaPunkti.Text = "0";
                DatoraPunkti.Text = "0";

                if (size >= 15 && size <= 25)
                {
                    GenerateArrow(size);
                }
                else { 
                    MessageBox.Show("Netika ievadīts nepieciešamais skaitļu virknes garums!",
                    "Kļūda", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }
            }
            catch
            {
                MessageBox.Show("Notikusi kļūda ievadītājā virknes skaitļu garumā!",
                "Kļūda", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
        }

        private void GenerateArrow(int size)
        {
            izvele_1.Enabled = false;
            izvele_2.Enabled = false;

            Random random = new Random();

            string arrow = "";

            for (int i = 0; i < size; i++)
            {
                int direction = random.Next(0, 2); // Generates 0 or 1
                arrow += direction;
            }

            if (izvele_1.Checked)
            {
                button1.Text = "Cilvēka gājiens";
                Cilveka_gajiens = true;
            }
            else
            {
                button1.Text = "Datora gājiens";
                Indeks.ReadOnly = true;
            }
            button1.Enabled = true;

            SkaitluVirkne.ReadOnly = true;
            Result.Text = arrow;
        }



        private void RESTART(object sender, EventArgs e)
        {
            Application.Restart();

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
