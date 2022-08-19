using System;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ConvertingCollectionProxyItemsRepeaterMemoryLeakDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly DispatcherTimer _timer;

        public MainPage()
        {
            this.InitializeComponent();

            _timer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromSeconds(1.5)
            };
            _timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, object e)
        {
            CountText.Text = $"Alive count: {ReferenceCounter.AliveCount}";
            Debug.WriteLine($"Alive count: {ReferenceCounter.AliveCount}");
        }

        private void CreateNewOneButton_Click(object sender, RoutedEventArgs e)
        {
            Host.Child = new MyUserControl();
            /*var g = new Grid();
            ReferenceCounter.Add(g);
            Host.Child = g;*/

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _timer.Start();
        }
    }
}
