using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASF.MRobot.Models
{
    public class Robot
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public char Orientation { get; private set; }
        public bool IsLost { get; private set; }

        public MarsGrid Grid { get; }

        public int PrevX { get; private set; }
        public int PrevY { get; private set; }

        public Robot(int x, int y, char orientation, MarsGrid grid)
        {
            X = x;
            Y = y;
            Orientation = orientation;
            Grid = grid;
            IsLost = false;
        }

        public void MoveForward()
        {
            StorePreviousPosition();

            switch (Orientation)
            {
                case 'N': Y++; break;
                case 'E': X++; break;
                case 'S': Y--; break;
                case 'W': X--; break;
            }
        }

        public void MoveBackward()
        {
            StorePreviousPosition();

            switch (Orientation)
            {
                case 'N': Y--; break;
                case 'E': X--; break;
                case 'S': Y++; break;
                case 'W': X++; break;
            }
        }

        public void TurnLeft()
        {
            Orientation = Orientation switch
            {
                'N' => 'W',
                'W' => 'S',
                'S' => 'E',
                'E' => 'N',
                _ => Orientation
            };
        }

        public void TurnRight()
        {
            Orientation = Orientation switch
            {
                'N' => 'E',
                'E' => 'S',
                'S' => 'W',
                'W' => 'N',
                _ => Orientation
            };
        }

        public void MarkLost()
        {
            IsLost = true;
        }

        public void RollbackMove()
        {
            X = PrevX;
            Y = PrevY;
        }

        private void StorePreviousPosition()
        {
            PrevX = X;
            PrevY = Y;
        }

        public override string ToString()
        {
            return $"{X} {Y} {Orientation}" + (IsLost ? " LOST" : "");
        }
    }
}
