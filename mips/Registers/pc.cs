using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mips.Registers
{
    class pc : Regs
    {
        public override void initialize()
        {
            data = new int[1];
        }

        public override List<regdm> getData()
        {
            List<regdm> ret = new List<regdm>();

            ret.Add(new regdm() { registerName = "PC", value = data[0] });

            return ret;
        }

        public void inc()
        {
            set(get() + 1);

        }

        public void inc(int x)
        {
            set(get() + x);
        }

        public void dec()
        {
            set(get() - 1);
        }

        public void dec(int x)
        {
            set(get() - x);
        }

        /// <summary>
        /// Retornará o valor dividido por quatro
        /// </summary>
        /// <returns>PC / 4</returns>
        public override int get()
        {
            return get(0) / 4;
        }

        /// <summary>
        /// Difinirá o valor multiplicado por quatro
        /// </summary>
        /// <param name="Val">valor da linha</param>
        public override void set(int Val)
        {
            set(Val * 4, 0);
        }
    }
}
