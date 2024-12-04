using Microsoft.Win32;
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
using System.Windows.Shapes;
using Content_Management_System.Classes;
using System.Collections.ObjectModel;
using Notification.Wpf;
using Content_Management_System;
using System.IO;

namespace Content_Management_System
{
    /// <summary>
    /// Interaction logic for AddModel.xaml
    /// </summary>
    public partial class AddModel : Window
    {
        private string picture = "";
        private NotificationManager notificationManager;
        public User savedUser = new User();
        public CarSpecifications carSpecifications = new CarSpecifications();
       
        public ObservableCollection<CarSpecifications> carSpecs { get; set; }
        public AddModel()
        {
            InitializeComponent();
            ComboBoxFontFamily.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            ComboBoxColorChange.ItemsSource = typeof(Colors).GetProperties().Where(p => p.PropertyType == typeof(Color)).OrderBy(p => p.Name).Select(p => (Color)p.GetValue(null)).ToList();
            ComboBoxFontSize.ItemsSource = Enumerable.Range(1,30).Select(i => (double)i).ToList();

            TextBoxModel.Text = "Input model type:";
            TextBoxModel.Foreground = Brushes.LightGray;
            notificationManager = new NotificationManager();

            TextBoxNumber.Text = "Input number:";
            TextBoxNumber.Foreground = Brushes.LightGray;

            TextBoxFuel.Text = "Input fuel:";
            TextBoxFuel.Foreground = Brushes.LightGray;

           
        }

     


        private bool Validate()
        {
            bool isValid = true;

            if(TextBoxModel.Text.Trim().Equals(string.Empty) || TextBoxModel.Text.Trim().Equals("Input model type:"))
            {
                isValid = false;
                ErrorLabelModel.Content = "Model field cannot be empty!";
                TextBoxModel.BorderBrush = Brushes.Red;
                
            }
            else
            {
                ErrorLabelModel.Content = string.Empty;
                TextBoxModel.BorderBrush= Brushes.Gray;
            }

            if(TextBoxNumber.Text.Trim().Equals(string.Empty) || TextBoxNumber.Text.Trim().Equals("Input number:"))
            {
                isValid = false;
                ErrorLabelName.Content = "Name field cannot be empty!";
                TextBoxNumber.BorderBrush = Brushes.Red;
            }
            else
            {
                ErrorLabelName.Content= string.Empty;
                TextBoxNumber.BorderBrush= Brushes.Gray;
            }

            if (TextBoxFuel.Text.Trim().Equals(string.Empty) || TextBoxFuel.Text.Trim().Equals("Input fuel:"))
            {
                isValid = false;
                ErrorLabelFuel.Content = "Fuel field cannot be empty!";
                TextBoxFuel.BorderBrush = Brushes.Red;
            }
            else
            {
                ErrorLabelFuel.Content = string.Empty;
                TextBoxFuel.BorderBrush = Brushes.Gray;
            }


            if (ImagePreview.Source == null)
            {
                isValid= false;
                ErrorLabelImage.Content = "You need to put image";
            }
             return isValid;
        }


        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void ButtonExitAdd_Click(object sender, RoutedEventArgs e)
        {
            
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to exit?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                this.Close();
                

            }
        }

        private void ComboBoxColorChange_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ComboBoxColorChange.SelectedItem != null && !RichTextBoxEdit.Selection.IsEmpty)
            {
                if(ComboBoxColorChange.SelectedItem is Color color)
                {
                    SolidColorBrush brush = new SolidColorBrush(color);
                    RichTextBoxEdit.Selection.ApplyPropertyValue(Inline.ForegroundProperty, brush);
                }
            }
            
        }

        private void ComboBoxFontSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ComboBoxFontSize.SelectedItem != null && !RichTextBoxEdit.Selection.IsEmpty)
            {
                RichTextBoxEdit.Selection.ApplyPropertyValue(Inline.FontSizeProperty, ComboBoxFontSize.SelectedItem);
            }
        }

        private void RichTextBoxEdit_SelectionChanged(object sender, RoutedEventArgs e)
        {
            object fontWeight1 = RichTextBoxEdit.Selection.GetPropertyValue(Inline.FontWeightProperty);
            ToggleButtonBold.IsChecked = (fontWeight1 != DependencyProperty.UnsetValue) && (fontWeight1.Equals(FontWeights.Bold));

            object fontStyle = RichTextBoxEdit.Selection.GetPropertyValue(Inline.FontStyleProperty);
            ToggleButtonItalic.IsChecked = (fontStyle != DependencyProperty.UnsetValue) && (fontStyle.Equals(FontStyles.Italic));

            object fontDecoration = RichTextBoxEdit.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            ToggleButtonItalic.IsChecked = (fontDecoration != DependencyProperty.UnsetValue) && (fontDecoration.Equals(TextDecorations.Underline));

            object foregroundColor = RichTextBoxEdit.Selection.GetPropertyValue(Inline.ForegroundProperty);
            if (foregroundColor is SolidColorBrush brush)
            {
                Color selectedColor = brush.Color;
                ComboBoxColorChange.SelectedItem = selectedColor;
            }

            object fontSize = RichTextBoxEdit.Selection.GetPropertyValue(Inline.FontSizeProperty);
            if (fontSize != DependencyProperty.UnsetValue)
            {
                ComboBoxFontSize.SelectedItem = (double)fontSize;
            }


        }

        private void RichTextBoxEdit_TextChanged(object sender, TextChangedEventArgs e)
        {
            WordsCount();
        }

        private void WordsCount()
        {
            string text = new TextRange(RichTextBoxEdit.Document.ContentStart, RichTextBoxEdit.Document.ContentEnd).Text;
            int wordsCount = text.Split(new char[] { ' ', '\n', '\r'},StringSplitOptions.RemoveEmptyEntries).Length;
            StatusBlockCountWords.Text = $"Words: {wordsCount}";
        }

        private void LabelImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            
            if(openFileDialog.ShowDialog() == true)
            {
                picture = openFileDialog.FileName;
                Uri uri = new Uri(picture);
                ImagePreview.Source = new BitmapImage(uri);
            }
        }

        private void DateOfPost_MouseEnter(object sender, MouseEventArgs e)
        {
            DateOfPost.Text = DateTime.Now.ToString();
            DateOfPost.IsEnabled = false;
        }

        private void ComboBoxFontFamily_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ComboBoxFontFamily.SelectedItem != null && !RichTextBoxEdit.Selection.IsEmpty)
            {
                RichTextBoxEdit.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, ComboBoxFontFamily.SelectedItem);
            }
        }

        

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if(Validate())
            {
                
                    TableWindow tableWindow = new TableWindow(savedUser);
                    string path = "Paths/cars"+".rtf";

                    TextRange textRange;
                    FileStream fileStream;
                    textRange = new TextRange(RichTextBoxEdit.Document.ContentStart, RichTextBoxEdit.Document.ContentEnd);
                    fileStream = new FileStream(path, FileMode.Create);
                    textRange.Save(fileStream, System.Windows.DataFormats.XamlPackage);
                    fileStream.Close();
                    this.Close();


                tableWindow.Specifications.Add(new Classes.CarSpecifications(TextBoxModel.Text, picture, DateOfPost.SelectedDate.Value.ToString(), TextBoxFuel.Text, path, Int32.Parse(TextBoxNumber.Text)));
                  
                     
            }
            else
            {
                MessageBox.Show("Check all!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void ShowToastNotification(ToastNotification notification)
        {
            notificationManager.Show(notification.Title, notification.Message, notification.Type, "WindowNotificationArea");
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            if((string.IsNullOrWhiteSpace(TextBoxModel.Text) || TextBoxModel.Text.Trim() == "Input model type:"
                && ImagePreview.Source == null && string.IsNullOrWhiteSpace(new TextRange(RichTextBoxEdit.Document.ContentStart, RichTextBoxEdit.Document.ContentEnd).Text)
                && string.IsNullOrWhiteSpace(TextBoxFuel.Text) || TextBoxFuel.Text.Trim() == "Input fuel:" && string.IsNullOrWhiteSpace(TextBoxNumber.Text) || TextBoxNumber.Text.Trim() == "Input number:"))
            {
                TableWindow tableWindow = new TableWindow(savedUser);
                tableWindow.Show();
                this.Close();
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to cancel?", "Cancel", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if(result == MessageBoxResult.Yes)
                {
                    TableWindow tableWindow = new TableWindow(savedUser);
                    tableWindow.Show();
                    this.Close();
                }
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TextBoxModel.Text.Trim().Equals("Input model type:"))
            {
                TextBoxModel.Text = "";
                TextBoxModel.Foreground = Brushes.Black;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TextBoxModel.Text.Trim().Equals(string.Empty))
            {
                TextBoxModel.Text = "Input model type:";
                TextBoxModel.Foreground = Brushes.LightGray;
            }
        }

        private void TextBoxNumber_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TextBoxNumber.Text.Trim().Equals("Input number:"))
            {
                TextBoxNumber.Text = "";
                TextBoxNumber.Foreground = Brushes.Black;
            }
        }

        private void TextBoxNumber_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TextBoxNumber.Text.Trim().Equals(string.Empty))
            {
                TextBoxNumber.Text = "Input number:";
                TextBoxNumber.Foreground = Brushes.LightGray;
            }
        }

        private void TextBoxFuel_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TextBoxFuel.Text.Trim().Equals("Input fuel:"))
            {
                TextBoxFuel.Text = "";
                TextBoxFuel.Foreground = Brushes.Black;
            }
        }

        private void TextBoxFuel_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TextBoxFuel.Text.Trim().Equals(string.Empty))
            {
                TextBoxFuel.Text = "Input fuel:";
                TextBoxFuel.Foreground = Brushes.LightGray;
            }
        }
    }
}
