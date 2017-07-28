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
    

    class NetworkInitiator
    {
        public Socket client { get; set; }
        public NetworkStream[] ns { get; set; }
        public StreamReader[] sr { get; set; } 
        public StreamWriter[] sw { get; set; }

        public NetworkInitiator()
        {
            ns = new NetworkStream[100];
            sr = new StreamReader[100];
            sw = new StreamWriter[100];
        }
    }

    
}
