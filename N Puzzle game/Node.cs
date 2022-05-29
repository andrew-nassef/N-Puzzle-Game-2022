using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N_Puzzle_game
{
    class Node
    {
        public int[] puzzle_1d;
        public int puzzle_dimension;
        public int heuristic_value = 0;
        public int cost_so_far;
        public int zero_pos;
        public Node parent;

        public Node(int[] puzzle_1d, int puzzle_dimension, Node parent, int cost_so_far)
        {
            this.puzzle_1d = new int[puzzle_dimension * puzzle_dimension];
            for (int i = 0; i < puzzle_dimension*puzzle_dimension; i++)
            {
                if (puzzle_1d[i] == 0)
                {
                    zero_pos = i;
                }
                this.puzzle_1d[i] = puzzle_1d[i];
            }
            this.puzzle_dimension = puzzle_dimension;
            this.cost_so_far = cost_so_far;            this.parent = parent;

        }

        public void calculate_manhattan_distance()
        {
            int distance = 0;
            int puzzle_size = puzzle_dimension * puzzle_dimension;
            for (int i = 0; i < puzzle_size; i++)
            {
                int v = puzzle_1d[i];
                if (v == 0)
                {
                    continue;
                }
                v = v - 1;

                int goal_x = v % puzzle_dimension;
                int goal_y = v / puzzle_dimension;

                int x = i % puzzle_dimension;
                int y = i / puzzle_dimension;

                int manhatten_cost = Math.Abs(x - goal_x) + Math.Abs(y - goal_y);
                distance += manhatten_cost;

            }
            heuristic_value = distance;
        }

        public void calculate_hamming_distance()
        {
            for (int i = 0; i < puzzle_dimension *puzzle_dimension; i++)
            {
                if (puzzle_1d[i] != i + 1 && puzzle_1d[i] != 0)
                    heuristic_value++;
            }
        }

        public int priority()
        {
            return heuristic_value + cost_so_far;  
        }
   
        public void print()
        {
            int counter = -1;
            for (int i = 0; i < puzzle_dimension*puzzle_dimension; i++)
            {
                counter++;
                if(counter == puzzle_dimension)
                {
                    counter = 0;
                    Console.WriteLine();
                }
                Console.Write(puzzle_1d[i] + " ");
                  
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        public bool Is_puzzle_exist(int[] puzzle2)
        {
            for (int i = 0; i < puzzle_dimension *puzzle_dimension; i++)
            {
                if (puzzle_1d[i] != puzzle2[i])
                    return false;
            }
            return true;
        }

    }

}
