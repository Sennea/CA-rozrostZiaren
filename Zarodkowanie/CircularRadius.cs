using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Zarodkowanie.Form1;


namespace Zarodkowanie
{
    class CircularRadius
    {
        private Neighbourhood neighbourhood;

        public CircularRadius(Neighbourhood neighbourhood)
        {
            this.neighbourhood = neighbourhood;
        }

        public GravityCell[,] getRadiusNeighbours(int x, int y, float circularRadius)
        {
            int a;
            int b;
            

            for (int i = (x - (int)circularRadius); i <= (x + (int)circularRadius); ++i)
                for (int j = (y - (int)circularRadius); j <= (y + (int)circularRadius); ++j)
                {
                    a = i;
                    b = j;

                    if (!neighbourhood.GetIsPeriodic())
                    {

                        if (i < 0) a = 0;
                        if (i >= neighbourhood.GetNodesPerWidth()) a = neighbourhood.GetNodesPerWidth() - 1;
                        if (j < 0) b = 0;
                        if (j >= neighbourhood.GetNodesPerHeight()) b = neighbourhood.GetNodesPerHeight() - 1;
                    }
                    else
                    {

                        if (i < 0) a = neighbourhood.GetNodesPerWidth() + i - 1;
                        if (i >= neighbourhood.GetNodesPerWidth()) a = i - neighbourhood.GetNodesPerWidth();
                        if (j < 0) b = neighbourhood.GetNodesPerHeight() + j - 1;
                        if (j >= neighbourhood.GetNodesPerHeight()) b = j - neighbourhood.GetNodesPerHeight();
                    }

                    if (neighbourhood.GetIsPeriodic() && (i < 0 || i >= neighbourhood.GetNodesPerWidth()))
                    {
                        if (neighbourhood.GetSeedTab()[a, b].GetValue() == 0 && neighbourhood.GetSeedTabNew()[a, b].GetValue() == 0 && twoPointsDifference(neighbourhood.GetSeedTab()[a, b], neighbourhood.GetSeedTab()[x, y]) < (circularRadius * PIXEL_SIZE + neighbourhood.GetNodesPerWidth() * PIXEL_SIZE))
                            neighbourhood.GetSeedTabNew()[a, b].SetValue(neighbourhood.GetSeedTab()[x, y].GetValue());
                    } else if (neighbourhood.GetIsPeriodic() && (j < 0 || j >= neighbourhood.GetNodesPerHeight()))
                    {
                        if (neighbourhood.GetSeedTab()[a, b].GetValue() == 0 && neighbourhood.GetSeedTabNew()[a, b].GetValue() == 0 && twoPointsDifference(neighbourhood.GetSeedTab()[a, b], neighbourhood.GetSeedTab()[x, y]) < (circularRadius * PIXEL_SIZE + neighbourhood.GetNodesPerHeight() * PIXEL_SIZE))
                            neighbourhood.GetSeedTabNew()[a, b].SetValue(neighbourhood.GetSeedTab()[x, y].GetValue());
                    }
                    else
                    {
                        if (neighbourhood.GetSeedTab()[a, b].GetValue() == 0 && neighbourhood.GetSeedTabNew()[a, b].GetValue() == 0 && twoPointsDifference(neighbourhood.GetSeedTab()[a, b], neighbourhood.GetSeedTab()[x, y]) < (circularRadius * PIXEL_SIZE))
                            neighbourhood.GetSeedTabNew()[a, b].SetValue(neighbourhood.GetSeedTab()[x, y].GetValue());
                    }
                        
                    
                }
            return neighbourhood.GetSeedTab();
        }

        private float twoPointsDifference(GravityCell a, GravityCell b)
        {
            return (float)Math.Sqrt(Math.Pow((a.GetGravityX() - b.GetGravityX()), 2) + Math.Pow((a.GetGravityY() - b.GetGravityY()), 2));
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
