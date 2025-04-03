using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleKingdom.Models
{
    public class Ennemi : Attaquant
    {

        #region CONSTRUCTEURS
        /// <summary>
        /// permet de creer le constructeur Ennemi
        /// </summary>
        /// <param name="pNom"> le nom de l'ennemi</param>
        /// <param name="pPositionX"> la position de l'ennemi a la position X</param>
        /// <param name="pPostionY"> la position de l ennemi a la position Y</param>
        /// <param name="pNbCasesDeplacementMax"> le nombre de case de deplacement max </param>
        /// <param name="pNbPointsVie"> le nombre de points de vie d'un ennemi</param>
        /// <param name="pArmeAttaquant"> l'arme de l attaquant</param>
        public Ennemi(string pNom, int pPositionX, int pPostionY, int pNbCasesDeplacementMax, int pNbPointsVie, Arme pArmeAttaquant) : base(pNom, pPositionX, pPostionY, pNbCasesDeplacementMax, pNbPointsVie, pArmeAttaquant)
        {
        }

        #endregion

        #region MÉTHODES
        /// <summary>
        /// permet de faire deplacer l'ennemi
        /// </summary>
        /// <param name="pPositionX">la position de l ennemi a X </param>
        /// <param name="pPositionY"> la position de  l ennemi a Y </param>
        public override void SeDeplacer(int pPositionX, int pPositionY)
        {

            base.SeDeplacer(pPositionX, pPositionY);
        }
        /// <summary>
        /// permet d'attaquer une ennemi
        /// </summary>
        /// <param name="pPersonnage">repersente la personne a attaquer </param>
        public override void Attaquer(Personnage pPersonnage)
        {
            base.Attaquer(pPersonnage);
        }
        #endregion


    }
}
