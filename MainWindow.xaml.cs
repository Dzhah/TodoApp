using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using TodoApp.Models;
using TodoApp.Services;

namespace TodoApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string PATH = $"{Environment.CurrentDirectory}\\todoDataList.json";
        private BindingList<TodoModel> _todoDataList;
        private FileIOService _fileIOService;
        System.Windows.Threading.DispatcherTimer Timer = new System.Windows.Threading.DispatcherTimer(); //  --- For WPF Clock
        public MainWindow()
        {
            InitializeComponent();
            Timer.Tick += new EventHandler(Timer_Click);    //  --- For WPF Clock
            Timer.Interval = new TimeSpan(0, 0, 1);         //  --- For WPF Clock
            Timer.Start();                                  //  --- For WPF Clock

        }
        private void Timer_Click(object sender, EventArgs e)    //  --- For WPF Clock

        {                                                         //  --- For WPF Clock
            DateTime d;                                             //  --- For WPF Clock
            d = DateTime.Now;                                         //  --- For WPF Clock
            dt.Text = d.Day + " " + d.ToString("MMMM").ToUpper();       //  --- For WPF Clock
            clk.Text = d.Hour+":"+d.Minute;                             //  --- For WPF Clock
            wk.Text = d.ToString("dddd").ToUpper();                     //  --- For WPF Clock
        }                                                               //  --- For WPF Clock
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _fileIOService = new FileIOService(PATH);
            try
            {
                _todoDataList = _fileIOService.LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Close();
            }

            dgTodoList.ItemsSource = _todoDataList;
            _todoDataList.ListChanged += _todoDataList_ListChanged;

        }

        private void _todoDataList_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemDeleted || e.ListChangedType == ListChangedType.ItemChanged)
            {
                try
                {
                    _fileIOService.SaveData(sender);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Close();
                }
            }

        }
        private void CopyCommand(object sender, ExecutedRoutedEventArgs e)
        {
            
        }
    }
}
