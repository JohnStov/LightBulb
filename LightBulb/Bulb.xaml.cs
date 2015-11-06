using System.Threading.Tasks;
using Windows.Devices.Gpio;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace LightBulb
{
    public sealed partial class Bulb : UserControl
    {
        private GpioPin ledPin;
        private const int LedGpio = 5;

        public Bulb()
        {
            InitializeComponent();

            Loaded += (sender, args) =>
            {
                var controller = GpioController.GetDefault();
                ledPin = controller.OpenPin(LedGpio);
                ledPin.SetDriveMode(GpioPinDriveMode.Output);
            };
        }

        public static readonly DependencyProperty IsOnProperty = DependencyProperty.Register("IsOn", typeof(bool), typeof(Bulb), new PropertyMetadata(false, OnIsOnChanged));

        private static void OnIsOnChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var bulb = (Bulb) o;
            var newValue = (bool) e.NewValue;
            if (newValue == bulb.IsOn)
                return;

            bulb.BulbOff.Visibility = newValue ? Visibility.Collapsed : Visibility.Visible;
            bulb.BulbOn.Visibility = newValue ? Visibility.Visible : Visibility.Collapsed;
            bulb.ledPin.Write(newValue ? GpioPinValue.High : GpioPinValue.Low);
        }

        public bool IsOn
        {
            get { return (bool) GetValue(IsOnProperty); }
            set { SetValue(IsOnProperty, value); }
        }
    }
}