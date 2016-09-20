using BookViewer.Model;
using Prism.Unity;
using BookViewer.Views;
using Microsoft.Practices.Unity;

namespace BookViewer
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();

            NavigationService.NavigateAsync("MainPage?title=Hello%20from%20Xamarin.Forms");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<MainPage>();

            Container.RegisterType<IBook, Book>(new ContainerControlledLifetimeManager());
        }
    }
}
