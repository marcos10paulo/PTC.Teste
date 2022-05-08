using PTC.Teste.Common.Interface;
using System;

namespace PTC.Teste.Common.Services
{
    public class Functions : IFunctions
    {

        public string ConfigureStringCondition(string pesquisa, string campo)
        {
            if (pesquisa.Equals("%"))
                return "(0 == 0)";
            else if (pesquisa.Substring(0, 1) == "%")
                return string.Format("{0}.ToLower().EndsWith({1}.ToLower())", campo, (char)34 + pesquisa.Replace("%", "") + (char)34);
            else if (pesquisa.Substring(pesquisa.Length - 1, 1) == "%")
                return string.Format("{0}.ToLower().StartsWith({1}.ToLower())", campo, (char)34 + pesquisa.Replace("%", "") + (char)34);
            else
                return string.Format("{0}.ToLower().Contains({1}.ToLower())", campo, (char)34 + pesquisa + (char)34);
        }

        public bool IsDouble(string s)
        {
            double resultado = 0;
            return double.TryParse(s, out resultado);
        }

        public string ComaToPoint(string s)
        {
            return s.Replace(',', '.');
        }

        public bool IsDateTime(string s)
        {
            DateTime resultado;
            return DateTime.TryParse(s, out resultado) && (s.IndexOf("/") > 0);
        }

        public string ConfigureDateCondition(string sData, string sCampo)
        {
            string sCondicao = "";
            int dia = 0;
            int mes = 0;
            int ano = 0;
            int i = 1;
            foreach (string s in sData.Split('/'))
            {
                if (IsNumberInt32(s))
                {
                    if (i == 1)
                    {
                        if (Convert.ToInt32(s) >= 1 && Convert.ToInt32(s) <= 31)
                            dia = Convert.ToInt32(s);
                    }
                    else if (i == 2)
                    {
                        if (Convert.ToInt32(s) >= 1 && Convert.ToInt32(s) <= 12)
                            mes = Convert.ToInt32(s);
                    }
                    else if (i == 3)
                        ano = Convert.ToInt32(s);
                }
                i++;
            }
            if ((dia != 0) || (mes != 0) || (ano != 0))
            {
                if (dia != 0)
                    sCondicao += sCampo + ".Day == " + dia.ToString();
                if (mes != 0)
                    sCondicao += (sCondicao != "" ? " and " : "") + sCampo + ".Month == " + mes.ToString();
                if (ano != 0)
                    sCondicao += (sCondicao != "" ? " and " : "") + sCampo + ".Year == " + ano.ToString();
            }
            return "(" + sCondicao + ")";
        }

        public bool IsTimeSpan(string s)
        {
            TimeSpan resultado;
            return TimeSpan.TryParse(s, out resultado);
        }

        public string ConfigureTimeCondition(string sHora, string sCampo)
        {
            string sCondicao = "";
            int hora = 0;
            int minuto = 0;
            int segundo = 0;
            int i = 1;

            foreach (string s in sHora.Split(':'))
            {
                if (IsNumberInt32(s))
                {
                    if (i == 1)
                    {
                        if (Convert.ToInt32(s) >= 0 && Convert.ToInt32(s) <= 23)
                            hora = Convert.ToInt32(s);
                    }
                    else if (i == 2)
                    {
                        if (Convert.ToInt32(s) >= 0 && Convert.ToInt32(s) <= 59)
                            minuto = Convert.ToInt32(s);
                    }
                    else if (i == 3)
                    {
                        if (Convert.ToInt32(s) >= 0 && Convert.ToInt32(s) <= 59)
                            segundo = Convert.ToInt32(s);
                    }
                }
                i++;
            }
            if ((hora != 0) || (minuto != 0) || (segundo != 0))
            {
                if (hora != 0)
                    sCondicao += sCampo + ".Hours == " + hora.ToString();
                if (minuto != 0)
                    sCondicao += (sCondicao != "" ? " and " : "") + sCampo + ".Minutes == " + minuto.ToString();
                if (segundo != 0)
                    sCondicao += (sCondicao != "" ? " and " : "") + sCampo + ".Seconds == " + segundo.ToString();
            }
            return sCondicao;
        }

        public bool IsNumberInt32(string s)
        {
            int resultado = 0;
            return int.TryParse(s, out resultado);
        }

        public string RemoveNaoNumericos(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"[^0-9]");
                string ret = reg.Replace(text, string.Empty);
                return ret;
            }

            return null;
        }

        public bool ValidaCPF(string cpf)
        {
            //Remove formatação do número, ex: "123.456.789-01" vira: "12345678901"
            cpf = this.RemoveNaoNumericos(cpf);

            if (cpf.Length > 11)
                return false;

            while (cpf.Length != 11)
                cpf = '0' + cpf;

            bool igual = true;
            for (int i = 1; i < 11 && igual; i++)
                if (cpf[i] != cpf[0])
                    igual = false;

            if (igual || cpf == "12345678909")
                return false;

            int[] numeros = new int[11];

            for (int i = 0; i < 11; i++)
                numeros[i] = int.Parse(cpf[i].ToString());

            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += (10 - i) * numeros[i];

            int resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)
                    return false;
            }
            else if (numeros[9] != 11 - resultado)
                return false;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += (11 - i) * numeros[i];

            resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[10] != 0)
                    return false;
            }
            else
                if (numeros[10] != 11 - resultado)
                return false;

            return true;
        }

        public bool ValidarEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }
    }
}
