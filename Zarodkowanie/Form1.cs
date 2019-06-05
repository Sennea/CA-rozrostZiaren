using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        private SolidBrush gravityBrush;
        private GravityCell[,] seedTab;
        private GravityCell[,] seedTabNew;
        private int nodesPerWidth;
        private int nodesPerHeight;
        public static Random random = new Random();
        private List<Grain> grains;
        private List<GravityCell[,]> steps;
        private List<GravityCell> grainsOnBorder = new List<GravityCell>();
        private List<GravityCell> grainsInMiddle = new List<GravityCell>();
        private List<GravityCell> lastRecrystalized = new List<GravityCell>();
        private List<GravityCell> currentlyRecrystalized = new List<GravityCell>();
        private int empty;
        private Boolean isPeriodic;
        private Boolean isPrepared;
        private Boolean isNeuman;
        private Boolean isMoore;
        private Boolean isPentagonal;
        private Boolean isHexagonal;
        private Boolean wantGrid;
        private Boolean isAvillableToStart;
        private Boolean isCircularRadius;
        private Boolean isEnergyShown;
        private int currentStep;
        private int monteCarloIterations;
        private BackgroundWorker worker = null;
        private List<float> dislocations;

        public readonly float A = (float)8.6711E+13;
        public readonly float B = (float)9.41E+00;
        public float CRITICAL_DISLOCATION = (float)(4.21584E+12);
        public static double DELTA_TIME = 0.001;
        public static double START_TIME = 0;
        public static double END_TIME = 0.2;
        public static int PIXEL_SIZE;
        public int PIXEL_SIZE_TMP;
        public readonly int DEFAULT_SIZE = 5;
        public static float DISLOCATION_PER_STEP;

        public Form1()
        {
            PIXEL_SIZE = DEFAULT_SIZE;
            InitializeComponent();
            pen = new Pen(Color.Black);
            g = pictureBox1.CreateGraphics();
            brush = new SolidBrush(Color.Black);
            brush2 = new SolidBrush(Color.White);
            gravityBrush = new SolidBrush(Color.Red);
            grains = new List<Grain>();
            steps = new List<GravityCell[,]>();
            isPeriodic = false;
            isPrepared = false;
            isAvillableToStart = false;
            isNeuman = true;
            isMoore = false;
            isPentagonal = false;
            isHexagonal = false;
            isCircularRadius = false;
            wantGrid = false;
            isEnergyShown = false;
            currentStep = 0;
            DissableButtonsAtStart();
            monteCarloIterations = 20;
            dislocations = new List<float>();

            nodesPerWidth = pictureBox1.Width / PIXEL_SIZE;
            nodesPerHeight = pictureBox1.Height / PIXEL_SIZE;

            textBox1.Text = nodesPerWidth.ToString();
            textBox2.Text = nodesPerHeight.ToString();

            nodesPerWidth = int.Parse(textBox1.Text);
            nodesPerHeight = int.Parse(textBox2.Text);

        }




        private void CountNextStep(Neighbourhood neighbourhood)
        {
            VonNeuman vonNeuman = new VonNeuman(neighbourhood);
            Pentagonal pentagonal = new Pentagonal(neighbourhood);
            Hexagonal hexagonal = new Hexagonal(neighbourhood);
            Moore moore = new Moore(neighbourhood);
            CircularRadius circularRadius = new CircularRadius(neighbourhood);

            int hexaCase = 2;
            int circularRadiusSize = int.Parse(circularRadiusSizeTextBox.Text);

            if (circularRadiusSize >= nodesPerHeight / 2 || circularRadiusSize >= nodesPerWidth / 2)
            {
                waringTextField.Text = "Radius too big-> set to 3";
                circularRadiusSize = 3;
            }
            if (leftHexaCheckBox.Checked)
                hexaCase = 1;
            if (rightHexaCheckBox.Checked)
                hexaCase = 0;



            for (int i = 0; i < nodesPerWidth; ++i)
                for (int j = 0; j < nodesPerHeight; ++j)
                {
                    if (isNeuman)
                    {
                        vonNeuman.GetNeumanNeighbours(i, j);
                    }
                    else if (isMoore)
                    {
                        moore.GetMooreNeighbours(i, j);
                    }
                    else if (isPentagonal)
                    {
                        int pentaCase = random.Next(4);
                        pentagonal.GetPentagonalNeighbours(i, j, pentaCase);
                    }
                    else if (isHexagonal)
                    {
                        if (hexaCase == 2) hexaCase = random.Next(2);
                        hexagonal.GetHexagonalNeighbours(i, j, hexaCase);
                    }
                    else if (isCircularRadius)
                    {
                        if (seedTab[i, j].GetValue() != 0)
                            circularRadius.getRadiusNeighbours(i, j, circularRadiusSize);
                    }
                }

            for (int i = 0; i < nodesPerWidth; ++i)
                for (int j = 0; j < nodesPerHeight; ++j)
                    if (seedTab[i, j].GetValue() == 0 && seedTabNew[i, j].GetValue() != 0)
                    {
                        seedTab[i, j].SetValue(seedTabNew[i, j].GetValue());
                        DrawCurrent(i, j);
                    }

        }



        private void CountDislocation(double t, Neighbourhood neighbourhood, StreamWriter sw)
        {
            float dislocation = A / B + (1 - A / B) * (float)Math.Exp(-B * t);
            float deltaDislocation = 0;
            sw.Write(dislocation + ";");
            dislocations.Add(dislocation);

            if (t == START_TIME)
                deltaDislocation = dislocation;
            else
                deltaDislocation = dislocations[(int)(t / DELTA_TIME)] - dislocations[(int)((t - DELTA_TIME) / DELTA_TIME)];


            float deltaPerOne = deltaDislocation / (nodesPerHeight * nodesPerWidth);
            float percent = 0.2f * deltaPerOne;

            RedistribuateStartDislocation(percent);
            RedistrobuateRestDislocation((deltaDislocation - (percent*(nodesPerHeight * nodesPerWidth)  )), sw);
            Nucleation(neighbourhood);
            Recrystalization(neighbourhood);
            for (int i = 0; i < lastRecrystalized.Count; ++i)
            {
                seedTab[lastRecrystalized[i].GetPositionX(), lastRecrystalized[i].GetPositionY()].setLastRecrystalized(false);
                lastRecrystalized.Remove(lastRecrystalized[i]);
            }
            for (int i = 0; i < currentlyRecrystalized.Count; ++i)
            {
                lastRecrystalized.Add(currentlyRecrystalized[i]);
                seedTab[currentlyRecrystalized[i].GetPositionX(), currentlyRecrystalized[i].GetPositionY()].setLastRecrystalized(true);
                currentlyRecrystalized.Remove(currentlyRecrystalized[i]);
            }
        
            sw.Write( DISLOCATION_PER_STEP+";");
            float dislocationStayed = 0;
            for(int i=0; i< nodesPerWidth; ++i)
                for(int j=0; j< nodesPerHeight; ++j)
                {
                    dislocationStayed += seedTab[i, j].getDislocationDensity();
                }
            sw.Write(dislocationStayed+ "\n");

        }

        private void RedistribuateStartDislocation(float value)
        {
            for (int i = 0; i < nodesPerWidth; ++i)
                for (int j = 0; j < nodesPerHeight; ++j)
                {
                    DISLOCATION_PER_STEP += value;
                    seedTab[i, j].setDislocationDensity(seedTab[i, j].getDislocationDensity() + value);
                }
        }

        private void RedistrobuateRestDislocation(float value, StreamWriter sw)
        {

            float dislocationPackage = 0.001f * value;
            do
            {
                value -= dislocationPackage;
                DISLOCATION_PER_STEP += dislocationPackage;

                int probability = random.Next(100);
                if (probability < 80)
                {
                    int randomGrain = random.Next(grainsOnBorder.Count);
                    int x = grainsOnBorder[randomGrain].GetPositionX();
                    int y = grainsOnBorder[randomGrain].GetPositionY();
                    seedTab[x, y].setDislocationDensity(seedTab[x, y].getDislocationDensity() + dislocationPackage);
                }
                else
                {
                    int randomGrain = random.Next(grainsInMiddle.Count);
                    int x = grainsInMiddle[randomGrain].GetPositionX();
                    int y = grainsInMiddle[randomGrain].GetPositionY();
                    seedTab[x, y].setDislocationDensity(seedTab[x, y].getDislocationDensity() + dislocationPackage);
                }

            } while (value >= dislocationPackage);
        }

        private void Nucleation(Neighbourhood neighbourhood)
        {
            foreach(GravityCell grain in grainsOnBorder)
            {
                if (seedTab[grain.GetPositionX(), grain.GetPositionY()].getDislocationDensity() > CRITICAL_DISLOCATION)
                {
                    seedTab[grain.GetPositionX(), grain.GetPositionY()].setDislocationDensity(CRITICAL_DISLOCATION);
                    currentlyRecrystalized.Add(seedTab[grain.GetPositionX(), grain.GetPositionY()]);
                    if(!seedTab[grain.GetPositionX(), grain.GetPositionY()].IsRecrystalized())
                         drawDislocation(grain.GetPositionX(), grain.GetPositionY());
                    seedTab[grain.GetPositionX(), grain.GetPositionY()].setRecrystalized(true);
                    
                }
            }
        }

        private void Recrystalization(Neighbourhood neighbourhood)
        {
            for(int i=0;i< nodesPerWidth; ++i)
                for(int j=0; j< nodesPerHeight; ++j)
                    if(shouldRecrystalize(i,j, neighbourhood) )
                    {
                        currentlyRecrystalized.Add(seedTab[i, j]);
                        seedTab[i, j].setDislocationDensity(0);

                       if(!seedTab[i, j].IsRecrystalized())
                            drawDislocation(i,j);

                        seedTab[i, j].setRecrystalized(true);
                    }
        }

        private bool shouldRecrystalize(int x, int y, Neighbourhood neighbourhood)
        {
            List<GravityCell> neighbours = GetNeighbours(x, y, neighbourhood);
            float sumaricDensity = getAllNeighboursDensity(x, y, neighbours);
            if (sumaricDensity < seedTab[x, y].getDislocationDensity())
            {
                foreach (GravityCell neighbour in neighbours)
                    if (neighbour.IsLastRecrystalized())
                        return true;
            }
            return false;
        }

        private float getAllNeighboursDensity(int x, int y, List<GravityCell> neighbours)
        {
            float sumaricDensity = 0;
            foreach (GravityCell neighbour in neighbours)
            {
                if (neighbour.IsLastRecrystalized())
                    sumaricDensity -= neighbour.getDislocationDensity();
                else
                    sumaricDensity += neighbour.getDislocationDensity();
            }
            return sumaricDensity;
        }

        private bool IsOnTheBorder(int x, int y, List<GravityCell> neighbours)
        {
            foreach (GravityCell neighbour in neighbours)
                if (neighbour.GetValue() != seedTab[x, y].GetValue())
                    return true;

            return false;
        }

        private float[] getMinAndMaxDensity()
        {
            float[] minMax = { 0, 0 };
            for(int i =0; i< nodesPerWidth; ++i)
                for(int j=0; j< nodesPerHeight; ++j)
                {
                    if (seedTab[i, j].getDislocationDensity() < minMax[0])
                        minMax[0] = seedTab[i, j].getDislocationDensity();
                    if (seedTab[i, j].getDislocationDensity() > minMax[1])
                        minMax[1] = seedTab[i, j].getDislocationDensity();
                }
            return minMax;
        }

       

        private List<GravityCell> getGrainsOnBorder(Neighbourhood neighbourhood)
        {
            List<GravityCell> grainsOnBorder = new List<GravityCell>();
            for (int i = 0; i < nodesPerWidth; ++i)
            for(int j=0; j< nodesPerHeight; ++j)
                {
                    if (IsOnTheBorder(i, j, GetNeighbours(i, j, neighbourhood)))
                        grainsOnBorder.Add(seedTab[i, j]);
                }

            return grainsOnBorder;
        }

        private List<GravityCell> getGrainsInMiddle(Neighbourhood neighbourhood)
        {
            List<GravityCell> grainsInMiddle = new List<GravityCell>();
            for (int i = 0; i < nodesPerWidth; ++i)
                for (int j = 0; j < nodesPerHeight; ++j)
                {
                    if (!IsOnTheBorder(i, j, GetNeighbours(i, j, neighbourhood)))
                        grainsInMiddle.Add(seedTab[i, j]);
                }

            return grainsInMiddle;
        }


        private List<GravityCell> GetNeighbours(int x, int y, Neighbourhood neighbourhood)
        {
            VonNeuman vonNeuman = new VonNeuman(neighbourhood);
            Pentagonal pentagonal = new Pentagonal(neighbourhood);
            Hexagonal hexagonal = new Hexagonal(neighbourhood);
            Moore moore = new Moore(neighbourhood);
            CircularRadius circularRadius = new CircularRadius(neighbourhood);
            List<GravityCell> neighbours = new List<GravityCell>();
            if (isNeuman)
                neighbours = vonNeuman.GetNeighbours(x, y);
            else if (isMoore)
                neighbours = moore.GetNeighbours(x, y);
            else if (isPentagonal)
            {
                int pentaCase = random.Next(4);
                neighbours = pentagonal.GetNeighbours(x, y, pentaCase);
            }
            else if (isHexagonal)
            {
                int hexaCase = random.Next(2);
                if (leftHexaCheckBox.Checked)
                    hexaCase = 1;
                if (rightHexaCheckBox.Checked)
                    hexaCase = 0;
                neighbours = hexagonal.GetNeighbours(x, y, hexaCase);
            }
            else if (isCircularRadius)
                neighbours = circularRadius.GetNeighbours(x, y);

            return neighbours;
        }


        private int CountEnergy(int x, int y, Neighbourhood neighbourhood)
        {

            List<GravityCell> neighbours = GetNeighbours(x, y, neighbourhood);

            int boundaryEnergy = 1;
            int kronckerNeighboursValue = 0;
            for (int i = 0; i < neighbours.Count; ++i)
            {
                kronckerNeighboursValue += (1 - countDeltaKroneckera(seedTab[x, y].GetValue(), neighbours[i].GetValue()));
            }
            int energy = boundaryEnergy * kronckerNeighboursValue;
            return energy;
        }

        private int countDeltaKroneckera(int start, int second)
        {
            if (start == second) return 1;
            else return 0;
        }

        private void MonteCarlo(int x, int y, Neighbourhood neighbourhood)
        {
            int startValue = seedTab[x, y].GetValue();
            List<GravityCell> neighbours = GetNeighbours(x, y, neighbourhood);
            int r = random.Next(neighbours.Count);
            int prevEne = CountEnergy(x, y, neighbourhood);

            seedTab[x, y].SetValue(neighbours[r].GetValue());
            int newEne = CountEnergy(x, y, neighbourhood);

            double kt = (double)ktSelector.Value;
            int deltaEne = newEne - prevEne;
            if (deltaEne <= 0)
            {
                DrawCurrent(x, y);
                return;
            }
            else
            {
                double probability = Math.Exp(-1 * deltaEne / kt) * 100;
                double changeEneProbability = random.NextDouble() * 100;
                if (changeEneProbability <= probability)
                {
                    DrawCurrent(x, y);
                    return;
                }
                else seedTab[x, y].SetValue(startValue);
            }
        }



        //------------------------------- BUTTONS --------------------------


        private void StartButton_Click(object sender, EventArgs e)
        {
            Neighbourhood neighbourhood = new Neighbourhood(grains, isPeriodic, nodesPerWidth, nodesPerHeight, seedTab, seedTabNew);

            Prepare();
            if (isAvillableToStart)
            {
                do
                {
                    DisableButtons();
                    CountNextStep(neighbourhood);
                    CheckIfEmpty();
                } while (empty == 0);
                EnableButtons();
            }
        }


        private void dislocationButton_Click(object sender, EventArgs e)
        {
            Neighbourhood neighbourhood = new Neighbourhood(grains, isPeriodic, nodesPerWidth, nodesPerHeight, seedTab, seedTabNew);
            double time = START_TIME;
            CRITICAL_DISLOCATION /= ((nodesPerWidth) * (nodesPerHeight));
            String path = @"C:\Users\Kasia\source\repos\Zarodkowanie\Zarodkowanie\";
            String fileName = "plik.csv";
            grainsOnBorder = getGrainsOnBorder(neighbourhood);
            grainsInMiddle = getGrainsInMiddle(neighbourhood);

            using (System.IO.FileStream fs = System.IO.File.Create(path + fileName))
            {
                for (byte i = 0; i < 100; i++)
                {
                    fs.WriteByte(i);
                }
            }

            StreamWriter sw = new StreamWriter(path + fileName);
            sw.Write("Time;");
            sw.Write("Normalne;");
            sw.Write( "Moje;");
            sw.Write("Rodzdzielone\n");
            BackgroundWorker backgroundWorker = new BackgroundWorker();
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.ProgressChanged += new ProgressChangedEventHandler((senders, args) =>
            {
                iteracjaTextBox.Text = time.ToString();

            });

            backgroundWorker.DoWork += new DoWorkEventHandler((senders, args) =>
            {
                do
                {
                    backgroundWorker.ReportProgress(0);

                    sw.Write(time + ";");
                    CountDislocation(time, neighbourhood, sw);
                    time += DELTA_TIME;
                    System.Threading.Thread.Sleep(10);

                } while (time <= END_TIME);
                sw.Close();
            });

            backgroundWorker.RunWorkerAsync();
           
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            Neighbourhood neighbourhood = new Neighbourhood(grains, isPeriodic, nodesPerWidth, nodesPerHeight, seedTab, seedTabNew);

            if (currentStep >= steps.Count)
            {
                nodesPerWidth = int.Parse(textBox1.Text);
                nodesPerHeight = int.Parse(textBox2.Text);
                GravityCell[,] seedTabTmp = new GravityCell[nodesPerWidth, nodesPerHeight];
                for (int i = 0; i < nodesPerWidth; ++i)
                    for (int j = 0; j < nodesPerHeight; ++j)
                    {
                        seedTabTmp[i, j] = new GravityCell(seedTab[i, j].GetGravityX(), seedTab[i, j].GetGravityY(), seedTab[i, j].GetValue(), i, j);
                    }
                steps.Add(seedTabTmp);
                CountNextStep(neighbourhood);
            }

            DrawStep(currentStep);
            currentStep++;
            CheckIfEmpty();
            if (empty == 1) nextButton.Enabled = false;
            else nextButton.Enabled = true;
            previousButton.Enabled = true;
        }


        private void PreviousButton_Click(object sender, EventArgs e)
        {
            isAvillableToStart = true;
            nextButton.Enabled = true;
            empty = 0;
            if (currentStep > 0)
            {
                currentStep--;
                DrawWholeWhite(steps[currentStep]);
                DrawStep(currentStep);
                if (currentStep == 0) previousButton.Enabled = false;
            }

        }

        private void EnergyButton_Click(object sender, EventArgs e)
        {
            if (!isEnergyShown)
            {
                Neighbourhood neighbourhood = new Neighbourhood(grains, isPeriodic, nodesPerWidth, nodesPerHeight, seedTab, seedTabNew);
                for (int i = 0; i < nodesPerWidth; ++i)
                    for (int j = 0; j < nodesPerHeight; ++j)
                    {
                        seedTab[i, j].SetEnergy(CountEnergy(i, j, neighbourhood));
                    }
                DrawCurrentTabForEnergy(seedTab);
            }
            else
            {
                for (int i = 0; i < nodesPerWidth; ++i)
                    for (int j = 0; j < nodesPerHeight; ++j)
                    {
                        DrawCurrent(i, j);
                    }
            }
            isEnergyShown = !isEnergyShown;
        }

        private void MonteCarloButton_Click(object sender, EventArgs e)
        {
            Neighbourhood neighbourhood = new Neighbourhood(grains, isPeriodic, nodesPerWidth, nodesPerHeight, seedTab, seedTabNew);
            List<GravityCell> grainsToCountMonteCarlo = new List<GravityCell>();
            GravityCell randomNeighbour;
            worker = new BackgroundWorker();
            worker.WorkerSupportsCancellation = true;
            int r = 0;

            worker.DoWork += new DoWorkEventHandler((state, args) =>
            {
                for (int a = 0; a < monteCarloIterations; a++)
                {
                    for (int i = 0; i < nodesPerWidth; ++i)
                        for (int j = 0; j < nodesPerHeight; ++j)
                        {
                            grainsToCountMonteCarlo.Add(seedTab[i, j]);
                        }
                    do
                    {
                        r = random.Next(grainsToCountMonteCarlo.Count);
                        randomNeighbour = grainsToCountMonteCarlo[r];
                        MonteCarlo(randomNeighbour.GetPositionX(), randomNeighbour.GetPositionY(), neighbourhood);
                        grainsToCountMonteCarlo.Remove(randomNeighbour);
                    } while (grainsToCountMonteCarlo.Count > 0);
                    if (worker.CancellationPending)
                        break;
                }
            });
            worker.RunWorkerAsync();
        }

        private void monteCarloCancelButton_Click(object sender, EventArgs e)
        {
            worker.CancelAsync();
        }

        private void ShowGravityButton_Click(object sender, EventArgs e)
        {
            DrawGravity();
        }


        private void RandomButton_Click(object sender, EventArgs e)
        {
            isAvillableToStart = true;
            Prepare();
            int amount = Convert.ToInt32(randomNumericUpDown.Value);
            int breakPoint = 0;
            for (int i = 0; i < amount; ++i)
            {
                breakPoint = 0;
                int x, y;
                do
                {
                    x = random.Next(0,nodesPerWidth);
                    y = random.Next(0, nodesPerHeight);
                    breakPoint++;
                    if (breakPoint > 2000)
                        break;
                } while (seedTab[x, y].GetValue() != 0);

                if (breakPoint < 2000)
                    CreateNewGrain(x, y);
            }

        }
        private void CollumnsRowButton_Click(object sender, EventArgs e)
        {
            isAvillableToStart = true;
            Prepare();
            int numberInColumn = Convert.ToInt32(ColumnNumericUpDown.Value);
            int numberInRow = Convert.ToInt32(RowNumericUpDown.Value);

            int maxInWidth = nodesPerWidth % 2 != 0 ? nodesPerWidth-- : nodesPerWidth;
            int mexInHeight = nodesPerHeight % 2 != 0 ? nodesPerHeight-- : nodesPerHeight;

            int colDelay = (maxInWidth) / (numberInColumn);
            int rowDelay = mexInHeight / (numberInRow);

            for (int i = 0; i < numberInColumn; ++i)
                for (int j = 0; j < numberInRow; ++j)
                {
                    CreateNewGrain(i * colDelay + colDelay / 2, j * rowDelay + rowDelay / 2);
                }
        }

        private void RadiusAmountButton_Click(object sender, EventArgs e)
        {
            waringTextField.Text = "";
            isAvillableToStart = true;
            Prepare();
            int radius = Convert.ToInt32(RadiusNumericUpDown.Value);
            int amount = Convert.ToInt32(AmountNumericUpDown.Value);
            bool added = false;
            radius++;
            int trying= 0;
            int amountOfAdded = 0;

            for (int k = 0; k < amount; ++k)
            {
                do
                {
                    trying++;
                    int x = random.Next(radius,nodesPerWidth-(radius+1));
                    int y = random.Next(radius,nodesPerHeight- (radius+1));
                    if (seedTab[x, y].GetValue() == 0 && CheckInRadius(x, y, radius))
                    {
                        CreateNewGrain(x, y);
                        added = true;
                        amountOfAdded++;
                        break;
                    }
                    if (trying > 10000)
                        break;

                } while (added);
                if (added == false)
                    break;
                added = false;
            }
            int noAdd = amount - amountOfAdded;
            waringTextField.Text = "Added: " + amountOfAdded + " Unable: " + noAdd;
        }

        private void PictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            isAvillableToStart = true;
            int x = e.X / PIXEL_SIZE;
            int y = e.Y / PIXEL_SIZE;

            Prepare();
            CreateNewGrain(x, y);
        }

        private void GridButton_Click(object sender, EventArgs e)
        {
            Prepare();
        }


        

       








        //------------------------------- METHODS --------------------------


        private void Prepare()
        {
            if (!isPrepared)
            {
                EnableButtonsAfterStart();
                isPrepared = true;
                DrawGrid();
                SeedEmptyTab();
            }
        }

        private void DrawWholeWhite(GravityCell[,] tab)
        {
            for (int i = 0; i < nodesPerWidth; ++i)
                for (int j = 0; j < nodesPerHeight; ++j)
                {
                    if(wantGrid)
                        g.FillRectangle(brush2, (i * PIXEL_SIZE + 1), (j * PIXEL_SIZE + 1), PIXEL_SIZE - 1, PIXEL_SIZE - 1);
                    else
                        g.FillRectangle(brush2, (i * PIXEL_SIZE), (j * PIXEL_SIZE), PIXEL_SIZE, PIXEL_SIZE);
                }
        }

        private void DrawStep(int index)
        {
            for (int i = 0; i < nodesPerWidth; ++i)
                for (int j = 0; j < nodesPerHeight; ++j)
                {
                    DrawCurrentTab(steps[index], i, j);
                }
        }
        
        private bool CheckInRadius(int i, int j, int radius)
        {
            int count = 0;
            for (int r = 0; r < radius; ++r)
                for (int p = 0; p < radius; ++p)
                    if (p + r < radius)
                    {
                        if (seedTab[i - p, j - r].GetValue() != 0) count++;
                        if (seedTab[i - p, j + r].GetValue() != 0) count++;
                        if (seedTab[i + p, j + r].GetValue() != 0) count++;
                        if (seedTab[i + p, j - r].GetValue() != 0) count++;
                        if (count != 0)
                            return false;
                    }
            return true;
        }

        private void CreateNewGrain(int x, int y)
        {
            if(seedTab[x,y].GetValue() == 0)
            {
                int r = random.Next(10, 225);
                int g = random.Next(10, 225);
                int b = random.Next(10, 225);
                int[] color = { r, g, b };

                Grain grain = new Grain(grains.Count + 1, color, x, y);

                grains.Add(grain);
                seedTab[x, y].SetValue(grain.GetIndex());
                DrawCurrent(x, y);
            }
        }

        private void SeedEmptyTab()
        {
            nodesPerWidth = int.Parse(textBox1.Text);
            nodesPerHeight = int.Parse(textBox2.Text);
            seedTab = new GravityCell[nodesPerWidth, nodesPerHeight];
            seedTabNew = new GravityCell[nodesPerWidth, nodesPerHeight];
            for (int i = 0; i < nodesPerWidth; ++i)
                for (int j = 0; j < nodesPerHeight; ++j)
                {
                    float gX = (float)(random.NextDouble()*(PIXEL_SIZE - 2) +1)+ (i*PIXEL_SIZE);
                    float gY = (float)(random.NextDouble()*(PIXEL_SIZE-2) + 1) + (j*PIXEL_SIZE);

                    seedTab[i, j] = new GravityCell(gX, gY, 0,i,j);
                    seedTabNew[i, j] = new GravityCell(gX, gY, 0,i,j);
                }
        }

        private void CheckIfEmpty()
        {
            for (int i = 0; i < nodesPerWidth; ++i)
                for (int j = 0; j < nodesPerHeight; ++j)
                    if (seedTab[i, j].GetValue() == 0)
                        return;
            empty = 1;
        }

        private void DrawGravity()
        {
            nodesPerWidth = int.Parse(textBox1.Text);
            nodesPerHeight = int.Parse(textBox2.Text);

            for(int i =0; i< nodesPerWidth; ++i)
                for(int j=0; j< nodesPerHeight; ++j)
                {
                    g.FillRectangle(gravityBrush, seedTab[i, j].GetGravityX() - 0.5f, seedTab[i, j].GetGravityY()- 0.5f, 1,1);
                }

        }

        private void DrawGrid()
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


        private void showDensityButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < nodesPerWidth; ++i)
                for (int j = 0; j < nodesPerHeight; ++j)
                {
                    DrawDensityDislocation(i,j);
                }
        }

        private void DrawDensityDislocation(int x, int y)
        {
            float min = CRITICAL_DISLOCATION- 100000;
            float max = getMinAndMaxDensity()[1];
            int red = 0, green = 0, blue = 0;
            float diff = (max - min) / 10;

            if (seedTab[x, y].getDislocationDensity() < min)
            {
                red = 140; green = 184; blue = 255;
            }
            else
            {
                for (int i = 0; i < 10; ++i)
                {
                    if (seedTab[x, y].getDislocationDensity() >= (min + diff * (i)) && seedTab[x, y].getDislocationDensity() < (min + diff * (i + 1)))
                    {
                        red = 0;
                        green = (255 - (i * 20));
                        blue = 10;
                    }
                }
            }

            SolidBrush densityBrush = new SolidBrush(Color.FromArgb(red, green, blue));

            if (wantGrid)
                g.FillRectangle(densityBrush, (x * PIXEL_SIZE + 1), (y * PIXEL_SIZE + 1), PIXEL_SIZE - 1, PIXEL_SIZE - 1);
            else
                g.FillRectangle(densityBrush, (x * PIXEL_SIZE), (y * PIXEL_SIZE), PIXEL_SIZE, PIXEL_SIZE);
        }

        private void drawDislocation(int x, int y)
        {
            int r = random.Next(0, 255);
            int gr = 0;
            int b = 0;

           SolidBrush dislocationBrush = new SolidBrush(Color.FromArgb(r, gr, b));


            if (wantGrid)
                g.FillRectangle(dislocationBrush, (x * PIXEL_SIZE + 1), (y * PIXEL_SIZE +1), PIXEL_SIZE - 1, PIXEL_SIZE - 1);
            else
                g.FillRectangle(dislocationBrush, (x * PIXEL_SIZE), (y * PIXEL_SIZE), PIXEL_SIZE, PIXEL_SIZE);
        }

        private void DrawCurrent( int x, int y)
        {
            if (seedTab[x, y].GetValue() != 0)
            {
                int val = seedTab[x, y].GetValue();
                if(wantGrid)
                    g.FillRectangle(grains[val-1].GetBrush(), (x * PIXEL_SIZE + 1), (y * PIXEL_SIZE + 1), PIXEL_SIZE - 1, PIXEL_SIZE - 1);
                else
                    g.FillRectangle(grains[val - 1].GetBrush(), (x * PIXEL_SIZE ), (y * PIXEL_SIZE ), PIXEL_SIZE , PIXEL_SIZE);
            }
           
        }

        private void DrawCurrentTab(GravityCell[,] tab, int x, int y)
        {
            if (tab[x, y].GetValue() != 0)
            {
                int val = seedTab[x, y].GetValue();
                if(wantGrid)
                    g.FillRectangle(grains[val - 1].GetBrush(), (x * PIXEL_SIZE + 1), (y * PIXEL_SIZE + 1), PIXEL_SIZE - 1, PIXEL_SIZE - 1);
                else
                    g.FillRectangle(grains[val - 1].GetBrush(), (x * PIXEL_SIZE), (y * PIXEL_SIZE), PIXEL_SIZE , PIXEL_SIZE);
            }

         
        }

        private void DrawCurrentTabForEnergy(GravityCell[,] tab)
        {
            for(int i=0; i< nodesPerWidth; ++i)
                for(int j=0; j< nodesPerHeight; ++j)
                {
                    if (wantGrid)
                        g.FillRectangle(GetEnergyBrush(tab[i,j].GetEnergy()) , (i * PIXEL_SIZE + 1), (j * PIXEL_SIZE + 1), PIXEL_SIZE - 1, PIXEL_SIZE - 1);
                    else
                        g.FillRectangle(GetEnergyBrush(tab[i, j].GetEnergy()), (i * PIXEL_SIZE), (j * PIXEL_SIZE), PIXEL_SIZE, PIXEL_SIZE);
                }
        }

        private SolidBrush GetEnergyBrush(float ene)
        {
            SolidBrush brush;

            if (ene == 0) brush = new SolidBrush(Color.FromArgb(160, 50, 255));
            else
            {
                int r = 120 - 30 * (int)ene;
                int g = 220;
                int b = 70;
                if (r <= 0)
                {
                    r = 80 - 20 * (int)(ene - 4);
                    g = 200;
                    b = 70;
                }
                brush = new SolidBrush(Color.FromArgb(r,g,b));
            }

            return brush;
        }

        private void ClearCurrent(int[,] tab, int pos, int iteracja)
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

        private void DisableButtons()
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
            energyButton.Enabled = false;
            monteCarloButton.Enabled = false;
        }

        private void EnableButtons()
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
            energyButton.Enabled = true;
            monteCarloButton.Enabled = true;
        }

        private void DissableButtonsAtStart()
        {
            nextButton.Enabled = false;
            previousButton.Enabled = false;
            button2.Enabled = false;
            showGravityButton.Enabled = false;
            energyButton.Enabled = false;
            monteCarloButton.Enabled = false;
        }

        private void EnableButtonsAfterStart()
        {
            nextButton.Enabled = true;
            previousButton.Enabled = true;
            button2.Enabled = true;
            showGravityButton.Enabled = true;
            energyButton.Enabled = true;
            monteCarloButton.Enabled = true;
        }



        private void NoGridButton_Click(object sender, EventArgs e)
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

        private void VonNeumanCheckBox_Click(object sender, EventArgs e)
        {
            if (vonNeumanCheckBox.Checked == false)
            {
                vonNeumanCheckBox.Checked = false;
                isNeuman = false;
            }
            else
            {
                vonNeumanCheckBox.Checked = true;
                mooreCheckBox.Checked = false;
                pentagolCheckBox.Checked = false;
                hexagonalCheckBox.Checked = false;
                leftHexaCheckBox.Checked = false;
                rightHexaCheckBox.Checked = false;
                circleRadiusCheckBox.Checked = false;

                isNeuman = true;
                isMoore = false;
                isPentagonal = false;
                isHexagonal = false;
                isCircularRadius = false;
            }
        }

        private void MooreCheckBox_Click(object sender, EventArgs e)
        {
            if (mooreCheckBox.Checked == false)
            {
                mooreCheckBox.Checked = false;
                isMoore = false;
            }
            else
            {
                vonNeumanCheckBox.Checked = false;
                mooreCheckBox.Checked = true;
                pentagolCheckBox.Checked = false;
                hexagonalCheckBox.Checked = false;
                leftHexaCheckBox.Checked = false;
                rightHexaCheckBox.Checked = false;
                circleRadiusCheckBox.Checked = false;

                isNeuman = false;
                isMoore = true;
                isPentagonal = false;
                isHexagonal = false;
                isCircularRadius = false;
            }
        }
        private void PentagolCheckBox_Click(object sender, EventArgs e)
        {
            if (pentagolCheckBox.Checked == false)
            {
                pentagolCheckBox.Checked = false;
                isPentagonal = false;
            }
            else
            {
                vonNeumanCheckBox.Checked = false;
                mooreCheckBox.Checked = false;
                pentagolCheckBox.Checked = true;
                hexagonalCheckBox.Checked = false;
                leftHexaCheckBox.Checked = false;
                rightHexaCheckBox.Checked = false;
                circleRadiusCheckBox.Checked = false;

                isNeuman = false;
                isMoore = false;
                isPentagonal = true;
                isHexagonal = false;
                isCircularRadius = false;
            }
        }

        private void HexagonalCheckBox_Click(object sender, EventArgs e)
        {
            if (hexagonalCheckBox.Checked == false)
            {
                hexagonalCheckBox.Checked = false;
                isHexagonal = false;
            }
            else
            {
                vonNeumanCheckBox.Checked = false;
                mooreCheckBox.Checked = false;
                pentagolCheckBox.Checked = false;
                hexagonalCheckBox.Checked = true;
                leftHexaCheckBox.Checked = false;
                rightHexaCheckBox.Checked = false;
                circleRadiusCheckBox.Checked = false;

                isNeuman = false;
                isMoore = false;
                isPentagonal = false;
                isHexagonal = true;
                isCircularRadius = false;
            }
        }

        private void LeftHexaCheckBox_Click(object sender, EventArgs e)
        {
            if (leftHexaCheckBox.Checked == false)
            {
                leftHexaCheckBox.Checked = false;
            }
            else
            {
                leftHexaCheckBox.Checked = true;
                rightHexaCheckBox.Checked = false;

                vonNeumanCheckBox.Checked = false;
                mooreCheckBox.Checked = false;
                pentagolCheckBox.Checked = false;
                hexagonalCheckBox.Checked = false;
                circleRadiusCheckBox.Checked = false;

                isNeuman = false;
                isMoore = false;
                isPentagonal = false;
                isHexagonal = true;
                isCircularRadius = false;
            }
        }

        private void RightHexaCheckBox_Click(object sender, EventArgs e)
        {
            if (rightHexaCheckBox.Checked == false)
            {
                rightHexaCheckBox.Checked = false;
            }
            else
            {
                rightHexaCheckBox.Checked = true;
                leftHexaCheckBox.Checked = false;

                vonNeumanCheckBox.Checked = false;
                mooreCheckBox.Checked = false;
                pentagolCheckBox.Checked = false;
                hexagonalCheckBox.Checked = false;
                circleRadiusCheckBox.Checked = false;

                isNeuman = false;
                isMoore = false;
                isPentagonal = false;
                isHexagonal = true;
                isCircularRadius = false;
            }
        }

        private void CircleRadiusCheckBox_Click(object sender, EventArgs e)
        {
            if (circleRadiusCheckBox.Checked == false)
            {
                circleRadiusCheckBox.Checked = false;
                isCircularRadius = false;
            }
            else
            {
                vonNeumanCheckBox.Checked = false;
                mooreCheckBox.Checked = false;
                pentagolCheckBox.Checked = false;
                hexagonalCheckBox.Checked = false;
                leftHexaCheckBox.Checked = false;
                rightHexaCheckBox.Checked = false;
                circleRadiusCheckBox.Checked = true;

                isNeuman = false;
                isMoore = false;
                isPentagonal = false;
                isHexagonal = false;
                isCircularRadius = true;
            }
        }

        private void CheckBox1_Click(object sender, EventArgs e)
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

        private void CheckBox2_Click(object sender, EventArgs e)
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

        private void ClearButton_Click(object sender, EventArgs e)
        {
            waringTextField.Text = "";
            isAvillableToStart = false;
            SeedEmptyTab();
            g.Clear(Color.White);
            currentStep = 0;
            empty = 0;
            grains.Clear();
            steps.Clear();
            isPrepared = false;
            EnableButtons();
            nextButton.Enabled = true;
            previousButton.Enabled = true;
            DissableButtonsAtStart();

        }

        private void ColumnNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            nodesPerWidth = int.Parse(textBox1.Text);
            nodesPerHeight = int.Parse(textBox2.Text);

            if (ColumnNumericUpDown.Value >= nodesPerWidth)
                ColumnNumericUpDown.Value = nodesPerWidth;

            if (ColumnNumericUpDown.Value < 1)
                ColumnNumericUpDown.Value = 1;
        }

        private void RowNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            nodesPerWidth = int.Parse(textBox1.Text);
            nodesPerHeight = int.Parse(textBox2.Text);

            if (RowNumericUpDown.Value >= nodesPerHeight)
                RowNumericUpDown.Value = nodesPerHeight;

            if (RowNumericUpDown.Value < 1)
                RowNumericUpDown.Value = 1;
        }

        private void AmountNumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            nodesPerWidth = int.Parse(textBox1.Text);
            nodesPerHeight = int.Parse(textBox2.Text);

            if (AmountNumericUpDown.Value >= (nodesPerHeight-1)*(nodesPerWidth-1)/RadiusNumericUpDown.Value)
                AmountNumericUpDown.Value = (nodesPerHeight - 1) * (nodesPerWidth - 1) / RadiusNumericUpDown.Value;

            if (AmountNumericUpDown.Value < 1)
                AmountNumericUpDown.Value = 1;
        }

        private void RadiusNumericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            nodesPerWidth = int.Parse(textBox1.Text);
            nodesPerHeight = int.Parse(textBox2.Text);

            int smaller = nodesPerWidth < nodesPerHeight ? nodesPerWidth : nodesPerHeight;
            if (AmountNumericUpDown.Value == 1)
            {
                int max = (smaller);
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

        private void RandomNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
             nodesPerWidth = int.Parse(textBox1.Text);
            nodesPerHeight = int.Parse(textBox2.Text);

            if (randomNumericUpDown.Value >= (nodesPerHeight - 1) * (nodesPerWidth - 1))
                randomNumericUpDown.Value = (nodesPerHeight - 1) * (nodesPerWidth - 1);

            if (randomNumericUpDown.Value < 1)
                randomNumericUpDown.Value = 1;
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            int size = pictureBox1.Width / PIXEL_SIZE;
            if (!int.TryParse(textBox1.Text, out size) || textBox1.Text == "" || int.Parse(textBox1.Text) < 1 || int.Parse(textBox1.Text) > pictureBox1.Width / PIXEL_SIZE)
                textBox1.Text = (pictureBox1.Width / PIXEL_SIZE).ToString();
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            int size = pictureBox1.Height / PIXEL_SIZE;
            if (!int.TryParse(textBox2.Text, out size) || textBox2.Text == "" || int.Parse(textBox2.Text) < 1 || int.Parse(textBox2.Text) > pictureBox1.Height / PIXEL_SIZE)
                textBox2.Text = (pictureBox1.Height / PIXEL_SIZE).ToString();
        }


        private void TrackBar1_Scroll(object sender, EventArgs e)
        {
            PIXEL_SIZE = trackBar1.Value;
            textBox2.Text = (pictureBox1.Height / PIXEL_SIZE).ToString();
            textBox1.Text = (pictureBox1.Width / PIXEL_SIZE).ToString();
        }


        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            g = pictureBox1.CreateGraphics();
        }

        private void MCStextBox_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(MCStextBox.Text, out monteCarloIterations))
            {
                monteCarloIterations = 20;
                MCStextBox.Text = 20.ToString();
            }
            else
            {
                monteCarloIterations = int.Parse(MCStextBox.Text);
                if (monteCarloIterations <= 0)
                {
                    monteCarloIterations = 20;
                    MCStextBox.Text = 20.ToString();
                }
                else if (monteCarloIterations > 500)
                {
                    monteCarloIterations = 500;
                    MCStextBox.Text = 500.ToString();
                }
            }
        }

        
    }
}
