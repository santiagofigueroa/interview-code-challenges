using ASF.MRobot.Controllers;
using ASF.MRobot.Instructions;
using ASF.MRobot.Interfaces;
using ASF.MRobot.Models;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddSingleton<MarsGrid>(new MarsGrid(5, 3));
services.AddSingleton<IInstructionHandler, FHandler>();
services.AddSingleton<IInstructionHandler, LHandler>();
services.AddSingleton<IInstructionHandler, RHandler>();
services.AddSingleton<IRobotController, RobotController>();

var provider = services.BuildServiceProvider();
var controller = provider.GetRequiredService<IRobotController>();