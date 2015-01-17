using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionMethods
{
    public static class extends
    {
        public static string GetLine(this string text, int lineNo)
        {
            //Converte e divide as linhas em string array
            string[] lines = text.Replace("\r", "").Split('\n');

            //retorna a linha X ou null se não existir
            return lines.Length >= lineNo ? lines[lineNo] : null;
        }

        public static int GetLine(this string text, string content)
        {
            //Converte e divide as linhas em string array
            string[] lines = text.Replace("\r", "").Split('\n');

            //Encontra a linha que contem a string e retorna o indice
            return Array.FindIndex(lines, row => row.Contains(content + ":"));
        }
    }
}