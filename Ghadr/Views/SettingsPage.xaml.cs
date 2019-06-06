using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Ghadr.Helpers;
using Ghadr.Services;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Ghadr.Views
{
    // TODO WTS: Add other settings as necessary. For help see https://github.com/Microsoft/WindowsTemplateStudio/blob/master/docs/pages/settings-codebehind.md
    // TODO WTS: Change the URL for your privacy policy in the Resource File, currently set to https://YourPrivacyUrlGoesHere
    public sealed partial class SettingsPage : Page, INotifyPropertyChanged
    {
        private ElementTheme _elementTheme = ThemeSelectorService.Theme;

        public ElementTheme ElementTheme
        {
            get { return _elementTheme; }

            set { Set(ref _elementTheme, value); }
        }

        private string _versionDescription;

        public string VersionDescription
        {
            get { return _versionDescription; }

            set { Set(ref _versionDescription, value); }
        }
        ObservableCollection<FontFamily> fonts = new ObservableCollection<FontFamily>();

        public SettingsPage()
        {
            InitializeComponent();
            fonts.Add(new FontFamily("A suls"));
            fonts.Add(new FontFamily("Qurantaha"));
            fonts.Add(new FontFamily("neirizi"));

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Initialize();
        }

        private void Initialize()
        {
            VersionDescription = GetVersionDescription();
        }

        private string GetVersionDescription()
        {
            var appName = "AppDisplayName".GetLocalized();
            var package = Package.Current;
            var packageId = package.Id;
            var version = packageId.Version;

            return $"{appName} - {version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
        }

        private async void ThemeChanged_CheckedAsync(object sender, RoutedEventArgs e)
        {
            var param = (sender as RadioButton)?.CommandParameter;

            if (param != null)
            {
                await ThemeSelectorService.SetThemeAsync((ElementTheme)param);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Set<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return;
            }

            storage = value;
            OnPropertyChanged(propertyName);
        }
        private void ArabicFontFamilyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ArabicTestTextBlock.FontFamily = (Windows.UI.Xaml.Media.FontFamily)ArabicFontFamilyComboBox.SelectedItem;
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage), e);
        }

        private void PersianFontFamilyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PersianTestTextBlock.FontFamily = (Windows.UI.Xaml.Media.FontFamily)PersianFontFamilyComboBox.SelectedItem;

        }
    }
}
