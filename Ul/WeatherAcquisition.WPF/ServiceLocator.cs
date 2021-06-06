using Microsoft.Extensions.DependencyInjection;
using WeatherAcquisition.WPF.ViewModels;

namespace WeatherAcquisition.WPF
{
    class ServiceLocator
    {
        public MainWindowViewModel MainModel => App.Services.GetRequiredService<MainWindowViewModel>();
    }
}
