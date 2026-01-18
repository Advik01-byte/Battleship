using System;
using System.Collections.Generic;

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

      Console.WriteLine("\n. = Undiscovered Area, M = Miss, H = Hit, X = Sunk Ship");
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

    // Helper function to check if a ship is sunk
    static bool IsShipSunk(Ship ship)
    {
      return ship.Position1 && ship.Position2 && ship.Position3 && ship.Position4;
    }

    // Helper function to mark ship as sunk on grid
    static void MarkShipAsSunk(char[,] grid, int[,,] Ships, int shipIndex)
    {
      for (int i = 0; i < 4; i++)
      {
        grid[Ships[shipIndex, i, 0], Ships[shipIndex, i, 1]] = 'X';
      }
    }

    // Main function to run the Battleship game
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

      // Create instances of the Ship structures
      Ship[] shipStructs = new Ship[4];
      int[] shipHitCounts = new int[4];

      char[,] grid = {
        {'.', '.', '.', '.', '.', '.', '.', '.', '.'},
        {'.', '.', '.', '.', '.', '.', '.', '.', '.'},
        {'.', '.', '.', '.', '.', '.', '.', '.', '.'},
        {'.', '.', '.', '.', '.', '.', '.', '.', '.'},
        {'.', '.', '.', '.', '.', '.', '.', '.', '.'},
        {'.', '.', '.', '.', '.', '.', '.', '.', '.'},
        {'.', '.', '.', '.', '.', '.', '.', '.', '.'},
        {'.', '.', '.', '.', '.', '.', '.', '.', '.'},
        {'.', '.', '.', '.', '.', '.', '.', '.', '.'}
      };
      
      // Tell how to play the game
      Console.WriteLine("\nWelcome to Battleship!\n");
      Console.WriteLine("In this game, you have to find the locations of the ships hidden.\nShips can be hidden diagonally, horizontally, and vertically.\nThere are four ships in total and their length is 4.\nAfter you find all of them, Your hits, misses, and tries will be displayed.\n");

      Console.WriteLine("Current state of the board:");
      CreateBoard(grid);
      
      // Main game loop
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

          Console.Write("Please specify the column you want to try to find a ship (0 - 8 or 9 to exit): ");
          int column = Convert.ToInt32(Console.ReadLine());

          if (column == 9)
          {
            break;
          }
          else if (row > 8 || row < 0 || column > 8 || column < 0)
          {
            Console.WriteLine("\nPlease enter a valid input. Input can only be from 0 to 8.\n");
            continue;
          }
          else if (grid[row, column] == 'H' || grid[row, column] == 'M' || grid[row, column] == 'X')
          {
            Console.WriteLine("\nYou already tried this position! Please try a different location.\n");
            continue;
          }
          else
          {
            if (ships[row, column] == 1)
            {
              Hits++;
              ships[row, column] = 0;
              grid[row, column] = 'H';

              // Find which ship was hit and update its structure
              int hitShipIndex = -1;
              for (int s = 0; s < 4; s++)
              {
                for (int p = 0; p < 4; p++)
                {
                  if (Ships[s, p, 0] == row && Ships[s, p, 1] == column)
                  {
                    hitShipIndex = s;
                    shipHitCounts[s]++;
                    
                    // Update the ship structure
                    switch (shipHitCounts[s])
                    {
                      case 1:
                        shipStructs[s].Position1 = true;
                        break;
                      case 2:
                        shipStructs[s].Position2 = true;
                        break;
                      case 3:
                        shipStructs[s].Position3 = true;
                        break;
                      case 4:
                        shipStructs[s].Position4 = true;
                        break;
                    }
                    break;
                  }
                }
                if (hitShipIndex != -1) break;
              }

              // Check if ship is sunk
              bool shipSunk = false;
              if (hitShipIndex != -1 && IsShipSunk(shipStructs[hitShipIndex]))
              {
                shipSunk = true;
                ShipsRemaining--;
                MarkShipAsSunk(grid, Ships, hitShipIndex);
              }

              if (shipSunk)
              {
                Console.WriteLine($"\nHit! SHIP SUNK! Ships remaining: {ShipsRemaining}\n");
                Console.WriteLine("Current state of the board:");
                CreateBoard(grid, 'X', row, column);
              }
              else
              {
                Console.WriteLine($"\nHit! Ships remaining: {ShipsRemaining}\n");
                Console.WriteLine("Current state of the board:");
                CreateBoard(grid, 'H', row, column);
              }
            }
            else
            {
              Misses++;
              Console.WriteLine($"\nMissed! Ships remaining: {ShipsRemaining}\n");
              Console.WriteLine("Current state of the board:");
              CreateBoard(grid, 'M', row, column);
            }
          }
        }
        catch (Exception e)
        {
          Console.WriteLine($"\nError: {e.Message}. Please enter a valid number.\n");
        }
      }

      Console.WriteLine("\nAll ships destroyed!\n");
      Console.WriteLine($"Your final score: Hits: {Hits}, Misses: {Misses}, Tries taken: {Hits + Misses}\n");

      Console.WriteLine("Goodbye! Thanks for playing!\n");
    }
  }
}