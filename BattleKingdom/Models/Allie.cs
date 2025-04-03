using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleKingdom.Models
{
    public class Allie : Personnage
    {

    
        #region CONSTRUCTEURS
        /// <summary>
        /// permet de creer le constructeur Allie
        /// </summary>
        /// <param name="pNom">le nom de l allie</param>
        /// <param name="pPositionX"> la position de l'allie a pa position x </param>
        /// <param name="pPostionY">la position de l'allie a pa position x</param>
        /// <param name="pNbCasesDeplacementMax">le nombre de case de deplacement max d'un allie</param>
        /// <param name="pNbPointsVie">le nombre de vie d'un allie</param>
        public Allie(string pNom, int pPositionX, int pPostionY, int pNbCasesDeplacementMax, int pNbPointsVie) : base(pNom, pPositionX, pPostionY, pNbCasesDeplacementMax, pNbPointsVie)
        {
        }

       

        #endregion

 
    }
}
