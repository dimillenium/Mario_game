using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Interop;

namespace BattleKingdom.Models;

 class Journalisation
{
    /// <summary>
    /// Initialise le fichier de journalisation
    /// </summary>
    /// <param name="pNomFichier">Nom du fichier</param>
    public static void InitialiserTrace(string pNomFichier)
    {
        Stream leFichier=File.Create(pNomFichier);
        TextWriterTraceListener leListener=new TextWriterTraceListener(leFichier);
        Trace.Listeners.Add(leListener);
    }

    /// <summary>
    /// Écrit dans le journal de trace
    /// </summary>
    /// <param name="pTexteATracer">Ligne de texte à tracer</param>
    public static void Tracer(string pTexteATracer)
    {
        Trace.WriteLine($"{DateTime.Now} : {pTexteATracer} ");
        
    }

    /// <summary>
    /// Fait la trace dans le journal ainsi que dans un contrôle de formulaire
    /// </summary>
    /// <param name="pLigne">Commentaire à tracer</param>
    /// <param name="pControle">Contrôle du WPF servant de trace</param>
    public static void Tracer( string pCommentaireATracer, TextBox pControle)
    {
        Trace.WriteLine($"{DateTime.Now} : {pCommentaireATracer} ");

        pControle.Text += $"{pCommentaireATracer} \n ";
        pControle.ScrollToEnd();

    }
}
