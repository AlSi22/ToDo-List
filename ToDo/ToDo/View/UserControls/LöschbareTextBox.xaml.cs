using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ToDo.Model;

namespace ToDo.View.UserControls
{
    /// <summary>
    /// LöschbareTextBox.xaml.cs
    
    /// Der Platzhaltertext wird angezeigt, solange kein Benutzereingabetext vorhanden ist.
    /// Durch Klick auf das "X"-Symbol wird der Text gelöscht und das Eingabefeld erhält erneut den Fokus.
    /// </summary>
    public partial class LöschbareTextBox : UserControl
    {
        public LöschbareTextBox()
        {
            InitializeComponent();
        }
       
        private string placeholder;

        public string Placeholder
        {
            get { return placeholder; }
            set
            {
                placeholder = value;
                tbPlaceholder.Text = placeholder;
            }
        }


        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtInput.Clear();
            txtInput.Focus();
        }

        private void txtInput_TextChanged(object sender, TextChangedEventArgs e)
        {
           Text = txtInput.Text;
            if (string.IsNullOrEmpty(txtInput.Text))
            {
                tbPlaceholder.Visibility = Visibility.Visible; //wenn kein Text, dann Platzhalter
            }
            else
            {
                tbPlaceholder.Visibility = Visibility.Hidden;
            }
        }

        public static readonly DependencyProperty TextProperty =
                        DependencyProperty.Register(
                        "Text", typeof(string), typeof(LöschbareTextBox),
                        new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, 
                            OnTextChanged));

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (LöschbareTextBox)d;
            control.txtInput.Text = (string)e.NewValue;
        }
    }
}