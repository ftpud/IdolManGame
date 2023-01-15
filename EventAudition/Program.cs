using AuditionEvent;
using UglyAppFramework;
using UglyAppFramework.DependencyManager.Attributes;
using UglyAppFramework.Interfaces;

UglyApp.Start(new Loader());
Console.WriteLine(":3");
Console.ReadLine();

namespace AuditionEvent
{
    class Loader:IUglyLoader
    {
        [Inject] private AuditionEventManager AuditionEventManager { get; set; }
        public void Load()
        {
            AuditionEventManager.StartListening();
        }
    }
}

