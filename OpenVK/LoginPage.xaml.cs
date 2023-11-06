using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using OpenVkNetApi;
using OpenVkNetApi.Models;
using Windows.Storage;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace OpenVK
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        private OvkApi api = new OvkApi();
        public AuthorizedUser user;
        private ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        public LoginPage()
        {
            this.InitializeComponent();
        }

        private async void loginButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(loginBox.Text) && !string.IsNullOrEmpty(passwordBox.Password) && !string.IsNullOrEmpty(instanceBox.Text))
            {
                user = await api.Authorize(loginBox.Text, passwordBox.Password, instanceBox.Text);
                await user.Account.SetOnline();
                localSettings.Values["instance"] = instanceBox.Text;
                localSettings.Values["token"] = user.AccessToken;
                Frame.Navigate(typeof(MainPage));
            }
        }
    }
}
