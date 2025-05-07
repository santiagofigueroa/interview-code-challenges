using ASF.MRobot.Interfaces;
using ASF.MRobot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
