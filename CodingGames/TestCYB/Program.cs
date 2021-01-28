using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;


namespace TestCYB
{
    public class Conference
    {
        public DateTime Debut { get; set; }

        public DateTime Fin { get; set; }

        public List<Conference> Chevauche = new List<Conference>();

        public int NbChevauchea { get { return Chevauche.Count; } }

        public bool? Correct { get; set; }
        public bool ChevaucheSiTrie(Conference c)
        {
            return Fin >= c.Debut;
        }
        //public bool ChevauchePas(Conference conf)
        //{
        //    return ChevauchePas(Debut, conf) && ChevauchePas(Fin, conf);
        //}

        //private bool ChevauchePas(DateTime date, Conference conf)
        //{
        //    return (date < conf.Debut)|| (date > conf.Fin);
        //}
    }
    class Program
    {

        static void Main(string[] args)
        {
            Tester("cassimple.txt", 2);
            Tester("simplegj.txt", 2);
            Tester("bissextile.txt", 5);
            Tester("Complexe.txt", 4);
            Tester("Complexegj.txt", 4);
            Tester("Complexegj2.txt", 3);
            Tester("Complexegj3.txt", 5);

        }

        private static void Tester(string fichier, int attendu)
        {
            List<Conference> conferences = LireFichier(fichier);

            Algo2(conferences.OrderBy(x => x.Debut).ToList(), attendu);
        }

        private static List<Conference> LireFichier(string fichier)
        {
            List<Conference> conferences = new List<Conference>();
            StreamReader sr = new StreamReader(fichier);
            int nb = Convert.ToInt32(sr.ReadLine());
            for (int i = 0; i < nb; i++)
            {
                string[] ligne = sr.ReadLine().Split(';');
                Conference c = new Conference { Debut = Convert.ToDateTime(ligne[0]) };
                c.Fin = c.Debut.AddDays(Convert.ToInt32(ligne[1]) - 1);
                conferences.Add(c);
            }
            return conferences;
        }

        public static void Algo2(List<Conference> conferences, int attendu)
        {
            for (int i = 0; i < conferences.Count; i++)
            {
                int j = i + 1;
                while (j < conferences.Count && conferences[i].ChevaucheSiTrie(conferences[j]))
                {
                    conferences[i].Chevauche.Add(conferences[j++]);
                }
            }
            for (int i = 0; i < conferences.Count; i++)
            {
                Conference c = conferences[i];
                if (c.Correct.HasValue == false)
                {
                    c.Correct = c.NbChevauchea <= c.Chevauche.Sum(x => x.NbChevauchea);
                    if (c.Correct.Value)
                    {
                        foreach (Conference conf in c.Chevauche)
                        {
                            conf.Correct = false;
                        }
                    }
                }
            }
            int resultat = conferences.Count(x => x.Correct.HasValue && x.Correct.Value);

            Debug.Assert(resultat == attendu, String.Format("{0}!={1}", attendu, resultat));
        }
        //private static int CompteNombreDe1(string nombre)
        //{
        //    string texte = nombre.Replace("0","");
        //    return texte.Length;
        //}
        private static void AlgoBF(List<Conference> conferences, int attendu)
        {
            int resultat = 0, nbConf = conferences.Count;
            int test = (int)Math.Pow(2, nbConf) - 1;

            while (test != 0)
            {
                string binaire = Convert.ToString(test, 2).PadLeft(conferences.Count(), '0');
                //if (Math.Sqrt(test) < resultat)
                //    break;
                //    List<Conference> c=new List<Conference>();
                //    for (int i = 0; i < nbConf; i++)
                //    {
                //        if (binaire.Substring(i,1)=="1")
                //        {
                //            c.Add(conferences[i]);
                //        }
                //    }
                //if (c.Count > resultat)
                //{
                //    if (EstValide(c))
                //    {
                //        resultat = c.Count;
                //        if (resultat == nbConf)
                //            break;
                //    }
                //}
                DateTime dateFin = conferences[0].Fin;
                int resultatEncours = 1;
                for (int i = 1; i < conferences.Count; i++)
                {
                    if (binaire.Substring(i, 1) == "1")
                    {
                        if (dateFin >= conferences[i].Debut)
                        {
                            resultatEncours = -1;
                            break;
                        }
                        resultatEncours++;
                        dateFin = conferences[i].Fin;
                    }
                }
                if (resultatEncours > resultat)
                    resultat = resultatEncours;
                test--;
            }

            Debug.Assert(resultat == attendu, String.Format("{0}!={1}", attendu, resultat));
        }

        //private static bool EstValide(List<Conference> conferences)
        //{
        //   for (int i = 0; i < conferences.Count-1; i++)
        //   {
        //       if (conferences[i].Fin>=conferences[i+1].Debut)
        //           return false;
        //   }
        //    return true;
        //}
    }
}
