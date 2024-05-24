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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Neq.Design.WPF.Windows
{
    public partial class NeqMessageBoxWindow : Window
    {
        public NeqMessageBoxWindow(string title, string message)
        {
            InitializeComponent();

            buttonClose.Click += (s, e) => Close();
            buttonMinimize.Click += (s, e) => WindowState = WindowState.Minimized;


        }
    }
}
