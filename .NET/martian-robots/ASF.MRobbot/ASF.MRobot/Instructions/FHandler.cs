using ASF.MRobot.Interfaces;
using ASF.MRobot.Models;

namespace ASF.MRobot.Instructions
{
    public class FHandler : IInstructionHandler
    {
        public void Handle(Robot robot, MarsGrid grid)
        {
            if (robot.IsLost) return;

            var (nextX, nextY) = robot.PeekNextPosition();

            if (IsOutOfBounds(nextX, nextY, robot, grid)) return;

            robot.MoveForward(); // Safe to move
        }

        private bool IsOutOfBounds(int nextX, int nextY, Robot robot, MarsGrid grid)
        {
            if (!grid.IsInBounds(nextX, nextY))
            {
                if (!grid.HasScent(robot.X, robot.Y, robot.Orientation))
                {
                    grid.LeaveScent(robot.X, robot.Y, robot.Orientation);
                    robot.MarkLost();
                }
                return true; // Out of bounds
            }
            return false; // In bounds
        }
    }
}
