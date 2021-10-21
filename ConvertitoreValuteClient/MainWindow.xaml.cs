using ServiceReference1;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace ConvertitoreValuteClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        ServiceClient service;//Richiamo service WCF
        ObservableCollection<Valuta> _liste_valute;

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string proprieta)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(proprieta));
        }
        public ObservableCollection<Valuta> lista_Valute {
            get
            {
                return this._liste_valute;
            }
            set
            {
                this._liste_valute = value;
            }
        }

        private Valuta selezione_valuta1;

        public Valuta Valuta_Selezionata
        {
            get { return this.selezione_valuta1; }
            set
            {
                selezione_valuta1 = value;
                OnPropertyChanged("Valuta_Selezionata");
            }
        }
        private Valuta selezione_valuta2;

        public Valuta Valuta_Selezionata2
        {
            get { return this.selezione_valuta2; }
            set
            {
                selezione_valuta2 = value;
                OnPropertyChanged("Valuta_Selezionata2");
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            service = new ServiceClient();
            _liste_valute = new ObservableCollection<Valuta>();
            lista_Valute = new ObservableCollection<Valuta>(service.GetValute());
            this.DataContext = this;
        }

        private void tbImporto_TextChanged(object sender, TextChangedEventArgs e)
        {
            lbRisultato.Content = tbImporto.Text;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Valuta v = Valuta_Selezionata;
            Valuta_Selezionata = Valuta_Selezionata2;
            Valuta_Selezionata2 = v;
            CambiaLabel();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            double importo = Convert.ToDouble(tbImporto.Text);
            lbRisultato.Content = service.converti(importo, Valuta_Selezionata.Sigla,Valuta_Selezionata2.Sigla).ToString();
            CambiaLabel();
        }

        public void CambiaLabel()
        {
            lbTasso.Content = "Tasso di scambio 1 " + Valuta_Selezionata.Nome + " -> " + Valuta_Selezionata2.Valore;
        }
    }
}
