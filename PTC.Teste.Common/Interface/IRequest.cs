using System;
using System.Collections.Generic;

namespace PTC.Teste.Common.Interface
{
    public interface IRequest
    {
        T Get<T>(string entidade, string metodo, Dictionary<string, IConvertible> parametros = null);
        string Post<T>(T model, string entidade, string metodo) where T : class;
        T Post<T>(string entidade, string metodo, string condicao, int? usuarioId) where T : class;
        string Delete(string entidade, string metodo, int id);
    }
}
