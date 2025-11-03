using System;

namespace Battleship
{
  class Program
  {
    static bool CheckZero(int[,] ships)
    {
      for (int i = 0; i < ships.GetLength(0); i++)
      {
        for (int j = 0; j < ships.GetLength(1); j++)
        {
          if (ships[i, j] == 1)
          {
            return false;
          }
        }
      }
      return true;
    }

    static void Main(string[] args)
    {
      int Hits = 0;
      int Misses = 0;
      int ShipsRemaining = 8;

      int[,] ships = {
        {
          1, 1, 0, 0
        },
        {
          0, 1, 1, 0
        },
        {
          1, 0, 0, 1
        },
        {
          0, 0, 1, 1
        }
      };

      Console.WriteLine("\nWelcome to Battleship!\n");
      Console.WriteLine("In this game, you have to find the locations of the ships hidden.\nAfter you find all of them, Your hits, misses, and tries will be displayed.\n");

      while (!CheckZero(ships))
      {
        Console.Write("Please specify the row you want to try to find a ship (0 - 3 or 5 to exit): ");

        try
        {
          int row = Convert.ToInt32(Console.ReadLine());
          if (row == 5)
          {
            break;
          }
          else
          {
            Console.Write("Please specify the column you want to try to find a ship (0 - 3 or 5 to exit): ");
            int column = Convert.ToInt32(Console.ReadLine());

            if (row == 5 || column == 5)
            {
              break;
            }
            else if (row > 3 || row < 0 || column > 3 || column < 0)
            {
              Console.WriteLine("\nPlease enter a valid input. Input can only be from 0 to 3.\n");
              continue;
            }
            else
            {
              if (ships[row, column] == 1)
              {
                Hits++;
                ships[row, column] = 0;
                Console.WriteLine($"\nHit! Ships remaining: {--ShipsRemaining}\n");
              }
              else if (ships[row, column] == 0)
              {
                Misses++;
                Console.WriteLine($"\nMissed! Ships remaining: {ShipsRemaining}\n");
              }
            }
          }
        }
        catch (Exception e)
        {
          Console.WriteLine(e.Message);
        }
      }

      Console.WriteLine("\nAll ships destroyed!\n");
      Console.WriteLine($"Your final score: Hits: {Hits}, Misses: {Misses}, Tries taken: {Hits + Misses}\n");

      Console.WriteLine("Goodbye! Thanks for playing!\n");
    }
  }
}