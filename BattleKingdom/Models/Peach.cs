using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleKingdom.Models
{
    public class Peach : FamilleMario
    {

        #region CONSTANTES

        #endregion

        #region ATTRIBUTS

        #endregion

        #region PROPRIÉTÉS

        #endregion

        #region CONSTRUCTEURS
        /// <summary>
        /// permet de creer le constructeur de Peach
        /// </summary>
        /// <param name="pNom"></param>
        /// <param name="pPositionX"></param>
        /// <param name="pPostionY"></param>
        /// <param name="pNbCasesDeplacementMax"></param>
        /// <param name="pNbPointsVie"></param>
        /// <param name="pArmeAttaquant"></param>
        public Peach(string pNom, int pPositionX, int pPostionY, int pNbCasesDeplacementMax, int pNbPointsVie, Arme pArmeAttaquant) : base(pNom, pPositionX, pPostionY, pNbCasesDeplacementMax, pNbPointsVie, pArmeAttaquant)
        {
        }

        #endregion

        #region MÉTHODES
        /// <summary>
        /// permet d activer les conpetences speciales de Peach
        /// </summary>
        public override void ActiverCompetenceSpeciale()
        {
           
            NbCasesDeplacementMax += (NbCasesDeplacementMax * 10) / 100;

            ArmeAttaquant.NbPointsDegat += (ArmeAttaquant.NbPointsDegat * 12) / 100;
            CompetenceSpecialeActive = true;

        }
        /// <summary>
        /// permet de desactiver les competences speciales de Peach
        /// </summary>
        public override void DesactiverCompetenceSpeciale()
        {
            
            if (CompetenceSpecialeActive == true)
            {
                NbCasesDeplacementMax -= (NbCasesDeplacementMax * 10) / 100;
                ArmeAttaquant.NbPointsDegat -=(ArmeAttaquant.NbPointsDegat * 12) / 100;
                CompetenceSpecialeActive = false;
            }
        }

        #endregion




    }
}
