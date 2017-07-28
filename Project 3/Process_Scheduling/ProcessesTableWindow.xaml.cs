using System;
using System.IO;
using System.Windows;

namespace Process_Scheduling
{
    /// <summary>
    /// Interaction logic for ProcessesTableWindow.xaml
    /// </summary>
    public partial class ProcessesTableWindow : Window
    {
        private ProcessScheduler processScheduler;

        public ProcessesTableWindow(string filePath, string outputFile, int mainQuantam)
        {
            InitializeComponent();
            processScheduler = new ProcessScheduler(mainQuantam, outputFile);
            ReadFile(filePath);
            processScheduler.ProcessingInput();
            foreach (var item in processScheduler.outputList)
            {
                listBox1.Items.Add(item);

            }
        }

        private void ReadFile(string filePath)
        {
            try
            {   // Open the text file using a stream reader.

                using (StreamReader streamReader = new StreamReader(filePath))
                {

                    // Read the stream to a string, and write the string to the console.
                    while (!streamReader.EndOfStream)
                    {
                        string currentLine = streamReader.ReadLine().ToString();
                        //char currentLine = (char)streamReader.Read();
                        //   if ((!currentLine.Contains("//")) || (currentLine.Equals("")))
                        // {
                        //if (!(currentLine.Equals(' ') || currentLine.Equals('\n')))
                        listBox.Items.Add(currentLine);
                        processScheduler.inputList.Add(currentLine);
                        // }
                        //words.Add(currentLine, currentLine.Length);

                    }
                    //mWords.Remove(""); // remove all empty lines
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }
    }
}
