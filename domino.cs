using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ITMO.Modelling.Dominos
{
    internal class Program
    {
        struct towerstruct
        {
            public int dominoesNeed;
            public int floors;
        }
        static towerstruct getTower(int dominoes_count)
        {
            int dominoesNeed = 0;
            int floors = 0;
            for (int i = 3; i <= dominoes_count; dominoesNeed += i, ++floors, dominoes_count -= i, i += 2) ;
            return new towerstruct() { dominoesNeed = dominoesNeed, floors = floors };
        }
        const int dominoes_into_kit = 28;
        const int count_of_domino_kits = 7;
        const int dominoes_count = dominoes_into_kit * count_of_domino_kits;
        static towerstruct tower = getTower(dominoes_count);
        static void Main(string[] args)
        {

            List<int>[] m_groups = new List<int>[13];
            for (int i = 0; i < m_groups.Length; ++i) { m_groups[i] = new List<int>(); }
            // Группируем все наборы по ценности
            for (int kitn = 0; kitn < count_of_domino_kits; ++kitn)
                for (int fn = 0; fn < 7; ++fn)
                    for (int sn = 0; sn <= fn; ++sn)
                        m_groups[fn + sn].Add(fn + sn);
            ////

            // building...
            int[] towerb = new int[tower.dominoesNeed];

            for(int u = 0; u < tower.dominoesNeed; ++u)
            {
                int dominoeIndex = -1;
                bool[] allB = new bool[m_groups.Length];
                for (int x = 0; x < m_groups.Length; ++x)
                {
                    if (m_groups[x].Count == 0) continue;
                    allB.Select(i => i = false);
                    for (int y = 0; y < m_groups.Length; ++y)
                    {
                        if (m_groups[x].Count >= m_groups[y].Count || x == y) allB[y] = true;
                        else break;
                    }
                    if (allB.All(i => i))
                    {
                        dominoeIndex = x;
                        break;
                    }
                }
                m_groups[dominoeIndex].RemoveAt(m_groups[dominoeIndex].Count - 1);
                towerb[u] = dominoeIndex;
                Console.WriteLine(u + " - " + dominoeIndex);
            }
            Console.WriteLine("\nRESULT : " + towerb[towerb.Length - 1]);
            
        }
    }
}
