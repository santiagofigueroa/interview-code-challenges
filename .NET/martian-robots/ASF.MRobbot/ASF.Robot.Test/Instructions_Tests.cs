using ASF.MRobot.Models;
using Xunit;

namespace ASF.MRobot.Tests
{
    public class RobotTests
    {
        [Fact]
        public void MoveForward_ShouldUpdatePositionCorrectly()
        {
            // Arrange
            var grid = new MarsGrid(5, 5);
            var robot = new Robot(2, 2, 'N', grid);

            // Act
            robot.MoveForward();

            // Assert
            Assert.Equal(2, robot.X);
            Assert.Equal(3, robot.Y);
            Assert.Equal('N', robot.Orientation);
        }

        [Fact]
        public void TurnLeft_ShouldUpdateOrientationCorrectly()
        {
            // Arrange
            var grid = new MarsGrid(5, 5);
            var robot = new Robot(2, 2, 'N', grid);

            // Act
            robot.TurnLeft();

            // Assert
            Assert.Equal('W', robot.Orientation);
        }

        [Fact]
        public void TurnRight_ShouldUpdateOrientationCorrectly()
        {
            // Arrange
            var grid = new MarsGrid(5, 5);
            var robot = new Robot(2, 2, 'N', grid);

            // Act
            robot.TurnRight();

            // Assert
            Assert.Equal('E', robot.Orientation);
        }
    }
}