using BattleKingdom.Models;
using System.Text.RegularExpressions;

 namespace BattleKingdom.Classes
{
    public static class Fonctions
    {
        public enum ValidationInitiales
        {
            Conformes,
            ErreurVides,
            ErreurFormat
        }
        public enum SantePersonnages
        {
            AucunGagnant,
            SuccesHeros,
            SuccesEnnemi
        }

        /// <summary>
        /// Valide les initiales du joueurs, il faut que ce soit 2 lettres majuscules
        /// </summary>
        /// <param name="pInitiales">Initiales saisies</param>
        /// <returns>Enum d'état de validation</returns>
        public static ValidationInitiales ValiderInitiales(string pInitiales)
        {
            Regex rxInitiale = new Regex("^[A-Z]{2}$");

            if (string.IsNullOrEmpty(pInitiales))
            {
                return ValidationInitiales.ErreurVides;
            }
            else if (rxInitiale.IsMatch(pInitiales))
            {
                return ValidationInitiales.Conformes;
            }
            else
            {
                return ValidationInitiales.ErreurFormat;
            }
        }

        /// <summary>
        /// Permet d'instancier les 3 ennemis du jeu (Ziggy, Smasher et Kong) et les rajouter
        /// à la liste des personnage en cours.
        /// </summary>
        /// <param name="pListePersonnages">Liste des personnages</param>
        public static void InitialiserEnnemis(List<Personnage> pListePersonnages)
        {
            
            

            Arme ArZiggy=new Arme("qwerty1",5,5);
            Arme ArSmarcher=new Arme("qwerty2",5,5);
            Arme ArKong=new Arme("qwerty3",5,5);

            Utils.CoordonneesGrille coordonneeGrille;
            coordonneeGrille = Utils.GenererPositionHasardPersonnage(pListePersonnages, TypePersonnage.Ennemi);
            Ennemi Ziggy = new Ennemi("Ziggy", coordonneeGrille.X,coordonneeGrille.Y, 10,12,ArZiggy);
            coordonneeGrille = Utils.GenererPositionHasardPersonnage(pListePersonnages, TypePersonnage.Ennemi);
            Ennemi Smasher = new Ennemi("Smasher", coordonneeGrille.X, coordonneeGrille.Y, 10,12,ArSmarcher);
            coordonneeGrille = Utils.GenererPositionHasardPersonnage(pListePersonnages, TypePersonnage.Ennemi);
            Ennemi Kong = new Ennemi("Kong", coordonneeGrille.X, coordonneeGrille.Y, 10,7,ArKong);

            pListePersonnages.Add(Ziggy);
            pListePersonnages.Add(Smasher);
            pListePersonnages.Add(Kong);
        }

        /// <summary>
        /// Retourne une chaîne de caractères pour bâtir l'infobulle d'une personnage afin de pouvoir
        /// connaître son nom, son nombre de cases de déplacement, le nombre de points de dégât de son arme
        /// et la distance d'attaque de son arme.
        /// </summary>
        /// <param name="pListePersonnages">La liste des personnages du jeu</param>
        /// <param name="pIndexPersonnage">La position d'index du personnage actif dans la liste</param>
        /// <returns>Retourne la chaîne de cartactères à afficher dans l'infobulle</returns>
        public static string FormattageInfoBulle(List<Personnage> pListePersonnages, int pIndexPersonnage)
        {
            Personnage personnageActif = pListePersonnages[pIndexPersonnage] ;

            
            string infoBulle = $"Nom: { personnageActif.Nom }({personnageActif.NbPointsVie}) \n Nombre de Cases de déplacement: { personnageActif.NbCasesDeplacementMax} \n";
            if (personnageActif is Attaquant)
            {
                infoBulle += $"Dégâts de l'arme: {(personnageActif as Attaquant).ArmeAttaquant.NbPointsDegat} \n Distance d'attaque de l'arme:{ (personnageActif as Attaquant).ArmeAttaquant.NbCaseDistanceMax}";
            }
            
            return infoBulle;

        }

        /// <summary>
        /// Évalue la santé des joueurs pour déterminer éventuellement quelle équipe gagne
        /// </summary>
        /// <param name="pListePersonnages">Liste des personnages en cours sur le jeu</param>
        /// <returns>L'état du jeu : y-a-t'il un gagnant et si oui, quelle équipe? (ennemis ou héros)</returns>
        public static SantePersonnages EvaluerSantePersonnages(List<Personnage> pListePersonnages)
        {
            int santeHeros = 0;
            int santeEnnemis = 0;

            foreach (Personnage personnage in pListePersonnages)
            {
                if (pListePersonnages.Contains( personnage as Heros) )
                {
                    santeHeros += personnage.NbPointsVie;
                }
                else if (pListePersonnages.Contains(personnage as Ennemi))
                {
                    santeEnnemis += personnage.NbPointsVie;
                }
            }

            if (santeHeros == 0)
            {
                return SantePersonnages.SuccesEnnemi;
            }
            else if (santeEnnemis == 0)
            {
                return SantePersonnages.SuccesHeros;
            }
            else
            {
                return SantePersonnages.AucunGagnant;
            }
        }
    }
}
