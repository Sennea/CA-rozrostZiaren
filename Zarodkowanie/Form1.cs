using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zarodkowanie
{
    public partial class Form1 : Form
    {

        private Graphics g;
        private Pen pen;
        private SolidBrush brush;
        private SolidBrush brush2;
        private int[,] seedTab;
        private int[,] seedTabTriangle;
        private int[,] seedTabNew;
        private int nodesPerWidth;
        private int nodesPerHeight;
        private Random random = new Random();
        private List<Grain> grains;
        private List<int[,]> steps;
        private int empty;
        private Boolean isPeriodic;
        private Boolean isPrepared;
        private Boolean isNeuman;
        private Boolean wantGrid;
        private Boolean isAvillableToStart;
        private int currentStep;


        public int PIXEL_SIZE;
        public int PIXEL_SIZE_TMP;
        public readonly int DEFAULT_SIZE = 9;

        public Form1()
        {
            PIXEL_SIZE = DEFAULT_SIZE;
            InitializeComponent();
            pen = new Pen(Color.Black);
            g = pictureBox1.CreateGraphics();
            brush = new SolidBrush(Color.Black);
            brush2 = new SolidBrush(Color.White);
            grains = new List<Grain>();
            steps = new List<int[,]>();
            isPeriodic = true;
            isPrepared = false;
            isAvillableToStart = false;
            isNeuman = true;
            wantGrid = false;
            currentStep = 0;
            dissableButtonsAtStart();


            nodesPerWidth = pictureBox1.Width / PIXEL_SIZE;
            nodesPerHeight = pictureBox1.Height / PIXEL_SIZE;

            textBox1.Text = nodesPerWidth.ToString();
            textBox2.Text = nodesPerHeight.ToString();

            nodesPerWidth = int.Parse(textBox1.Text);
            nodesPerHeight = int.Parse(textBox2.Text);


            seedTabTriangle = new int[nodesPerWidth * 2, nodesPerHeight * 2];
            for (int i = 0; i < nodesPerWidth * 2; ++i)
                for (int j = 0; j < nodesPerHeight * 2; ++j)
                    seedTabTriangle[i, j] = 0;
        }



        class Grain
        {
            private int index;
            private SolidBrush brush;
            private int x;
            private int y;

            public Grain(int index, int[] color, int x, int y)
            {
                this.index = index;
                brush = new SolidBrush(Color.FromArgb(color[0], color[1], color[2]));
                this.x = x;
                this.y = y;
            }

            public SolidBrush getBrush()
            {
                return brush;
            }

            public int getIndex()
            {
                return index;
            }

            public int getX()
            {
                return x;
            }

            public int getY()
            {
                return y;
            }
        }






        private void vonNeuman(int x, int y)
        {
            int[] neighbours = new int[grains.Count];
            for (int i = 0; i < grains.Count; ++i)
                neighbours[i] = 0;
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


            if (seedTab[left, y] != 0) neighbours[seedTab[left, y] - 1]++;
            if (seedTab[right, y] != 0) neighbours[seedTab[right, y] - 1]++;
            if (seedTab[x, up] != 0) neighbours[seedTab[x, up] - 1]++;
            if (seedTab[x, down] != 0) neighbours[seedTab[x, down] - 1]++;

            int max = 0;
            for (int i = 0; i < grains.Count; ++i)
                if (neighbours[i] > max)
                    max = neighbours[i];

            if (max != 0)
            {
                List<int> mosts = new List<int>();
                for (int i = 0; i < grains.Count; ++i)
                    if (neighbours[i] == max)
                        mosts.Add(i +1);

                int index = random.Next(mosts.Count);
                seedTabNew[x, y] = mosts[index];
            }
        }


        private void moore(int x, int y)
        {
            int[] neighbours = new int[grains.Count];
            for (int i = 0; i < grains.Count; ++i)
                neighbours[i] = 0;
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


            if (seedTab[left, up] != 0) neighbours[seedTab[left, up] - 1]++;
            if (seedTab[left, y] != 0) neighbours[seedTab[left, y] - 1]++;
            if (seedTab[left, down] != 0) neighbours[seedTab[left, down] - 1]++;

            if (seedTab[right, up] != 0) neighbours[seedTab[right, up] - 1]++;
            if (seedTab[right, y] != 0) neighbours[seedTab[right, y] - 1]++;
            if (seedTab[right, down] != 0) neighbours[seedTab[right, down] - 1]++;

            if (seedTab[x, up] != 0) neighbours[seedTab[x, up] - 1]++;

            if (seedTab[x, down] != 0) neighbours[seedTab[x, down] - 1]++;

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
                seedTabNew[x, y] = mosts[index];
            }
        }


        private void hexagonal(int x, int y)
        {
            int[] neighbours = new int[grains.Count];
            for (int i = 0; i < grains.Count; ++i)
                neighbours[i] = 0;
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


            if (seedTab[left, y] != 0) neighbours[seedTab[left, y] - 1]++;
            if (seedTab[right, y] != 0) neighbours[seedTab[right, y] - 1]++;
            if (seedTab[x, up] != 0) neighbours[seedTab[x, up] - 1]++;
            if (seedTab[x, down] != 0) neighbours[seedTab[x, down] - 1]++;

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
                seedTabNew[x, y] = mosts[index];
            }
        }




        //------------------------------- BUTTONS --------------------------


        private void startButton_Click(object sender, EventArgs e)
        {
            
            prepare();
            if(isAvillableToStart)
            {
                do
                {
                    disableButtons();
                    countNextStep();
                    checkIfEmpty();
                    //System.Threading.Thread.Sleep(50);
                } while (empty == 0);
                enableButtons();
            }
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            if (currentStep >= steps.Count)
            {
                nodesPerWidth = int.Parse(textBox1.Text);
                nodesPerHeight = int.Parse(textBox2.Text);
                int[,] seedTabTmp = new int[nodesPerWidth, nodesPerHeight];
                for (int i = 0; i < nodesPerWidth; ++i)
                    for (int j = 0; j < nodesPerHeight; ++j)
                        seedTabTmp[i, j] = seedTab[i, j];
                steps.Add(seedTabTmp);
                countNextStep();
            }
            
            drawStep(currentStep);
            currentStep++;
            checkIfEmpty();
            if (empty == 1) nextButton.Enabled = false;
            else nextButton.Enabled = true;
            previousButton.Enabled = true;
        }

        private void previousButton_Click(object sender, EventArgs e)
        {
            isAvillableToStart = true;
            nextButton.Enabled = true;
            empty = 0;
            if (currentStep > 0)
            {
                currentStep--;
                drawWholeWhite(steps[currentStep]);
                drawStep(currentStep);
                if (currentStep == 0) previousButton.Enabled = false;
            }

        }

        private void randomButton_Click(object sender, EventArgs e)
        {
            isAvillableToStart = true;
            prepare();
            int amount = Convert.ToInt32(randomNumericUpDown.Value);
            int breakPoint = 0;
            for (int i = 0; i < amount; ++i)
            {
                breakPoint = 0;
                int x, y;
                do
                {
                    x = random.Next(0, nodesPerWidth);
                    y = random.Next(0, nodesPerHeight);
                    breakPoint++;
                    if (breakPoint > 2000)
                        break;
                } while (seedTab[x, y] != 0);

                if (breakPoint < 2000)
                    createNewGrain(x, y);
            }

        }

        private void collumnsRowButton_Click(object sender, EventArgs e)
        {
            isAvillableToStart = true;
            prepare();
            int numberInColumn = Convert.ToInt32(ColumnNumericUpDown.Value);
            int numberInRow = Convert.ToInt32(RowNumericUpDown.Value);

            int maxInWidth = nodesPerWidth % 2 != 0 ? nodesPerWidth-- : nodesPerWidth;
            int mexInHeight = nodesPerHeight % 2 != 0 ? nodesPerHeight-- : nodesPerHeight;

            int colDelay = (maxInWidth) / (numberInColumn);
            int rowDelay = mexInHeight / (numberInRow);

            for (int i = 0; i < numberInColumn; ++i)
                for (int j = 0; j < numberInRow; ++j)
                {
                    createNewGrain(i * colDelay + colDelay / 2, j * rowDelay + rowDelay / 2);
                }

        }


        private void radiusAmountButton_Click(object sender, EventArgs e)
        {
            isAvillableToStart = true;
            prepare();
            int radius = Convert.ToInt32(RadiusNumericUpDown.Value);
            int amount = Convert.ToInt32(AmountNumericUpDown.Value);
            bool added = false;
            radius++;

            for (int k = 0; k < amount; ++k)
            {
                for (int i = radius - 1; i < nodesPerWidth - radius; ++i)
                {
                    for (int j = radius - 1; j < nodesPerHeight - radius; ++j)
                    {
                        if (seedTab[i, j] == 0 && checkInRadius(i, j, radius))
                        {
                            createNewGrain(i, j);
                            added = true;
                            break;
                        }
                    }
                    if (added) break;
                }
                added = false;
            }

        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            isAvillableToStart = true;
            int x = e.X / PIXEL_SIZE;
            int y = e.Y / PIXEL_SIZE;

            prepare();
            createNewGrain(x, y);
        }

        private void gridButton_Click(object sender, EventArgs e)
        {
            prepare();
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int size = pictureBox1.Width / PIXEL_SIZE;
            if (!int.TryParse(textBox1.Text, out size) || textBox1.Text == "" || int.Parse(textBox1.Text) < 1 || int.Parse(textBox1.Text) > pictureBox1.Width / PIXEL_SIZE)
                textBox1.Text = (pictureBox1.Width / PIXEL_SIZE).ToString();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            int size = pictureBox1.Height / PIXEL_SIZE;
            if (!int.TryParse(textBox2.Text, out size)  || textBox2.Text == "" || int.Parse(textBox2.Text) < 1 || int.Parse(textBox2.Text) > pictureBox1.Height / PIXEL_SIZE)
                textBox2.Text = (pictureBox1.Height / PIXEL_SIZE).ToString();
        }


        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            PIXEL_SIZE = trackBar1.Value;
            textBox2.Text = (pictureBox1.Height / PIXEL_SIZE).ToString();
            textBox1.Text = (pictureBox1.Width / PIXEL_SIZE).ToString();
        }


        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            g = pictureBox1.CreateGraphics();
        }

       








        //------------------------------- METHODS --------------------------


        private void prepare()
        {
            if (!isPrepared)
            {
                enableButtonsAfterStart();
                isPrepared = true;
                drawGrid();
                seedEmptyTab();
            }
        }

        private void drawWholeWhite(int[,] tab)
        {
            for (int i = 0; i < nodesPerWidth; ++i)
                for (int j = 0; j < nodesPerHeight; ++j)
                {
                    g.FillRectangle(brush2, (i * PIXEL_SIZE + 1), (j * PIXEL_SIZE + 1), PIXEL_SIZE - 1, PIXEL_SIZE - 1);
                }
        }

        private void drawStep(int index)
        {
            for (int i = 0; i < nodesPerWidth; ++i)
                for (int j = 0; j < nodesPerHeight; ++j)
                {
                    drawCurrentTab(steps[index], i, j);
                }
        }

        private void countNextStep()
        {
            for (int i = 0; i < nodesPerWidth; ++i)
                for (int j = 0; j < nodesPerHeight; ++j)
                {
                    if (isNeuman)
                        vonNeuman(i, j);
                    else
                        moore(i, j);

                }

            for (int i = 0; i < nodesPerWidth; ++i)
                for (int j = 0; j < nodesPerHeight; ++j)
                    if (seedTab[i, j] == 0 && seedTabNew[i, j] != 0)
                    {
                        seedTab[i, j] = seedTabNew[i, j];
                        drawCurrent(i, j);
                    }
        }

        private bool checkInRadius(int i, int j, int radius)
        {
            int count = 0;
            for (int r = 0; r < radius; ++r)
                for (int p = 0; p < radius; ++p)
                    if (p + r <= radius)
                    {
                        if (seedTab[i - p, j - r] != 0) count++;
                        if (seedTab[i - p, j + r] != 0) count++;
                        if (seedTab[i + p, j + r] != 0) count++;
                        if (seedTab[i + p, j - r] != 0) count++;
                        if (count != 0)
                            return false;
                    }
            return true;
        }

        private void createNewGrain(int x, int y)
        {
            if(seedTab[x,y] == 0)
            {
                int r = random.Next(10, 245);
                int g = random.Next(10, 245);
                int b = random.Next(10, 245);
                int[] color = { r, g, b };

                Grain grain = new Grain(grains.Count + 1, color, x, y);

                grains.Add(grain);
                seedTab[x, y] = grain.getIndex();
                drawCurrent(x, y);
            }
        }

        private void seedEmptyTab()
        {
            nodesPerWidth = int.Parse(textBox1.Text);
            nodesPerHeight = int.Parse(textBox2.Text);
            seedTab = new int[nodesPerWidth, nodesPerHeight];
            seedTabNew = new int[nodesPerWidth, nodesPerHeight];
            for (int i = 0; i < nodesPerWidth; ++i)
                for (int j = 0; j < nodesPerHeight; ++j)
                {
                    seedTab[i, j] = 0;
                    seedTabNew[i, j] = 0;
                }
        }

        private void checkIfEmpty()
        {
            for (int i = 0; i < nodesPerWidth; ++i)
                for (int j = 0; j < nodesPerHeight; ++j)
                    if (seedTab[i, j] == 0)
                        return;
            empty = 1;
        }




        private void drawTriangleGrid()
        {
            drawGrid();
            for (int i = 0; i <= nodesPerHeight-1; ++i)
                for(int j=0; j<= nodesPerWidth-1; ++j)
                {
                    g.DrawLine(pen, j*PIXEL_SIZE, i * PIXEL_SIZE, (j+1) * PIXEL_SIZE, (i+1) * PIXEL_SIZE);
                }
        }

        private void drawGrid()
        {
            nodesPerWidth = int.Parse(textBox1.Text);
            nodesPerHeight = int.Parse(textBox2.Text);

            if (wantGrid)
            {
                for (int i = 0; i <= nodesPerHeight; ++i)
                    g.DrawLine(pen, 0, i * PIXEL_SIZE, (nodesPerWidth) * PIXEL_SIZE, i * PIXEL_SIZE);

                for (int i = 0; i <= nodesPerWidth; ++i)
                    g.DrawLine(pen, i * PIXEL_SIZE, 0, i * PIXEL_SIZE, PIXEL_SIZE * (nodesPerHeight));
            }
        }


        private void drawCurrent( int x, int y)
        {
            if (seedTab[x, y] != 0)
            {
                int val = seedTab[x, y];
                if(wantGrid)
                    g.FillRectangle(grains[val-1].getBrush(), (x * PIXEL_SIZE + 1), (y * PIXEL_SIZE + 1), PIXEL_SIZE - 1, PIXEL_SIZE - 1);
                else
                    g.FillRectangle(grains[val - 1].getBrush(), (x * PIXEL_SIZE ), (y * PIXEL_SIZE ), PIXEL_SIZE , PIXEL_SIZE);
            }
        }

        private void drawCurrentTab(int[,] tab, int x, int y)
        {
            if (tab[x, y] != 0)
            {
                int val = seedTab[x, y];
                if(wantGrid)
                    g.FillRectangle(grains[val - 1].getBrush(), (x * PIXEL_SIZE + 1), (y * PIXEL_SIZE + 1), PIXEL_SIZE - 1, PIXEL_SIZE - 1);
                else
                    g.FillRectangle(grains[val - 1].getBrush(), (x * PIXEL_SIZE), (y * PIXEL_SIZE), PIXEL_SIZE , PIXEL_SIZE);
            }
        }

        private void clearCurrent(int[,] tab, int pos, int iteracja)
        {
            if (tab[pos, iteracja] != 0)
            {
                if(wantGrid)
                    g.FillRectangle(brush2, (pos * PIXEL_SIZE + 1), (iteracja * PIXEL_SIZE + 1), PIXEL_SIZE - 1, PIXEL_SIZE - 1);
                else
                    g.FillRectangle(brush2, (pos * PIXEL_SIZE), (iteracja * PIXEL_SIZE ), PIXEL_SIZE, PIXEL_SIZE);
            }
        }


















        //------------------------------- SETTINGS --------------------------

        private void disableButtons()
        {
            button1.Enabled = false;
            button2.Enabled = false;
            clearButton.Enabled = false;
            collumnsRowButton.Enabled = false;
            radiusAmountButton.Enabled = false;
            randomButton.Enabled = false;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            checkBox1.Enabled = false;
            checkBox2.Enabled = false;
            trackBar1.Enabled = false;
            vonNeumanCheckBox.Enabled = false;
            mooreCheckBox.Enabled = false;
            noGridButton.Enabled = false;
        }

        private void enableButtons()
        {
            button1.Enabled = true;
            button2.Enabled = true;
            clearButton.Enabled = true;
            collumnsRowButton.Enabled = true;
            radiusAmountButton.Enabled = true;
            randomButton.Enabled = true;
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            checkBox1.Enabled = true;
            checkBox2.Enabled = true;
            trackBar1.Enabled = true;
            vonNeumanCheckBox.Enabled = true;
            mooreCheckBox.Enabled = true;
            noGridButton.Enabled = true;
        }

        private void dissableButtonsAtStart()
        {
            nextButton.Enabled = false;
            previousButton.Enabled = false;
            button2.Enabled = false;
        }

        private void enableButtonsAfterStart()
        {
            nextButton.Enabled = true;
            previousButton.Enabled = true;
            button2.Enabled = true;
        }



        private void noGridButton_Click(object sender, EventArgs e)
        {
            if (noGridButton.Checked == false)
            {
                noGridButton.Checked = false;
                wantGrid = true;
            }
            else
            {
                noGridButton.Checked = true;
                wantGrid = false;
            }
        }

        private void vonNeumanCheckBox_Click(object sender, EventArgs e)
        {
            if (vonNeumanCheckBox.Checked == false)
            {
                vonNeumanCheckBox.Checked = false;
                mooreCheckBox.Checked = true;
                isNeuman = false;
            }
            else
            {
                vonNeumanCheckBox.Checked = true;
                mooreCheckBox.Checked = false;
                isNeuman = true;
            }
        }

        private void mooreCheckBox_Click(object sender, EventArgs e)
        {
            if (mooreCheckBox.Checked == false)
            {
                mooreCheckBox.Checked = false;
                vonNeumanCheckBox.Checked = true;
                isNeuman = true;
            }
            else
            {
                mooreCheckBox.Checked = true;
                vonNeumanCheckBox.Checked = false;
                isNeuman = false;
            }
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false)
            {
                checkBox1.Checked = false;
                checkBox2.Checked = true;
                isPeriodic = false;
            }
            else
            {
                checkBox1.Checked = true;
                checkBox2.Checked = false;
                isPeriodic = true;
            }
        }

        private void checkBox2_Click(object sender, EventArgs e)
        {
            if (checkBox2.Checked == false)
            {
                checkBox2.Checked = false;
                checkBox1.Checked = true;
                isPeriodic = true;
            }
            else
            {
                checkBox2.Checked = true;
                checkBox1.Checked = false;
                isPeriodic = false;
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            isAvillableToStart = false;
            seedEmptyTab();
            g.Clear(Color.White);
            currentStep = 0;
            empty = 0;
            grains.Clear();
            steps.Clear();
            isPrepared = false;
            enableButtons();
            nextButton.Enabled = true;
            previousButton.Enabled = true;
            dissableButtonsAtStart();

        }

        private void ColumnNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (ColumnNumericUpDown.Value >= nodesPerWidth)
                ColumnNumericUpDown.Value = nodesPerWidth;

            if (ColumnNumericUpDown.Value < 1)
                ColumnNumericUpDown.Value = 1;
        }

        private void RowNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (RowNumericUpDown.Value >= nodesPerHeight)
                RowNumericUpDown.Value = nodesPerHeight;

            if (RowNumericUpDown.Value < 1)
                RowNumericUpDown.Value = 1;
        }

        private void amountNumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (AmountNumericUpDown.Value >= (nodesPerHeight-1)*(nodesPerWidth-1))
                AmountNumericUpDown.Value = (nodesPerHeight - 1) * (nodesPerWidth - 1);

            if (AmountNumericUpDown.Value < 1)
                AmountNumericUpDown.Value = 1;
        }

        private void radiusNumericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            int smaller = nodesPerWidth < nodesPerHeight ? nodesPerWidth : nodesPerHeight;
            if (AmountNumericUpDown.Value == 1)
            {
                int max = (smaller / 2 - 1);
                if (RadiusNumericUpDown.Value >= (max / AmountNumericUpDown.Value))
                    RadiusNumericUpDown.Value = max;
            }
            else
            {
                if (RadiusNumericUpDown.Value >= (smaller / AmountNumericUpDown.Value))
                    RadiusNumericUpDown.Value = (smaller / AmountNumericUpDown.Value);

                if (RadiusNumericUpDown.Value < 1)
                    RadiusNumericUpDown.Value = 1;
            }
           
        }

        private void randomNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (randomNumericUpDown.Value >= (nodesPerHeight - 1) * (nodesPerWidth - 1))
                randomNumericUpDown.Value = (nodesPerHeight - 1) * (nodesPerWidth - 1);

            if (randomNumericUpDown.Value < 1)
                randomNumericUpDown.Value = 1;
        }

        
    }
}
