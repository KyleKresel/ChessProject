using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class Board
    {
        Dictionary<char, int> letters = new Dictionary<char, int>();
        bool BlackPlayer = false;
        char[,] board = new char[8, 8];
        public void setupDictionary()
        {
            letters.Add('a', 0);
            letters.Add('b', 1);
            letters.Add('c', 2);
            letters.Add('d', 3);
            letters.Add('e', 4);
            letters.Add('f', 5);
            letters.Add('g', 6);
            letters.Add('h', 7);
        }
        public void movepeace(string input)
        {
            int column1 = letters[input[0]];
            int row1 = int.Parse(input[1].ToString());
            --row1;

            int column2 = letters[input[3]];
            int row2 = int.Parse(input[4].ToString());
            --row2;

            char originalPiece = board[row1, column1];
            if (BlackPlayer)
            {
                if (!Char.IsUpper(originalPiece))
                {
                    Console.WriteLine("That's not your piece. Try again");
                    return;
                }
            }
            else
            {
                if (Char.IsUpper(originalPiece))
                {
                    Console.WriteLine("That's not your piece. Try again");
                    return;
                }
            }

            BlackPlayer = !BlackPlayer;

            originalPiece = Char.ToLower(originalPiece);
            int rowsMoved = Math.Abs(row2 - row1);
            int columnsMoved = Math.Abs(column2 - column1);
            bool valid = false;
            if (originalPiece == 'q')
            {
                valid = (rowsMoved == 0 && columnsMoved > 0) || (columnsMoved == 0 && rowsMoved > 0) || (rowsMoved == columnsMoved);

            }
            else if (originalPiece == 'k')
            {
                valid = (rowsMoved == 1 && columnsMoved == 0) || (columnsMoved == 1 && rowsMoved == 1) || (rowsMoved == 0 && columnsMoved == 1);
            }
            else if (originalPiece == 'n')
            {
                valid = (rowsMoved == 1 && columnsMoved == 2) || (columnsMoved == 1 && rowsMoved == 2);
            }
            else if (originalPiece == 'r')
            {
                valid = (rowsMoved == 0 && columnsMoved > 0) || (columnsMoved == 0 && rowsMoved > 0);
            }
            else if (originalPiece == 'b')
            {
                valid = rowsMoved == columnsMoved;
            }
            
            if (!valid)
            {
                Console.WriteLine(input + " is invaled movemnet");
                return;
            }

            if (input.Length == 6)
            {
                if (board[row2, column2] != '\0')
                {
                    board[row2, column2] = board[row1, column1];
                    board[row1, column1] = '\0';
                }
                else
                {
                    Console.WriteLine("tryed to move " + row1 + ", " + column1 + " to " + row2 + ", " + column2);
                }
            }
            else
            {
                if (board[row2, column2] == '\0')
                {
                    board[row2, column2] = board[row1, column1];
                    board[row1, column1] = '\0';
                }
                else
                {
                    Console.WriteLine("tryed to move " + row1 + ", " + column1 + " to " + row2 + ", " + column2);
                }
            }
        }

        public bool InCheck()
        {
            for (int i = 0; i < board.GetUpperBound(0); ++i)
            {
                for (int j = 0; j < board.GetUpperBound(1); ++j)
                {
                    char currentPiece = board[i, j];
                    bool check = true;
                    switch (Char.ToLower(currentPiece))
                    {
                        case 'r': check = RookCheck(i, j);
                            break;
                        case 'b': check = BishopCheck(i, j);
                            break;
                        case 'q': check = QueenCheck(i, j);
                            break;
                        case 'n': check = KnightCheck(i, j);
                            break;
                    }
                    if (check)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool RookCheck(int row, int column)
        {
            bool isBlack = Char.IsUpper(board[row, column]);
            for (int i = row + 1; i < board.GetUpperBound(0); ++i)
            {
                char spotChecking = board[i, column];
                if (spotChecking != '\0')
                {
                    if (Char.ToLower(spotChecking) == 'k' && isBlack != Char.IsUpper(spotChecking))
                    {
                        return true;
                    }
                    break;
                }
            }
            for (int i = row - 1; i >= 0; --i)
            {
                char spotChecking = board[i, column];
                if (spotChecking != '\0')
                {
                    if (Char.ToLower(spotChecking) == 'k' && isBlack != Char.IsUpper(spotChecking))
                    {
                        return true;
                    }
                    break;
                }
            }
            for (int j = column + 1; j < board.GetUpperBound(1); ++j)
            {
                char spotChecking = board[row, j];
                if (spotChecking != '\0')
                {
                    if (Char.ToLower(spotChecking) == 'k' && isBlack != Char.IsUpper(spotChecking))
                    {
                        return true;
                    }
                    break;
                }
            }
            for (int j = column - 1; j >= 0; --j)
            {
                char spotChecking = board[row, j];
                if (spotChecking != '\0')
                {
                    if (Char.ToLower(spotChecking) == 'k' && isBlack != Char.IsUpper(spotChecking))
                    {
                        return true;
                    }
                    break;
                }
            }
            return false;
        }

        private bool BishopCheck(int row, int column)
        {
            bool isBlack = Char.IsUpper(board[row, column]);
            for (int i = 1; row + i < board.GetUpperBound(0) && column + i < board.GetUpperBound(1); ++i)
            {
                char spotChecking = board[row + i, column + i];
                if (spotChecking != '\0')
                {
                    if (Char.ToLower(spotChecking) == 'k' && isBlack != Char.IsUpper(spotChecking))
                    {
                        return true;
                    }
                    break;
                }
            }
            for (int i = 1; row - i >= 0 && column + i < board.GetUpperBound(1); ++i)
            {
                char spotChecking = board[row - i, column + i];
                if (spotChecking != '\0')
                {
                    if (Char.ToLower(spotChecking) == 'k' && isBlack != Char.IsUpper(spotChecking))
                    {
                        return true;
                    }
                    break;
                }
            }
            for (int i = 1; row + i < board.GetUpperBound(0) && column - i >= 0; ++i)
            {
                char spotChecking = board[row + i, column - i];
                if (spotChecking != '\0')
                {
                    if (Char.ToLower(spotChecking) == 'k' && isBlack != Char.IsUpper(spotChecking))
                    {
                        return true;
                    }
                    break;
                }
            }
            for (int i = 1; row - i >= 0 && column - i >= 0; ++i)
            {
                char spotChecking = board[row - i, column - i];
                if (spotChecking != '\0')
                {
                    if (Char.ToLower(spotChecking) == 'k' && isBlack != Char.IsUpper(spotChecking))
                    {
                        return true;
                    }
                    break;
                }
            }

            return false;
        }

        private bool QueenCheck(int row, int column)
        {
            return RookCheck(row, column) || BishopCheck(row, column);
        }

        private bool KnightCheck(int row, int column)
        {
            char originalPiece = board[row, column];
            bool currentBlack = Char.IsUpper(originalPiece);

            char pieceChecking;
            bool isKing;
            bool isBlack;
            if (row + 1 <= 7 && column + 2 <= 7) {
                pieceChecking = board[row + 1, column + 2];
                isKing = Char.ToLower(pieceChecking) == 'k';
                isBlack = Char.IsUpper(pieceChecking);

                if (currentBlack != isBlack)
                {
                    return true;
                }
            }
            if (row + 2 <= 7 && column + 1 <= 7) {
                pieceChecking = board[row + 2, column + 1];
                isKing = Char.ToLower(pieceChecking) == 'k';
                isBlack = Char.IsUpper(pieceChecking);

                if (currentBlack != isBlack)
                {
                    return true;
                }
            }
            if (row + 2 <= 7 && column - 1 >= 0)
            {
                pieceChecking = board[row + 2, column - 1];
                isKing = Char.ToLower(pieceChecking) == 'k';
                isBlack = Char.IsUpper(pieceChecking);

                if (currentBlack != isBlack)
                {
                    return true;
                }
            }
            if (row -2 >= 0 && column + 1 <= 7)
            {
                pieceChecking = board[row - 2, column + 1];
                isKing = Char.ToLower(pieceChecking) == 'k';
                isBlack = Char.IsUpper(pieceChecking);

                if (currentBlack != isBlack)
                {
                    return true;
                }
            }
            if (row + 1 <= 7 && column - 2 >= 0) {
                pieceChecking = board[row + 1, column - 2];
                isKing = Char.ToLower(pieceChecking) == 'k';
                isBlack = Char.IsUpper(pieceChecking);

                if (currentBlack != isBlack)
                {
                    return true;
                }
            }
            if (row - 1 >= 0 && column + 2 <= 7)
            {
                pieceChecking = board[row - 1, column + 2];
                isKing = Char.ToLower(pieceChecking) == 'k';
                isBlack = Char.IsUpper(pieceChecking);

                if (currentBlack != isBlack)
                {
                    return true;
                }
            }
            if (row - 2 >= 0 && column - 1>= 0) {
                pieceChecking = board[row - 2, column - 1];
                isKing = Char.ToLower(pieceChecking) == 'k';
                isBlack = Char.IsUpper(pieceChecking);

                if (currentBlack != isBlack)
                {
                    return true;
                }
            }
            if (row - 1 >= 0 && column - 2 >= 0)
            {
                pieceChecking = board[row - 1, column - 2];
                isKing = Char.ToLower(pieceChecking) == 'k';
                isBlack = Char.IsUpper(pieceChecking);

                if (currentBlack != isBlack)
                {
                    return true;
                }
            }
            return false;
        }

        public void interpretPiece(string input)
        {
            string piece = input[0].ToString();
            char secondLetter = input[1];
            string place = input.Substring(2, 2);
            int column = letters[place[0]];
            int row = int.Parse(place[1].ToString());
            --row;
            if (secondLetter == 'l')
            {
                piece = piece.ToLower();
            }
            if (secondLetter == 'd')
            {
                piece = piece.ToUpper();
            }
            board[row , column] = piece[0];
        }

        public void displayBoard()
        {
            for (int row = 0; row <= board.GetUpperBound(0); row++)
            {
                Console.Write("| ");
                for (int column = 0; column <= board.GetUpperBound(1); column++)
                {
                    char piece = board[row, column];
                    if (piece == '\0')
                    {
                        Console.Write('-');
                    }
                    else
                    {
                        Console.Write(piece);
                    }
                    Console.Write(" | ");
                }
                Console.WriteLine();
            }
        }

        public void MovetwoPiece(string input)
        {
            int column1 = letters[input[0]];
            int row1 = int.Parse(input[1].ToString());
            --row1;

            int column2 = letters[input[3]];
            int row2 = int.Parse(input[4].ToString());
            --row2;

            int column3 = letters[input[6]];
            int row3 = int.Parse(input[7].ToString());
            --row3;

            int column4 = letters[input[9]];
            int row4 = int.Parse(input[10].ToString());
            --row4;

            if (board[row2, column2] != '\0' && board[row1, column1] != '\0')
            {
                board[row2, column2] = board[row1, column1];
                board[row1, column1] = '\0';
            }
            else
            {
                Console.WriteLine("tryed to move " + row1 + ", " + column1 + " to " + row2 + ", " + column2);
            }
            if (board[row4, column4] != '\0' && board[row3, column3] != '\0')
            {
                board[row4, column4] = board[row1, column1];
                board[row3, column3] = '\0';
            }
            else
            {
                Console.WriteLine("tryed to move " + row3 + ", " + column3 + " to " + row4 + ", " + column4);
            }
        }
        public void SetUpBoard() {
            board[0, 0] = 'R';
            board[0, 1] = 'N';
            board[0, 2] = 'B';
            board[0, 3] = 'K';
            board[0, 4] = 'Q';
            board[0, 5] = 'B';
            board[0, 6] = 'N';
            board[0, 7] = 'R';

            board[7, 0] = 'r';
            board[7, 1] = 'n';
            board[7, 2] = 'b';
            board[7, 3] = 'q';
            board[7, 4] = 'k';
            board[7, 5] = 'b';
            board[7, 6] = 'n';
            board[7, 7] = 'r';
        }

    }
    
}
