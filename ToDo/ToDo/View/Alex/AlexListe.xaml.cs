using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Printing;
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
using ToDo.Model;
using ToDo.View;
using ToDo.ViewModel.Alex;
using ToDo.ViewModel.Main;

namespace ToDo.View.Alex
{
    /// <summary>
    /// Interaktionslogik für Window1.xaml
    /// </summary>
    public partial class AlexListe : Window
    {
        
        public AlexListe(AlexViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var alexviewModel = DataContext as AlexViewModel;
            if (alexviewModel == null || sender is not Button button) return;
            
            switch (button.Name)
            {
                case "EntfernAlex":
                    var selected = AlexListBox.SelectedItem as Eintrag;
                    if (selected != null)
                    {
                        alexviewModel.Entfernen(selected);
                    }
                    break;

            }
        }
    }
}
