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
  