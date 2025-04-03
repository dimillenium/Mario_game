using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleKingdom.Models
{
    public abstract class FamilleMario : Heros
    {

        #region PROPRIÉTÉS
        public bool CompetenceSpecialeActive = false;

        #endregion

        #region CONSTRUCTEURS
        /// <summary>
        /// permet de creer le constructeur de la famille mario
        /// </summary>
        /// <param name="pNom"></param>
        /// <param name="pPositionX"></param>
        /// <param name="pPostionY"></param>
        /// <param name="pNbCasesDeplacementMax"></param>
        /// <param name="pNbPointsVie"></param>
        /// <param name="pArmeAttaquant"></param>
        public FamilleMario(string pNom, int pPositionX, int pPostionY, int pNbCasesDeplacementMax, int pNbPointsVie, Arme pArmeAttaquant) : base(pNom, pPositionX, pPostionY, pNbCasesDeplacementMax, pNbPointsVie, pArmeAttaquant)
        {
        }

        #endregion

        #region MÉTHODES
        /// <summary>
        /// permet d activer les competence des personnages fessant parti de la famille mario
        /// </summary>
        public  abstract void ActiverCompetenceSpeciale();

        /// <summary>
        /// permet de deactiver les competence des personnage fessant parti de la famille marion
        /// </summary>
        public abstract void DesactiverCompetenceSpeciale();
       

        #endregion

    }
}
