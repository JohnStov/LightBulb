using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace LightBulb
{
    public sealed partial class Bulb : UserControl
    {
        public Bulb()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty IsOnProperty = DependencyProperty.Register("IsOn", typeof(bool), typeof(Bulb), new PropertyMetadata(false, OnIsOnChanged));

        private static void OnIsOnChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var bulb = (Bulb) o;
            var newValue = (bool) e.NewValue;
            bulb.BulbOff.Visibility = newValue ? Visibility.Collapsed : Visibility.Visible;
            bulb.BulbOn.Visibility = newValue ? Visibility.Visible : Visibility.Collapsed;
        }

        public bool IsOn
        {
            get { return (bool) GetValue(IsOnProperty); }
            set { SetValue(IsOnProperty, value); }
        }
    }
}