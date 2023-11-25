
namespace snake
{
    public struct screen
    {
        public screen(int x, int y, ConsoleColor color, int pixel = 3)
        {
            X = x;
            Y = y;
            Color = color;
            Pixel = pixel;
        }
        public int X { get; }
        public int Y {  get; }
        public ConsoleColor Color { get; }
        public int Pixel { get; }

        public void Drawing()
        {
            Console.ForegroundColor = Color;
            for (int x = 0;x<Pixel;x++)
            {
                for (int y = 0;y<Pixel;y++)
                {
                    Console.SetCursorPosition(X * Pixel+x, Y * Pixel+y);
                    Console.Write("█");
                }
            }
        }
        public void Clearing()
        {
            for (int x = 0; x < Pixel; x++)
            {
                for (int y = 0; y < Pixel; y++)
                {
                    Console.SetCursorPosition(X *Pixel + x, Y * Pixel + y);
                    Console.Write(" ");
                }
            }
        }
    }
}
