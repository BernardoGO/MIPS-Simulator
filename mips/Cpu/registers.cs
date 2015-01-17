using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mips.Cpu
{
    class registers
    {
        // Prefixo usado para reflection
        public string Prefix = "Reg_";

        public Registers.a Reg_a = new Registers.a();
        public Registers.s Reg_s = new Registers.s();
        public Registers.t Reg_t = new Registers.t();
        public Registers.v Reg_v = new Registers.v();

        public Registers.zero Reg_0 = new Registers.zero();

        public Registers.pc Reg_PC = new Registers.pc();
        public Registers.fp Reg_FP = new Registers.fp();
        public Registers.hi Reg_HI = new Registers.hi();
        public Registers.lo Reg_LO = new Registers.lo();
        public Registers.ra Reg_RA = new Registers.ra();
        public Registers.sp Reg_SP = new Registers.sp();

        public void reset()
        {
            Reg_a = new Registers.a();
            Reg_s = new Registers.s();
            Reg_t = new Registers.t();
            Reg_v = new Registers.v();
            Reg_PC = new Registers.pc(); //Program Counter
            Reg_FP = new Registers.fp();
            Reg_HI = new Registers.hi();
            Reg_LO = new Registers.lo();
            Reg_RA = new Registers.ra();
            Reg_SP = new Registers.sp();
        }

        public List<Regs.regdm> printRegisters()
        {
            List<Regs.regdm> ret = new List<Regs.regdm>();

            ret.AddRange(Reg_PC.getData());
            ret.AddRange(Reg_FP.getData());
            ret.AddRange(Reg_HI.getData());
            ret.AddRange(Reg_LO.getData());
            ret.AddRange(Reg_RA.getData());
            ret.AddRange(Reg_SP.getData());
            ret.AddRange(Reg_a.getData());
            ret.AddRange(Reg_s.getData());
            ret.AddRange(Reg_t.getData());
            ret.AddRange(Reg_v.getData());

            return ret;

        }
    }
}
