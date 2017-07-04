using System;

namespace MP.Application.Facade
{
    public interface INavigationPage
    {
        Type PageType { get; set; }
    }
}