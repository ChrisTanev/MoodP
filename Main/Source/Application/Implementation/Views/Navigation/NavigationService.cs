using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using MP.Application.Facade;

namespace MP.Application.Implementation.Views.Navigation
{
    public class NavigationService : INavigationService
    {
        private readonly Frame _navigationFrame = Window.Current.Content as Frame;

        public void NavigateBack()
        {
            _navigationFrame.GoBack();
        }

        public void Navigate(Type page)
        {
            _navigationFrame.Navigate(page);
        }
    }
}