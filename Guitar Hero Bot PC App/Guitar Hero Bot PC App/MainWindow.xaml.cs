using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Guitar_Hero_Bot_PC_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            fileParser = new DataFileParser();
            controller = new GuitarBotAppController();
            player = new GuitarBotPlayer();

            this.MainGrid.DataContext = controller;
        }

        private void Click_Load_File(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "C:\\";
            openFileDialog.Filter = "GHB Data files (*.ghbd)|*.ghbd|All files (*.*)|*.*";
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                controller.FileText = openFileDialog.FileName;
                this.ParseButton.IsEnabled = true;
                this.ConnectButton.IsEnabled = false;
                this.StartButton.IsEnabled = false;
            }
        }

        private void Click_Parse_File(object sender, RoutedEventArgs e)
        {
            fileParser.Init(controller.FileText);
            if (!fileParser.ParseDataFile())
            {
                controller.StatusText = fileParser.GetErrorStatus();
            }
            else
            {
                controller.StatusText = "Successfully parsed file.";
                this.ConnectButton.IsEnabled = true;
            }
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            this.StartButton.IsEnabled = true;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            player.Initialize(fileParser, controller);
            player.StartPlayback();
        }

        private GuitarBotAppController controller;
        private DataFileParser fileParser;
        private GuitarBotPlayer player;
    }
}
