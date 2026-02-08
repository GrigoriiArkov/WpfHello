using System;
using System.CodeDom;
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

namespace WpfHello
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool isDataDirty = false;
        string nameFile = "C:\\Users\\myarkovy\\Desktop\\ИТМО\\05_Разработка Windows приложений\\ДЗ\\WPF Практическая 1\\Упр 1\\username.txt";
        public MyWindow myWin { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            CommandBinding abinding = new CommandBinding(); //Добавил в рамках Практической №4
            abinding.Command = CustomCommands.Launch;
            abinding.Executed += new ExecutedRoutedEventHandler(Launch_Handler);
            abinding.CanExecute += new CanExecuteRoutedEventHandler(LaunchEnabled_Handler); //Добавил в рамках Практической №4
            this.CommandBindings.Add(abinding);
            lbl.Content = "Добрый день!";
            setBut.IsEnabled = false;
            retBut.IsEnabled = false;
            this.Closing += Window_Closing;
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void setText_TextChanged(object sender, TextChangedEventArgs e)
        {
            setBut.IsEnabled = true;
            isDataDirty = true;
        }

        private void Window_Closing (object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.isDataDirty)
            {
                string msg = "Данные были изменены, но не сохранены! \n Закрыть окно без сохранения?";
                MessageBoxResult result = MessageBox.Show(msg, "Контроль данных", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SetBut() //Добавил в рамках Практической №3
        {
            System.IO.StreamWriter sw = new System.IO.StreamWriter(nameFile);
            sw.WriteLine(setText.Text);
            sw.Close();
            retBut.IsEnabled = true;
            isDataDirty = false;
        }

        private void RetBut() //Добавил в рамках Практической №3
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(nameFile);
            string content = sr.ReadToEnd();
            setText.Text = content;
            retLabel.Content = "Приветствую вас, уважаемый " + content;
            sr.Close();
            setBut.IsEnabled = true;
            isDataDirty = false;
        }
        private void Grid_Click(object sender, RoutedEventArgs e) //Добавил в рамках Практической №3
        {
            FrameworkElement feSource = e.Source as FrameworkElement;
            try
            {
                switch (feSource.Name)
                {
                    case "setBut":
                        SetBut();
                        break;
                    case "retBut":
                        RetBut();
                        break;
                }
                e.Handled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void LaunchEnabled_Handler (object sender, CanExecuteRoutedEventArgs e) //Добавил в рамках Практической №4
        {
            e.CanExecute = (bool)check.IsChecked;
        }
        private void Launch_Handler (object sender, ExecutedRoutedEventArgs e) //Добавил в рамках Практической №4
        {
            if (myWin == null)
                myWin = new MyWindow();
            myWin.Owner = this;
            var Location = New_Win.PointToScreen(new Point(0, 0));
            myWin.Top = Location.X + New_Win.Width;
            myWin.Left = Location.Y;
            myWin.Show();
        }
    }
}
