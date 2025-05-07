using ASF.MRobot.Interfaces;
using ASF.MRobot.Models;

namespace ASF.MRobot.Instructions
{
    public class RHandler : IInstructionHandler
    {
        public void Handle(Robot robot, MarsGrid grid)
        {
            robot.TurnRight();
        }
    }
}
