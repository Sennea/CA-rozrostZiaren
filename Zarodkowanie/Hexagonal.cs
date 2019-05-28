﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Zarodkowanie.Form1;


namespace Zarodkowanie
{
    class Hexagonal
    {
        private Neighbourhood neighbourhood;
        public Hexagonal(Neighbourhood neighbourhood)
        {
            this.neighbourhood = neighbourhood;
        }
        public GravityCell[,] GetHexagonalNeighbours(int x, int y, int hexaCase)
        {
            int[] neighbours = new int[neighbourhood.GetGrains().Count];
            for (int i = 0; i < neighbourhood.GetGrains().Count; ++i)
                neighbours[i] = 0;

            int[] closeNeighbours = neighbourhood.GetPeriodicIndex(x, y);
            int left = closeNeighbours[0];
            int right = closeNeighbours[1];
            int up = closeNeighbours[2];
            int down = closeNeighbours[3];


            switch (hexaCase)
            {
                case 0:
                    if (neighbourhood.GetSeedTab()[x, up].GetValue() != 0) neighbours[neighbourhood.GetSeedTab()[x, up].GetValue() - 1]++;
                    if (neighbourhood.GetSeedTab()[right, up].GetValue() != 0) neighbours[neighbourhood.GetSeedTab()[right, up].GetValue() - 1]++;
                    if (neighbourhood.GetSeedTab()[right, y].GetValue() != 0) neighbours[neighbourhood.GetSeedTab()[right, y].GetValue() - 1]++;
                    if (neighbourhood.GetSeedTab()[left, y].GetValue() != 0) neighbours[neighbourhood.GetSeedTab()[left, y].GetValue() - 1]++;
                    if (neighbourhood.GetSeedTab()[left, down].GetValue() != 0) neighbours[neighbourhood.GetSeedTab()[left, down].GetValue() - 1]++;
                    if (neighbourhood.GetSeedTab()[x, down].GetValue() != 0) neighbours[neighbourhood.GetSeedTab()[x, down].GetValue() - 1]++;
                    break;
                case 1:
                    if (neighbourhood.GetSeedTab()[x, up].GetValue() != 0) neighbours[neighbourhood.GetSeedTab()[x, up].GetValue() - 1]++;
                    if (neighbourhood.GetSeedTab()[left, up].GetValue() != 0) neighbours[neighbourhood.GetSeedTab()[left, up].GetValue() - 1]++;
                    if (neighbourhood.GetSeedTab()[left, y].GetValue() != 0) neighbours[neighbourhood.GetSeedTab()[left, y].GetValue() - 1]++;
                    if (neighbourhood.GetSeedTab()[right, y].GetValue() != 0) neighbours[neighbourhood.GetSeedTab()[right, y].GetValue() - 1]++;
                    if (neighbourhood.GetSeedTab()[right, down].GetValue() != 0) neighbours[neighbourhood.GetSeedTab()[right, down].GetValue() - 1]++;
                    if (neighbourhood.GetSeedTab()[x, down].GetValue() != 0) neighbours[neighbourhood.GetSeedTab()[x, down].GetValue() - 1]++;
                    break;
                default:
                    break;
            }
            
            return neighbourhood.GetBiggestNaighbour(neighbours, x, y);
        }

        public List<int> GetNeighbourValues(int x, int y, int hexaCase)
        {
            int[] closeNeighbours = neighbourhood.GetPeriodicIndex(x, y);
            int left = closeNeighbours[0];
            int right = closeNeighbours[1];
            int up = closeNeighbours[2];
            int down = closeNeighbours[3];
            List<int> neighboursVal = new List<int>();

            switch (hexaCase)
            {
                case 0:
                    neighboursVal.Add(neighbourhood.GetSeedTab()[left, y].GetValue());
                        neighboursVal.Add(neighbourhood.GetSeedTab()[right, y].GetValue());
                        neighboursVal.Add(neighbourhood.GetSeedTab()[x, up].GetValue());
                        neighboursVal.Add(neighbourhood.GetSeedTab()[right, up].GetValue());
                        neighboursVal.Add(neighbourhood.GetSeedTab()[x, down].GetValue());
                    neighboursVal.Add(neighbourhood.GetSeedTab()[left, down].GetValue());
                    break;
                   
                case 1:
                    neighboursVal.Add(neighbourhood.GetSeedTab()[left, y].GetValue());
                       neighboursVal.Add(neighbourhood.GetSeedTab()[right, y].GetValue());
                       neighboursVal.Add(neighbourhood.GetSeedTab()[x, up].GetValue());
                      neighboursVal.Add(neighbourhood.GetSeedTab()[left, up].GetValue());
                       neighboursVal.Add(neighbourhood.GetSeedTab()[x, down].GetValue());
                    neighboursVal.Add(neighbourhood.GetSeedTab()[right, down].GetValue());
                    break;
                default:
                    break;
            }
            

            return neighboursVal;
        }

    }
}