﻿using System;
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
            textBox2.AppendText("200 100 50 20 10 5 2 1");      // Defaul