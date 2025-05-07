using ASF.MRobot.Interfaces;
using ASF.MRobot.Models;

namespace ASF.MRobot.Instructions
{
    public class LHandler : IInstructionHandler
    {
        public char Instruction => 'L';

        public void Handle(Robot robot, MarsGrid grid)
        {
            robot.TurnLeft();
        }
    }
}
