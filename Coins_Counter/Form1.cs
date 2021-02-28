using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Coins_Counter
{
    public partial class Coins_counter : Form
    {
        public Coins_counter()
        {
            InitializeComponent();
            textBox1.AppendText("200");                         // Default sum (can be changed in main form)
            textBox2.AppendText("200 100 50 20 10 5 2 1");      // Default cortege of coins (can be changed in main form)
        }
        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear(); 
            int difference = 0;                           //Difference helps us to define the value of coin we take to our current collection. 
            int CortegeSum = 0;                           //CortegeSum stands for sum of our current set of coins: whether it higher than start sum or lower.
            int NumberOfCombinations = 0;                 //Counter for different sets (corteges) of coints for start sum.
            int Sum = 0;                                  //value which we will try to represent in all possible ways with our coins.
            List<int> NominalsList = new List<int>() { }; //List of different coins nominals.
            List<int> Cortege = new List<int>() { };      //List of current set of coins which are processing.
            Sum = Convert.ToInt32(textBox1.Text);
            string NominalsString = RemoveSpaces(textBox2.Text);     //Reading the values of coins.
            for (int i = 0; i < NominalsString.Trim().Split(' ').Length; i++)
                {
                    NominalsList.Add(Convert.ToInt32(NominalsString.Trim().Split(' ')[i])); //Getting different values of coins.
                }
            NominalsList.Sort();
            Cortege.Add(NominalsList[NominalsList.Count - 1]);            //Adding first, maximal valued coin to our cortege.
            while (Cortege.Count < Sum / NominalsList[0])                 //Processing untill our cortege will be represented with the cheapest coins.
            {
                foreach (int element in Cortege)
                {
                    CortegeSum += element;                                 //Counting the current sum of current cortege. 
                }
                difference = Sum - CortegeSum;
                if (difference > 0)                                        //If the difference is higher than zero, we still looking for coins to add to our current cortege.
                {
                    int NextCoin = ClosestCoin(difference, NominalsList);
                    if (NextCoin > 0)
                    {
                        Cortege.Add(NextCoin);
                    }
                    else
                    {
                        break;                                               //If there is no coin, cheaper than the difference – break.
                    }
                }
                else
                {
                    if (difference == 0)                                    //Means that we get successful set of coins, equal to starting sum.
                    {
                        NumberOfCombinations++;                              //increment counter of successful corteges.
                        DisplayCombinations(NumberOfCombinations, Cortege); //Shows us the successful corteges if checkbox is checked.
                    }
                    CortegeChanging(Cortege, NominalsList, CortegeSum, Sum); //Changes the last not the cheapest coin in cortege for getting different corteges (Greedy search)

                }
                CortegeSum = 0;                                             //Set to zero cortege sum for the next iteration.
            }
            if (Cortege[0] * Cortege.Count == Sum)                          //If the last cortege coincides with the starting sum – adding it to collection.
            {
                NumberOfCombinations++;
                DisplayCombinations(NumberOfCombinations, Cortege);
            }
            richTextBox1.AppendText("\n"+"Total number of combinations: " + NumberOfCombinations);
        }
        internal Tuple<List<int>, int> CortegeChanging(List<int>Cortege, List<int>NominalsList, int CortegeSum, int Sum)
        {
            for (int i = Cortege.Count - 1; i >= 0; i--)
            {
                if (Cortege[i] > NominalsList[0]) //Looking from the end of collection for last non-cheapest coin in cortege.
                {
                    Cortege[i] = NominalsList[NominalsList.IndexOf(Cortege[i]) - 1]; //Replace it with the cheaper one.
                    if (Cortege.Count - 1 > i) //If it is not the last coin in cortege:
                    {
                        int range = Cortege.Count - 1 - i;
                        Cortege.RemoveRange(i + 1, Cortege.Count - 1 - i); //  Replace the rest of cheap coins.
                        CortegeSum = 0; 
                        foreach (int element in Cortege)
                        {
                            CortegeSum += element;      // Recalculate the sum of current cortege.
                        }
                        for (int ChangeElements = 0; ChangeElements < (Sum - CortegeSum) / Cortege[i]; ChangeElements++)
                        {
                            Cortege.Add(Cortege[i]); //Adding as much coins of current nominal as possible without exceeding the starting sum.
                        }
                    }
                    break;
                }
            }
            return Tuple.Create(Cortege, CortegeSum);
        }
        internal void DisplayCombinations(int NumberOfCombinations, List<int> Cortege)
        {
            if ((checkBox1.Checked == true) && (NumberOfCombinations < 11))     //Showing us first 10 successful corteges in richtextbox. (can be changed).
            {
                foreach (var element in Cortege)
                {
                    richTextBox1.AppendText(element + " ");
                }
                richTextBox1.Appen