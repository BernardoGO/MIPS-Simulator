using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExtensionMethods;

namespace mips
{
    public partial class Form1 : Form
    {
        Cpu.mips cpu = new Cpu.mips();
        public Form1()
        {
            InitializeComponent();
            //Registers.initialize();
            cpu.Finished += new EventHandler(finished);
        }

        private void finished(object sender, EventArgs e)
        {
            boxCode.SelectAll();
            boxCode.SelectionColor = Color.Black;
            last = 0;
            didNoop = false;
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            noop();

            cpu.start(boxCode.Text);
            updateView();

            boxCode.SelectAll();
            boxCode.SelectionColor = Color.Blue;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            registerView.AutoGenerateColumns = true;
            updateView();
        }

        public void updateView()
        {
            registerView.DataSource = cpu.regs.printRegisters();
            registerView.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            runTextChanged = false;
            cpu.regs.reset();
            updateView();
            runTextChanged = true;
        }

        int last = 0;

        private void button3_Click(object sender, EventArgs e)
        {
            runTextChanged = false;
            int indx2 = boxCode.Text.GetLine(last).IndexOf("#") > 0 ? boxCode.Text.GetLine(last).IndexOf("#") : boxCode.Text.GetLine(last).Length;

            boxCode.Select(boxCode.GetFirstCharIndexFromLine(last), indx2);
            boxCode.SelectionColor = Color.Blue;

            int lst2 = boxCode.Text.GetLine(last).Length - indx2;
            boxCode.Select(boxCode.GetFirstCharIndexFromLine(last) + indx2, lst2);
            boxCode.SelectionColor = Color.Green;
            last = cpu.regs.Reg_PC.get();

            noop();

            int indx = boxCode.Text.GetLine(last).IndexOf("#") > 0 ? boxCode.Text.GetLine(last).IndexOf("#") : boxCode.Text.GetLine(last).Length;

            boxCode.Select(boxCode.GetFirstCharIndexFromLine(last), indx);
            boxCode.SelectionColor = Color.Red;

            int lst = boxCode.Text.GetLine(last).Length - indx;
            boxCode.Select(boxCode.GetFirstCharIndexFromLine(last) + indx, lst);
            boxCode.SelectionColor = Color.Green;

            cpu.nextStep(boxCode.Text);
            

            updateView();
            runTextChanged = true;
        }

        bool didNoop = false;
        public void noop()
        {
            if (didNoop) return;
            didNoop = true;
            if (boxNop.Checked) while (boxCode.Text.Contains("\n\n")) boxCode.Text = boxCode.Text.Replace("\n\n", "\nNOP\n");

            for (int i = 0; i < boxCode.Lines.Count(); i++)
            {
                int indx = boxCode.Text.GetLine(i).IndexOf("#") > 0 ? boxCode.Text.GetLine(i).IndexOf("#") : boxCode.Text.GetLine(i).Length;

                boxCode.Select(boxCode.GetFirstCharIndexFromLine(i), indx);
                boxCode.SelectionColor = Color.Black;

                int lst = boxCode.Text.GetLine(i).Length - indx;
                boxCode.Select(boxCode.GetFirstCharIndexFromLine(i) + indx, lst);
                boxCode.SelectionColor = Color.Green;
            }
        }

        bool runTextChanged = true;
        private void boxCode_TextChanged(object sender, EventArgs e)
        {
            /*
            if (!runTextChanged) return;
            int xxx = boxCode.SelectionStart;
            for (int i = 0; i < boxCode.Lines.Count(); i++)
            {
                int indx = boxCode.Text.GetLine(i).IndexOf("#") > 0 ? boxCode.Text.GetLine(i).IndexOf("#") : boxCode.Text.GetLine(i).Length;

                boxCode.Select(boxCode.GetFirstCharIndexFromLine(i), indx);
                boxCode.SelectionColor = Color.Black;

                int lst = boxCode.Text.GetLine(i).Length - indx;
                boxCode.Select(boxCode.GetFirstCharIndexFromLine(i) + indx, lst);
                boxCode.SelectionColor = Color.Green;
            }
            boxCode.Select(xxx, 0);
             * */
        }
    }
}

/*
addi $s0, $0, 2 # Define $s0 como a soma de 2 + 0
addi $s1, $s0, 3 # Define $s1 como soma de $s0 + 3

jal repetir10 # Chama função repetir10 e define o endereço de retorno

mult $s2, $s1 # Multiplica $s2 por $s1

addi $v0, $0, 10 # Define o v0 e chama o syscall para terminar
syscall # Executa saida


repetir10: # Definição da função
addi $s3, $0, 10 # $s3 = 0 + 10
repeat:
addi $s2, $s2, 1 
bne $s2, $s3, repeat # se $s2 != $s3 goto repeat
jr ra # retorna o contador de programa para onde a função foi chamada 
*/