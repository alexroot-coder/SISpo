using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Globalization;
namespace printff
{
    internal class Program
    {
        public static string OctalToASCII(string oct)
        {
            string ascii = string.Empty;

            for (int i = 0; i < oct.Length; i += 3)
            {
                ascii += (char)OctalToDecimal(oct.Substring(i, 3));
            }
            return ascii;
        }
        private static int OctalToDecimal(string octal)
        {
            int octLength = octal.Length;
            double dec = 0;
            for (int i = 0; i < octLength; ++i)
            {
                dec += ((byte)octal[i] - 48) * Math.Pow(8, ((octLength - i) - 1));
            }
            return (int)dec;
        }

        public static List<string> indexes(string a, char b)
        {
            List<string> listrange = new List<string>();
            int cnt = 0;
            foreach (char c in a)
            {
                if (c == b)
                {
                    var sym = '%';
                    listrange.Add(sym.ToString() + a[cnt + 1]);
                }

                cnt++;
            }
            return listrange;
        }

        public static string ConvertHex(String hexString)
        {
            try
            {
                string ascii = string.Empty;

                for (int i = 0; i < hexString.Length; i += 2)
                {
                    String hs = string.Empty;

                    hs = hexString.Substring(i, 2);
                    uint decval = System.Convert.ToUInt32(hs, 16);
                    char character = System.Convert.ToChar(decval);
                    ascii += character;

                }

                return ascii;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return string.Empty;
        }

        public static string ConvertUnic(string text)
        {
            if (string.IsNullOrEmpty(text)) return null;

            string temp = null;
            bool flag = false;

            int len = text.Length / 4;
            if (text.StartsWith("0x") || text.StartsWith("0X"))
            {
                len = text.Length / 6;//0x in Unicode string
                flag = true;
            }

            StringBuilder sb = new StringBuilder(len);
            for (int i = 0; i < len; i++)
            {
                if (flag)
                    temp = text.Substring(i * 6, 6).Substring(2);
                else
                    temp = text.Substring(i * 4, 4);

                byte[] bytes = new byte[2];
                bytes[1] = byte.Parse(int.Parse(temp.Substring(0, 2), NumberStyles.HexNumber).ToString());
                bytes[0] = byte.Parse(int.Parse(temp.Substring(2, 2), NumberStyles.HexNumber).ToString());
                sb.Append(Encoding.Unicode.GetString(bytes));
            }
            return sb.ToString();
        }

        public static string preob_5(string console)
        {
            string cnsl = console;
            var aaa = console.Split(new string[] { "\\U" }, StringSplitOptions.None);
            for (int i = 1; i < aaa.Length; i++)
            {
                var t = ConvertUnic((aaa[i][0].ToString() + aaa[i][1].ToString() + aaa[i][2].ToString() + aaa[i][3].ToString() + aaa[i][4].ToString() + aaa[i][5].ToString() + aaa[i][6].ToString() + aaa[i][7].ToString()));
                cnsl = cnsl.Replace("\\U" + (aaa[i][0].ToString() + aaa[i][1].ToString() + aaa[i][2].ToString() + aaa[i][3].ToString() + aaa[i][4].ToString() + aaa[i][5].ToString() + aaa[i][6].ToString() + aaa[i][7].ToString()), t);

            }
            return cnsl;
        }

        public static string preob_4(string console)
        {
            string cnsl = console;
            var aaa = console.Split(new string[] { "\\u" }, StringSplitOptions.None);
            for (int i = 1; i < aaa.Length; i++)
            {
                var t = ConvertUnic((aaa[i][0].ToString() + aaa[i][1].ToString() + aaa[i][2].ToString() + aaa[i][3].ToString()));
                cnsl = cnsl.Replace("\\u" + (aaa[i][0].ToString() + aaa[i][1].ToString() + aaa[i][2].ToString() + aaa[i][3].ToString()), t);

            }
            return cnsl;
        }

        public static string preob_3(string console)
        {
            string cnsl = console;
            var aaa = console.Split(new string[] { "\\x" }, StringSplitOptions.None);
            for (int i = 1; i < aaa.Length; i++)
            {
                var t = ConvertHex((aaa[i][0].ToString() + aaa[i][1].ToString()));
                cnsl = cnsl.Replace("\\x" + (aaa[i][0].ToString() + aaa[i][1].ToString()), t);

            }
            return cnsl;
        }

        public static string preob_2(string console)
        {
            string cnsl = console;
            var aaa = console.Split(new string[] { "\\" }, StringSplitOptions.None);
            for (int i = 1; i < aaa.Length; i++)
            {

                try
                {
                    if (Char.IsNumber(aaa[i][0]) && Char.IsNumber(aaa[i][1]) && Char.IsNumber(aaa[i][2]))
                    {
                        var t = OctalToASCII((aaa[i][0].ToString() + aaa[i][1].ToString() + aaa[i][2].ToString()));
                        cnsl = cnsl.Replace("\\" + (aaa[i][0].ToString() + aaa[i][1].ToString() + aaa[i][2].ToString()), t);
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    //Console.WriteLine(console.Replace("\\\\","\\"));

                }

            }
            return cnsl;
        }

        public static string preob(string console)
        {
            string cnsl = console;
            int index_of;
            string[] tmp = new[] { "\\b", "\\r", "\\t", "%y", "\\v", "%z", "\\\"", "%%" ,"\\n"};

            for (int i = 0; i < tmp.Length; i++)
            {
                while (cnsl.Contains(tmp[i]))
                {
                    index_of = console.IndexOf(tmp[i]);
                    if ((tmp[i] == "\\b"))
                    {
                        if ((index_of != 0))
                        {
                            cnsl = cnsl.Remove(index_of - 1, 3);
                        }
                        else
                        {
                            return cnsl;
                        }
                    }

                    if ((tmp[i] == "\\t"))
                    {
                        cnsl = cnsl.Replace("\\t", "\t");
                    }
                    if ((tmp[i] == "\\r"))
                    {
                        cnsl = cnsl.Replace("\\r", "\r");
                    }
                    if ((tmp[i] == "\\v"))
                    {
                        cnsl = cnsl.Replace("\\v", "");
                    }
                    if ((tmp[i] == "%y"))
                    {
                        cnsl = cnsl.Replace("%y", "\\");
                    }
                    if ((tmp[i] == "%z"))
                    {
                        cnsl = cnsl.Replace("%z", "%");
                    }
                    if ((tmp[i] == "\\\""))
                    {
                        cnsl = cnsl.Replace("\\\"", "\"");
                    }
                    if ((tmp[i] == "%%"))
                    {
                        cnsl = cnsl.Replace("%%", "%");
                    }
                    if ((tmp[i] == "\\n"))
                    {
                        cnsl = cnsl.Replace("\\n", "\n");
                    }
                }
            }
            return cnsl;
        }

        public static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                return 0;
            }

            if (args[0] == "--help")
            {

                Console.Write("ОПИСАНИЕ \nВывод ЗНАЧЕНИЙ согласно указанному ФОРМАТУ.\n--help\tпоказать справку и выйти\n--version\n\tпоказать информацию о версии и выйти\nДирективы ФОРМАТА такие же как в функции printf на C.Интерпретируемые символы:\n\\tдвойные кавычки\n\\NNN\n\tсимвол с восьмеричным кодом NNN(от 1 до 3 цифр)\n\\\\\n\tобратная косая черта\n\\a\n\tсигнал(BEL)\n\\b\n\tзабой\n\\c\n\tне производить дальнейшую обработку данных \n\\f\n\tперевод страницы\n\\n\n\t\nновая строка \\r\n\tвозврат каретки \\t\n\tгоризонтальная табуляция \\v\n\tвертикальная табуляция\n\\xHH\n\tсимвол с шестнадцатеричным кодом HH(1 или 2 цифры)\\uHHHH\n\tсимвол Unicode(ISO / IEC 10646) с шестнадцатеричным кодом HHHH(4 цифры)\\UHHHHHHHH\n\tсимвол Unicode с шестнадцатеричным кодом HHHHHHHH(8 цифр)%%\n\tсимвол %% b\n\tЗНАЧЕНИЕ интерпретируется как строка с символами пропуска,\n\tза исключением того случая, когда восьмерично представленный символ пропуска имеет форму \0 или \0NNN ");
                return 0;
            }



            var consoleout = args[0];

            if (args[0].Contains("%%"))
            {
                consoleout = consoleout.Replace("%%", "%z");
            }

            if (args[0].Contains("\\a"))
            {
                SystemSounds.Beep.Play();
                consoleout = consoleout.Replace("\\a", "");
                args[0] = args[0].Replace("\\a", "");
            }

            var a = indexes(consoleout, '%');
            var index_of = args[0].IndexOf("%");

            for (int i = 0; i < a.Count; i++)
            {
                if (a[i] == "%z")
                {
                    a.RemoveAt(i);
                }
            }


            if (a.Count == args.Length - 1)
            {
                for (int i = 0; i < a.Count; i++)
                {
                    if (a[i] == "%s")
                    {
                        index_of = consoleout.IndexOf(a[i]);
                        consoleout = consoleout.Insert(index_of, args[i + 1]);
                        index_of = consoleout.IndexOf(a[i]);
                        consoleout = consoleout.Remove(index_of, 2);
                    }

                    if (a[i] == "%d")
                    {
                        if (args[i + 1].Contains("-0x"))
                        {
                            if (args[i + 1].Replace("-0x", "0x").Contains("0x"))
                            {
                                index_of = consoleout.IndexOf(a[i]);
                                consoleout = consoleout.Insert(index_of,
                                    "-" + Convert.ToInt32(args[i + 1].Replace("-0x", "0x"), 16).ToString());
                                index_of = consoleout.IndexOf(a[i]);
                                consoleout = consoleout.Remove(index_of, 2);
                            }
                            else
                            {
                                var aa = Convert.ToInt32(args[i + 1]);
                                index_of = consoleout.IndexOf(a[i]);
                                consoleout = consoleout.Insert(index_of,
                                    aa.ToString());
                                index_of = consoleout.IndexOf(a[i]);
                                consoleout = consoleout.Remove(index_of, 2);
                            }
                        }
                        else
                        {
                            if (args[i + 1].Contains("0x"))
                            {
                                index_of = consoleout.IndexOf(a[i]);
                                consoleout = consoleout.Insert(index_of,
                                    Convert.ToInt32(args[i + 1], 16).ToString());
                                index_of = consoleout.IndexOf(a[i]);
                                consoleout = consoleout.Remove(index_of, 2);
                            }
                            else
                            {
                                var aa = Convert.ToInt32(args[i + 1]);
                                index_of = consoleout.IndexOf(a[i]);
                                consoleout = consoleout.Insert(index_of,
                                    aa.ToString());
                                index_of = consoleout.IndexOf(a[i]);
                                consoleout = consoleout.Remove(index_of, 2);
                            }
                        }
                    }

                    if (a[i] == "%x")
                    {
                        if (args[i + 1].Contains("0x"))
                        {
                            index_of = consoleout.IndexOf(a[i]);
                            consoleout = consoleout.Insert(index_of,
                                (args[i + 1].Replace("0x", "")).ToUpper());
                            index_of = consoleout.IndexOf(a[i]);
                            consoleout = consoleout.Remove(index_of, 2);
                        }
                        else
                        {
                            index_of = consoleout.IndexOf(a[i]);
                            consoleout = consoleout.Insert(index_of,
                                (Int64.Parse(args[i + 1])).ToString("X").ToString().ToLower());
                            index_of = consoleout.IndexOf(a[i]);
                            consoleout = consoleout.Remove(index_of, 2);
                        }
                    }

                    if (a[i] == "%X")
                    {
                        if (args[i + 1].Contains("0x"))
                        {
                            index_of = consoleout.IndexOf(a[i]);
                            consoleout = consoleout.Insert(index_of,
                                (args[i + 1].Replace("0x", "")).ToUpper());
                            index_of = consoleout.IndexOf(a[i]);
                            consoleout = consoleout.Remove(index_of, 2);
                        }
                        else
                        {
                            index_of = consoleout.IndexOf(a[i]);
                            consoleout = consoleout.Insert(index_of,
                                (Int64.Parse(args[i + 1])).ToString("X").ToString().ToUpper());
                            index_of = consoleout.IndexOf(a[i]);
                            consoleout = consoleout.Remove(index_of, 2);
                        }
                    }

                    if (a[i] == "%o")
                    {
                        var bin = Convert.ToInt32(args[i + 1], 10);
                        index_of = consoleout.IndexOf(a[i]);
                        consoleout = consoleout.Insert(index_of, Convert.ToString(bin, 2));
                        index_of = consoleout.IndexOf(a[i]);
                        consoleout = consoleout.Remove(index_of, 2);
                    }

                    if (a[i] == "%f")
                    {
                        var dec = Convert.ToInt32(args[i + 1], 10);
                        var flt = Convert.ToSingle(dec);
                        index_of = consoleout.IndexOf(a[i]);
                        consoleout = consoleout.Insert(index_of, string.Format("{0:F6}", dec));
                        index_of = consoleout.IndexOf(a[i]);
                        consoleout = consoleout.Remove(index_of, 2);
                    }
                }
            }
            else
            {
                if (a.Count > args.Length - 1)
                {
                    for (int i = 0; i < a.Count; i++)
                    {
                        if (i < args.Length - 1)
                        {
                            if (a[i] == "%s")
                            {
                                index_of = consoleout.IndexOf(a[i]);
                                consoleout = consoleout.Insert(index_of, args[i + 1]);
                                index_of = consoleout.IndexOf(a[i]);
                                consoleout = consoleout.Remove(index_of, 2);
                            }

                            if (a[i] == "%d")
                            {
                                if (args[i + 1].Contains("-0x"))
                                {
                                    if (args[i + 1].Replace("-0x", "0x").Contains("0x"))
                                    {
                                        index_of = consoleout.IndexOf(a[i]);
                                        consoleout = consoleout.Insert(index_of,
                                            "-" + Convert.ToInt32(args[i + 1].Replace("-0x", "0x"), 16).ToString());
                                        index_of = consoleout.IndexOf(a[i]);
                                        consoleout = consoleout.Remove(index_of, 2);
                                    }
                                    else
                                    {
                                        var aa = Convert.ToInt32(args[i + 1]);
                                        index_of = consoleout.IndexOf(a[i]);
                                        consoleout = consoleout.Insert(index_of,
                                            aa.ToString());
                                        index_of = consoleout.IndexOf(a[i]);
                                        consoleout = consoleout.Remove(index_of, 2);
                                    }
                                }
                                else
                                {
                                    if (args[i + 1].Contains("0x"))
                                    {
                                        index_of = consoleout.IndexOf(a[i]);
                                        consoleout = consoleout.Insert(index_of,
                                            Convert.ToInt32(args[i + 1], 16).ToString());
                                        index_of = consoleout.IndexOf(a[i]);
                                        consoleout = consoleout.Remove(index_of, 2);
                                    }
                                    else
                                    {
                                        var aa = Convert.ToInt32(args[i + 1]);
                                        index_of = consoleout.IndexOf(a[i]);
                                        consoleout = consoleout.Insert(index_of,
                                            aa.ToString());
                                        index_of = consoleout.IndexOf(a[i]);
                                        consoleout = consoleout.Remove(index_of, 2);
                                    }
                                }
                            }

                            if (a[i] == "%x")
                            {
                                if (args[i + 1].Contains("0x"))
                                {

                                    index_of = consoleout.IndexOf(a[i]);
                                    consoleout = consoleout.Insert(index_of,
                                        (args[i + 1].Replace("0x", "")).ToLower());
                                    index_of = consoleout.IndexOf(a[i]);
                                    consoleout = consoleout.Remove(index_of, 2);
                                }
                                else
                                {
                                    index_of = consoleout.IndexOf(a[i]);
                                    consoleout = consoleout.Insert(index_of,
                                        (Int64.Parse(args[i + 1])).ToString("X").ToString().ToLower());
                                    index_of = consoleout.IndexOf(a[i]);
                                    consoleout = consoleout.Remove(index_of, 2);
                                }
                            }

                            if (a[i] == "%X")
                            {
                                if (args[i + 1].Contains("0x"))
                                {

                                    index_of = consoleout.IndexOf(a[i]);
                                    consoleout = consoleout.Insert(index_of,
                                        (args[i + 1].Replace("0x", "")).ToUpper());
                                    index_of = consoleout.IndexOf(a[i]);
                                    consoleout = consoleout.Remove(index_of, 2);
                                }
                                else
                                {
                                    index_of = consoleout.IndexOf(a[i]);
                                    consoleout = consoleout.Insert(index_of,
                                        (Int64.Parse(args[i + 1])).ToString("X").ToString().ToUpper());
                                    index_of = consoleout.IndexOf(a[i]);
                                    consoleout = consoleout.Remove(index_of, 2);
                                }
                            }

                            if (a[i] == "%o")
                            {
                                var bin = Convert.ToInt32(args[i + 1], 10);
                                index_of = consoleout.IndexOf(a[i]);
                                consoleout = consoleout.Insert(index_of, Convert.ToString(bin, 2));
                                index_of = consoleout.IndexOf(a[i]);
                                consoleout = consoleout.Remove(index_of, 2);
                            }

                            if (a[i] == "%f")
                            {
                                var dec = Convert.ToInt32(args[i + 1], 10);
                                var flt = Convert.ToSingle(dec);
                                index_of = consoleout.IndexOf(a[i]);
                                consoleout = consoleout.Insert(index_of, string.Format("{0:F6}", dec));
                                index_of = consoleout.IndexOf(a[i]);
                                consoleout = consoleout.Remove(index_of, 2);
                            }
                        }
                        else
                        {
                            break;
                        }
                    }

                    for (int k = 0; k < a.Count; k++)
                    {
                        if (consoleout.Contains(a[k]))
                        {
                            index_of = consoleout.IndexOf(a[k]);
                            consoleout = consoleout.Insert(index_of, "\0");
                            index_of = consoleout.IndexOf(a[k]);
                            consoleout = consoleout.Remove(index_of, 2);
                        }
                    }
                }
                else
                {
                    int j = 1;
                    int tmp = 0;
                    while (j < args.Length)
                    {
                        if (tmp == a.Count)
                        {
                            tmp = 0;
                            Console.WriteLine(preob(consoleout));
                            consoleout = args[0];
                        }

                        if (a[tmp] == "%s")
                        {
                            index_of = consoleout.IndexOf(a[tmp]);
                            consoleout = consoleout.Insert(index_of, args[j]);
                            index_of = consoleout.IndexOf(a[tmp]);
                            consoleout = consoleout.Remove(index_of, 2);
                        }

                        if (a[tmp] == "%d")
                        {
                            if (args[j].Contains("-0x"))
                            {
                                if (args[j].Replace("-0x", "0x").Contains("0x"))
                                {
                                    index_of = consoleout.IndexOf(a[tmp]);
                                    consoleout = consoleout.Insert(index_of,
                                        "-" + Convert.ToInt32(args[j].Replace("-0x", "0x"), 16).ToString());
                                    index_of = consoleout.IndexOf(a[tmp]);
                                    consoleout = consoleout.Remove(index_of, 2);
                                }
                                else
                                {
                                    var aa = Convert.ToInt32(args[j]);
                                    index_of = consoleout.IndexOf(a[tmp]);
                                    consoleout = consoleout.Insert(index_of,
                                        aa.ToString());
                                    index_of = consoleout.IndexOf(a[tmp]);
                                    consoleout = consoleout.Remove(index_of, 2);
                                }
                            }
                            else
                            {
                                if (args[j].Contains("0x"))
                                {
                                    index_of = consoleout.IndexOf(a[tmp]);
                                    consoleout = consoleout.Insert(index_of,
                                        Convert.ToInt32(args[j], 16).ToString());
                                    index_of = consoleout.IndexOf(a[tmp]);
                                    consoleout = consoleout.Remove(index_of, 2);
                                }
                                else
                                {
                                    var aa = Convert.ToInt32(args[j]);
                                    index_of = consoleout.IndexOf(a[tmp]);
                                    consoleout = consoleout.Insert(index_of,
                                        aa.ToString());
                                    index_of = consoleout.IndexOf(a[tmp]);
                                    consoleout = consoleout.Remove(index_of, 2);
                                }
                            }
                        }
                        if (a[tmp] == "%x")
                        {
                            if (args[j].Contains("0x"))
                            {

                                index_of = consoleout.IndexOf(a[tmp]);
                                consoleout = consoleout.Insert(index_of,
                                    (args[j].Replace("0x", "")).ToUpper());
                                index_of = consoleout.IndexOf(a[tmp]);
                                consoleout = consoleout.Remove(index_of, 2);
                            }
                            else
                            {
                                index_of = consoleout.IndexOf(a[tmp]);
                                consoleout = consoleout.Insert(index_of,
                                    (Int64.Parse(args[j])).ToString("X").ToString().ToLower());
                                index_of = consoleout.IndexOf(a[tmp]);
                                consoleout = consoleout.Remove(index_of, 2);
                            }
                        }

                        if (a[tmp] == "%X")
                        {
                            if (args[j].Contains("0x"))
                            {

                                index_of = consoleout.IndexOf(a[tmp]);
                                consoleout = consoleout.Insert(index_of,
                                    (args[j].Replace("0x", "")).ToUpper());
                                index_of = consoleout.IndexOf(a[tmp]);
                                consoleout = consoleout.Remove(index_of, 2);
                            }
                            else
                            {
                                index_of = consoleout.IndexOf(a[tmp]);
                                consoleout = consoleout.Insert(index_of,
                                    (Int64.Parse(args[j])).ToString("X").ToString().ToUpper());
                                index_of = consoleout.IndexOf(a[tmp]);
                                consoleout = consoleout.Remove(index_of, 2);
                            }
                        }

                        if (a[tmp] == "%o")
                        {
                            var bin = Convert.ToInt32(args[j], 10);
                            index_of = consoleout.IndexOf(a[tmp]);
                            consoleout = consoleout.Insert(index_of, Convert.ToString(bin, 2));
                            index_of = consoleout.IndexOf(a[tmp]);
                            consoleout = consoleout.Remove(index_of, 2);
                        }

                        if (a[tmp] == "%f")
                        {
                            var dec = Convert.ToInt32(args[j], 10);
                            var flt = Convert.ToSingle(dec);
                            index_of = consoleout.IndexOf(a[tmp]);
                            consoleout = consoleout.Insert(index_of, string.Format("{0:F6}", dec));
                            index_of = consoleout.IndexOf(a[tmp]);
                            consoleout = consoleout.Remove(index_of, 2);
                        }
                        tmp++;
                        j++;
                    }
                    for (int k = 0; k < a.Count; k++)
                    {
                        if (consoleout.Contains(a[k]))
                        {
                            index_of = consoleout.IndexOf(a[k]);
                            consoleout = consoleout.Insert(index_of, "\0");
                            index_of = consoleout.IndexOf(a[k]);
                            consoleout = consoleout.Remove(index_of, 2);
                        }
                    }
                }
            }
            Console.WriteLine(preob_5(preob_4(preob_3(preob_2(preob(consoleout.Replace("\\\\", "\\")))))));
            return 0;
        }
    }
}
