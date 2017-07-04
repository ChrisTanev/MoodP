using System;
using MP.Application.Facade;

namespace MP.Application.Implementation.Views.Navigation
{
    public abstract class NavigationPage<T> : INavigationPage
    {
        protected NavigationPage()
        {
            PageType = typeof(T);
        }

        public Type PageType { get; set; }
    }
}