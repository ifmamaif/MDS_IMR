using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AtestatInformatica
{
    public partial class Form2 : Form
    {
        int size = 30, viteza = 10;
        int n = 20;// maxim 25 ; 20 preferabil
        int[,] a = new int[22, 22];
        int npcx, npcy;
        bool sus, jos, dreapta, stanga, miscare, comunicare, taste = true;
        public string moment = "normal", matrice;
        PictureBox[,] pictureBoxpadure1 = new PictureBox[21, 21];
        int[] v = new int[2];
        public Form2()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
            tabControl1.Enabled = false;
            tabControl1.Top = 0;
            tabControl1.Left = 0;
            tabControl1.SendToBack();
            timer1.Interval = 60;
            timer1.Enabled = true;
            timer1.Stop();

            for (int i = 1; i <= n + 1; i++) a[n + 1, i] = 1;
            for (int i = 1; i <= n + 1; i++) a[i,n+1] = 1;

            this.AutoSize = true;
        }

        private void Form2_Load(object sender, EventArgs e)
        {          
               for (int i = 1; i <= n; i++)
                for (int j = 1; j <= n; j++)
                {
                    pictureBoxpadure1[i, j] = new PictureBox();
                    pictureBoxpadure1[i, j].Top = size * (i - 1);
                    pictureBoxpadure1[i, j].Left = size * (j - 1);
                    pictureBoxpadure1[i, j].Height = size;
                    pictureBoxpadure1[i, j].Width =  size;
                    pictureBoxpadure1[i, j].SizeMode = PictureBoxSizeMode.StretchImage;
                    tabPage3.Controls.Add(pictureBoxpadure1[i, j]);
                    pictureBoxpadure1[i, j].SendToBack();
                    pictureBoxpadure1[i, j].Visible = true;
                    pictureBoxpadure1[i, j].Enabled = false;
                }
           pictureBox2.Top =pictureBox2.Left = 0;
           pictureBox2.Height =pictureBox2.Width = size * n;
           pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
           pictureBox2.SendToBack();
           pictureBox2.ImageLocation = "Resources/teren01.bmp";
           pictureBox2.Visible = true;
           pictureBox3.Top =pictureBox3.Left = 0;
           pictureBox3.Height =pictureBox3.Width = size * n;
           pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
           pictureBox3.SendToBack();
           pictureBox3.ImageLocation = "Resources/tcpc.bmp";
           pictureBox3.Visible = true;

            CitesteFisier("matrice.txt");
            timer1.Start();
            pictureBox1.Visible = true;
        }

        private void CitesteFisier(string matrice)
        {
            System.IO.StreamReader t = new System.IO.StreamReader("Resources/" + matrice);
            var a2 = t.ReadToEnd().Split('\n');
            n = a2.Length; t.Close(); t = null;
            for (int i = 1; i <= n; i++)
            {
                var a3 = a2[i - 1].Split(' ');
                a[i, 1] = 0;
                for (int j = 1; j <= n; j++)
                {
                    a[i, j] = Convert.ToInt32(a3[j - 1]);
                    if (a[i, j] == 13) activJucator(i-1, j-1); else
                    if(a[i,j] == -11)
                    {
                        npcx = i - 1; npcy = j - 1;
                        npc.Top = npcx * size;
                        npc.Left = npcy * size;
                        npc.Height = size;
                        npc.Width = size;
                        npc.SizeMode = PictureBoxSizeMode.StretchImage;             
                        npc.Enabled = true;
                        npc.Visible = true;
                        npc.ImageLocation = "Resources/npcjos.bmp";                 
                        npc.BringToFront();
                                                   
                    }
                }
                a3 = null;
            }
            a2 = null;
        }

        private void activJucator(int x, int y)
        {        
            pictureBox1.Top = size * x;
            pictureBox1.Left = size * y;
            pictureBox1.Height = size;
            pictureBox1.Width = size;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.BringToFront();
            pictureBox1.Visible = true;
            if (moment == "normal") pictureBox1.ImageLocation = "Resources/bd02.bmp";
            else
            pictureBox1.ImageLocation = "Resources/bd22.bmp";
        }

        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            if (taste == true)
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult dialogResult = MessageBox.Show("Esti sigur ca vrei sa inchizi aplicatia?", "Sistem de iesire", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    this.Close();
                    //do something
                }
                else if (dialogResult == DialogResult.No)
                {
                    //do something else
                }
            }
            if (e.KeyCode == Keys.Down)
            {
                timer1.Start();
                jos = true;
                comunicare = true;
            }
            if (e.KeyCode == Keys.Up)
            {
                timer1.Start();
                sus = true;
                comunicare = true;
            }
            if (e.KeyCode == Keys.Left)
            {
                timer1.Start();
                stanga = true;
                comunicare = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                timer1.Start();
                dreapta = true;
                comunicare = true;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            if ((pictureBox1.Top) / size == npcx && (pictureBox1.Left + size - viteza) / size - 1 == npcy && stanga == true)
                {
                    timer1.Stop();
                npc.ImageLocation = "Resources/npcdreapa.bmp";
                mesaj("da");
                    timer1.Start();
                }
                else
                if ((pictureBox1.Top) / size == npcx && (pictureBox1.Left) / size + 1 == npcy && dreapta == true)
                {
                    timer1.Stop();
                npc.ImageLocation = "Resources/npcstanga.bmp";
                mesaj("da");
                    timer1.Start();
                }
                else
                if ((pictureBox1.Top) / size + 1 == npcx && (pictureBox1.Left) / size == npcy && jos == true)
                {
                    timer1.Stop();
                npc.ImageLocation = "Resources/npcsus.bmp";
                mesaj("da");
                    timer1.Start();
                }
                else
                if ((pictureBox1.Top + size - viteza) / size - 1 == npcx && (pictureBox1.Left) / size == npcy && sus == true)
                {
                    timer1.Stop();
                npc.ImageLocation = "Resources/npcjos.bmp";
                mesaj("da");
                    timer1.Start();
                }
        }

        private void Form2_KeyUp(object sender, KeyEventArgs e)
        {
            if (taste == true)   
            if (e.KeyCode == Keys.Down)
            {
                jos = false;
                if (moment == "normal")
                    pictureBox1.ImageLocation = "Resources/bd02.bmp";
                else
                    pictureBox1.ImageLocation = "Resources/bd22.bmp";
            }
            if (e.KeyCode == Keys.Up)
            {
                sus = false;
                if (moment == "normal")
                    pictureBox1.ImageLocation = "Resources/bu02.bmp";
                else
                    pictureBox1.ImageLocation = "Resources/bu22.bmp";
            }
            if (e.KeyCode == Keys.Left)
            {
                stanga = false;
                if (moment == "normal")
                    pictureBox1.ImageLocation = "Resources/bl02.bmp";
                else
                    pictureBox1.ImageLocation = "Resources/bl22.bmp";
            }
            if (e.KeyCode == Keys.Right)
            {
                dreapta = false;
                if (moment == "normal")
                    pictureBox1.ImageLocation = "Resources/br02.bmp";
                else
                    pictureBox1.ImageLocation = "Resources/br22.bmp";
            }
        }


        private void mesaj(string text)
        {
            if (comunicare == true)
            {
                comunicare = false;
                timer1.Stop();
                miscare = stanga = dreapta = sus = jos = false;
                DialogResult d = MessageBox.Show("Te rog sa ma ajuti cu stransul florilor alfabet! \r\nFlorile arata astfel : A B C D E F G H I J K L M N O P Q R S T U W X Y Z", "Sofie :", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (d == DialogResult.Yes) MessageBox.Show("Multumesc!", "Sofie :", MessageBoxButtons.OK);
                
                else if (d == DialogResult.No) MessageBox.Show("Nu te pot obliga!", "Sofie :", MessageBoxButtons.OK);
                timer1.Start();
            }
        }

        private void copac()
        {
            timer1.Stop();
            tabControl1.SelectedIndex = 1;
            pictureBox1.Visible = false;
            tabPage2.Controls.Add(pictureBox1);
            moment = "copac";
            CitesteFisier("copac.txt");
            pictureBox1.Visible = true;
            timer1.Start();

        }

        private void normal(bool ok)
        {
            pictureBox1.Visible = false;
            timer1.Stop();
            tabControl1.SelectedIndex = 0;
            pictureBox1.Visible = false;
            tabPage1.Controls.Add(pictureBox1);
            moment = "normal";
            CitesteFisier("matrice.txt");
            if (ok == true)
                activJucator(10, 15); else
            { activJucator(19, 12); pictureBox1.ImageLocation = "Resources/bu02.bmp"; }
            pictureBox1.Visible = true;
            timer1.Start();
            
        }

        private void padure(int poz)
        {
            a[(pictureBox1.Top) / size + 1, (pictureBox1.Left) / size + 1] = 0;
            if (poz == 4) v[0]--;
            else
               if (poz == 6) v[0]++;
            else
               if (poz == 2) v[1]--;
            else
               if (poz == 8) v[1]++;
            if (v[0] == 0 && v[1] == 0) normal(false); else
            {
                pictureBox1.Visible = false;
                timer1.Stop();
                taste = false;
                stanga = dreapta = sus = jos = false;
                pictureBox1.Visible = false;
                tabControl1.SelectedIndex = 2;
                tabPage3.Controls.Add(pictureBox1);
                int x; Random nr = new Random();
                for (int i = 1; i <= n; i++)
                    for (int j = 1; j <= n; j++)
                    {
                        if (i == 1 && a[i, j] > 0) a[i, j] = 10;
                        else
                        if (j == 1 && a[i, j] > 0) a[i, j] = 10;
                                                else
                        if (i == n && a[i, j] > 0) a[i, j] = 10;
                                                else
                        if (j == n && a[i, j] > 0) a[i, j] = 10;
                        x = nr.Next(1, 100);
                        if (x < 65)
                        {
                            a[i, j] = 1;
                            pictureBoxpadure1[i, j].ImageLocation = "Resources/simple.bmp";
                        }
                        else if (x < 99)
                        {
                            a[i, j] = 0;
                            pictureBoxpadure1[i, j].ImageLocation = "Resources/tc.bmp";
                        }
                        else
                         if(i>1 && j>1 && i<n && j < n)
                        { 
                            a[i, j] = 71;
                            x = nr.Next(1, 27);
                            pictureBoxpadure1[i, j].ImageLocation = "Resources/litere/" + x + ".png";
                        }

                    }
                for (int i = 1; i <= n; i++)
                {
                    x = nr.Next(1, n);
                    if (poz == 2) { if (a[n, x] >=1) { pictureBox1.Top = size * (n - 1); pictureBox1.Left = size * x; break; } } 
                    if (poz == 4) { if (a[x, 1] >= 1) { pictureBox1.Top = size * x; pictureBox1.Left = 0; break; } } 
                    if (poz == 6) { if (a[x, n] >= 1) { pictureBox1.Top = size * x; pictureBox1.Left = size * (n - 1); break; } } 
                    if (poz == 8) { if (a[1, x] >= 1) { pictureBox1.Top = 0; pictureBox1.Left = size * x; break; } }
                }
                pictureBox1.BringToFront();
                pictureBox1.Visible = true;
                stanga = dreapta = sus = jos = false;
                taste = true;
            }

        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }
        private void iesireToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void versiuneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Program realizat de Ifrim Marius", "Versiune Program", MessageBoxButtons.OK);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = pictureBox1.Top + " " + pictureBox1.Left + " "+ a[(pictureBox1.Top) / size + 1, (pictureBox1.Left) / size + 1];
            if (pictureBox1.Top < 0) { a[(pictureBox1.Top) / size + 1, (pictureBox1.Left) / size + 1] = 0; padure(2); }
            if (pictureBox1.Top > 570) { a[(pictureBox1.Top) / size + 1, (pictureBox1.Left) / size + 1] = 0; padure(8); }
            if (pictureBox1.Left < 0) { a[(pictureBox1.Top) / size + 1, (pictureBox1.Left) / size + 1] = 0; padure(4); }
            if (pictureBox1.Left > 570) { a[(pictureBox1.Top) / size + 1, (pictureBox1.Left) / size + 1] = 0; padure(6); }
            if (a[(pictureBox1.Top) / size + 1, (pictureBox1.Left) / size +1] >= 70)
            {
                a[(pictureBox1.Top) / size + 1, (pictureBox1.Left) / size + 1] = 1;
                pictureBoxpadure1[(pictureBox1.Top) / size + 1, (pictureBox1.Left) / size + 1].ImageLocation = "Resources/simple.bmp";
            }
            if (sus == true)
            {
                if (miscare == false)
                {
                    if (moment == "normal")
                        pictureBox1.ImageLocation = "Resources/bu01.bmp";
                    else
                        pictureBox1.ImageLocation = "Resources/bu21.bmp";
                    miscare = true;
                }
                else
                {
                    if (moment == "normal")
                        pictureBox1.ImageLocation = "Resources/bu03.bmp";
                    else
                        pictureBox1.ImageLocation = "Resources/bu23.bmp";
                    miscare = false;
                }
                if ((pictureBox1.Top) / size >= 0)
                {
                    if ((pictureBox1.Left) % size == 0)
                    {
                        if (a[(pictureBox1.Top - viteza) / size + 1, (pictureBox1.Left) / size + 1] > 0)
                        {
                            pictureBox1.Top -= viteza;
                        }
                    }
                    else
                        if (a[(pictureBox1.Top - viteza) / size + 1, (pictureBox1.Left) / size + 1] > 0 &&
                            a[(pictureBox1.Top - viteza) / size + 1, (pictureBox1.Left) / size + 2] > 0 )
                    {
                        pictureBox1.Top -= viteza;
                    }
                } 
            }
            if (jos == true)
            {
                if (miscare == false)
                {
                    if (moment == "normal")
                        pictureBox1.ImageLocation = "Resources/bd01.bmp";
                    else
                        pictureBox1.ImageLocation = "Resources/bd21.bmp";
                    miscare = true;
                }
                else
                {
                    if (moment == "normal")
                        pictureBox1.ImageLocation = "Resources/bd03.bmp";
                    else
                        pictureBox1.ImageLocation = "Resources/bd23.bmp";
                    miscare = false;
                }
                if ((pictureBox1.Top) / size <= n+1)
                {
                    if ((pictureBox1.Left) % size == 0)
                    {
                        if (a[(pictureBox1.Top) / size + 2, (pictureBox1.Left) / size + 1] > 0)
                        {
                            pictureBox1.Top += viteza;
                        }
                    }
                    else
                        if (a[(pictureBox1.Top) / size + 2, (pictureBox1.Left) / size + 1] > 0 &&
                            a[(pictureBox1.Top) / size + 2, (pictureBox1.Left) / size + 2] > 0)
                    {
                        pictureBox1.Top += viteza;
                    }
                }
            }
            if (dreapta == true)
            {
                if (miscare == false)
                {
                    if (moment == "normal")
                        pictureBox1.ImageLocation = "Resources/br01.bmp";
                    else
                        pictureBox1.ImageLocation = "Resources/br21.bmp";
                    miscare = true;
                }
                else
                {
                    if (moment == "normal")
                        pictureBox1.ImageLocation = "Resources/br03.bmp";
                    else
                        pictureBox1.ImageLocation = "Resources/br23.bmp";
                    miscare = false;
                }
                if ((pictureBox1.Left) / size <= n )
                {
                    if ((pictureBox1.Top) % size == 0)
                    {
                        if (a[(pictureBox1.Top) / size + 1, (pictureBox1.Left) / size + 2] > 0)
                        {
                            pictureBox1.Left += viteza;
                        }
                    }
                    else
                        if (a[(pictureBox1.Top) / size + 1, (pictureBox1.Left) / size + 2] > 0 &&
                            a[(pictureBox1.Top) / size + 2, (pictureBox1.Left) / size + 2] > 0)
                    {
                        pictureBox1.Left += viteza;
                    }
                }
            }
            if (stanga == true)
            {
                if (pictureBox1.Left >= -1)
                {
                    if (miscare == false)
                    {
                        if (moment == "normal")
                            pictureBox1.ImageLocation = "Resources/bl01.bmp";
                        else
                            pictureBox1.ImageLocation = "Resources/bl21.bmp";
                        miscare = true;
                    }
                    else
                    {
                        if (moment == "normal")
                            pictureBox1.ImageLocation = "Resources/bl03.bmp";
                        else
                            pictureBox1.ImageLocation = "Resources/bl23.bmp";
                        miscare = false;
                    }
                    if ((pictureBox1.Left) / size >= 0)
                    {
                        if ((pictureBox1.Top) % size == 0)
                        {
                            if (a[(pictureBox1.Top) / size + 1, (pictureBox1.Left - viteza) / size + 1] > 0)
                            {
                                if (pictureBox1.Left != 0) pictureBox1.Left -= viteza;
                            }
                        }
                        else
                            if (a[(pictureBox1.Top) / size + 1, (pictureBox1.Left - viteza) / size + 1] > 0 &&
                                a[(pictureBox1.Top) / size + 2, (pictureBox1.Left - viteza) / size + 1] > 0)
                        {
                            pictureBox1.Left -= viteza;
                        }
                    }
                }
            }
            switch (a[(pictureBox1.Top) / size + 1, (pictureBox1.Left) / size + 1])
            {
                case 52: { copac(); break; }
                case 51: { normal(true); break; }
            }

        }
    }
}
