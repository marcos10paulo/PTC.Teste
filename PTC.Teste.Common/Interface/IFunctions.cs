using System;

namespace PTC.Teste.Common.Interface
{
    public interface IFunctions
    {
        string ConfigureStringCondition(string pesquisa, string campo);
        bool IsDouble(string s);
        string ComaToPoint(string s);
        bool IsDateTime(string s);
        string ConfigureDateCondition(string sData, string sCampo);
        bool IsTimeSpan(string s);
        string ConfigureTimeCondition(string sHora, string sCampo);
        bool IsNumberInt32(string s);
        string RemoveNaoNumericos(string text);
        bool ValidaCPF(string cpf);
        bool ValidarEmail(string email);
    }
}
