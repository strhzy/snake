using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snake
{
    internal class Snake
    {
        private readonly ConsoleColor _headColor;
        private readonly ConsoleColor _bodyColor;
        public Snake(int initialX, int initialY, ConsoleColor headColor, ConsoleColor bodyColor, int bodyLenght = 3)
        {
            _headColor = headColor;
            _bodyColor = bodyColor;
            Head = new screen(initialX, initialY, _headColor);
            for(int i = bodyLenght; i >= 0; i--)
            {
                Body.Enqueue(new screen(Head.X - i - 1, initialY, _bodyColor));
            }
            Drawing();
        }
        public screen Head {  get; private set; }
        public Queue<screen> Body { get; } = new Queue<screen>();
        public void Moving(Direction direction, bool eat = false)
        {
            Clearing();
            Body.Enqueue(new screen(Head.X,Head.Y,_bodyColor));
            if (!eat)
            {
                Body.Dequeue();
            }
            
            Head = direction switch
            {
                Direction.Up => new screen(Head.X, Head.Y - 1, _headColor),
                Direction.Down => new screen(Head.X, Head.Y + 1, _headColor),
                Direction.Left => new screen(Head.X - 1, Head.Y, _headColor),
                Direction.Right => new screen(Head.X + 1, Head.Y, _headColor),
            };
            Drawing();
        }
        public void Drawing()
        {
            Head.Drawing();

            foreach(screen screen in Body)
            {
                screen.Drawing();
            }
        }
        public void Clearing()
        {
            Head.Clearing();

            foreach(screen screen in Body)
            {
                screen.Clearing();
            }
        }
    }
}
