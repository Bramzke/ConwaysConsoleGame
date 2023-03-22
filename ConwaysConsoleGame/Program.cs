using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ConwaysConsoleGame
{
    class Program
    {
        static void Zeichne(sbyte[,] arri)
        {
            for (int i = 0; i <= 60; i++)
            {
                Console.Write("{0,2:D2}", i);
            }
            Console.WriteLine();
            for (int i = 1; i <= 60; i++)
            {
                Console.Write("{0,2:D2}", i);
                for (int j = 1; j <= 60; j++)
                {
                    if (arri[i, j] >= 1)
                    {
                        if (arri[i, j] > 14)
                        {
                            arri[i, j] = 14;
                        }
                        Console.ForegroundColor = (ConsoleColor)arri[i, j];
                        Console.Write("[]");
                        Console.ForegroundColor = (ConsoleColor)15;
                    }
                    else
                    {
                        Console.Write("  ");
                    }
                }
                Console.WriteLine("{0,2:D2}", i);
            }
            for (int i = 0; i <= 60; i++)
            {
                Console.Write("{0,2:D2}", i);
            }
            Console.WriteLine();
        }

        static sbyte[,] Rechne(sbyte[,] arri)
        {
            sbyte[,] nxt = new sbyte[62, 62];
            sbyte zahl = 0;

            arri[0, 0] = arri[60, 60];
            arri[61, 61] = arri[1, 1];
            arri[61, 0] = arri[1, 60];
            arri[0, 61] = arri[60, 1];
            for (int i = 1; i <= 60; i++)
            {
                arri[61, i] = arri[1, i];
                arri[0, i] = arri[60, i];
                arri[i, 0] = arri[i, 60];
                arri[i, 61] = arri[i, 1];
            }
            for (int a = 1; a <= 60; a++)
            {
                for (int b = 1; b <= 60; b++)
                {

                    for (int i = a - 1; i <= a + 1; i++)
                    {
                        for (int j = b - 1; j <= b + 1; j++)
                        {
                            if (!(i == a && j == b))
                            {
                                if (arri[i, j] >= 1)
                                {
                                    zahl++;
                                }
                            }
                        }
                    }
                    if (arri[a, b] == 0 && zahl == 3)
                    {
                        nxt[a, b] = 1;
                    }
                    else if (arri[a, b] >= 1)
                    {
                        if (zahl < 2)
                        {
                            nxt[a, b] = 0;
                        }
                        else if (zahl <= 3)
                        {
                            nxt[a, b] = (sbyte)(arri[a, b] + 1);
                        }
                        else if (zahl > 3)
                        {
                            nxt[a, b] = 0;
                        }
                    }
                    zahl = 0;
                }
            }
            return nxt;
            //Array.Copy(arri, nxt, (int)Math.Pow(62, 2));
        }

        static sbyte[] Einlesen()
        {
            sbyte[] fourP = { 0, 0, 0, 0 };

            Console.Write("Startpunkt x: ");
            while (!sbyte.TryParse(Console.ReadLine(), out fourP[0]) || fourP[0] < 1 || fourP[0] > 60)
            {
                Console.Write("Bitte Wert zwischen 1 und 60 eingeben: ");
            }
            Console.Write("Startpunkt y: ");
            while (!sbyte.TryParse(Console.ReadLine(), out fourP[1]) || fourP[1] < 1 || fourP[1] > 60)
            {
                Console.Write("Bitte Wert zwischen 1 und 60 eingeben: ");
            }
            Console.Write("Endpunkt x: ");
            while (!sbyte.TryParse(Console.ReadLine(), out fourP[2]) || fourP[2] < 1 || fourP[2] > 60)
            {
                Console.Write("Bitte Wert zwischen 1 und 60 eingeben: ");
            }
            Console.Write("Endpunkt y: ");
            while (!sbyte.TryParse(Console.ReadLine(), out fourP[3]) || fourP[3] < 1 || fourP[3] > 60)
            {
                Console.Write("Bitte Wert zwischen 1 und 60 eingeben: ");
            }

            return fourP;
        }

        static void Quadrupel(ref sbyte[,] arri)
        {
            for (int v = 1, h = 60; v <= 30; v++, h--)
            {
                for (int j = 1; j <= 60; j++)
                {
                    if (arri[h, j] == 0)
                    {
                        arri[h, j] = arri[v, j];
                    }
                    if (arri[v, j] == 0)
                    {
                        arri[v, j] = arri[h, j];
                    }
                    //arri[i, j] = arri[m, j];
                }
            }
            for (int v = 1, h = 60; v <= 30; v++, h--)
            {
                for (int j = 1; j <= 60; j++)
                {
                    if (arri[j, h] == 0)
                    {
                        arri[j, h] = arri[j, v];
                    }
                    if (arri[j, v] == 0)
                    {
                        arri[j, v] = arri[j, h];
                    }
                    //arri[j, i] = arri[j, m]; // wäre transponieren
                }
            }
        }

        static void Line(ref sbyte[,] arri, sbyte x, sbyte y, sbyte x2, sbyte y2)
        {//Bresenham-Algorithmus
            sbyte w = (sbyte)(x2 - x);
            sbyte h = (sbyte)(y2 - y);
            sbyte dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
            if (w < 0) dx1 = -1; else if (w > 0) dx1 = 1;
            if (h < 0) dy1 = -1; else if (h > 0) dy1 = 1;
            if (w < 0) dx2 = -1; else if (w > 0) dx2 = 1;
            sbyte longest = Math.Abs(w);
            sbyte shortest = Math.Abs(h);
            if (!(longest > shortest))
            {
                longest = Math.Abs(h);
                shortest = Math.Abs(w);
                if (h < 0) dy2 = -1; else if (h > 0) dy2 = 1;
                dx2 = 0;
            }
            sbyte numerator = (sbyte)(longest >> 1);
            for (int i = 0; i <= longest; i++)
            {
                arri[x, y] = 1;
                numerator += shortest;
                if (!(numerator < longest))
                {
                    numerator -= longest;
                    x += dx1;
                    y += dy1;
                }
                else
                {
                    x += dx2;
                    y += dy2;
                }
            }
        }


        static void Main(string[] args)
        {
            Console.SetWindowSize(126, 63);
            Console.SetBufferSize(126, 63);
            Console.SetWindowPosition(0, 0);
            sbyte[,] spielF = new sbyte[62, 62];
            sbyte x1, y1, x2, y2, tmp;
            int singlePts;
            string action;
            Console.WriteLine(" xXXXXXXXXXXXXXXXXXXXXXXXXXXXx ");
            Console.WriteLine("XXX  Conway's Game of Life  XXX");
            Console.WriteLine(" xXXXXXXXXXXXXXXXXXXxxXXXXXXXx \n");

            do
            {
                Console.WriteLine("r->draw rectangle | s->draw single points | l->draw line | p->point-symmetrise | go->start simulation");
                action = Console.ReadLine();
                switch (action)
                {
                    default:
                        continue;

                    case "r":
                        sbyte[] newi = Einlesen();
                        x1 = newi[0]; y1 = newi[1]; x2 = newi[2]; y2 = newi[3];

                        if (x1 > x2)
                        {
                            tmp = x1;
                            x1 = x2;
                            x2 = tmp;
                        }
                        if (y1 > y2)
                        {
                            tmp = y1;
                            y1 = y2;
                            y2 = tmp;
                        }
                        for (int i = x1; i <= x2; i++)
                        {
                            for (int j = y1; j <= y2; j++)
                            {
                                if (spielF[i, j] == 0)
                                {
                                    spielF[i, j] = 1;
                                }
                                else if (spielF[i, j] == 1)
                                {
                                    spielF[i, j] = 0;
                                }
                            }
                        }
                        Zeichne(spielF);
                        break;

                    case "s":
                        Console.WriteLine("Wieviele Zellen wollen sie setzen?");
                        do
                        {
                            Console.Write("Bitte Zahl eingeben: ");
                        } while (!int.TryParse(Console.ReadLine(), out singlePts));
                        Console.WriteLine("Koordinateneingabe für lebende Punkte:");

                        for (int i = 0; i < singlePts; i++)
                        {
                            Console.Write("{0}.Punkt x: ", i + 1);
                            while (!sbyte.TryParse(Console.ReadLine(), out x1) || x1 < 1 || x1 > 60)
                            {
                                Console.Write("Bitte Wert zwischen 1 und 60 eingeben: ");
                            }
                            Console.Write("{0}.Punkt y: ", i + 1);
                            while (!sbyte.TryParse(Console.ReadLine(), out y1) || y1 < 1 || y1 > 60)
                            {
                                Console.Write("Bitte Wert zwischen 1 und 60 eingeben: ");
                            }

                            spielF[x1, y1] = 1;
                            Zeichne(spielF);
                        }
                        break;
                    case "p":
                        Quadrupel(ref spielF);
                        Zeichne(spielF);
                        break;

                    case "l":
                        newi = Einlesen();
                        Line(ref spielF, newi[0], newi[1], newi[2], newi[3]);
                        Zeichne(spielF);
                        break;
                }

            } while (action != "go");

            Console.Clear();
            while (true)
            {
                Console.SetCursorPosition(0, 0);
                Zeichne(spielF);
                spielF = Rechne(spielF);
                Thread.Sleep(500);
            }

        }
    }
}
