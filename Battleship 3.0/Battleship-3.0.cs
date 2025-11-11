using System;

namespace Battleship
{
  class Program
  {
    // Create a function to check if all ships are destroyed or not
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

    // Create the function that will be used in CreateBoard() in the switch statement to create a line.
    static void CreateLine(char[,] grid, int i, int num)
    {
      for (int j = 0; j < grid.GetLength(1); j++)
      {
        if (j == 0)
        {
          Console.Write($"\n{num} " + grid[i, j] + "  ");
        }
        else
        {
          Console.Write(grid[i, j] + "  ");
        }
      }
    }

    // Create the function that is needed to create the board for the ships
    static void CreateBoard(char[,] grid, char letter = '.', int row = 0, int column = 0)
    {
      grid[row, column] = letter;

      Console.WriteLine("\n. = Undiscovered Area, M = Miss, H = Hit");
      Console.WriteLine("\n  0  1  2  3  4  5  6  7  8");

      Console.Write("0 ");

      for (int i = 0; i < grid.GetLength(0); i++)
      {
        switch (i)
        {
          case 0:
            for (int j = 0; j < grid.GetLength(1); j++)
            {
              Console.Write(grid[i, j] + "  ");
            }
            break;
          case 1:
            CreateLine(grid, i, 1);
            break;
          case 2:
            CreateLine(grid, i, 2);
            break;
          case 3:
            CreateLine(grid, i, 3);
            break;
          case 4:
            CreateLine(grid, i, 4);
            break;
          case 5:
            CreateLine(grid, i, 5);
            break;
          case 6:
            CreateLine(grid, i, 6);
            break;
          case 7:
            CreateLine(grid, i, 7);
            break;
          case 8:
            for (int j = 0; j < grid.GetLength(1); j++)
            {
              if (j == 0)
              {
                Console.Write("\n8 " + grid[i, j] + "  ");
              }
              else if (j == 8)
              {
                Console.Write(grid[i, j] + "  \n");
              }
              else
              {
                Console.Write(grid[i, j] + "  ");
              }
            }
            break;
        }
      }
    }

    static void Main(string[] args)
    {
      int Hits = 0;
      int Misses = 0;
      int ShipsRemaining = 4;

      int[,,] Ships = {
        { {0,0}, {1,0}, {2,0}, {3,0} },
        { {0,5}, {0,6}, {0,7}, {0,8} },
        { {5,4}, {6,4}, {7,4}, {8,4} },
        { {2,5}, {3,6}, {4,7}, {5,8} }
      };

      int[,] ships = {
        {1, 0, 0, 0, 0, 1, 1, 1, 1},
        {1, 0, 0, 0, 0, 0, 0, 0, 0},
        {1, 0, 0, 0, 0, 1, 0, 0, 0},
        {1, 0, 0, 0, 0, 0, 1, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 1, 0},
        {0, 0, 0, 0, 1, 0, 0, 0, 1},
        {0, 0, 0, 0, 1, 0, 0, 0, 0},
        {0, 0, 0, 0, 1, 0, 0, 0, 0},
        {0, 0, 0, 0, 1, 0, 0, 0, 0},
      };

      char[,] grid = {
        {
          '.', '.', '.', '.', '.', '.', '.', '.', '.'
        },
        {
          '.', '.', '.', '.', '.', '.', '.', '.', '.'
        },
        {
          '.', '.', '.', '.', '.', '.', '.', '.', '.'
        },
        {
          '.', '.', '.', '.', '.', '.', '.', '.', '.'
        },
        {
          '.', '.', '.', '.', '.', '.', '.', '.', '.'
        },
        {
          '.', '.', '.', '.', '.', '.', '.', '.', '.'
        },
        {
          '.', '.', '.', '.', '.', '.', '.', '.', '.'
        },
        {
          '.', '.', '.', '.', '.', '.', '.', '.', '.'
        },
        {
          '.', '.', '.', '.', '.', '.', '.', '.', '.'
        }
      };

      Console.WriteLine("\nWelcome to Battleship!\n");
      Console.WriteLine("In this game, you have to find the locations of the ships hidden.\nShips can be hidden diagonally, horizontally, and vertically.\nThere are four ships in total and their length is 4.\nAfter you find all of them, Your hits, misses, and tries will be displayed.\n");

      Console.WriteLine("Current state of the board:");
      CreateBoard(grid);

      while (!CheckZero(ships))
      {
        Console.Write("Please specify the row you want to try to find a ship (0 - 8 or 9 to exit): ");

        try
        {
          int row = Convert.ToInt32(Console.ReadLine());
          if (row == 9)
          {
            break;
          }
          else
          {
            Console.Write("Please specify the column you want to try to find a ship (0 - 8 or 9 to exit): ");
            int column = Convert.ToInt32(Console.ReadLine());

            if (row == 9 || column == 9)
            {
              break;
            }
            else if (row > 8 || row < 0 || column > 8 || column < 0)
            {
              Console.WriteLine("\nPlease enter a valid input. Input can only be from 0 to 8.\n");
              continue;
            }
            else
            {
              if (ships[row, column] == 1)
              {
                Hits++;
                ships[row, column] = 0;

                grid[row, column] = 'H';

                if ((grid[Ships[0, 0, 0], Ships[0, 0, 1]] == 'H' && grid[Ships[0, 1, 0], Ships[0, 1, 1]] == 'H' && grid[Ships[0, 2, 0], Ships[0, 2, 1]] == 'H' && grid[Ships[0, 3, 0], Ships[0, 3, 1]] == 'H') || (grid[Ships[1, 0, 0], Ships[1, 0, 1]] == 'H' && grid[Ships[1, 1, 0], Ships[1, 1, 1]] == 'H' && grid[Ships[1, 2, 0], Ships[1, 2, 1]] == 'H' && grid[Ships[1, 3, 0], Ships[1, 3, 1]] == 'H') || (grid[Ships[2, 0, 0], Ships[2, 0, 1]] == 'H' && grid[Ships[2, 1, 0], Ships[2, 1, 1]] == 'H' && grid[Ships[2, 2, 0], Ships[2, 2, 1]] == 'H' && grid[Ships[2, 3, 0], Ships[2, 3, 1]] == 'H') || (grid[Ships[3, 0, 0], Ships[3, 0, 1]] == 'H' && grid[Ships[3, 1, 0], Ships[3, 1, 1]] == 'H' && grid[Ships[3, 2, 0], Ships[3, 2, 1]] == 'H' && grid[Ships[3, 3, 0], Ships[3, 3, 1]] == 'H'))
                {
                  Console.WriteLine($"\nHit! SHIP SUNK! Ships remaining: {--ShipsRemaining}\n");
                  Console.WriteLine("Current state of the board:");

                  CreateBoard(grid, 'H', row, column);
                }
                else
                {
                  Console.WriteLine($"\nHit! Ships remaining: {ShipsRemaining}\n");
                  Console.WriteLine("Current state of the board:");

                  CreateBoard(grid, 'H', row, column);
                }
              }
              else if (ships[row, column] == 0)
              {
                Misses++;
                Console.WriteLine($"\nMissed! Ships remaining: {ShipsRemaining}\n");
                Console.WriteLine("Current state of the board:");

                CreateBoard(grid, 'M', row, column);
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