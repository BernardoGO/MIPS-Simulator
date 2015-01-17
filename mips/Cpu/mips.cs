using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExtensionMethods;



namespace mips.Cpu
{
    
    

    class mips
    {
        public registers regs = new registers();
        
        bool running = false;
        String instructions = "";

        public void start(string instructions)
        {
            running = true;
            regs.Reg_PC.set(0); // Definir contador de programa para o início
            this.instructions = instructions;

            while (running) //running será false quando acabar a execução
            {
                execInstruction(instructions.GetLine(regs.Reg_PC.get())); //Encontra a instrução e despacha para execução
                regs.Reg_PC.inc(); //Incrementa o contador de programa em 4 para próxima instrução
            }
            Finished(null,null); //Dispara o evento de termino
        }

        public void nextStep(string instructions)
        {
            running = true; // Se identificado que é a ultima instrução, será falso

            this.instructions = instructions;

            execInstruction(instructions.GetLine(regs.Reg_PC.get())); //Encontra a instrução e despacha para execução
            regs.Reg_PC.inc(); //Incrementa o contador de programa em 4 para próxima instrução

            if (!running)
            {
                regs.Reg_PC.set(0); //Zerar o contador de programa se terminado
                Finished(null,null);
            }
        }

        public EventHandler Finished = null;

        public void execInstruction(string line)
        {
            if(line.Contains('#')) // Separar comentarios no assembly
                line = line.Substring(0, line.IndexOf('#'));

            if (line.Length < 1) return; // Identificar linhas vazias
            if (line.Contains(':')) return; // Identificar labels para goto

            string[] splt = line.Split(' '); //Dividir a instrução para identificar registradores

            switch (splt[0].ToLower()) // Decodificar instrução
            {
                case "nop":
                    break;
                case "noop":
                    break;

                case "addi":
                    setRegister(splt[1], getRegister(splt[2]) + Convert.ToInt32(splt[3]));
                    break;
                case "add":
                    setRegister(splt[1], getRegister(splt[2]) + getRegister(splt[3]));
                    break;

                case "slt":
                    if (getRegister(splt[2]) < getRegister(splt[3]))
                        setRegister(splt[1], 1);
                    else setRegister(splt[1], 0);
                    break;
                case "slti":
                    if (getRegister(splt[2]) < Convert.ToInt32(splt[3]))
                        setRegister(splt[1], 1);
                    else setRegister(splt[1], 0);
                    break;

                case "sll":
                    setRegister(splt[1], getRegister(splt[2]) << Convert.ToInt32(splt[3]));
                    break;
                case "sllv":
                    setRegister(splt[1], getRegister(splt[2]) << getRegister(splt[3]));
                    break;
                case "srl":
                    setRegister(splt[1], getRegister(splt[2]) >> Convert.ToInt32(splt[3]));
                    break;
                case "srlv":
                    setRegister(splt[1], getRegister(splt[2]) >> getRegister(splt[3]));
                    break;

                case "sra":
                    int temp = (int)((uint)getRegister(splt[2]) & (uint)2147483648);
                    setRegister(splt[1], getRegister(splt[2]) >> Convert.ToInt32(splt[3]));
                    break;

                case "lui":
                    setRegister(splt[1], getRegister(splt[2]) << 16);
                    break;
                
                case "mfhi":
                    setRegister(splt[1], regs.Reg_HI.get());
                    break;
                case "mflo":
                    setRegister(splt[1], regs.Reg_LO.get());
                    break;
                    
                case "subi":
                    setRegister(splt[1], getRegister(splt[2]) - Convert.ToInt32(splt[3]));
                    break;
                case "sub":
                    setRegister(splt[1], getRegister(splt[2]) - getRegister(splt[3]));
                    break;
                    
                case "div":
                    regs.Reg_LO.set((int)(getRegister(splt[1]) / getRegister(splt[2])));
                    regs.Reg_HI.set((int)(getRegister(splt[1]) % getRegister(splt[2])));
                    break;

                case "mult":
                    regs.Reg_LO.set((int)(getRegister(splt[1]) * getRegister(splt[2])));
                    
                    break;
                    
                case "andi":
                    setRegister(splt[1], getRegister(splt[2]) & Convert.ToInt32(splt[3]));
                    break;
                case "and":
                    setRegister(splt[1], getRegister(splt[2]) & getRegister(splt[3]));
                    break;

                case "ori":
                    setRegister(splt[1], getRegister(splt[2]) | Convert.ToInt32(splt[3]));
                    break;
                case "or":
                    setRegister(splt[1], getRegister(splt[2]) | getRegister(splt[3]));
                    break;

                case "xori":
                    setRegister(splt[1], getRegister(splt[2]) ^ Convert.ToInt32(splt[3]));
                    break;
                case "xor":
                    setRegister(splt[1], getRegister(splt[2]) ^ getRegister(splt[3]));
                    break;


                case "beq":
                    if (getRegister(splt[1]) == getRegister(splt[2]))
                    {
                        regs.Reg_PC.set(instructions.GetLine(splt[3]));
                    }
                    break;

                case "bgez":
                    if (getRegister(splt[1]) >= 0)
                    {
                        regs.Reg_PC.set(instructions.GetLine(splt[2]));
                    }
                    break;

                case "bgezal":
                    if (getRegister(splt[1]) >= 0)
                    {
                        regs.Reg_RA.set(regs.Reg_PC.get() * 4);
                        regs.Reg_PC.set(instructions.GetLine(splt[2]));
                    }
                    break;

                case "bgtz":
                    if (getRegister(splt[1]) > 0)
                    {
                        regs.Reg_PC.set(instructions.GetLine(splt[2]));
                    }
                    break;

                case "blez":
                    if (getRegister(splt[1]) <= 0)
                    {
                        regs.Reg_PC.set(instructions.GetLine(splt[2]));
                    }
                    break;

                case "bltzal":
                    if (getRegister(splt[1]) < 0)
                    {
                        regs.Reg_RA.set(regs.Reg_PC.get() * 4);
                        regs.Reg_PC.set(instructions.GetLine(splt[2]));
                    }
                    break;

                case "bne":
                    if (getRegister(splt[1]) != getRegister(splt[2]))
                    {
                        regs.Reg_PC.set(instructions.GetLine(splt[3]));
                    }
                    break;

                case "jal":
                    regs.Reg_RA.set(regs.Reg_PC.get() * 4);
                    int ln = instructions.GetLine(splt[1]);
                    regs.Reg_PC.set(ln);
                    break;

                case "j":
                    regs.Reg_PC.set(instructions.GetLine(splt[1]));
                    break;

                case "jr":
                    regs.Reg_PC.set(regs.Reg_RA.get() /4);
                    break;

                case "syscall":
                    syscall();
                    break;
            }
        }

        public void syscall()
        {
            switch (regs.Reg_v.get())
            {
                case 10:
                    running = false;
                    break;
                case 1:
                case 2:
                case 3:
                case 4:
                    MessageBox.Show(regs.Reg_a.get().ToString());
                    break;
            }

        }

        public void setRegister(string regName, int value)
        {
            char reg = regName[1]; 
            Func<int, int> adct = x => Convert.ToInt32(regName[x].ToString()); //Função para pegar o nome

            string regnm = "";

            //Procurar se existe o registro no objeto CPU o registrador
            bool hasRegister = regs.GetType().GetFields().Count(p => p.Name.Contains(regs.Prefix + reg)) > 0;
            //Se não existe, adiciona o segundo caractere do nome do registrador
            regnm = hasRegister ? reg.ToString() : (regName[1].ToString() + regName[2].ToString()).ToUpper();

            //Seleciona o registrador por reflection
            var xei = regs.GetType().GetField(regs.Prefix + regnm).GetValue(regs) as Regs;

            //define o valor
            if (hasRegister)
                xei.set(value, adct(xei.defaultChar)); 
            else xei.set(value);
        }

        public int getRegister(string regName)
        {

            char reg = regName[1];
            Func<int, int> adct = x => Convert.ToInt32(regName[x].ToString());//Função para pegar o nome

            string regnm = "";

            //Procurar se existe o registro no objeto CPU o registrador
            bool hasRegister = regs.GetType().GetFields().Count(p => p.Name.Contains(regs.Prefix + reg)) > 0;
            //Se não existe, adiciona o segundo caractere do nome do registrador
            regnm = hasRegister ? reg.ToString() : (regName[1].ToString() + regName[2].ToString()).ToUpper();

            //Seleciona o registrador por reflection
            var xei = regs.GetType().GetField(regs.Prefix + regnm).GetValue(regs) as Regs;

            //Get o valor
            int value = hasRegister ? xei.get(adct(xei.defaultChar)) : xei.get();
            return value;
            
        }
    }

}
