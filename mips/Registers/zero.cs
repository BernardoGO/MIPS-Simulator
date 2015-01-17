using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mips.Registers
{
    class zero : Regs
    {
        public override void initialize()
        {
            data = new int[1];
            defaultChar = 1;
        }

        public override List<regdm> getData()
        {
            return new List<regdm>();
        }

        public override int get()
        {
            return 0; // $0 sempre retorna zero
        }

        public override void set(int Val)
        {
            set(0); // $0 não aceita novos valores
        }
    }
}
