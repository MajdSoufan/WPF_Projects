using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace Process_Scheduling
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ProcessesTableWindow resultsWindow;
        private List<Process> processesList;

        public MainWindow()
        {
            InitializeComponent();
            processesList = new List<Process>();

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if ((!textBoxInput.Text.Equals("")) || (!textBoxOutput.Text.Equals("")) || (!textBoxQuantum.Text.Equals("")))
            {
                string fileName = textBoxInput.Text;
                string path = Environment.CurrentDirectory + "\\" + fileName + ".dat";
                try
                {   // Open the text file using a stream reader.

                    using (StreamReader streamReader = new StreamReader(path))
                    {
                        int quantum = Int32.Parse(textBoxQuantum.Text);

                        this.resultsWindow = new ProcessesTableWindow(path, textBoxOutput.Text, quantum);


                        resultsWindow.Show();
                        this.Close();
                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message + "\nMain Please try again!!!");
                }


            }
            else
                MessageBox.Show("Invalid Input!!\nPlease try again!!!");


        }



        private void textBoxQuantum_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }

        private void textBoxQuantum_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }
    }
}
