using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mips
{

    abstract class Regs
    {
        public int[] data = new int[0]; //Quantidade de registradores do subconjunto T

        //Identificador para encontrar o registrador
        public string regId = "";

        //Valores usados para identificar o registrador
        public int defaultChar = 2;
        public int defaultCharLength = 1;

        public struct regdm
        {
            public string registerName { get; set; }
            public int value { get; set; }

        }

        public Regs()
        {
            initialize();
            for (int i = 0; i < data.Count(); i++)
            {
                data[i] = 0;
            }

            regId = this.GetType().Name; //Define o nome padrão do registrador baseado no nome da classe
        }

        public abstract void initialize();

        public virtual List<regdm> getData()
        {
            List<regdm> ret = new List<regdm>();

            for (int i = 0; i < data.Count(); i++)
            {
                //Valor será xY, onde x é o nome do registrador e Y é o seu index no subconjunto
                ret.Add(new regdm() { registerName = regId + i, value = data[i] });
            }

            return ret;

        }

        public T getId<T>()
        {
            //Recebe o Id em qualquer tipo
            return (T)Convert.ChangeType(regId, typeof(T));
        }

        /// <summary>
        /// Gets o valor do primeiro registrador no subconjunto
        /// </summary>
        /// <returns></returns>
        public virtual int get()
        {
            return get(0);
        }

        /// <summary>
        /// Gets o valor de um registrador especifico no subconjunto
        /// </summary>
        /// <param name="Index"></param>
        /// <returns></returns>
        public virtual int get(int Index)
        {
            return data[Index];
        }

        /// <summary>
        /// Define o valor do primeiro registrador do subconjunto
        /// </summary>
        /// <param name="Val"></param>
        public virtual void set(int Val)
        {
            set(Val, 0);
        }

        /// <summary>
        /// Define o valor de um registrador especifico no subconjunto
        /// </summary>
        /// <param name="Val"></param>
        /// <param name="Index"></param>
        public virtual void set(int Val, int Index)
        {
            data[Index] = Val;
        }
    }
}
