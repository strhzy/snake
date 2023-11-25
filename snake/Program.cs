using System.Diagnostics;
using System.Reflection;

namespace snake
{
    class Program
    {
        private const int Width = 30;
        private const int Height = 20;

        private const int ScreenWidth = Width * 3; 
        private const int ScreenHeight = Height * 3; 

        private const int FrameMs = 200;

        private const ConsoleColor BorderColor = ConsoleColor.Gray;

        private const ConsoleColor headColor = ConsoleColor.DarkGreen;
        private const ConsoleColor bodyColor = ConsoleColor.Green;
        private const ConsoleColor foodColor = ConsoleColor.Red;

        private static readonly Random random = new Random();
        static void Main()
        {
            
            Console.CursorVisible = false;
            Console.WriteLine("Нажмите Enter чтобы начать");
            ConsoleKey keyStart = Console.ReadKey(true).Key;
            
            if (keyStart == ConsoleKey.Enter)
            {
                Console.SetWindowSize(ScreenWidth, ScreenHeight);
                
                Console.SetBufferSize(ScreenWidth, ScreenHeight);
                Direction currentMovement = Direction.Right;
                Border();
                var snake = new Snake(10, 5, headColor, bodyColor);
                screen food = GenFood(snake);
                food.Drawing();
                Stopwatch sw = new Stopwatch();
                while (true)
                {

                    sw.Restart();

                    Direction oldMovement = currentMovement;

                    while (sw.ElapsedMilliseconds <= FrameMs)
                    {
                        if (currentMovement == oldMovement)
                        {
                            currentMovement = Movement(currentMovement);
                        }
                    }

                    if(snake.Head.X == food.X && snake.Head.Y == food.Y)
                    {
                        snake.Moving(currentMovement,true);
                    }

                    snake.Moving(currentMovement);

                    if (snake.Head.X == ScreenWidth - 3 || snake.Head.X == 1 || snake.Head.Y == ScreenHeight - 3 || snake.Head.Y == 1)
                    {
                        Console.Clear();
                        Console.WriteLine("GAME OVER");
                        Main();
                    }
                }
            }
            else
            {
                Console.WriteLine("Ну и ладно");
            }
                
        }
        static screen GenFood(Snake snake)
        {
            screen food;
            do
            {
                food = new screen(random.Next(1, ScreenWidth - 3), random.Next(1, ScreenHeight - 3), foodColor);
            } while (snake.Head.X == food.X && snake.Head.Y == food.Y || snake.Body.Any(b => b.X == food.X && b.Y == food.Y));
            return food;
        }
        static Direction Movement(Direction currentDirection)
        {
            if(!Console.KeyAvailable)
            {
                return currentDirection;
            }
            ConsoleKey key = Console.ReadKey(true).Key;
            currentDirection = key switch
            {
                ConsoleKey.UpArrow when currentDirection != Direction.Down => Direction.Up,
                ConsoleKey.DownArrow when currentDirection != Direction.Up => Direction.Down,
                ConsoleKey.LeftArrow when currentDirection != Direction.Right => Direction.Left,
                ConsoleKey.RightArrow when currentDirection != Direction.Left => Direction.Right,
                _ => currentDirection
            };
            return currentDirection;
        }
        static void Border()
        {
            for (int i = 0; i < ScreenWidth; i++)
            {
                new screen(i, 0, BorderColor).Drawing();
                new screen(i, ScreenHeight-1, BorderColor).Drawing();
            }
            for (int i = 0; i < ScreenHeight; i++)
            {
                new screen(0, i, BorderColor).Drawing();
                new screen(ScreenWidth - 1, i, BorderColor).Drawing();
            }
        }
    }
}