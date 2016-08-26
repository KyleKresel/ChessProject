using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class Program
    {
        static Board b = new Board();
        // letters are colums
        // rows are numbers 
        
    
        static void Main(string[] args)
        {
            b.setupDictionary();
            b.SetUpBoard();
            //string[] lines = System.IO.File.ReadAllLines(args[0]);
            //for (int i = 0; i < lines.Length; ++i)
            //{
            //    string input = lines[i];
            //    if (input.Length == 4) {
            //    b.interpretPiece(input);

            //    }
            //    if (input.Length == 5 || input.Length == 6) {
            //    b.movepeace(input);

            //    }
            //    if (input.Length == 10)
            //    {
            //        b.MovetwoPiece(input);

            //    }
            //}
            b.displayBoard();
            string input = "";
            while (!input.Equals("q"))
            {
                Console.WriteLine("Input your desired move. Type q to quit");
                input = Console.ReadLine();
                if (input.Length == 4)
                {
                    b.interpretPiece(input);
                }
                if (input.Length == 5 || input.Length == 6)
                {
                    b.movepeace(input);
                }
                if (input.Length == 10)
                {
                    b.MovetwoPiece(input);
                }
                if (b.InCheck())
                {
                    Console.WriteLine("Opposing player has been put in check");
                }
                if (!input.Equals("q"))
                {
                    b.displayBoard();
                }
            }
        }

     

    }
}
