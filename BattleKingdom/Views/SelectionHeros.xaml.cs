using BattleKingdom.Classes;
using BattleKingdom.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace BattleKingdom.Views
{

    public partial class SelectionHeros : Window
    {
        private string _initiales;
        private int _nbHerosSelectionnes = 0;
        private List<string> _nomHerosSelectionnes = new List<string>();

        public SelectionHeros(string pInitiales)
        {
            InitializeComponent();

            Journalisation.Tracer("Ouverture de la fenêtre SelectionHeros");

            _initiales = pInitiales;

            // On peut mettre cette ligne en commentaire... ;)
            trameSonore.Play();
        }

        private void GestionSelection(object pSender, RoutedEventArgs pEvent)
        {
            ToggleButton bouton = pSender as ToggleButton;

            if ((bool)bouton.IsChecked)
            {
                if (_nbHerosSelectionnes < MoteurJeu.NB_HEROS)
                {
                    _nomHerosSelectionnes.Add(bouton.Name);
                    txtNbHerosSelectionne.Text = ++_nbHerosSelectionnes + "/" + MoteurJeu.NB_HEROS;

                    Journalisation.Tracer($"Ajout du héros {bouton.Name}");
                }
            }

            if (!(bool)bouton.IsChecked)
            {
                _nomHerosSelectionnes.Remove(bouton.Name);
                txtNbHerosSelectionne.Text = --_nbHerosSelectionnes + "/" + MoteurJeu.NB_HEROS;

                Journalisation.Tracer($"Retrait du héros {bouton.Name}");
            }

            if (_nbHerosSelectionnes == MoteurJeu.NB_HEROS)
            {               
                foreach (ToggleButton enfant in grille.Children)
                {
                    if (enfant.IsChecked == false)
                        enfant.IsEnabled = false;
                }
            }
            else
            {
                foreach (ToggleButton enfant in grille.Children)
                    enfant.IsEnabled = true;
            }
        }

        private void Ajout(object pSender, RoutedEventArgs pEvent)
        {
            if (_nbHerosSelectionnes < MoteurJeu.NB_HEROS)
            {
                _nomHerosSelectionnes.Add((pSender as Control).Name);
                txtNbHerosSelectionne.Text = ++_nbHerosSelectionnes + "/" + MoteurJeu.NB_HEROS;

                Journalisation.Tracer($"Ajout du héros {(pSender as Control).Name}");
            }
            else
                (pSender as ToggleButton).IsChecked = false;
        }

        private void Retrait(object pSender, RoutedEventArgs pEvent)
        {
            if ((bool)(pSender as ToggleButton).IsChecked)
            {
                _nomHerosSelectionnes.Remove((pSender as Control).Name);
                txtNbHerosSelectionne.Text = --_nbHerosSelectionnes + "/" + MoteurJeu.NB_HEROS;

                Journalisation.Tracer($"Retrait du héros {(pSender as Control).Name}");
            }
            else
                (pSender as ToggleButton).IsChecked = true;
        }

        private void DemarrerJeu(object pSender, RoutedEventArgs pEvent)
        {
            if (_nbHerosSelectionnes == 3)
            {
                Jeu jeu = new Jeu(_initiales, _nomHerosSelectionnes);

                jeu.Show();
                Close();

                Journalisation.Tracer($"Sélection finale des héros: {string.Join(", ", _nomHerosSelectionnes)}");
                Journalisation.Tracer("Fermeture de la fenêtre SelectionHeros");
            }
        }
    }
}