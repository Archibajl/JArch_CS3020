﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midterm
{
    abstract class Toy : VendingMachineOption
    {
        public int AgeRequirement { get;  set; }

        //Takes the general variables to be used by the Electornic and NonElectronic classes.
        public Toy(string name, float price, int quantity, int ageRequirement) : base( name, price, quantity)
        {
          
            AgeRequirement = ageRequirement;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
