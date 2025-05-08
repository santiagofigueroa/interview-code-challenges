using Xunit;
using ASF.MRobot.Models;
using ASF.MRobot.Interfaces;
using System.Collections.Generic;
using ASF.MRobot.Instructions;
using ASF.MRobot.Controllers;

public class RobotTests
{
    private readonly List<IInstructionHandler> _handlers;
    private readonly MarsGrid _grid;

    public RobotTests()
    {
        // Initialize the grid (as it does not change) and instruction handlers
        _grid = new MarsGrid(5, 3);
        _handlers = new List<IInstructionHandler>
        {
            new FHandler(),
            new LHandler(),
            new RHandler()
        };
    }

    [Fact]
    public void Robot_Should_Not_Be_Lost_And_Return_Correct_Position_TC1()
    {

        var robot = new Robot(1, 1, 'E',_grid);
        var executor = new RobotController(_handlers);

        executor.ExecuteInstructions(robot,"RFRFRFRF");

        Assert.Equal(1, robot.X);
        Assert.Equal(1, robot.Y);
        Assert.Equal('E', robot.Orientation);
        Assert.False(robot.IsLost);
    }

    [Fact]
    public void Robot_Should_Be_Lost_And_Leave_Scent_TC2()
    {
        var robot = new Robot(3, 2, 'N',_grid);
        var executor = new RobotController(_handlers);
        executor.ExecuteInstructions(robot, "FRRFLLFFRRFLL");

        Assert.Equal(3, robot.X);
        Assert.Equal(3, robot.Y);
        Assert.Equal('N', robot.Orientation);
        Assert.True(robot.IsLost);
    }

    [Fact]
    public void Robot_Should_Avoid_Lost_Because_Of_Scent_TC3()
    {
        var executor = new RobotController(_handlers);

        // Simulate scent left by previous robot at 3,3,N
        _grid.LeaveScent(3, 3, 'N');

        var robot = new Robot(0, 3, 'W', _grid);
        executor.ExecuteInstructions(robot, "LLFFFLFLFL");

        Assert.Equal(2, robot.X);
        Assert.Equal(3, robot.Y);
        Assert.Equal('S', robot.Orientation);
        Assert.False(robot.IsLost);
    }
}
