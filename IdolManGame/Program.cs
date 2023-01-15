using IdolManGame.Game.ViewModel;
using UglyTgApplication;

//WorldEngine we = new WorldEngine();

Console.WriteLine("Loading...");
UglyTgApp.Start(File.ReadAllText("token.txt"), typeof(WelcomePage));
Console.WriteLine("Done");
Console.ReadLine();