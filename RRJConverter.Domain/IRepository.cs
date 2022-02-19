using RRJConverter.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RRJConverter.Domain
{
    public interface IRepository : IDisposable
    {
        IEnumerable<DomainConvertingOperationModel> GetBookList();
        DomainConvertingOperationModel GetBook(int id);
        void Create(DomainConvertingOperationModel item);
        void Update(DomainConvertingOperationModel item);
        void Delete(int id);
        void Save();
    }
}
