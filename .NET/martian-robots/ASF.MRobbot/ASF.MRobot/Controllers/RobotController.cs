using ASF.MRobot.Interfaces;
using ASF.MRobot.Models;
namespace ASF.MRobot.Controllers
{
    public class RobotController :IRobotController
    {
        private readonly IDictionary<char, IInstructionHandler> _handlers;

        public RobotController(IEnumerable<IInstructionHandler> handlers)
        {
            _handlers = handlers.ToDictionary(
                h => h.GetType().Name[0],  // 'F', 'L', 'R'
                h => h
            );
        }

        public string ExecuteInstructions(Robot robot, string instructions)
        {
            foreach (var cmd in instructions)
            {
                if (robot.IsLost) break;
                if (_handlers.TryGetValue(cmd, out var handler))
                    handler.Handle(robot, robot.Grid);
            }

            return robot.ToString();
        }
    }
}
