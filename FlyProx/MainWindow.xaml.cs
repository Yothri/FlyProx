using FlyProxCore;
using System;
using System.Windows;
using FlyProxCore.Util.Ext;
using FlyProxCore.Cryptography;

namespace FlyProx
{
    public partial class MainWindow : Window
    {
        private readonly FlyProxContext Context;

        public MainWindow()
        {
            InitializeComponent();
            Context = new FlyProxContext();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //uint tick = 0x1646d942;
            //uint desiredHash = 0x4acb403d;
            //byte[] data = @"14 03 00 00".ToByteArrayFromHexString();
            
            //for(uint crc =0; crc < uint.MaxValue; crc++)
            //{
            //    uint lenHash = ~(Crc32.ComputeChecksum(data, crc) ^ tick);
            //    if (lenHash == desiredHash)
            //        Console.WriteLine(crc.ToString("X8"));
            //}
            //Console.WriteLine("Done");
            //Console.ReadLine();
            

            Context.Initialize();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Context.Uninitialize();
            Context.Dispose();
        }
    }
}
