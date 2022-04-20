using System;
using System.Collections.Generic;

namespace WebStore.Models
{
    public interface IRepository<T>
    {
        void Add(T newEntity);
        IEnumerable<T> FindAll();
        IEnumerable<T> FindBy(Func<T, bool> predicate);
        T FindById(int id);
        void Remove(T entity);
    }
}