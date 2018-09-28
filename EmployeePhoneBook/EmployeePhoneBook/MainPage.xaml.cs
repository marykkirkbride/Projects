using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace EmployeePhoneBook
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

        }
        private void NavView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            On_BackRequested();
        }

        private bool On_BackRequested()
        {
            if (!contentFrame.CanGoBack)
                return false;

            // Don't go back if the nav pane is overlayed
            if (nvTopLevelNav.IsPaneOpen &&
                (nvTopLevelNav.DisplayMode == NavigationViewDisplayMode.Compact ||
                nvTopLevelNav.DisplayMode == NavigationViewDisplayMode.Minimal))
                return false;

            contentFrame.GoBack();
            return true;
        }

        private void nvTopLevelNav_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (NavigationViewItemBase item in nvTopLevelNav.MenuItems)
            {
                if (item is NavigationViewItem && item.Tag.ToString() == "Display")
                {
                    nvTopLevelNav.SelectedItem = item;
                    break;
                }
            }
            contentFrame.Navigate(typeof(DisplayPage));
        }

        private void nvTopLevelNav_ItemsInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            TextBlock ItemContent = args.InvokedItem as TextBlock;
            if (ItemContent != null)
            {
                switch (ItemContent.Tag)
                {
                    case "Nav_Display":
                        contentFrame.Navigate(typeof(DisplayPage));
                        break;

                    case "Nav_NewEmployee":
                        contentFrame.Navigate(typeof(AddEmployee));
                        break;
                }
            }
        }
    }
}