using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication6
{
    class Program
    {
        static Dictionary<char, int> vL = new Dictionary<char, int>();
        static void Main(string[] args)
        {
          
            vL.Add('A',0);
            vL.Add('N', 0);
            vL.Add('L', 0);
            vL.Add('O', 0);
            vL.Add('S', 0);
            vL.Add('M', 0);
            int start = 1; int compte = 0;
            for (int iA =  start; iA <= 20; iA++) 
            {
                vL['A'] = iA;
                for (int iN = start; iN <= 20; iN++)
                {
                
                    vL['N'] = iN;
                    int calcul = Calcul("ANNA") ;
             
                    if (calcul==20)
                    {

                        for (int iL = start; iL <= 11; iL++)
                        {
                            vL['L'] = iL;
                            for (int iO = start; iO <= 11; iO++)
                            {
                             
                                vL['O'] = iO;
                                if (Calcul("LOAN") == 17)
                                {
                                 //   Console.WriteLine("{0} {1} {2} {3}", vL['A'], vL['N'], vL['L'], vL['O']);    
                                    for (int iR = start; iR <= 9; iR++)
                                    {
                                        vL['R'] = iR;
                                       if (Calcul("ROLLO") == 21)
                                        {
                                          
                                           // Console.WriteLine("{0} {1} {2} {3} {4}", vL['A'], vL['N'], vL['L'], vL['O'], vL['R']);
                                            for (int iS = start; iS <= 9; iS++)
                                            {
                                                vL['S'] = iS;
                                                for (int iM = start; iM <= 9; iM++)
                                                {
                                                  
                                                    vL['M'] = iM;
                                                    if (Calcul("SAM") == 18 && Calcul("SALMON")==33)
                                                    {
                                                        for (int iD = start; iD <= 9; iD++)
                                                        {
                                                            vL['D'] = iD;
                                                            for (int iG = start; iG <= 9; iG++)
                                                            {
                                                                vL['G'] = iG;
                                                                for (int iE = start; iE <= 9; iE++)
                                                                {
                                                          
                                                                  
                                                                    vL['E'] = iE;
                                                                    if (Calcul("DAMAGES") == 30)
                                                                    {
                                                                        compte++;
                                                                        Console.WriteLine(Calcul("GARDNER"));
                                                                        break;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            Console.WriteLine(compte);
        }

        private static int Calcul(string p)
        {
            int result = 0;
            foreach (char c in p)
            {
                result += vL[c];
            }
            return result;
        }
    }
}
