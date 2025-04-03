using BattleKingdom.Models;

namespace BattleKingdom.Classes
{
    public class MoteurJeu
    {
        public enum TypeAction
        {
            Aucune,
            Attaque,
            Deplacement,
            Competence
        }

        public const int NB_HEROS = 3;
        public const int NB_ENNEMIS = 3;
        public const int LARGEUR_GRILLE = 20;

        public List<Personnage> ListePersonnages { get; set; }
        public Personnage HerosCourant { get; set; }
        public Personnage EnnemiCourant { get; set; }
        public TypeAction ActionCourante { get; set; }

        public int NbActionRestante { get; set; }

        public MoteurJeu(List<string> pNomHerosSelectionnes)
        {
            ListePersonnages = new List<Personnage>();

            Utils.CoordonneesGrille positionPersonnage;

            Arme arme01 = new Arme("Gun01", 10, 10);
            Arme arme02 = new Arme("Gun02", 10, 10);
            Arme arme03 = new Arme("Gun03", 10, 10);

            positionPersonnage = Utils.GenererPositionHasardPersonnage(ListePersonnages, TypePersonnage.Heros);
            ListePersonnages.Add(Activator.CreateInstance(DefinirClasseHeros(pNomHerosSelectionnes[0]), pNomHerosSelectionnes[0], positionPersonnage.X, positionPersonnage.Y, 10, 15, arme01) as Personnage);

            positionPersonnage = Utils.GenererPositionHasardPersonnage(ListePersonnages, TypePersonnage.Heros);
            ListePersonnages.Add(Activator.CreateInstance(DefinirClasseHeros(pNomHerosSelectionnes[1]), pNomHerosSelectionnes[1], positionPersonnage.X, positionPersonnage.Y, 10, 15, arme02) as Personnage);

            positionPersonnage = Utils.GenererPositionHasardPersonnage(ListePersonnages, TypePersonnage.Heros);
            ListePersonnages.Add(Activator.CreateInstance(DefinirClasseHeros(pNomHerosSelectionnes[2]), pNomHerosSelectionnes[2], positionPersonnage.X, positionPersonnage.Y, 10, 15, arme03) as Personnage);

            Fonctions.InitialiserEnnemis(ListePersonnages);

            ActionCourante = TypeAction.Aucune;
            NbActionRestante = 3;
        }

        private Type DefinirClasseHeros(string pHeros)
        {
            List<string> familleMario = new List<string> { "Mario", "Luigi", "Yoshi", "Peach" };

            if (familleMario.Contains(pHeros))
                return Type.GetType($"BattleKingdom.Models.{pHeros}, BattleKingdom");
            else
                // Choisir le type ici (TYPEaDEFINIR)

                return Type.GetType($"BattleKingdom.Models.FamilleLapin, BattleKingdom");
        }

        public bool EstAttaquePossible(Personnage pAttaquant, Personnage pAttaque)
        {
            if (pAttaquant == null || pAttaque == null)
                return false;

            int distance = Utils.CalculerDistance(new Utils.CoordonneesGrille(pAttaquant.PositionX, pAttaquant.PositionY), new Utils.CoordonneesGrille(pAttaque.PositionX, pAttaque.PositionY));

            // Modifier le type (à droite de IS) représentant les personnages qui peuvent attaquer
            if (distance <= (pAttaquant as Attaquant).ArmeAttaquant.NbCaseDistanceMax)
                return true;
            else
                return false;
        }

        public bool EstDeplacementPossible(int pPositionX, int pPositionY)
        {
            if (HerosCourant == null)
                return false;

            int distance = Utils.CalculerDistance(new Utils.CoordonneesGrille(HerosCourant.PositionX, HerosCourant.PositionY), new Utils.CoordonneesGrille(pPositionX, pPositionY));

            if (distance <= HerosCourant.NbCasesDeplacementMax)
                return true;
            else
                return false;
        }

        public bool EstCompetencePossible()
        {
            if (HerosCourant == null)
                return false;

            // Modifier le type (à droite de IS) représentant les personnages ayant une compétence spéciale
            (HerosCourant as FamilleMario).ActiverCompetenceSpeciale();

            return true;
        }

        public void ActionCompletee()
        {
            NbActionRestante--;

            // Tour terminé
            if (NbActionRestante == 0)
            {
                //Modifier le type (à droite de IS) représentant les personnages ayant une COMPÉTENCE SPÉCIALE
                foreach (Personnage personnage in ListePersonnages.FindAll(p => p is FamilleMario && p.NbPointsVie > 0))
                {
                    //Modifier le type (à droite de AS) représentant les personnages ayant une COMPÉTENCE SPÉCIALE
                    (personnage as FamilleMario).DesactiverCompetenceSpeciale();
                }
            }
        }

        public Personnage TrouverHerosPlusProche(Personnage pEnnemi)
        {
            Personnage herosPlusProche = null;
            int distancePlusProche = LARGEUR_GRILLE;

            // Modifier le type (à droite de IS) représentant les personnages héros
            foreach (Personnage heros in ListePersonnages.FindAll(p => p is Heros && p.NbPointsVie > 0))
            {
                int distance = Utils.CalculerDistance(new Utils.CoordonneesGrille(pEnnemi.PositionX, pEnnemi.PositionY), new Utils.CoordonneesGrille(heros.PositionX, heros.PositionY));

                if (distance < distancePlusProche || herosPlusProche is null)
                {
                    herosPlusProche = heros;
                    distancePlusProche = distance;
                }
            }

            return herosPlusProche;
        }

        public void DeplacerVersHerosPlusProche(Personnage pEnnemi, Personnage pHerosPlusProche )
        {
            int distance = Utils.CalculerDistance(new Utils.CoordonneesGrille(pEnnemi.PositionX, pEnnemi.PositionY), new Utils.CoordonneesGrille(pHerosPlusProche.PositionX, pHerosPlusProche.PositionY));
            int distanceRestante;

            int nouvellePositionX = pEnnemi.PositionX;
            int nouvellePositionY = pEnnemi.PositionY;

            // Si la distance entre le héros et l'ennemi est moins grande que ce que l'ennemi peut faire au maximum
            if (distance >= pEnnemi.NbCasesDeplacementMax)
                distanceRestante = pEnnemi.NbCasesDeplacementMax;
            else
                distanceRestante = distance - 1;

            while (distanceRestante > 0)
            {
                if (distanceRestante > 0 && nouvellePositionX < pHerosPlusProche.PositionX)
                {
                    nouvellePositionX++;
                    distanceRestante--;
                }
                else if (distanceRestante > 0 && nouvellePositionX > pHerosPlusProche.PositionX)
                {
                    nouvellePositionX--;
                    distanceRestante--;
                }

                if (distanceRestante > 0 && nouvellePositionY < pHerosPlusProche.PositionY)
                {
                    nouvellePositionY++;
                    distanceRestante--;
                }
                else if (distanceRestante > 0 && nouvellePositionY > pHerosPlusProche.PositionY)
                {
                    nouvellePositionY--;
                    distanceRestante--;
                }

                if (!Utils.EstCollisionDetectee(ListePersonnages, new Utils.CoordonneesGrille(nouvellePositionX, nouvellePositionY)))
                    pEnnemi.SeDeplacer(nouvellePositionX, nouvellePositionY);
                else
                    break;
            }
        }
    }
}