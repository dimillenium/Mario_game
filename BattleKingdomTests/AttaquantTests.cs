using BattleKingdom.Models;
using System;
using System.Diagnostics;
using static BattleKingdom.Models.Personnage;

namespace BattleKingdomTests
{
    public class AttaquantTests
    {
        private Attaquant CreerAttanquant()
        {
            Arme arme = new Arme("armeLuigi", 5, 5);
            return new Attaquant("qwerty", 1, 2, 3, 4, arme);
        }
        
        [Fact]
        public void Nom_Set_Devrait_lancer_Argument_Exception_Quand_Nom_vide_Ou_Null()
        {
            // Arrange
            Attaquant attaquant = CreerAttanquant();
            string pNomInvalide1 = "";
            string pNomInvalide2 = null;

            //Act & Assert
            Assert.Throws<ArgumentException>(()=>attaquant.Nom= pNomInvalide1);
            Assert.Throws<ArgumentException>(()=>attaquant.Nom= pNomInvalide2);
        }
        [Fact]
        public void PositionX_Set_devrait_Lancer_ArgumentOutOfRangeException_Quand_PositionX_Inferieur_0()
        {
            // Arrange
            Attaquant attaquant = CreerAttanquant();
            int pPositionX = 0;
            
            //Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(()=>attaquant.PositionX = Personnage.POSITION_MIN-1);
 
        }
        [Fact]
        public void PositionY_Set_devrait_Lancer_ArgumentOutOfRangeException_Quand_PositionX_Inferieur_0()
        {
            // Arrange
            Attaquant attaquant = CreerAttanquant();
            int pPositionY = 0;
            
            //Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => attaquant.PositionY = Personnage.POSITION_MIN-1);

        }
        [Fact]
        public void PositionX_Set_devrait_Lancer_ArgumentOutOfRangeException_Quand_PositionX_Superieur_19()
        {
            // Arrange
            Attaquant attaquant = CreerAttanquant();

            //Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => attaquant.PositionX = Personnage.POSITION_MAX+1);

        }
        [Fact]
        public void PositionY_Set_devrait_Lancer_ArgumentOutOfRangeException_Quand_PositionX_Superieur_19()
        {

            //Act & Assert
            Attaquant attaquant = CreerAttanquant();
            Assert.Throws<ArgumentOutOfRangeException>(() => attaquant.PositionY = Personnage.POSITION_MAX+1);

        }

        [Fact]
        public void NbPointsVie_Set_Devrait_Lancer_ArgumentOutOfRangeException_Quand_Position_NbPointsVie_Est_Inferieur_0()
        {
            // Arrange
            Attaquant attaquant = CreerAttanquant();

            Attaquant attaquantNonValide = new Attaquant("azerty", 1, 2, 3, -2);

            //Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => attaquant.NbPointsVie = attaquantNonValide.NbPointsVie);

        }
     
        [Fact]
        public void Set_NbDeplacementMax_Devrait_Lancer_NbDeplacemmentInvalide_Quand_NbDeplacement_Est_Hors_Ligne()
        {
            //*** Arrange ***
            Attaquant attaquant = CreerAttanquant();
            //*** Act ***

            int nbDeplacement1 = -1;
            int nbDeplacement2 = 21;

            //*** Assert ***
            Assert.Throws<NbDeplacemmentInvalide>(() => attaquant.NbCasesDeplacementMax = nbDeplacement1);
            Assert.Throws<NbDeplacemmentInvalide>(() => attaquant.NbCasesDeplacementMax = nbDeplacement2);

            
        }


    }
}