using ASF.MRobot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASF.MRobot.Interfaces
{
    public interface IInstructionHandler
    {
        void Handle(Robot robot, MarsGrid grid);
    }
}
