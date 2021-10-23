using Stylet;
using StyletIoC;

namespace StyletTest
{
    public class Bootstrapper : Bootstrapper<Window1ViewModel>
    {
        protected override void ConfigureIoC(IStyletIoCBuilder builder)
        {
            // Configure the IoC container in here
        }

        protected override void Configure()
        {
            // Perform any other configuration before the application starts
        }
    }
}
