using BattleKingdom.Classes;
using BattleKingdom.Models;


using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
//using static System.Net.Mime.MediaTypeNames;

namespace BattleKingdom.Views
{
    public partial class Jeu : Window
    {
        private string _initiales;
        private MoteurJeu _moteurJeu;
        
        public Jeu(string pInitiales, List<string> pNomHerosSelectionnes)
        {
            InitializeComponent();

            Journalisation.Tracer("Ouverture de la fenêtre Jeu");

            _initiales = pInitiales;
            _moteurJeu = new MoteurJeu(pNomHerosSelectionnes);

            CreationGrilleTerrain();
            CreationPanneau();
            CreationPersonnages();

            DemarrerJeu();
        }

        private void CreationGrilleTerrain()
        {
            Journalisation.Tracer("Création de la grille du terrain");

            for (int i = 0; i < MoteurJeu.LARGEUR_GRILLE; i++)
            {
                grilleTerrain.RowDefinitions.Add(new RowDefinition());
                grilleTerrain.ColumnDefinitions.Add(new ColumnDefinition());

                for (int j = 0; j < MoteurJeu.LARGEUR_GRILLE; j++)
                {
                    Button boutonCase = new Button();

                    boutonCase.MouseEnter += new MouseEventHandler(SurCase);
                    boutonCase.MouseLeave += new MouseEventHandler(HorsCase);
                    boutonCase.Click += new RoutedEventHandler(SelectionCase);

                    Grid.SetRow(boutonCase, i);
                    Grid.SetColumn(boutonCase, j);

                    grilleTerrain.Children.Add(boutonCase);
                }
            }
        }

        private void CreationPanneau()
        {
            Journalisation.Tracer("Création de la grille de contrôles");

            for (int i = 0; i < MoteurJeu.NB_HEROS; i++)
            {
                Grid ligneControle = new Grid();
                ligneControle.ColumnDefinitions.Add(new ColumnDefinition());
                ligneControle.ColumnDefinitions.Add(new ColumnDefinition());
                ligneControle.ColumnDefinitions.Add(new ColumnDefinition());
                ligneControle.ColumnDefinitions.Add(new ColumnDefinition());

                Grid.SetRow(ligneControle, i);

                Image heros = new Image
                {
                    Name = _moteurJeu.ListePersonnages[i].Nom + "xPersonnage",
                    Uid = i.ToString(),
                    Source = new BitmapImage(new Uri("/Resources/Images/" + _moteurJeu.ListePersonnages[i].Nom + ".png", UriKind.Relative)),
                    ToolTip = _moteurJeu.ListePersonnages[i].Nom
                };
                RenderOptions.SetBitmapScalingMode(heros, BitmapScalingMode.Fant);
                RegisterName(heros.Name, heros);
                Grid.SetColumn(heros, 0);
                ligneControle.Children.Add(heros);

                Image deplacement = new Image
                {
                    Name = _moteurJeu.ListePersonnages[i].Nom + "x" + MoteurJeu.TypeAction.Deplacement.ToString(),
                    ToolTip = MoteurJeu.TypeAction.Deplacement.ToString(),
                    Uid = i.ToString(),
                    Source = new BitmapImage(new Uri("/Resources/Images/Deplacement.png", UriKind.Relative))
                };
                RenderOptions.SetBitmapScalingMode(deplacement, BitmapScalingMode.Fant);
                RegisterName(deplacement.Name, deplacement);
                deplacement.MouseDown += new MouseButtonEventHandler(SelectionAction);
                Grid.SetColumn(deplacement, 1);
                ligneControle.Children.Add(deplacement);

                Image attaque = new Image
                {
                    Name = _moteurJeu.ListePersonnages[i].Nom + "x" + MoteurJeu.TypeAction.Attaque.ToString(),
                    ToolTip = MoteurJeu.TypeAction.Attaque.ToString(),
                    Uid = i.ToString(),
                    Source = new BitmapImage(new Uri("/Resources/Images/Attaque.png", UriKind.Relative))
                };
                RenderOptions.SetBitmapScalingMode(attaque, BitmapScalingMode.Fant);
                RegisterName(attaque.Name, attaque);
                attaque.MouseDown += new MouseButtonEventHandler(SelectionAction);
                Grid.SetColumn(attaque, 2);
                ligneControle.Children.Add(attaque);

                // Modifier le type (à droite de IS) représentant les personnages ayant une COMPÉTENCE SPÉCIALE
                if (_moteurJeu.ListePersonnages[i] is FamilleMario) 
                {
                    Image competence = new Image
                    {
                        Name = _moteurJeu.ListePersonnages[i].Nom + "x" + MoteurJeu.TypeAction.Competence.ToString(),
                        ToolTip = MoteurJeu.TypeAction.Competence.ToString(),
                        Uid = i.ToString(),
                        Source = new BitmapImage(new Uri("/Resources/Images/Competence.png", UriKind.Relative))
                    };
                    RenderOptions.SetBitmapScalingMode(competence, BitmapScalingMode.Fant);
                    RegisterName(competence.Name, competence);
                    competence.MouseDown += new MouseButtonEventHandler(SelectionAction);
                    Grid.SetColumn(competence, 3);
                    ligneControle.Children.Add(competence);
                }

                grilleControles.Children.Add(ligneControle);
            }
        }

        private void CreationPersonnages()
        {
            Journalisation.Tracer("Création des personnages");
            
            for (int i = 0; i < _moteurJeu.ListePersonnages.Count; i++)
            {
                Image personnage = new Image
                {
                    Name = _moteurJeu.ListePersonnages[i].Nom,
                    Uid = i.ToString(),
                    Source = new BitmapImage(new Uri("/Resources/Images/" + _moteurJeu.ListePersonnages[i].Nom + ".png", UriKind.Relative)),
                    ToolTip = Fonctions.FormattageInfoBulle(_moteurJeu.ListePersonnages, i)
                };

                RenderOptions.SetBitmapScalingMode(personnage, BitmapScalingMode.Fant);

                // Pour permettre la recherche du contrôle avec FindName
                RegisterName(personnage.Name, personnage);

                // Modifier le type (à droite de IS) représentant les personnages ennemis
                if (_moteurJeu.ListePersonnages[i] is Ennemi)
                    personnage.MouseDown += new MouseButtonEventHandler(SelectionEnnemi);

                Grid.SetColumn(personnage, _moteurJeu.ListePersonnages[i].PositionX);
                Grid.SetRow(personnage, _moteurJeu.ListePersonnages[i].PositionY);

                grilleTerrain.Children.Add(personnage);
            }
        }

        private void SelectionAction(object pSender, RoutedEventArgs pEvent)
        {
            _moteurJeu.HerosCourant = _moteurJeu.ListePersonnages[int.Parse((pSender as Image).Uid)];
            _moteurJeu.ActionCourante = (MoteurJeu.TypeAction)Enum.Parse(typeof(MoteurJeu.TypeAction), (pSender as Image).Name.Substring((pSender as Image).Name.IndexOf("x") + 1));

            if (_moteurJeu.ActionCourante == MoteurJeu.TypeAction.Competence && _moteurJeu.EstCompetencePossible())
            {
                Journalisation.Tracer($"{_moteurJeu.HerosCourant.Nom} a utilisé sa compétence", txtTrace);
                ActionCompletee();
            }
            else
                Journalisation.Tracer($"{_initiales} sélectionne l'action {_moteurJeu.ActionCourante} du héros {_moteurJeu.HerosCourant.Nom}", txtTrace);
        }

        private void SelectionEnnemi(object pSender, RoutedEventArgs pEvent)
        {
            _moteurJeu.EnnemiCourant = _moteurJeu.ListePersonnages[int.Parse((pSender as Image).Uid)];

            if (_moteurJeu.ActionCourante == MoteurJeu.TypeAction.Attaque)
            {
                if (_moteurJeu.EstAttaquePossible(_moteurJeu.HerosCourant, _moteurJeu.EnnemiCourant))
                {
                    // Modifier le type (à droite de AS) représentant les personnages pouvant attaquer
                    (_moteurJeu.HerosCourant as Attaquant).Attaquer(_moteurJeu.EnnemiCourant);

                    Journalisation.Tracer($"{_moteurJeu.HerosCourant.Nom} a attaqué l'ennemi {_moteurJeu.EnnemiCourant.Nom}", txtTrace);

                    EvaluerMorts(true);
                    ActionCompletee();
                }
                else
                {

                    Journalisation.Tracer($"{_moteurJeu.EnnemiCourant.Nom} est trop loin de {_moteurJeu.HerosCourant.Nom}, choisir un autre personnage ou action, ou encore passer son tour", txtTrace);

                }

            }
        }

        private void SurCase(object pSender, RoutedEventArgs pEvent)
        {
            if (_moteurJeu.HerosCourant != null && _moteurJeu.ActionCourante == MoteurJeu.TypeAction.Deplacement)
            {
                int positionX = int.Parse((pSender as Button).GetValue(Grid.ColumnProperty).ToString());
                int positionY = int.Parse((pSender as Button).GetValue(Grid.RowProperty).ToString());

                if (_moteurJeu.EstDeplacementPossible(positionX, positionY))
                    (pSender as Button).Background = Brushes.Green;
                else
                    (pSender as Button).Background = Brushes.Red;
            }
        }

        private void HorsCase(object pSender, RoutedEventArgs pEvent)
        {
            (pSender as Button).Background = Brushes.White;
        }

        private void SelectionCase(object pSender, RoutedEventArgs pEvent)
        {
            int positionX = int.Parse((pSender as Button).GetValue(Grid.ColumnProperty).ToString());
            int positionY = int.Parse((pSender as Button).GetValue(Grid.RowProperty).ToString());

            if (_moteurJeu.ActionCourante == MoteurJeu.TypeAction.Deplacement && _moteurJeu.EstDeplacementPossible(positionX, positionY))
            {
                _moteurJeu.HerosCourant.SeDeplacer(positionX, positionY);
                Journalisation.Tracer($"{_moteurJeu.HerosCourant.Nom} s'est déplacé vers la case {positionX},{positionY}", txtTrace);
                ActionCompletee();
            }
        }

        private void DemarrerJeu()
        {
            Journalisation.Tracer("Démarrage du jeu", txtTrace);

            TourHeros();
        }

        private void ActionCompletee()
        {
            _moteurJeu.ActionCompletee();

            if (!EstPartieTerminee())
            {
                // Si on est rendu là, c'est qu'il n'y a toujours pas de gagnant ou de perdant
                for (int i = 0; i < _moteurJeu.ListePersonnages.Count; i++)
                {
                    // Désactivation des contrôles utilisés
                    if (_moteurJeu.HerosCourant != null && _moteurJeu.ListePersonnages[i].Nom == _moteurJeu.HerosCourant.Nom)
                    {
                        Image controleUtilise = FindName(_moteurJeu.HerosCourant.Nom + "x" + _moteurJeu.ActionCourante.ToString()) as Image;
                        controleUtilise.IsEnabled = false;
                        controleUtilise.Opacity = 0.5;
                    }

                    // Rafraîchissement des tooltips et retrait des contrôles des personnages morts
                    if (_moteurJeu.ListePersonnages[i].NbPointsVie > 0)
                    {
                        Grid.SetColumn(FindName(_moteurJeu.ListePersonnages[i].Nom) as Image, _moteurJeu.ListePersonnages[i].PositionX);
                        Grid.SetRow(FindName(_moteurJeu.ListePersonnages[i].Nom) as Image, _moteurJeu.ListePersonnages[i].PositionY);

                        (FindName(_moteurJeu.ListePersonnages[i].Nom) as Image).ToolTip = Fonctions.FormattageInfoBulle(_moteurJeu.ListePersonnages, i);
                    }
                    else
                    {
                        grilleTerrain.Children.Remove(FindName(_moteurJeu.ListePersonnages[i].Nom) as Image);

                        // Lorsqu'un héros est mort, on désactive ses contrôles
                        // Modifier le type (à droite de IS) représentant les personnages héros
                        if (_moteurJeu.ListePersonnages[i] is Heros)
                        {
                            (FindName(_moteurJeu.ListePersonnages[i].Nom + "xPersonnage") as Image).IsEnabled = false;
                            (FindName(_moteurJeu.ListePersonnages[i].Nom + "xPersonnage") as Image).Opacity = 0.5;

                            (FindName(_moteurJeu.ListePersonnages[i].Nom + "x" + MoteurJeu.TypeAction.Attaque.ToString()) as Image).IsEnabled = false;
                            (FindName(_moteurJeu.ListePersonnages[i].Nom + "x" + MoteurJeu.TypeAction.Attaque.ToString()) as Image).Opacity = 0.5;

                            (FindName(_moteurJeu.ListePersonnages[i].Nom + "x" + MoteurJeu.TypeAction.Deplacement.ToString()) as Image).IsEnabled = false;
                            (FindName(_moteurJeu.ListePersonnages[i].Nom + "x" + MoteurJeu.TypeAction.Deplacement.ToString()) as Image).Opacity = 0.5;

                            // Modifier le type (à droite de IS) représentant les personnages ayant une COMPÉTENCE SPÉCIALE
                            if (_moteurJeu.ListePersonnages[i] is FamilleMario) 
                            {
                                (FindName(_moteurJeu.ListePersonnages[i].Nom + "x" + MoteurJeu.TypeAction.Competence.ToString()) as Image).IsEnabled = false;
                                (FindName(_moteurJeu.ListePersonnages[i].Nom + "x" + MoteurJeu.TypeAction.Competence.ToString()) as Image).Opacity = 0.5;
                            }
                        }
                    }
                }

                // Lorsqu'un tour (3 actions) est complété
                if (_moteurJeu.HerosCourant != null)
                {
                    // Tour des héros complété
                    if (_moteurJeu.NbActionRestante == 0)
                    {
                        ReactiverControlesHeros();
                        TourEnnemi();
                    }
                    else
                        Journalisation.Tracer($"\n> Tour restant pour le héros {_initiales}: {_moteurJeu.NbActionRestante}", txtTrace);
                }
                else
                {
                    if (_moteurJeu.NbActionRestante == 0)
                        TourHeros();
                    else
                        Journalisation.Tracer($"\n> Tour restant pour l'ennemi: {_moteurJeu.NbActionRestante}", txtTrace);
                }  
            }
        }

        private void EvaluerMorts(bool estHeros)
        {
            if (estHeros)
            {
                // Modifier le type (à droite de IS) représentant les personnages ennemis
                
                List<Personnage> ennemisMorts = _moteurJeu.ListePersonnages.FindAll(p => p is Ennemi && p.NbPointsVie <= 0);

                if (ennemisMorts.Count > 0)  
                    Journalisation.Tracer($"Liste des ennemis morts: {string.Join(", ", ennemisMorts.Select(p => p.Nom))}", txtTrace);
            }
            else
            {
                // Modifier le type (à droite de IS) représentant les personnages héros
                List<Personnage> herosMorts = _moteurJeu.ListePersonnages.FindAll(p => p is Heros && p.NbPointsVie <= 0);

                if (herosMorts.Count > 0)
                    Journalisation.Tracer($"Liste des héros morts: {string.Join(",", herosMorts.Select(p => p.Nom))}", txtTrace);
            }
        }

        private bool EstPartieTerminee()
        {
            bool estSuccesHeros = false;

            switch (Fonctions.EvaluerSantePersonnages(_moteurJeu.ListePersonnages))
            {
                case Fonctions.SantePersonnages.AucunGagnant:
                    return false;
                case Fonctions.SantePersonnages.SuccesHeros:
                    estSuccesHeros = true;

                    break;
                case Fonctions.SantePersonnages.SuccesEnnemi:
                    estSuccesHeros = false;

                    break;
            }

            Fin fin = new Fin(estSuccesHeros, _initiales);
            fin.Show();
            Close();

            return true;
        }

        private void TourHeros()
        {
            _moteurJeu.NbActionRestante = 3;

            _moteurJeu.HerosCourant = null;
            _moteurJeu.EnnemiCourant = null;
            _moteurJeu.ActionCourante = MoteurJeu.TypeAction.Aucune;

            Journalisation.Tracer($"\n\n_____C'est au tour du héros {_initiales}_____", txtTrace);
            Journalisation.Tracer($"\n> Tour restant pour {_initiales}: {_moteurJeu.NbActionRestante}", txtTrace);
        }

        private void ReactiverControlesHeros()
        {

            // Réactivation des contrôles des héros
            // Modifier le type (à droite de IS) représentant les personnages héros
            foreach (Personnage personnage in _moteurJeu.ListePersonnages.FindAll(p => p is Heros && p.NbPointsVie > 0))
            {
                Image controleAttaque = (Image)FindName(personnage.Nom + "x" + MoteurJeu.TypeAction.Attaque.ToString());
                controleAttaque.IsEnabled = true;
                controleAttaque.Opacity = 1;

                Image controleDeplacement = (Image)FindName(personnage.Nom + "x" + MoteurJeu.TypeAction.Deplacement.ToString());
                controleDeplacement.IsEnabled = true;
                controleDeplacement.Opacity = 1;

                // Modifier le type (à droite de IS) représentant les personnages ayant une COMPÉTENCE SPÉCIALE
                if (personnage is FamilleMario) 
                {
                    Image controleCompetence = (Image)FindName(personnage.Nom + "x" + MoteurJeu.TypeAction.Competence.ToString());
                    controleCompetence.IsEnabled = true;
                    controleCompetence.Opacity = 1;
                }
            }
        }

        private void PasserTourHeros(object pSender, RoutedEventArgs pEvent)
        {
            Journalisation.Tracer($"Le héros {_initiales} passe son tour", txtTrace);

            ReactiverControlesHeros();
            TourEnnemi();
        }

        private void TourEnnemi()
        {
            _moteurJeu.NbActionRestante = 3;

            _moteurJeu.HerosCourant = null;
            _moteurJeu.EnnemiCourant = null;
            _moteurJeu.ActionCourante = MoteurJeu.TypeAction.Aucune;

            Journalisation.Tracer("\n\n_____C'est au tour de l'ennemi_____", txtTrace);
            Journalisation.Tracer($"\n> Tour restant pour l'ennemi: {_moteurJeu.NbActionRestante}", txtTrace);

            // Modifier le type (à droite de IS) représentant les personnages ennemis
            foreach (Personnage ennemi in _moteurJeu.ListePersonnages.FindAll(p => p is Ennemi && p.NbPointsVie > 0))
            {
                Personnage herosPlusProche = _moteurJeu.TrouverHerosPlusProche(ennemi);

                if (herosPlusProche != null)
                {
                    _moteurJeu.DeplacerVersHerosPlusProche(ennemi, herosPlusProche);

                    Journalisation.Tracer($"Déplacement de {ennemi.Nom} vers {herosPlusProche.Nom}", txtTrace);
                    ActionCompletee();

                    // Si le nombre d'action est à 3, c'est qu'on est rendu au Héros
                    if (_moteurJeu.NbActionRestante == 3)
                        return;

                    if (_moteurJeu.EstAttaquePossible(ennemi, herosPlusProche))
                    {
                        // Modifier le type (à droite de AS) représentant les personnages ennemis en tant qu'attaquant (cast)
                        (ennemi as Ennemi).Attaquer(herosPlusProche);

                        Journalisation.Tracer($"{ennemi.Nom} a attaqué {herosPlusProche.Nom}", txtTrace);

                        EvaluerMorts(false);
                        ActionCompletee();
                    }
                    else
                        Journalisation.Tracer($"{ennemi.Nom} ne peut pas attaquer {herosPlusProche.Nom}", txtTrace);
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Trace.Flush();
        }
    }
}
