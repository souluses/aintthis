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

namespace sogrand
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string s;
        int[,] arr = new int[7, 7];
        double[,] B = new double[7, 7];
        //double Det;

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void информацияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Бударова А.С., группа 622п.", "Информация",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void выводToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int Num = (cmbbx.SelectedIndex+1);
                if (Num == 0) throw new Exception("Начальная вершина не выбрана!");

                arr[0, 2] = 1; arr[0, 3] = 1; arr[0, 5] = 1;
                arr[1, 0] = 1; arr[1, 4] = 1; arr[1, 6] = 1;
                arr[2, 1] = 1; arr[2, 5] = 1;
                arr[3, 0] = 1; arr[3, 1] = 1;
                arr[4, 2] = 1; arr[4, 6] = 1;
                arr[5, 0] = 1; arr[5, 4] = 1;
                arr[6, 1] = 1; arr[6, 3] = 1;
                Output_Matrix(arr, dataGridView1);
                countD(arr, Num);
                var Det = DetRec(B);
                textBox1.Text = Det.ToString();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }

        }

        public void Output_Matrix(int[,] Mas, DataGridView dtgrdvw)
        {
            char nextChar = 'A';
            dtgrdvw.ColumnCount = 0;
            dtgrdvw.ColumnCount = 7;
            dtgrdvw.Rows.Add(7);
            dtgrdvw.RowCount =7;
            dtgrdvw.ColumnCount = 7;
            for (int i = 0; i < 7; ++i)
                for (int j = 0; j < 7; ++j)
                {
                    dtgrdvw.Rows[i].Cells[j].Value = Mas[i, j];
                }

            for (int i = 0; i < 7; i++)

            {
                dtgrdvw.Columns[i].ReadOnly = true;
                dtgrdvw.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                dtgrdvw.Columns[i].Name = i.ToString();
                dtgrdvw.Columns[i].Width = 20;
                dtgrdvw.RowHeadersWidth = 50;
                dtgrdvw.Columns[i].Width = 30;
                dtgrdvw.Columns[i].HeaderText = nextChar.ToString();
                dtgrdvw.Rows[i].HeaderCell.Value = nextChar.ToString();
                nextChar = (char)(((int)nextChar) + 1);

            }
        }

        public void countD(int[,] Mas, int Num)
        {
            int[,] D = new int[7, 7];
            for (int i = 0; i < 7; i++)
                for (int j = 0; j < 7; j++)
                {
                    if (arr[j, i] == 1)
                        D[i, i]++;
                }

            for (int i = 0; i < 7; i++)
                for (int j = 0; j < 7; j++)
                {
                    B[i, j] = D[i, j] - Mas[i, j];
                }

            //int[,] F = new int[7, 7];
            //for (int i = 0; i < 8; i++)
            //    for (int j = 0; j < 8; j++)
            //    {
            //        if (j < Num)
            //        {
            //            F[i, j] = B[i, j];
            //            F[j, i] = B[j, i];
            //        }
            //        else
            //        {
            //            F[i, j] = B[i, j + 1];
            //            F[j, i] = B[j + 1, i];
            //        }
            //    }
        }

        private static double DetRec(double[,] matrix)
        {
            if (matrix.Length == 4)
            {
                return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
            }
            double sign = 1, result = 0;
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                double[,] minor = GetMinor(matrix, i);
                result += sign * matrix[0, i] * DetRec(minor);
                sign = -sign;
            }
            return result;
        }

        private static double[,] GetMinor(double[,] matrix, int n)
        {
            double[,] result = new double[matrix.GetLength(0) - 1, matrix.GetLength(0) - 1];
            for (int i = 1; i < matrix.GetLength(0); i++)
            {
                for (int j = 0, col = 0; j < matrix.GetLength(1); j++)
                {
                    if (j == n)
                        continue;
                    result[i - 1, col] = matrix[i, j];
                    col++;
                }
            }
            return result;
        }
    }
}
