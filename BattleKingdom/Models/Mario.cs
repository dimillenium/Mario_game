using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleKingdom.Models
{
    public class Mario : FamilleMario
    {


        #region CONSTRUCTEURS
        /// <summary>
        /// permet de creer le constructeur de Mario
        /// </summary>
        /// <param name="pNom"></param>
        /// <param name="pPositionX"></param>
        /// <param name="pPostionY"></param>
        /// <param name="pNbCasesDeplacementMax"></param>
        /// <param name="pNbPointsVie"></param>
        /// <param name="pArmeAttaquant"></param>
        public Mario(string pNom, int pPositionX, int pPostionY, int pNbCasesDeplacementMax, int pNbPointsVie, Arme pArmeAttaquant) : base(pNom, pPositionX, pPostionY, pNbCasesDeplacementMax, pNbPointsVie, pArmeAttaquant)
        {
            
        }

        #endregion

        #region MÉTHODES
        /// <summary>
        /// permet d activer les competence special de Mario
        /// </summary>
        public override void ActiverCompetenceSpeciale()
        {
            ArmeAttaquant.NbPointsDegat +=(ArmeAttaquant.NbPointsDegat *10)/100;
            CompetenceSpecialeActive = true;
        }
        /// <summary>
        /// permet de desactiver les competences speciales de Mario
        /// </summary>
        public override void DesactiverCompetenceSpeciale()
        {
            if (CompetenceSpecialeActive==true)
            {
                
                ArmeAttaquant.NbPointsDegat -= (ArmeAttaquant.NbPointsDegat * 10) / 100;
                CompetenceSpecialeActive = false;
            }
             
        }

        #endregion

    }
}
