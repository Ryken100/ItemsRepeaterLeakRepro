using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace ConvertingCollectionProxyItemsRepeaterMemoryLeakDemo
{
    public sealed partial class MyUserControl : UserControl
    {
        private readonly ObservableCollection<object> _collection = new ObservableCollection<object>();

        public MyUserControl()
        {
            this.InitializeComponent();

            ReferenceCounter.Add(Repeater);

            Repeater.ItemsSource = _collection;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            _collection.Add(new object());
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            _collection.Clear();
            Repeater.ItemsSource = null;
        }
    }
}
