using System;
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
using ToDo.Model;
using ToDo.ViewModel.Verena;

namespace ToDo.View.Verena
{
    /// <summary>
    /// Interaktionslogik für VerenaListe.xaml
    /// </summary>
    public partial class VerenaListe : Window
    {
        public VerenaListe(VerenaViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var verenaviewModel = DataContext as VerenaViewModel;
            if (verenaviewModel == null || sender is not Button button) return;

            switch (button.Name)
            {
                case "EntfernVerena":
                    var selected = VerenaListBox.SelectedItem as Eintrag;
                    if (selected != null)
                    {
                        verenaviewModel.Entfernen(selected);
                    }
                    break;

            }
        }
    }
}
