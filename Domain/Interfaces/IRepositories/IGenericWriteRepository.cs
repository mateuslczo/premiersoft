namespace BankMore.Domain.Interfaces.IRepositories
{
    /// <summary>
    /// Interface que define metodos de escrita
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IGenericWriteRepository<TEntity, TKey> where TEntity : class
    {
        /// <summary>
        /// Adiciona um registro na tabela.
        /// </summary>
        Task<TEntity> InsertAsync(TEntity entity);

        /// <summary>
        /// Atualiza um registro na tabela.
        /// </summary>
        Task<TEntity> UpdateAsync(TEntity entity);

        /// <summary>
        /// Remove um registro na tabela.
        /// </summary>
        Task<bool> DeleteAsync(TKey id);
    }
}