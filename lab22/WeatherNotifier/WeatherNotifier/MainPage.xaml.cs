using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using WeatherNotifier.Models;
                                                                                                            

namespace WeatherNotifier
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Helper.Helper weatherHelper;
        public MainPage()
        {
            this.InitializeComponent();
            weatherHelper = new Helper.Helper();
        }

        private void mainPageLoaded(object sender, RoutedEventArgs e)
        {
            var bounds = ApplicationView.GetForCurrentView().VisibleBounds;
            var scaleFactor = DisplayInformation.GetForCurrentView().RawPixelsPerViewPixel;
            var size = new Size(bounds.Width * scaleFactor, bounds.Height * scaleFactor);
            Grid.Height = size.Height;
            Grid.Width = size.Width;
            Map.Height = Grid.Height;
            Map.Width = Grid.Width * 0.6;
            Panel.Margin = new Thickness(Map.Width, 0, 0, 0);
            Panel.Visibility = Visibility.Collapsed;
        }

        private void SetRelativeSize()
        {
           
        }
        private void ResizeForm(object sender, SizeChangedEventArgs e)
        {
            var bounds = ApplicationView.GetForCurrentView().VisibleBounds;
            var scaleFactor = DisplayInformation.GetForCurrentView().RawPixelsPerViewPixel;
            var size = new Size(bounds.Width * scaleFactor, bounds.Height * scaleFactor);
            Grid.Height = size.Height;
            Grid.Width = size.Width;
            Map.Height = Grid.Height;
            Map.Width = Grid.Width * 0.6;
            Panel.Margin = new Thickness(Map.Width, 0, 0, 0);
        }
        private void MapUserTapped(MapControl sender, MapInputEventArgs args)
        {
            BasicGeoposition geoPosition = args.Location.Position;
            GetWeatherForGeoPosition(geoPosition);
        }

        private void GetWeatherForGeoPosition(BasicGeoposition geoPosition)
        {
            var weather = weatherHelper.GetWeatherByCoords(geoPosition.Latitude, geoPosition.Longitude).Result;
            DisplayData(weather);
        }

        private void DisplayData(OpenWeather weather)
        {
            Latitude.Text = "Latitude: " + weather.coord.lat;
            Longitude.Text = "Longitude: " + weather.coord.lon;
            CityName.Text = "City: " + weather.name;
            CurrentTemp.Text = "Current temperature: " + weather.main.temp + " °C";
            MinTemp.Text = "Min temperature: " + weather.main.temp_min + " °C";
            MaxTemp.Text = "Max temperature: " + weather.main.temp_max + " °C";
            GroundLevel.Text = "Ground level: " + weather.main.grnd_level + " meters";
            SeaLevel.Text = "Sea level: " + weather.main.sea_level + " meters";
            Speed.Text = "Speed: " + weather.wind.speed + " km/h";
            SpeedDegree.Text = "Degree " + weather.wind.deg + " °";
            MainInfo.Text = weather.weather[0].main;
            Description.Text = "Description: " + weather.weather[0].description;
            Pressure.Text = "Pressure:" + weather.main.pressure + "hPa";
            Humidity.Text = "Humidity: " + weather.main.humidity + "%";
            Panel.Visibility = Visibility.Visible;
        }


    }
}
