using System.Linq.Expressions;

namespace DbAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {

        // Crud 
        // Create
        void Add(T entity);
        // Read
        T Get(int id);
        //Read All
        IEnumerable<T> GetAll();
        // Update
        void Update(T entity);
        // Delete
        void Delete(int id);

        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

    }
}

