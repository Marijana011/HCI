using Content_Management_System.Classes;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;
using Notification.Wpf;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;



namespace Content_Management_System
{
    /// <summary>
    /// Interaction logic for TableWindow.xaml
    /// </summary>
    public partial class TableWindow : Window
    {
        public ObservableCollection<CarSpecifications> Specifications { get; set; }
       
        public User savedUser = new User(); 
        public static bool CheckBoxS = false;
        public static BindingList<CarSpecifications> delete = new BindingList<CarSpecifications>();
        public static BindingList<CarSpecifications> CarSpecifications {  get; set; }

        public TableWindow(User user)
        {
            InitializeComponent();
 
            



            DataContext = this;

            

            LoadFromXML();
        }

        public void LoadFromXML()
        {
            string path = "/Paths/cars.xml";
            if(File.Exists(path))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<CarSpecifications>));
                using (FileStream fileStream = new FileStream(path, FileMode.Open))
                {
                    Specifications = (ObservableCollection<CarSpecifications>)serializer.Deserialize(fileStream);
                }
            }
            else
            {
                Specifications = new ObservableCollection<CarSpecifications>();
            }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            var textBlock = (TextBlock)sender;
            var item = textBlock.DataContext as CarSpecifications;

            if(item != null)
            {
                if(savedUser.Role == UserRole.Admin)
                {
                    EditWindow editWindow = new EditWindow(item,savedUser);
                    editWindow.Show();
                    this.Close();
                }
                else
                {
                    EditWindow editWindow = new EditWindow(item, savedUser);
                    editWindow.TextBoxModel.IsReadOnly = true;
                    editWindow.TextBoxFuel.IsReadOnly = true;
                    editWindow.TextBoxFuel.IsReadOnly = true;
                    editWindow.ButtonImage.Visibility = Visibility.Hidden;
                }
            }
            else
            {
                MessageBox.Show("Item not found!");
            }
        }

        private void ButtonAddModel_Click(object sender, RoutedEventArgs e)
        {
            AddModel addModel = new AddModel();
            addModel.ShowDialog();
            this.Close();
        }

        
        
        private void ButtonRemove_Click(object sender, RoutedEventArgs e)
        {
            for(int i = 0; i < delete.Count; i++)
            {
                CarSpecifications.Remove(delete[i]);
            }
            for(int i = 0; i <= delete.Count; i++)
            {
                if (delete[i] != null)
                {
                    string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, delete[i].Rtf);

                    try
                    {
                        File.Delete(filePath);
                    }
                    catch(IOException exp) 
                    {
                        Console.WriteLine(exp.Message);
                    }
                }
            }
        }

        private void ButtonExitTable_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to exit?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                this.Close();
                mainWindow.ShowDialog();
            }
        }

        private void CheckBox_MouseEnter(object sender, MouseEventArgs e)
        {
            CheckBoxS = true;
        }
    }
}
