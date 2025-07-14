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
    /// Interaktionslogik für Listen.xaml
    /// </summary>
    public partial class Listen : UserControl
    {
        public Listen()
        {
            InitializeComponent();
        }
        

        public static readonly DependencyProperty ElementeProperty =
            DependencyProperty.Register("Elemente", typeof(ObservableCollection<Eintrag>), typeof(Listen));

        public ObservableCollection<Eintrag> Elemente
        {
            get => (ObservableCollection<Eintrag>)GetValue(ElementeProperty);
            set => SetValue(ElementeProperty, value);
        }
    }
}

