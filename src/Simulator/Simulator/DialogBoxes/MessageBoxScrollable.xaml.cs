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

namespace DotNetForHtml5.EmulatorWithoutJavascript
{
    /// <summary>
    /// Interaction logic for InputBox.xaml
    /// </summary>
    public partial class MessageBoxScrollable : Window
    {
        bool _isClickOK;

        public UIElement AdditionalContentToDisplay { get; set; }

        public MessageBoxScrollable()
        {
            InitializeComponent();

            this.Loaded += MessageBox_Loaded;
            this.Closed += MessageBox_Closed;
        }

        void MessageBox_Loaded(object sender, RoutedEventArgs e)
        {
            _isClickOK = false;

            // Display the additional content, if any:
            if (this.AdditionalContentToDisplay != null
                && VisualTreeHelper.GetParent(this.AdditionalContentToDisplay) == null)
            {
                AdditionalContentContainer.Child = this.AdditionalContentToDisplay;
            }
        }

        void MessageBox_Closed(object sender, EventArgs e)
        {
            if (Callback != null)
                Callback(_isClickOK);
        }

        public string Value
        {
            get { return TextBox1.Text; }
            set { TextBox1.Text = value; }
        }

        public Action<bool> Callback { get; set; }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            _isClickOK = true;
            this.Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }
    }
}
