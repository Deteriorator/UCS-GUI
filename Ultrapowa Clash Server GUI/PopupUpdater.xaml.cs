﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Ultrapowa_Clash_Server_GUI
{
    /// <summary>
    /// Logica di interazione per PopupUpdater.xaml
    /// </summary>
    public partial class PopupUpdater : Window
    {
        private bool IsGoingPage = false;

        public PopupUpdater()
        {
            Opacity = 0;
            InitializeComponent();
            RTB_Console.Document.Blocks.Clear();
            RTB_Console.AppendText(Sys.ConfUCS.Changelog);
            Version thisAppVer = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            lbl_CurVer.Content = "Current UCS version: " + thisAppVer.Major +"." + thisAppVer.Minor + "." + thisAppVer.Build + "." + thisAppVer.MinorRevision;
            lbl_NewVer.Content = "New UCS version: " + Sys.ConfUCS.NewVer.Major + "." + Sys.ConfUCS.NewVer.Minor + "." + Sys.ConfUCS.NewVer.Build + "." + Sys.ConfUCS.NewVer.MinorRevision;
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btn_GoPage_Click(object sender, RoutedEventArgs e)
        {
            IsGoingPage = true;
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            OpInW();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            OpOutW(sender, e);
        }

        private void OpInW()
        {
            var OpIn = new DoubleAnimation(1, (Duration)TimeSpan.FromSeconds(0.125));
            this.BeginAnimation(UIElement.OpacityProperty, OpIn);
        }

        private void OpOutW(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Closing -= Window_Closing;
            e.Cancel = true;
            var OpOut = new DoubleAnimation(0, (Duration)TimeSpan.FromSeconds(0.125));
            OpOut.Completed += (s, _) => { this.Close(); MainWindow.IsFocusOk = true; if (IsGoingPage) System.Diagnostics.Process.Start(Sys.ConfUCS.UrlPage);  IsGoingPage = false; };
            this.BeginAnimation(UIElement.OpacityProperty, OpOut);
        }
    }
}