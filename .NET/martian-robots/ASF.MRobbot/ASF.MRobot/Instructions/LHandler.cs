using ASF.MRobot.Interfaces;
using ASF.MRobot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
