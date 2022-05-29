using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace N_Puzzle_game
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Main_Menu());
        }

        public static PriorityQueue<Node, int> priorityQueue = new PriorityQueue<Node, int>();
        public static int expantion_order = 0;
        public static long time_taken = 0;
        public static string Hamming_or_Manhattan;

        public static bool is_puzzle_solvable(int puzzle_dimension, int[] puzzle_1d_array)
        {
            int no_of_inversions = 0;
            int blank_space_pos = 0;
            for (int i = 0; i < puzzle_dimension * puzzle_dimension; i++)
            {
                if (puzzle_1d_array[i] == 0)
                {
                    blank_space_pos = i / puzzle_dimension + 1;
                    continue;
                }
                for (int j = i + 1; j < puzzle_dimension * puzzle_dimension; j++)
                {
                    if (puzzle_1d_array[i] > puzzle_1d_array[j] && puzzle_1d_array[j] != 0)
                    {
                        no_of_inversions++;
                    }
                }

            }
            //if N is even
            if (puzzle_dimension % 2 == 0)
            {
                if (no_of_inversions % 2 == 0 && (puzzle_dimension - blank_space_pos) % 2 == 0 ||
                    no_of_inversions % 2 != 0 && (puzzle_dimension - blank_space_pos) % 2 != 0)
                {
                    return true;
                }

            }
            //if N is odd
            else
            {
                if (no_of_inversions % 2 == 0)
                {
                    return true;
                }
            }


            return false;
        }

        public static Node[] node_array = new Node[1000];
        public static int node_index = 0;
     
        public static Node Solve_puzzle(Node node)
        {
            priorityQueue.Clear();
            expantion_order = 0;
            time_taken = 0;

            while (node.heuristic_value != 0)
            {
                time_taken++;
                if (node.heuristic_value == 0)
                {
                    return node;
                }
                Node node1;
                
                if (time_taken > 5000000000 && Hamming_or_Manhattan == "2")
                {
                    MessageBox.Show("Puzzle is not solvable with hamming distance");
                    break;
                }    

                // is up node 
                if (node.zero_pos - node.puzzle_dimension >= 0)
                {
                    node1 = new Node(node.puzzle_1d, node.puzzle_dimension, node, node.cost_so_far + 1);
                    node1.puzzle_1d[node.zero_pos] = node1.puzzle_1d[node.zero_pos - node.puzzle_dimension];
                    node1.puzzle_1d[node.zero_pos - node.puzzle_dimension] = 0;
                    node1.zero_pos = node1.zero_pos - node1.puzzle_dimension;
                    if (Hamming_or_Manhattan == "1")
                        node1.calculate_manhattan_distance();
                    else if (Hamming_or_Manhattan == "2")
                        node1.calculate_hamming_distance();
                    if (node1.cost_so_far < 2 || 
                        (node1.cost_so_far >= 2 && !node1.Is_puzzle_exist(node1.parent.parent.puzzle_1d)))
                        priorityQueue.Enqueue(node1, node1.priority());
                }
               
                // is down node
                if ((node.zero_pos + node.puzzle_dimension) < (node.puzzle_dimension * node.puzzle_dimension))
                {
                    node1 = new Node(node.puzzle_1d, node.puzzle_dimension, node, node.cost_so_far + 1);
                    node1.puzzle_1d[node.zero_pos] = node1.puzzle_1d[node.zero_pos + node.puzzle_dimension];
                    node1.puzzle_1d[node.zero_pos + node.puzzle_dimension] = 0;
                    node1.zero_pos = node.zero_pos + node.puzzle_dimension;
                    if (Hamming_or_Manhattan == "1")
                        node1.calculate_manhattan_distance();
                    else if (Hamming_or_Manhattan == "2")
                        node1.calculate_hamming_distance();
                    if (node1.cost_so_far < 2 ||
                        (node1.cost_so_far >= 2 && !node1.Is_puzzle_exist(node1.parent.parent.puzzle_1d)))
                        priorityQueue.Enqueue(node1, node1.priority());
                }
                
                // is right node
                if ((node.zero_pos % node.puzzle_dimension) != (node.puzzle_dimension - 1))
                {
                    node1 = new Node(node.puzzle_1d, node.puzzle_dimension, node, node.cost_so_far + 1);
                    node1.puzzle_1d[node.zero_pos] = node1.puzzle_1d[node.zero_pos + 1];
                    node1.puzzle_1d[node.zero_pos + 1] = 0;
                    node1.zero_pos = node.zero_pos + 1;
                    if (Hamming_or_Manhattan == "1")
                        node1.calculate_manhattan_distance();
                    else if (Hamming_or_Manhattan == "2")
                        node1.calculate_hamming_distance();
                    if (node1.cost_so_far < 2 ||
                        (node1.cost_so_far >= 2 && !node1.Is_puzzle_exist(node1.parent.parent.puzzle_1d)))
                        priorityQueue.Enqueue(node1, node1.priority());
                }
               
                // is left node
                if ((node.zero_pos % node.puzzle_dimension) != 0)
                {
                    node1 = new Node(node.puzzle_1d, node.puzzle_dimension, node, node.cost_so_far + 1);
                    node1.puzzle_1d[node.zero_pos] = node1.puzzle_1d[node.zero_pos - 1];
                    node1.puzzle_1d[node.zero_pos - 1] = 0;
                    node1.zero_pos = node.zero_pos - 1;
                    if (Hamming_or_Manhattan == "1")
                        node1.calculate_manhattan_distance();
                    else if (Hamming_or_Manhattan == "2")
                        node1.calculate_hamming_distance();
                    if (node1.cost_so_far < 2 ||
                        (node1.cost_so_far >= 2 && !node1.Is_puzzle_exist(node1.parent.parent.puzzle_1d)))
                        priorityQueue.Enqueue(node1, node1.priority());

                }
               
                node = priorityQueue.Dequeue();

            }
            return (node);
        }   
        
    }

}