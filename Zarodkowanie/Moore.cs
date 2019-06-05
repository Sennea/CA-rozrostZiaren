using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zarodkowanie
{
    class Moore
    {
        private Neighbourhood neighbourhood;

        public Moore(Neighbourhood neighbourhood)
        {
            this.neighbourhood = neighbourhood;
        }

        public GravityCell[,] GetMooreNeighbours(int x, int y)
        {
            int[] neighbours = new int[neighbourhood.GetGrains().Count];
            for (int i = 0; i < neighbourhood.GetGrains().Count; ++i)
                neighbours[i] = 0;

            int[] closeNeighbours = neighbourhood.GetPeriodicIndex(x, y);
            int left = closeNeighbours[0];
            int right = closeNeighbours[1];
            int up = closeNeighbours[2];
            int down = closeNeighbours[3];


            if (neighbourhood.GetSeedTab()[left, up].GetValue() != 0) neighbours[neighbourhood.GetSeedTab()[left, up].GetValue() - 1]++;
            if (neighbourhood.GetSeedTab()[left, y].GetValue() != 0) neighbours[neighbourhood.GetSeedTab()[left, y].GetValue() - 1]++;
            if (neighbourhood.GetSeedTab()[left, down].GetValue() != 0) neighbours[neighbourhood.GetSeedTab()[left, down].GetValue() - 1]++;

            if (neighbourhood.GetSeedTab()[right, up].GetValue() != 0) neighbours[neighbourhood.GetSeedTab()[right, up].GetValue() - 1]++;
            if (neighbourhood.GetSeedTab()[right, y].GetValue() != 0) neighbours[neighbourhood.GetSeedTab()[right, y].GetValue() - 1]++;
            if (neighbourhood.GetSeedTab()[right, down].GetValue() != 0) neighbours[neighbourhood.GetSeedTab()[right, down].GetValue() - 1]++;

            if (neighbourhood.GetSeedTab()[x, up].GetValue() != 0) neighbours[neighbourhood.GetSeedTab()[x, up].GetValue() - 1]++;

            if (neighbourhood.GetSeedTab()[x, down].GetValue() != 0) neighbours[neighbourhood.GetSeedTab()[x, down].GetValue() - 1]++;

            return neighbourhood.GetBiggestNaighbour(neighbours, x, y);
        }

        public List<GravityCell> GetNeighbours(int x, int y)
        {
            int[] closeNeighbours = neighbourhood.GetPeriodicIndex(x, y);
            int left = closeNeighbours[0];
            int right = closeNeighbours[1];
            int up = closeNeighbours[2];
            int down = closeNeighbours[3];
            List<GravityCell> neighbours = new List<GravityCell>();
            neighbours.Add(neighbourhood.GetSeedTab()[left, y]);
            neighbours.Add(neighbourhood.GetSeedTab()[right, y]);
            neighbours.Add(neighbourhood.GetSeedTab()[x, up]);
            neighbours.Add(neighbourhood.GetSeedTab()[left, up]);
            neighbours.Add(neighbourhood.GetSeedTab()[right, up]);
            neighbours.Add(neighbourhood.GetSeedTab()[x, down]);
            neighbours.Add(neighbourhood.GetSeedTab()[left, down]);
            neighbours.Add(neighbourhood.GetSeedTab()[right, down]);

            return neighbours;
        }
    }
}
