namespace BankMore.Domain.Interfaces.IRepositories
{
    /// <summary>
    /// Interface que define metodos de leitura
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IReadRepository<TEntity, TKey> where TEntity : class
    {
        /// <summary>
        /// Recupera um registro pela sua chave primária.
        /// </summary>
        /// <param name="id">identificador</param>
        /// <returns>entidade ou null</returns>
        Task<TEntity> GetRecordByIdAsync(TKey id);

        /// <summary>
        /// Retorna uma lista de registros.
        /// </summary>
        /// <returns>lista de registros</returns>
        Task<IEnumerable<TEntity>> GetAllRecordAsync();

        /// <summary>
        /// Verifica se um determinado registro existe com base no Id.
        /// </summary>
        /// <returns>true(existe)/false(não existe)</returns>
        Task<bool> HasRecordAsync(TKey id);
    }
}
