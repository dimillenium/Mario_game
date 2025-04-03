using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleKingdom.Models
{
    public  class Attaquant : Personnage
    {


        #region ATTRIBUTS
        private Arme _armeAttaquant;

        #endregion

        #region PROPRIÉTÉS
        /// <summary>
        /// permet d obtenir l'arme d'hero 
        /// </summary>
        public Arme ArmeAttaquant
        {
            get { return _armeAttaquant; }
            set { _armeAttaquant = value; }
        }

        #endregion

        #region CONSTRUCTEURS
        /// <summary>
        /// permet de creer le constructeur Attaquant  a parti de son parent
        /// </summary>
        /// <param name="pNom">le nom de l'attaquant</param>
        /// <param name="pPositionX">la position de l attaquant a X</param>
        /// <param name="pPostionY">la position de l'attaquant a la position Y</param>
        /// <param name="pNbCasesDeplacementMax">le nombre de case de deplacement max  d'un attaquant</param>
        /// <param name="pNbPointsVie">le nombre de points de vie d'un attaquant</param>
        public Attaquant(string pNom, int pPositionX, int pPostionY, int pNbCasesDeplacementMax, int pNbPointsVie) : base(pNom, pPositionX, pPostionY, pNbCasesDeplacementMax, pNbPointsVie)
        {
        }
        /// <summary>
        /// permet de creer le contructeur Attaquant avec ses propre caracteristique tel que une arme
        /// </summary>
        /// <param name="pNom">le nom de l'attaquant</param>
        /// <param name="pPositionX">la position de l attaquant a X</param>
        /// <param name="pPostionY">la position de l'attaquant a la position Y</param>
        /// <param name="pNbCasesDeplacementMax">le nombre de case de deplacement max  d'un attaquant</param>
        /// <param name="pNbPointsVie">le nombre de points de vie d'un attaquant</param>
        /// <param name="pArmeAttaquant">l'arme de l'attaquant</param>
        public Attaquant(string pNom, int pPositionX, int pPostionY, int pNbCasesDeplacementMax, int pNbPointsVie,Arme pArmeAttaquant):this(pNom, pPositionX, pPostionY, pNbCasesDeplacementMax, pNbPointsVie)
        {
            ArmeAttaquant=pArmeAttaquant;
        }

        
        #endregion

        #region MÉTHODES
        /// <summary>
        /// permet a un personnage hero de se deplacer en prennant en compte la position initial
        /// </summary>
        /// <param name="pPositionX"></param>
        /// <param name="pPositionY"></param>
        public override void SeDeplacer(int pPositionX, int pPositionY)
        {
            base.SeDeplacer(pPositionX, pPositionY);
        }
        /// <summary>
        /// permet d attaquer un ennemi en diminuant ses points de vie
        /// </summary>
        /// <param name="pPersonnage"></param>
        public virtual void Attaquer(Personnage pPersonnage)
        {

            if (ArmeAttaquant.NbPointsDegat < pPersonnage.NbPointsVie)
            {
                pPersonnage.NbPointsVie -= ArmeAttaquant.NbPointsDegat;
            }
            else
            {
                pPersonnage.NbPointsVie = 0;
            }



        }

     

        #endregion

    }
}
