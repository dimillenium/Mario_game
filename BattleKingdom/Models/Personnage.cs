using BattleKingdom.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace BattleKingdom.Models
{
    public abstract class Personnage
    {

        #region CONSTANTES
        public const int POSITION_MIN = 0;
        public const int POSITION_MAX = 19;
        public const int NBDEPLACEMENT_MIN = 0;
        public const int NBDEPLACEMENT_MAX = 20;
        public const int NBPOINTSVIE_MIN = 0;

        #endregion

        #region ATTRIBUTS
        private string _nom;
        private int _positionX;
        private int _positionY;
        private int _nbCasesDeplacementMax;
        private int _nbPointsVie;


        #endregion

        #region PROPRIÉTÉS
        /// <summary>
        /// permet d obtenir le Nom d'un personnage
        /// </summary>
        public string Nom
        {
            get { return _nom; }
            set {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Le nom ne peut pas être nul ou vide.");
                }
                value = value.Trim();

                _nom = value;

                
            }
        }
        /// <summary>
        /// permet d'obtenir la position a X d'un personnage
        /// </summary>
        public int PositionX
        {
            get { return _positionX; }
            set {
                if (value < POSITION_MIN || value > POSITION_MAX)
                {
                    throw new ArgumentOutOfRangeException("value");
                }
                _positionX = value; 
            }
        }
        /// <summary>
        /// permet d'obetnir la position a Y d'un personnage
        /// </summary>
        public int PositionY
        {
            get { return _positionY; }
            set {
                if (value < POSITION_MIN || value > POSITION_MAX)
                {
                    throw new ArgumentOutOfRangeException("value");
                }
                _positionY = value; 
            }
        }
        /// <summary>
        /// permet d'obtenir le nombre de case de deplacement  max d'un personnage
        /// </summary>
        public int NbCasesDeplacementMax
        {
            get { return _nbCasesDeplacementMax; }
            set {
                try
                {
                    ValiderNbCaseDeplacement(value);
                    _nbCasesDeplacementMax = value;
                }
                catch (NbDeplacemmentInvalide ex)
                {
                    Journalisation.Tracer($"Une erreur s'est produite: {ex.Message}");
                }
                    
               
                _nbCasesDeplacementMax = value; 
            }
        }
        /// <summary>
        /// permet d'obtenir le nombre de points de vie d'un personnage
        /// </summary>
        public int NbPointsVie
        {
            get {
                if (_nbPointsVie < NBPOINTSVIE_MIN)
                {
                    throw new ArgumentOutOfRangeException("Le nombre de points de vie doit être supérieur à 0 ");
                }
                return _nbPointsVie; }
            set {
                
                _nbPointsVie = value;
            }
        }


        #endregion

        #region CONSTRUCTEURS
        /// <summary>
        /// permet de creer le constructeur Personnage
        /// </summary>
        /// <param name="pNom"></param>
        /// <param name="pPositionX"></param>
        /// <param name="pPostionY"></param>
        /// <param name="pNbCasesDeplacementMax"></param>
        /// <param name="pNbPointsVie"></param>
        public  Personnage(string pNom,int pPositionX,int pPostionY,int pNbCasesDeplacementMax,int pNbPointsVie) 
        {
            Nom= pNom;
            PositionX= pPositionX;
            PositionY = pPostionY;
            NbCasesDeplacementMax= pNbCasesDeplacementMax;
            
            NbPointsVie= pNbPointsVie;
        }

        #endregion
        /// <summary>
        /// permet de faire deplacer un personnage
        /// </summary>
        /// <param name="pPositionX"></param>
        /// <param name="pPositionY"></param>
        #region MÉTHODES
        public virtual void SeDeplacer(int pPositionX,int pPositionY)
        {
           
                PositionX = pPositionX;
                PositionY = pPositionY;
           
        }


        public static void ValiderNbCaseDeplacement(int pNbCaseDeplacement)
        {
            if (pNbCaseDeplacement <= NBDEPLACEMENT_MIN || pNbCaseDeplacement >= NBDEPLACEMENT_MAX)
            {
                throw new NbDeplacemmentInvalide(pNbCaseDeplacement);
            }
        }


        #endregion
        /// <summary>
        /// permet de creer exception ExceptionDuDeplacementHorsJeu qui valide si le nombre de deplacement est valide
        /// </summary>
        public class NbDeplacemmentInvalide : Exception
        {
            public NbDeplacemmentInvalide() { }
            public NbDeplacemmentInvalide(int pNbCaseDeplacement) : base($"le nombre de case de deplacement {pNbCaseDeplacement} n'est pas valide")
            {

            }

        }

    }
}
