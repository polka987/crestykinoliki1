using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
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

namespace Ya_nolik_simki_net
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static bool nolik = false;
        static List<int> user = new List<int>();
        static List<int> bot = new List<int>();
        static List<List<int>> wins = new List<List<int>>()
        {
            new List<int>(){ 1,2,3 },
            new List<int>(){ 4,5,6 },
            new List<int>(){ 7,8,9 },
            new List<int>(){ 2,5,8 },
            new List<int>(){ 1,5,9 },
            new List<int>(){ 3,5,7 },
            new List<int>(){ 1,4,7 },
            new List<int>(){ 3,6,9 },
        };
        static List<int> isenable = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void size(object sender, RoutedEventArgs e)
        {
            SystemCommands.MaximizeWindow(this);
            if (WindowState == WindowState.Maximized)
            {
                SystemCommands.RestoreWindow(this);
            }
        }

        private void Hide(object sender, RoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            nolik = true;
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            nolik = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            set();
            if (nolik)
                bot_hod();
        }
        void set()
        {
            user.Clear();
            bot.Clear();
            start.IsEnabled = !start.IsEnabled;
            game.IsEnabled = !game.IsEnabled;
            rezult.IsEnabled = !rezult.IsEnabled;
            isenable = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 }; 
            foreach (var a in game.Children)
            {
                (a as Button).Content = "";
                (a as Button).IsEnabled = true;
            }
        }

        private void click(object sender, RoutedEventArgs e)
        {
            if (nolik) (sender as Button).Content = "o";
            else (sender as Button).Content = "x";
            int a; 
            int.TryParse(string.Join("", (sender as Button).Name.ToString().Where(c => char.IsDigit(c))), out a);
            (game.Children[a - 1] as Button).IsEnabled = false;
            user.Add(a);
            isenable.Remove(a);
            if (iswin(user))
            {
                MessageBox.Show("Вы победили");
                set();
                return;
            }
            if (isenable.Count == 0)
            {
                MessageBox.Show("Ничья!");
                set();
                return;
            }
            bot_hod();
        }
        void bot_hod()
        {
            int r = isenable[new Random().Next(isenable.Count)];
            if (nolik)
                (game.Children[r-1]as Button).Content = "x";
            else
                (game.Children[r-1] as Button).Content = "o";
            (game.Children[r - 1] as Button).IsEnabled = false;
            bot.Add(r);
            isenable.Remove(r);
            if (iswin(bot))
            {
                MessageBox.Show("Бот победил");
                set();
                return;
            }


        }
        bool iswin(List<int> us)
        {
            int count = 0;
            foreach (var a in wins)
            {
                foreach (var b in us)
                {
                    if(a.Contains(b))
                        count++;
                }
                if (count == 3)
                    return true;
                count = 0;
            }
            return false;
        }
    }
}
