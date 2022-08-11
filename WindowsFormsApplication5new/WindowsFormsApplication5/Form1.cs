using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace WindowsFormsApplication5
{

    public enum Months
    {
        January = 1,
        Febraury,
        March,
        April,
        May,
        June,
        July,
        August,
        September,
        October,
        November,
        December

    };
    public enum Days
    {
        Su = 0,
        M,
        Tu,
        W,
        Th,
        Fr,
        Sa
    }
    public partial class Form1 : Form
    {

        static int CurrentDisplayMonth;
        static int CurrentDisplayYear;
        static int WeekDay;
        static int selectedDay;
        static int NumberOfDays;
        static Dictionary<string, string> HolidayDates = new Dictionary<string, string>();
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Label lbl = (Label)sender;
            selectedDay = Convert.ToInt32(lbl.Text);
            labelDate.Text = lbl.Text + " " + (Months)CurrentDisplayMonth + " " + CurrentDisplayYear;
        }
        private void label1_Hover(object sender, EventArgs e)
        {
            Label lbl = (Label)sender;
            lbl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        }
        private void label1_Leave(object sender, EventArgs e)
        {
            Label lbl = (Label)sender;
            lbl.BorderStyle = System.Windows.Forms.BorderStyle.None;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString("hh:mm:ss tt");
            //  label1.Update();
            DateTime dateValue = new DateTime(2008, 6, 11);
            label2.Text = DateTime.Now.ToString("dddd , MMM dd yyyy");
            label3.Text = (Months)CurrentDisplayMonth + "  " + CurrentDisplayYear;


        }
        private void Form1_Load(object sender, EventArgs e)
        {
            CurrentDisplayMonth = DateTime.Now.Month;
            CurrentDisplayYear = DateTime.Now.Year;

            labelDate.Text = DateTime.Now.ToString("dd MMMM yyyy");
            GridCreate(7, 7);
            CalendarPrint(CurrentDisplayMonth, CurrentDisplayYear);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            CurrentDisplayMonth--;
            if (CurrentDisplayMonth == 0)
            {
                CurrentDisplayMonth = 12;
                CurrentDisplayYear--;
            }
            CalendarPrint(CurrentDisplayMonth, CurrentDisplayYear);
            CheckEvent();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            CurrentDisplayMonth++;
            if (CurrentDisplayMonth == 13)
            {
                CurrentDisplayMonth = 1;
                CurrentDisplayYear++;
            }
            CalendarPrint(CurrentDisplayMonth, CurrentDisplayYear);
            CheckEvent();
        }
        public void CalendarPrint(int Month, int Year)
        {
            NumberOfDays = 0;
            int LastNumberOfDays = 0;
            int LastMonth = Month - 1;
            if (LastMonth == 0)
            {
                LastMonth = 12;
            }
            NumberOfDays=GetDays(Month, Year);
            LastNumberOfDays = GetDays(LastMonth, Year);
            DateTime dateValue = new DateTime(Year, Month, 1);
            WeekDay = (int)dateValue.DayOfWeek;

            for (int i = WeekDay + 11; i < NumberOfDays + 11 + WeekDay; i++)
            {

                Label L = Controls.Find(String.Format("Label{0}", i), true).FirstOrDefault() as Label;
                L.Text = (i - WeekDay - 10).ToString();
                L.BackColor = Color.Black;
                L.ForeColor = Color.White;
                //L.Text = (i).ToString();

            }
            for (int i = NumberOfDays + 11 + WeekDay; i < (46 + 7); i++)
            {

                Label L = Controls.Find(String.Format("Label{0}", i), true).FirstOrDefault() as Label;
                L.Text = ((i + 1) - (NumberOfDays + 11 + WeekDay)).ToString();
                L.BackColor = Color.Black;
                L.ForeColor = Color.Gray;
                //L.Text = (i).ToString();

            }
            for (int i = WeekDay + 10; i != 10; i--)
            {
                Label L = Controls.Find(String.Format("Label{0}", i), true).FirstOrDefault() as Label;
                L.Text = (LastNumberOfDays).ToString();
                LastNumberOfDays--;
                L.BackColor = Color.Black;
                L.ForeColor = Color.Gray;
            }
        }
        public void GridCreate(int row,int column) {
            int DayCount = 4;
            Label[] Tiles = new Label[(row*column)];
            for (int i = 0; i < row; i++)
            {
                for (int n = 0; n < column; n++)
                {

                    Tiles[i] = new Label();
                    Tiles[i].Size = new Size(45, 45);
                    Tiles[i].Location = new System.Drawing.Point(n * 45 + 100, 200 + i * 45);
                    Tiles[i].Name = String.Format("Label{0}", DayCount);
                    Tiles[i].ForeColor = System.Drawing.Color.Black;
                    Tiles[i].BackColor = System.Drawing.Color.White;
                    Tiles[i].BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D; ;
                    Tiles[i].AutoSize = false;
                    Tiles[i].BorderStyle = System.Windows.Forms.BorderStyle.None;
                    Tiles[i].Click += new System.EventHandler(this.label1_Click);
                    Tiles[i].MouseHover += new System.EventHandler(this.label1_Hover);
                    // Tiles[i].MouseHover += new System.EventHandler(this.label1_Hover);
                    Tiles[i].MouseLeave += new System.EventHandler(this.label1_Leave);
                    Tiles[i].TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                    this.Controls.Add(Tiles[i]);
                    
                    toolTip1.SetToolTip(Tiles[i], "No event");
                    if (i == 0)
                    {
                        Tiles[i].Text = ((Days)n).ToString();
                        Tiles[i].ForeColor = Color.White;
                        Tiles[i].BackColor = Color.Black;
                    }
                    else
                        Tiles[i].Text = (i).ToString();

                    DayCount++;
                }

            }
        }
  
        public void CheckEvent() {
            for (int i = 10 + WeekDay; i < 10 + WeekDay + NumberOfDays; i++)
            {
                foreach (KeyValuePair<string, string> element in HolidayDates)
                {
                    Label L = Controls.Find(String.Format("Label{0}", i), true).FirstOrDefault() as Label;
                    if ((L.Text + "/" + (CurrentDisplayMonth).ToString() + "/" + (CurrentDisplayYear).ToString()) == element.Key)
                    {

                        L.BackColor = Color.LightBlue;
                        toolTip1.SetToolTip(L, element.Value);
                        break;
                    }
                    else
                    {
                        toolTip1.SetToolTip(L, "No event");
                    }
                }
            }
           
        }
        public int GetDays(int month,int Year) {
            int NumberOfDays=0;
        switch (month)
        {
            case 1:
            case 3:
            case 5:
            case 7:
            case 8:
            case 10:
            case 12:

                NumberOfDays = 31;
                break;
            case 4:
            case 6:
            case 9:
            case 11:
                NumberOfDays = 30;
                break;
            case 2:
                if (((Year % 4 == 0) &&
                     !(Year % 100 == 0))
                     || (Year % 400 == 0))
                    NumberOfDays = 29;
                else
                    NumberOfDays = 28;
                break;
        }
        return NumberOfDays; 
        }


        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string date = (selectedDay).ToString() + "/" + (CurrentDisplayMonth).ToString() + "/" + (CurrentDisplayYear).ToString();
            bool exist = false;
            foreach (KeyValuePair<string, string> element in HolidayDates)
            {
                if (((selectedDay).ToString() + "/" + (CurrentDisplayMonth).ToString() + "/" + (CurrentDisplayYear).ToString()) == element.Key)
                {
                    exist = true;
                }
            }
            if (!exist)
            {
               HolidayDates.Add(date, textBox1.Text);
            }
            foreach (KeyValuePair<string, string> element in HolidayDates)
            {
                if (((selectedDay).ToString() + "/" + (CurrentDisplayMonth).ToString() + "/" + (CurrentDisplayYear).ToString()) == element.Key)
                {
                    Label L = Controls.Find(String.Format("Label{0}", selectedDay + 10+WeekDay), true).FirstOrDefault() as Label;
                    if (L.BackColor != Color.LightBlue)
                    {
                        L.BackColor = Color.LightBlue;
                        toolTip1.SetToolTip(L, element.Value);
                    }
                    else break;
                }
            }
        }

    }
}
       

   

