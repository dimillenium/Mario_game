using BattleKingdom.Models;
using static BattleKingdom.Models.Personnage;

namespace BattleKingdom.Classes
{
    public static class Utils
    {
        public class CoordonneesGrille
        {
            public int Y { get; set; }
            public int X { get; set; }

            public CoordonneesGrille(int pX, int pY)
            {
                Y = pY;
                X = pX;
            }

            public override bool Equals(object pObj)
            {
                if (pObj == null)
                    return false;
                if (!(pObj is CoordonneesGrille))
                    return false;
                return Y == ((CoordonneesGrille)pObj).Y && X == ((CoordonneesGrille)pObj).X;
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
        }

        public static CoordonneesGrille GenererPositionHasardPersonnage(List<Personnage> pListePersonnages, TypePersonnage pTypePersonnage)
        {
            int X;
            int Y;

            CoordonneesGrille coordonnees;

            do
            {
                X = new Random().Next(MoteurJeu.LARGEUR_GRILLE);

                switch (pTypePersonnage)
                {
                    case TypePersonnage.Heros:
                        Y = MoteurJeu.LARGEUR_GRILLE - 1 - new Random().Next(3);
                        break;
                    case TypePersonnage.Ennemi:
                        Y = new Random().Next(3);
                        break;
                    case TypePersonnage.Allie:
                        Y = MoteurJeu.LARGEUR_GRILLE - 1;
                        break;
                    default:
                        Y = MoteurJeu.LARGEUR_GRILLE - 1;
                        break;
                }

                coordonnees = new CoordonneesGrille(X, Y);
            } while (EstCollisionDetectee(pListePersonnages, coordonnees));

            return coordonnees;
        }

        public static bool EstCollisionDetectee(List<Personnage> pListePersonnages, CoordonneesGrille pCoordonnees)
        {
            foreach (Personnage perso in pListePersonnages)
            {
                if (perso.PositionX.Equals(pCoordonnees.X) && perso.PositionY.Equals(pCoordonnees.Y))
                    return true;
            }

            return false;
        }

        public static int CalculerDistance(CoordonneesGrille pSource, CoordonneesGrille pDestination)
        {
            return Math.Abs(pSource.X - pDestination.X) + Math.Abs(pSource.Y - pDestination.Y);
        }
    }
}