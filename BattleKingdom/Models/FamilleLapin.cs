using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleKingdom.Models
{
    public class FamilleLapin : Heros
    {

        #region CONSTRUCTEURS
        /// <summary>
        /// permet de creer le constructeur FamilleLapin
        /// </summary>
        /// <param name="pNom"> le nom des personnage marioLapin</param>
        /// <param name="pPositionX"> la position  du personnage a X</param>
        /// <param name="pPostionY">la position  du personnage a Y </param>
        /// <param name="pNbCasesDeplacementMax">le nombre de case de deplacement du personnage</param>
        /// <param name="pNbPointsVie">le nombre de points de vie de la famille lapin</param>
        /// <param name="pArmeAttaquant">l arme des personnages de la famille lapin</param>
        public FamilleLapin(string pNom, int pPositionX, int pPostionY, int pNbCasesDeplacementMax, int pNbPointsVie, Arme pArmeAttaquant) : base(pNom, pPositionX, pPostionY, pNbCasesDeplacementMax, pNbPointsVie, pArmeAttaquant)
        {
        }

        #endregion

    
    }
}
