using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Threading;
using System.ComponentModel;

namespace MemoryMangementProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private MemoryMangementUnit MMU;
        private BackgroundWorker bkw1 = new BackgroundWorker();
        private BackgroundWorker bkw2 = new BackgroundWorker();
        private BackgroundWorker bkw3 = new BackgroundWorker();
        public MainWindow()
        {
            InitializeComponent();
            button.IsEnabled = false;            
        }

    

        private void button_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                ReadFile();
                SettingThreadsUp();
                bkw1.RunWorkerAsync("Message to Worker");
                bkw2.RunWorkerAsync("Message to Worker2");
                // bkw3.RunWorkerAsync("Message to Worker3");
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

        }

        private void ReadFile()
        {

            


            string fileName = inputFileTextBox.Text;
            string path = Environment.CurrentDirectory + "\\inputfiles" + "\\" + fileName + ".dat";
            listBox.Items.Clear();

            // Open the text file using a stream reader.
            using (StreamReader streamReader = new StreamReader(path))
            {
                MMU = new MemoryMangementUnit(Int32.Parse(numberOfFramestextBox.Text));

                // Read the stream to a string, and write the string to the console.
                while (!streamReader.EndOfStream)
                {
                    string currentLine = streamReader.ReadLine().ToString();
                    string[] inputSplitter = currentLine.Split(' ');
                    MMU.processPagesList.Add(new PPReference(Int32.Parse(inputSplitter[0]), Int32.Parse(inputSplitter[1])));
                    listBox.Items.Add(currentLine);

                }
            }

        }

        private void SettingThreadsUp()
        {
            // Setting up Threads to tun the three algorithm 
            bkw1.DoWork += new DoWorkEventHandler((object sender, DoWorkEventArgs e) => MMU.RunLRU());
            bkw2.DoWork += new DoWorkEventHandler((object sender, DoWorkEventArgs e) => MMU.RunFIFO());
            bkw3.DoWork += new DoWorkEventHandler((object sender, DoWorkEventArgs e) => MMU.RunOPT());

            bkw1.RunWorkerCompleted += new RunWorkerCompletedEventHandler((object sender, RunWorkerCompletedEventArgs e) => {
                LRUfaultPagesCount.Content = MMU.numPageFaultsLRU.ToString();
            });
            bkw2.RunWorkerCompleted += new RunWorkerCompletedEventHandler((object sender, RunWorkerCompletedEventArgs e) => {
                FIFOfaultPagesCount.Content = MMU.numPageFaultsFIFO.ToString();
            }); bkw3.RunWorkerCompleted += new RunWorkerCompletedEventHandler((object sender, RunWorkerCompletedEventArgs e) => {
                OPTfaultPagesCount.Content = MMU.numPageFaultsOPT.ToString();
            });
        }


        private void numberOfFramestextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }

        private void numberOfFramestextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (numberOfFramestextBox.Text.Length != 0 && inputFileTextBox.Text.Trim().Length != 0)
            {
                button.IsEnabled = true;
            }
            else
            {
                button.IsEnabled = false;

            }
        }

        private void inputFileTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
       {
            numberOfFramestextBox_TextChanged(sender, e);
        }

        private void numberOfFramestextBox_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void HButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("For input file name do not include an extention.", "Help Center", MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.OK);
        }
    }
}