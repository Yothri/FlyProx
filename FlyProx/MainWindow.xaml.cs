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
            uint tick = 0x5208089b;
            byte[] data = @"FF FF FF FF F6 00 00 00 08 00 00 00 32 30 31 30
30 34 31 32 43 4A 39 DA 04 00 00 00 74 65 73 74
20 00 00 00 38 39 64 31 65 64 32 32 61 61 63 35
38 66 35 62 62 65 61 35 33 62 32 66 64 65 38 31
61 39 34 36 01 00 00 00
".ToByteArrayFromHexString();
            uint lenHash = ~(Crc32.ComputeChecksum(data) ^ tick);

            Console.WriteLine(BitConverter.GetBytes(lenHash).ToHexString());


            Context.Initialize();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Context.Uninitialize();
            Context.Dispose();
        }
    }
}
