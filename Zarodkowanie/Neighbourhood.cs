using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Zarodkowanie.Form1;


namespace Zarodkowanie
{
    class Neighbourhood
    {
        private List<Grain> grains;
        private Boolean isPeriodic;
        private int nodesPerWidth;
        private int nodesPerHeight;
        private GravityCell[,] seedTab;
        private GravityCell[,] seedTabNew;

        public Neighbourhood (List<Grain> grains, Boolean isPeriodic, int nodesPerWidth, int nodesPerHeight, GravityCell[,] seedTab, GravityCell[,] seedTabNew)
        {
            this.grains = grains;
            this.isPeriodic = isPeriodic;
            this.nodesPerWidth = nodesPerWidth;
            this.nodesPerHeight = nodesPerHeight;
            this.seedTab = seedTab;
            this.seedTabNew = seedTabNew;
        }

        public int[] GetPeriodicIndex(int x, int y)
        {
            int left = x - 1;
            int right = x + 1;
            int up = y - 1;
            int down = y + 1;
            if (!isPeriodic)
            {
                if (left < 0) left = 0;
                if (right >= nodesPerWidth) right = nodesPerWidth - 1;
                if (up < 0) up = 0;
                if (down >= nodesPerHeight) down = nodesPerHeight - 1;
            }
            else
            {
                if (left < 0) left = nodesPerWidth - 1;
                if (right >= nodesPerWidth) right = 0;
                if (up < 0) up = nodesPerHeight - 1;
                if (down >= nodesPerHeight) down = 0;
            }
            int[] closeNeighbours = {left, right, up, down};
            return closeNeighbours;
        }

        public GravityCell[,] GetBiggestNaighbour(int[] neighbours, int x, int y)
        {
            int max = 0;
            for (int i = 0; i < grains.Count; ++i)
                if (neighbours[i] > max)
                    max = neighbours[i];

            if (max != 0)
            {
                List<int> mosts = new List<int>();
                for (int i = 0; i < grains.Count; ++i)
                    if (neighbours[i] == max)
                        mosts.Add(i + 1);

                int index = random.Next(mosts.Count);
                seedTabNew[x, y].SetValue(mosts[index]);
            }
            return seedTabNew;
        }

        public List<Grain> GetGrains()
        {
            return grains;
        }
        public GravityCell[,] GetSeedTab()
        {
            return seedTab;
        }

        public Boolean GetIsPeriodic()
        {
            return isPeriodic;
        }

        public GravityCell[,] GetSeedTabNew()
        {
            return seedTabNew;
        }

        public int GetNodesPerWidth()
        {
            return nodesPerWidth;
        }

        public int GetNodesPerHeight()
        {
            return nodesPerHeight;
        }
    }
}
