using Neq.Design.WPF.Navigation.Exceptions;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Neq.Design.WPF.Navigation
{
    public class Navigator
    {
        public static Navigator Current { get; set; }

        private NavigationService _navService;
        public NavigationService Service
        {
            get => _navService;
            set
            {
                if (_navService != null)
                    _navService.Navigated -= OnNavigated;

                _navService = value;
                _navService.Navigated += OnNavigated;
            }
        }

        public IPageResolver PageResolver { get; set; }

        public IWindowResolver WindowResolver { get; set; }

        public Navigator(NavigationService navService, IPageResolver pageResolver, IWindowResolver windowResolver)
        {
            Service = navService;
            PageResolver = pageResolver;
            WindowResolver = windowResolver;
        }

        private void OnNavigated(object sender, NavigationEventArgs e)
        {
            if (e.Content is Page page)
                page.DataContext = e.ExtraData;
        }

        public void Navigate(Page page, object context)
        {
            if (Service == null)
                throw new NavigationException($"{nameof(Service)} was null");

            if (page == null)
                throw new ArgumentNullException(nameof(page));

            Service.Navigate(page, context);
        }

        public void Navigate(Page page) => Navigate(page, null);

        public void Navigate(string alias, object context)
        {
            if (PageResolver == null)
                throw new NavigationException($"{nameof(PageResolver)} was null");

            Navigate(PageResolver.Resolve(alias), context);
        }

        public void Navigate(string alias) => Navigate(alias, null);

        private Window GetWindow(string alias, object context)
        {
            if (WindowResolver == null)
                throw new NavigationException($"{nameof(WindowResolver)} was null");

            var window = WindowResolver.Resolve(alias);
            window.DataContext = context;

            return window;
        }

        public void ShowWindow(string alias, object context) => GetWindow(alias, context).Show();

        public void ShowWindow(string alias) => ShowWindow(alias, null);

        public bool? ShowDialogWindow(string alias, object context) => GetWindow(alias, context).ShowDialog();

        public bool? ShowDialogWindow(string alias) => ShowDialogWindow(alias, null);
    }
}
