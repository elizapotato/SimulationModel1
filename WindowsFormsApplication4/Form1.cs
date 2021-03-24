using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication4
{
    public partial class Form1 : Form
    {
        Dictionary<CheckBox, Cell> field = new Dictionary<CheckBox, Cell>();
        private int day = 0;
        private int money = 100;

        public Form1()
        {
            InitializeComponent();
            foreach (CheckBox cb in panel1.Controls)
                field.Add(cb, new Cell());
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (sender as CheckBox);
            if (cb.Checked) Plant(cb);
            else Harvest(cb);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (CheckBox cb in panel1.Controls)
                NextStep(cb);
            day++;
            labDay.Text = day + " день";
        }

        private void Plant(CheckBox cb)
        {
            field[cb].Plant();
            UpdateBox(cb);
            money = money - 2;
            labMoney.Text = money + " $";
        }

        private void Harvest(CheckBox cb)
        {
            switch (field[cb].state)
            {
                case CellState.Planted:
                    break;
                case CellState.Green:
                    break;
                case CellState.Immature:
                    money = money + 3;
                    labMoney.Text = money + " $";
                    break;
                case CellState.Mature:
                    money = money + 5;
                    labMoney.Text = money + " $";
                    break;
                case CellState.Overgrown:
                    money = money - 1;
                    labMoney.Text = money + " $";
                    break;
            }
            field[cb].Harvest();
            UpdateBox(cb);           
        }

        private void NextStep(CheckBox cb)
        {
            field[cb].NextStep();
            UpdateBox(cb);
        }

        private void UpdateBox(CheckBox cb)
        {
            Color c = Color.White;
            switch (field[cb].state)
            {
                case CellState.Planted: c = Color.Black;
                    break;
                case CellState.Green: c = Color.Green;
                    break;
                case CellState.Immature: c = Color.Yellow;
                    break;
                case CellState.Mature: c = Color.Red;
                    break;
                case CellState.Overgrown: c = Color.Brown;
                    break;
            }
            cb.BackColor = c;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timeUp_CheckedChanged(object sender, EventArgs e)
        {
            timeBack.BackColor = Color.White;
            timeDown.BackColor = Color.White;
            timeUp.BackColor = Color.DarkGray;
            timer1.Interval = 50;          

        }

        private void timeBack_CheckedChanged(object sender, EventArgs e)
        {
            timeDown.BackColor = Color.White;
            timeUp.BackColor = Color.White;
            timeBack.BackColor = Color.DarkGray;
            timer1.Interval = 100;
        }

        private void timeDown_CheckedChanged(object sender, EventArgs e)
        {
            timeBack.BackColor = Color.White;
            timeUp.BackColor = Color.White;
            timeDown.BackColor = Color.DarkGray;
            timer1.Interval = 200;            
        }
    }

    enum CellState
    {
        Empty,
        Planted,
        Green,
        Immature,
        Mature,
        Overgrown
    }

    class Cell
    {
        public CellState state = CellState.Empty;
        public int progress = 0;

        private const int prPlanted = 20;
        private const int prGreen = 100;
        private const int prImmature = 120;
        private const int prMature = 140;

        public void Plant()
        {
            state = CellState.Planted;
            progress = 1;
        }

        public void Harvest()
        {
            state = CellState.Empty;
            progress = 0;
        }

        public void NextStep()
        {
            if ((state != CellState.Empty) && (state != CellState.Overgrown))
            {
                progress++;
                if (progress < prPlanted) state = CellState.Planted;
                else if (progress < prGreen) state = CellState.Green;
                else if (progress < prImmature) state = CellState.Immature;
                else if (progress < prMature) state = CellState.Mature;
                else state = CellState.Overgrown;
            }
        }
    }

}
