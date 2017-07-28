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
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.ComponentModel;

namespace TechPoint
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List <User> usersList = new List<User>();
        private Random ran = new Random();
        private int randomNumber = 0;
        private NetworkInitiator newNetwork = new NetworkInitiator();

        private delegate void SetTextCallback(String text);
        private delegate void SetIntCallbCk(int theadnum);
        private BackgroundWorker backgroundWorker1 = new BackgroundWorker();
        private bool alreadyStarted = false;

        private BackgroundWorker[] bkw1 = new BackgroundWorker[100];
        private List<int> AvailableClientNumbers = new List<int>(100);
        private List<int> UsedClientNumbers = new List<int>(100);
        private int clientcount = 0;

        public MainWindow()
        {
            InitializeComponent();
        }


        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            String printtext;
            TcpListener newsocket = new TcpListener(IPAddress.Any, 9090);  //Create TCP Listener on server
            newsocket.Start();
            for (int i = 0; i < 100; i++)
            {
                AvailableClientNumbers.Add(i);
            }
            while (AvailableClientNumbers.Count > 0)
            {
                InsertText("waiting for client");                   //wait for connection
                printtext = "Available Clients = " + AvailableClientNumbers.Count;
                InsertText(printtext);                   //wait for connection
                newNetwork.client = newsocket.AcceptSocket();     //Accept Connection
                clientcount = AvailableClientNumbers.First();
                AvailableClientNumbers.Remove(clientcount);
                newNetwork.ns[clientcount] = new NetworkStream(newNetwork.client);                            //Create Network stream
                newNetwork.sr[clientcount] = new StreamReader(newNetwork.ns[clientcount]);
                newNetwork.sw[clientcount] = new StreamWriter(newNetwork.ns[clientcount]);
                string welcome = "Welcome";
                InsertText("client connected");
                newNetwork.sw[clientcount].WriteLine(welcome);     //Stream Reader and Writer take away some of the overhead of keeping track of Message size.  By Default WriteLine and ReadLine use Line Feed to delimit the messages
                newNetwork.sw[clientcount].Flush();
                bkw1[clientcount] = new BackgroundWorker();
                bkw1[clientcount].DoWork += new DoWorkEventHandler(client_DoWork);
                bkw1[clientcount].RunWorkerAsync(clientcount);
                UsedClientNumbers.Add(clientcount);
            }

        }

        private void client_DoWork(object sender, DoWorkEventArgs e)
        {
            int clientnum = (int)e.Argument;
            bkw1[clientnum].WorkerSupportsCancellation = true;

            User newUser = new User();
            newUser.userID = newNetwork.sr[clientcount].ReadLine();
            usersList.Add(newUser);

            while (true)
            {
                string inputStream;
                try
                {
                    inputStream = newNetwork.sr[clientnum].ReadLine();
                    if (inputStream.Contains("Guess"))
                    {
                        string guess = inputStream.Substring(5, inputStream.Length - 5);
                        int theGuess = Convert.ToInt32(guess);
                        handleGuess(newUser.userID, theGuess);
                        string message = newUser.userID + " guessed " + guess + " || Computer guessed " + randomNumber;
                        SendMessage(message);

                    }
                    else if (inputStream.Equals("GetCoins"))
                    {
                        int coins = getCoins(newUser.userID);
                        string message = "Coins " + coins + " out of " + newUser.attemptsNum + " attempts";
                        SendMessage(message);

                    }
                    else if (inputStream.Contains("start_guess"))
                    {
                        startGuessing(newUser.userID);
                    }


                }
                catch
                {
                    MessageBox.Show("Catch");
                    newNetwork.sr[clientnum].Close();
                    newNetwork.sw[clientnum].Close();
                    newNetwork.ns[clientnum].Close();
                    InsertText("Client " + clientnum + " has disconnected");
                    KillMe(clientnum);
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

        private void KillMe(int threadnum)
        {
            if (this.listBox1.Dispatcher.CheckAccess())
            {
                UsedClientNumbers.Remove(threadnum);
                AvailableClientNumbers.Add(threadnum);
                bkw1[threadnum].CancelAsync();
                bkw1[threadnum].Dispose();
                bkw1[threadnum] = null;
                GC.Collect();

            }
            else
            {
                listBox1.Dispatcher.BeginInvoke(new SetIntCallbCk(KillMe), threadnum);
            }

        }

        private void ConnectButton_Click_1(object sender, RoutedEventArgs e)
        {
            if (!alreadyStarted)
            {
                backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
                backgroundWorker1.RunWorkerAsync("Message to Worker");

            }
            alreadyStarted = true;
        }


        private bool handleGuess(string userID, int guess)
        {
            randomNumber = ran.Next(1, 10);

            foreach (var user in usersList)
            {
                if (user.userID.Equals(userID))
                {
                    user.incrementAttempts();
                    if (guess == randomNumber)
                    {
                        user.incrementCoins();
                        return true;
                    }  
                }
            }

            return false;
        }

        private int getCoins(string userID)
        {
            foreach (var user in usersList)
            {
                if (user.userID.Equals(userID))
                    return user.coinsNum;
            }
            return 0;

        }

        private void startGuessing(string userID)
        {     
            Random rnd1 = new Random();
            for (int i = 0; i < 10; i++)
            {
                int guess = rnd1.Next(1, 10);
                handleGuess(userID, guess);
                string message = userID + " guessed " + guess + " || Computer guessed " + randomNumber;
                SendMessage(message);
            }

        }

        private void SendMessage(string message)
        {
            newNetwork.sw[clientcount].WriteLine(message);
            newNetwork.sw[clientcount].Flush();
        }

        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            Help win = new Help();
            win.Show();
        }

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            About win = new About();
            win.Show();
        }

        
    }
}
