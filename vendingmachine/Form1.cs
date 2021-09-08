using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Threading;

namespace vendingmachine1
{
    public partial class Form1 : Form
    {
        private static ArrayList ListItems = new ArrayList(new String[] { "Cola", "Crisps", "Chacolate", "Water" });
        private static ArrayList ListPrice = new ArrayList(new Double[] { 1.00, 0.50, 0.65, 0.90 });
        private static ArrayList AllowedCoins = new ArrayList(new Double[] { 0.05, 0.10, 0.20, 0.50, 1.0, 2.0 });
        private static double total = 0.00;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < ListItems.Count; i++)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridView1);
                row.Cells[0].Value = ListItems[i];
                row.Cells[1].Value = ListPrice[i];
                dataGridView1.Rows.Add(row);
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            richTxtInsertCoins.Text = "Selected Item " + ListItems[e.RowIndex];
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtAddCoins.Text == null || txtAddCoins.Text.Trim() == "")
            {
                richTxtInsertCoins.Text = richTxtInsertCoins.Text + "\n" + "Insert Coin and then press Add";
            } else
            {
                String amt = txtAddCoins.Text.Replace("£", "");
                Double val = 0.00;
                if(amt.Contains("p") || amt.Contains("P"))
                {
                    amt = amt.Replace("p", "");
                    amt = amt.Replace("P", "");
                    Double v = Convert.ToDouble(amt);
                    val = v / 100;
                }
                else
                {
                    val = Convert.ToDouble(amt);
                }
                if(AllowedCoins.Contains(val))
                {
                    total = total + val; ;
                    richTxtInsertCoins.Text = "TOTAL : " + total.ToString();
                } else
                {
                    rejectedCoins.Text = rejectedCoins.Text + txtAddCoins.Text + "\n";
                }
            }


        }

        private void btnBuy_Click(object sender, EventArgs e)
        {
            String item = (String) ListItems[dataGridView1.SelectedCells[0].RowIndex];
            Double price = (Double)ListPrice[dataGridView1.SelectedCells[0].RowIndex];
            if(price > total)
            {
                richTxtInsertCoins.Text = "PRICE : " + price.ToString();
            } else
            {
                richTxtInsertCoins.Text = "THANK YOU. \nHAVE A GOOD DAY !!";
                
                rejectedCoins.Text = Convert.ToDouble(txtAddCoins.Text) - price + "\n";
                btnReset.Visible = true;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            richTxtInsertCoins.Text = "INSERT COINS";
            total = 0.00;
        }
    }
}
