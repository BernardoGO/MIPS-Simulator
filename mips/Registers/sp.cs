﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mips.Registers
{
    class sp : Regs
    {
        public override void initialize()
        {
            data = new int[1];
        }

        public override List<regdm> getData()
        {
            List<regdm> ret = new List<regdm>();

            ret.Add(new regdm() { registerName = "SP", value = data[0] });

            return ret;
        }

    }
}
