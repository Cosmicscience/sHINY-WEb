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
            textBox1.AppendText("200");                         // Default sum (can