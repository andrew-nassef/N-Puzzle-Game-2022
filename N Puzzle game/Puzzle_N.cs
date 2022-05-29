using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace N_Puzzle_game
{
    public partial class Puzzle_N : Form
    {
        public Puzzle_N()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;

            Program.priorityQueue.Clear();
            Program.Hamming_or_Manhattan = "1";
            if (Program.is_puzzle_solvable(Main_Menu.Puzzle_dimention, Level.puzzle_1d_array))
            {

                Node root = new Node(Level.puzzle_1d_array, Main_Menu.Puzzle_dimention, null, 0);

                if (Program.Hamming_or_Manhattan == "1")
                    root.calculate_manhattan_distance();
                else if (Program.Hamming_or_Manhattan == "2")
                    root.calculate_hamming_distance();

                Program.priorityQueue.Enqueue(root, root.priority());

                //call function that solves it 
                Node goal = Program.Solve_puzzle(Program.priorityQueue.Dequeue());
                int moves = 0;

                while (goal.parent != null)
                {
                    goal = goal.parent;
                    moves++;
                }
                button21.Text = moves.ToString();
                button4.Text = Program.time_taken.ToString();

            }
            else
            {
                MessageBox.Show("Puzzle is not Solvable");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;

            Program.priorityQueue.Clear();
            Program.Hamming_or_Manhattan = "2";

            if (Program.is_puzzle_solvable(Main_Menu.Puzzle_dimention, Level.puzzle_1d_array))
            {

                Node root = new Node(Level.puzzle_1d_array, Main_Menu.Puzzle_dimention, null, 0);

                if (Program.Hamming_or_Manhattan == "1")
                    root.calculate_manhattan_distance();
                else if (Program.Hamming_or_Manhattan == "2")
                    root.calculate_hamming_distance();

                Program.priorityQueue.Enqueue(root, root.priority());

                //call function that solves it 
                Node goal = Program.Solve_puzzle(Program.priorityQueue.Dequeue());
                int moves = 0;

                while (goal.parent != null)
                {
                    goal = goal.parent;
                    moves++;
                }
                button21.Text = moves.ToString();
                button4.Text = Program.time_taken.ToString();
            }
            else
            {
                MessageBox.Show("Puzzle is not Solvable");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Main_Menu menu = new Main_Menu();
            this.Hide();
            menu.ShowDialog();
            this.Close();
        }
    }
}
