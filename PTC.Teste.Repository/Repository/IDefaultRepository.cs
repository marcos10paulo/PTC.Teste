using System.Linq;

namespace PTC.Teste.Repository.Repository
{
    public interface IDefaultRepository<T> where T : class
    {
        string Incluir(T entity);
        string Alterar(T entity);
        string Excluir(int id);
        T Selecionar(int id);
        IQueryable<T> SelecionarTodos();
        IQueryable<T> Filtrar(string condicao);
    }
}
