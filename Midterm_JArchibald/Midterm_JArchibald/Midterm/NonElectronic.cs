﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midterm
{
    class NonElectronic : Toy
    {
        public bool ChokingHazard { get; private set; }        

        public NonElectronic(string name, float price, int quantity, int ageRequirement, bool chokingHazard): base( name, price, quantity, ageRequirement)
        {
            this.ChokingHazard = chokingHazard;            
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
