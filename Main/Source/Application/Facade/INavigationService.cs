using System;

namespace MP.Application.Facade
{
    public interface INavigationService
    {
        void Navigate(Type page);
        void NavigateBack();
    }
}