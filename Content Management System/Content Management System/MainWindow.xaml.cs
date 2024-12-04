using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Content_Management_System;
using Content_Management_System.Classes;
using Notification.Wpf;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics.Eventing.Reader;
using System.Xml.Serialization;
using System.IO;

namespace Content_Management_System
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataIO serializer = new DataIO();
        private NotificationManager notificationManager;
        public static string name;
        public static string password;
        private List<User> users;
        private const string usersPath = "/Paths/users.xml";
        public MainWindow()
        {
            InitializeComponent();
            notificationManager = new NotificationManager();

            users = serializer.DeSerializeObject<List<User>>("Users.xml");
            if(users == null )
            {
                users = new List<User>();
            }
     

            TextBoxUserName.Text = "Input Username";
            TextBoxUserName.Foreground = Brushes.LightGray;

            TextBoxPassword.Text = "Input User Password";
            TextBoxPassword.Foreground = Brushes.LightGray;

            users = new List<User>
            {
                    new User{ Name = "admin1", Password = "123", Role = UserRole.Admin},
                    new User{ Name = "visitor", Password = "456", Role = UserRole.Visitor},
                    new User{ Name = "admin2", Password = "789", Role = UserRole.Admin}
            };

        }

        public void ShowToastNotification(ToastNotification notification)
        {
            notificationManager.Show(notification.Title, notification.Message, notification.Type, "WindowNotificationArea");
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private bool ValidateForm()
        {
            bool isValid = true;

            if(TextBoxUserName.Text.Trim().Equals(string.Empty) || TextBoxUserName.Text.Trim().Equals("Input Username") &&
                TextBoxPassword.Text.Trim().Equals(string.Empty) || TextBoxPassword.Text.Trim().Equals("Input User Password"))
            {
                isValid = false;
                ErrorLabelNamePassword.Content = "Username and password cannot be emty!";
                TextBoxUserName.BorderBrush = Brushes.Red;
                TextBoxPassword.BorderBrush = Brushes.Red;
            }
            else
            {
                ErrorLabelNamePassword.Content = string.Empty;
                TextBoxUserName.BorderBrush = Brushes.Gray;
                TextBoxUserName.BorderBrush = Brushes.Gray;
            }

            

            if (RadioButtonVisitor.IsChecked == false && RadioButtonAdmin.IsChecked == false)
            {
                isValid = false;
                ErrorLabelUser.Content = "An option must be chosen!";
            }
            else
            {
                ErrorLabelUser.Content = string.Empty;
            }
            

            return isValid;
        }


        private void TextBoxUserName_LostFocus(object sender, RoutedEventArgs e)
        {
            if(TextBoxUserName.Text.Trim().Equals(string.Empty))
            {
                TextBoxUserName.Text = "Input Username";
                TextBoxUserName.Foreground = Brushes.LightGray;
            }
        }

        private void TextBoxUserName_GotFocus(object sender, RoutedEventArgs e)
        {
            if(TextBoxUserName.Text.Trim().Equals("Input Username"))
            {
                TextBoxUserName.Text = "";
                TextBoxUserName.Foreground= Brushes.Black;
            }
        }

        private void TextBoxPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TextBoxPassword.Text.Trim().Equals(string.Empty))
            {
                TextBoxPassword.Text = "Input User Password";
                TextBoxPassword.Foreground = Brushes.LightGray;
            }
        }

        private void TextBoxPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TextBoxPassword.Text.Trim().Equals("Input User Password"))
            {
                TextBoxPassword.Text = "";
                TextBoxPassword.Foreground = Brushes.Black;
            }
        }


        private void ButtonLogIn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            string username = TextBoxUserName.Text;
            string password = TextBoxPassword.Text;
            
            
            if (ValidateForm())
            {      
                User user = users.Find( u => u.Name == username && u.Password == password);
                if (TextBoxUserName.Text.Contains("admin") && RadioButtonAdmin.IsChecked == true)
                {                  
                    
                        mainWindow.ShowToastNotification(new ToastNotification("You are now Loged In as Admin", "Success", NotificationType.Success));
                        TableWindow tableWindow = new TableWindow(user);
                        mainWindow.Close();
                        tableWindow.ShowDialog();
                        
                }
                else if(TextBoxUserName.Text.Contains("visitor") && RadioButtonVisitor.IsChecked == true)
                { 
                        mainWindow.ShowToastNotification(new ToastNotification("You are now Loged In as Visitor", "Success", NotificationType.Success));
                        TableWindow tableWindow = new TableWindow(user);
                        tableWindow.ButtonAddModel.Visibility = Visibility.Hidden;
                        tableWindow.ButtonRemoveModel.Visibility = Visibility.Hidden;
                        mainWindow.Close();
                        tableWindow.ShowDialog();
                        
                   
                }
                else
                {
                    mainWindow.ShowToastNotification(new ToastNotification("Invalid Username, Password or User role", "Error!", NotificationType.Error));

                    ErrorLabelNamePassword.Content = "Invalid username or password!";
                    ErrorLabelUser.Content = "Invalid user role!";
                    TextBoxUserName.BorderBrush = Brushes.Red;
                    TextBoxPassword.BorderBrush = Brushes.Red;
                    
                }               

            }
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveDataAsXML()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<User>));
            using (FileStream fileStream = new FileStream(usersPath, FileMode.Open))
            {
                serializer.Serialize(fileStream, this);
            }

        }

        private void ButtonExitMain_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to exit?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(messageBoxResult == MessageBoxResult.Yes)
            {
                SaveDataAsXML();
                this.Close();
            }
            
        }

        private void ButtonMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}
