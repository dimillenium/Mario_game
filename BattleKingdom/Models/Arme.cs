using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleKingdom.Models
{
    public class Arme
    {

        #region ATTRIBUTS
        private string _nom;
        private int _nbCaseDistanceMax;
        private int _nbPointsDegat;

        #endregion

        #region PROPRIÉTÉS
        /// <summary>
        /// permet d'obtenir le nom de l'arme
        /// </summary>
        public string Nom
        {
            get { return _nom; }
            set { _nom = value; }
        }
        /// <summary>
        /// permet d obtenir le nombre de case distant max d'une arme
        /// </summary>
        public int NbCaseDistanceMax
        {
            get { return _nbCaseDistanceMax; }
            set { _nbCaseDistanceMax = value; }
        }
        /// <summary>
        /// permet d obtenir  le nombre de points de degats d'une arme
        /// </summary>
        public int NbPointsDegat
        {
            get { return _nbPointsDegat; }
            set { _nbPointsDegat = value; }
        }


        #endregion

        #region CONSTRUCTEURS
        /// <summary>
        /// permet de creer le constructeur Arme
        /// </summary>
        /// <param name="pNom">le nom de l'arme</param>
        /// <param name="pNbCaseDistanceMax"> le nombre de case distance de l'arme</param>
        /// <param name="pNbPointsDegat">le nombre de point de degat de l'arme</param>
        public Arme(string pNom,int pNbCaseDistanceMax,int pNbPointsDegat)
        {
            Nom = pNom;
            NbCaseDistanceMax = pNbCaseDistanceMax;
            NbPointsDegat = pNbPointsDegat;
        }

        #endregion


    }
}
