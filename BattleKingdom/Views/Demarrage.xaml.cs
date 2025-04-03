using BattleKingdom.Classes;
using BattleKingdom.Models;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace BattleKingdom.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Demarrage : Window
    {

        public Demarrage()
        {
            InitializeComponent();
            string nomFichier = "FihierTrace.txt";
            Journalisation.InitialiserTrace(nomFichier);
            Trace.WriteLine($"{DateTime.Now}: Initialisation de la fenetre de connexion");
        }


        private void Confirmer(object pSender, RoutedEventArgs pEvent)
        {
            string initiales = txtInitiales.Text.Trim();

            switch (Fonctions.ValiderInitiales(initiales))
            {
                case Fonctions.ValidationInitiales.Conformes:
                    SelectionHeros selectionHeros = new SelectionHeros(initiales);

                    selectionHeros.Show();
                    Close();

                    Journalisation.Tracer($"Fermeture de la fenêtre Init avec initiales sélectionnées: {initiales}");
                    break;
                case Fonctions.ValidationInitiales.ErreurVides:
                    ErreurValidationsInitiales(txtMessage, txtInitiales, "Les initiales doivent être renseignées");

                    break;
                case Fonctions.ValidationInitiales.ErreurFormat:
                    ErreurValidationsInitiales(txtMessage, txtInitiales, "Les initiales doivent être 2 caractères majuscules");

                    break;
            }
        }

        private void ErreurValidationsInitiales(TextBlock pControleAffichageErreur, TextBox pControleEnErreur, string pMessageErreur)
        {
            Journalisation.Tracer(pMessageErreur);

            pControleAffichageErreur.Text = pMessageErreur;
            pControleEnErreur.ToolTip = pMessageErreur;
        }
    }
}