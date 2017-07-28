using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Net.Sockets;   //include sockets class
using System.Net;  //needed for type IPAddress
using System.IO;
using System.Threading;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private NetworkStream ns;
        private StreamReader sr;
        private StreamWriter sw;
        private delegate void SetTextCallback(String text);
        private BackgroundWorker backgroundWorker1 = new BackgroundWorker();
        private bool alreadyConnected = false;
        private string count;
        private string clientID;

        public MainWindow()
        {
            InitializeComponent();
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
        }

        private void btn_Connect_Click(object sender, RoutedEventArgs e)
        {
            if (!textBox.Text.Equals(""))
            {
                if (!alreadyConnected) 
                {
                    clientID = textBox.Text;
                    TcpClient newcon = new TcpClient();
                    newcon.Connect("127.0.0.1", 9090);  //IPAddress of Server 127.0.0.1
                    initiateNetwork(newcon);
                    sw.WriteLine(textBox.Text);
                    sw.Flush();
                    backgroundWorker1.RunWorkerAsync("Message to Worker");
                }
                else
                    MessageBox.Show("You are already connected as " + clientID);

                alreadyConnected = true;
            }
            else
                MessageBox.Show("Please enter a user ID");

            
        }

        private void initiateNetwork(TcpClient newcon)
        {
            ns = newcon.GetStream();
            sr = new StreamReader(ns);  //Stream Reader and Writer take away some of the overhead of keeping track of Message size.  By Default WriteLine and ReadLine use Line Feed to delimit the messages
            sw = new StreamWriter(ns);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            
            while (true)
            {
                try
                {
                    string inputStream = sr.ReadLine();  //Note Read only reads into a byte array.  Also Note that Read is a "Blocking Function"
                    InsertText(inputStream);
                    
                }
                catch
                {
                    ns.Close();
                    System.Environment.Exit(System.Environment.ExitCode); //close all 
                }

            }

        }

        private void InsertText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.listBox1.Dispatcher.CheckAccess())
            {
                this.listBox1.Items.Insert(0, text);

            }
            else
            {
                listBox1.Dispatcher.BeginInvoke(new SetTextCallback(InsertText), text);
            }
        }

        private void disconnect_button_Click(object sender, RoutedEventArgs e)
        {
            sw.WriteLine("disconnect");
            sw.Flush();
            sr.Close();
            sw.Close();
            ns.Close();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (alreadyConnected)
            {
                guessBox.Text = "disconnect";


                if (!guessBox.Text.Trim().Equals(""))
                {
                    sw.WriteLine("Majd" + count + ">> " + guessBox.Text);
                    sw.Flush();
                    if (guessBox.Text == "disconnect")
                    {
                        sw.Close();
                        sr.Close();
                        ns.Close();
                        System.Environment.Exit(System.Environment.ExitCode); //close all 

                    }
                    guessBox.Text = "";
                }
            }
            
        }

        private void guessBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }

        private void guessBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void GuessButton_Click(object sender, RoutedEventArgs e)
        {
            if (!guessBox.Text.Equals(""))
            {
                int guess = Convert.ToInt32(guessBox.Text);
                if ((guess > 0) && (guess < 11))
                {
                    sw.WriteLine("Guess" + guessBox.Text);
                    sw.Flush();
                }
                else
                    MessageBox.Show("Your guess should be between 1 and 10!!!");
            }
            else
                MessageBox.Show("Your did not set a guess!!!");

        }

        private void CoinsButton_Click(object sender, RoutedEventArgs e)
        {
            sw.WriteLine("GetCoins");
            sw.Flush();
        }

        private void StartGuessButton_Click(object sender, RoutedEventArgs e)
        {
            sw.WriteLine("start_guess");
            sw.Flush();
        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            listBox1.Items.Clear();
        }
    }
}
