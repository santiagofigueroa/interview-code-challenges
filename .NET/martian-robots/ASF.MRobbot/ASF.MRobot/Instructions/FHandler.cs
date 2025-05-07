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

            if (!grid.IsInBounds(nextX, nextY))
            {
                if (!grid.HasScent(robot.X, robot.Y, robot.Orientation))
                {
                    grid.LeaveScent(robot.X, robot.Y, robot.Orientation);
                    robot.MarkLost();
                }
                // else: ignore the move
                return;
            }

            robot.MoveForward(); // Safe to move
        }
    }
}
