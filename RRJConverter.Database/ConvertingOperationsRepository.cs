using Microsoft.EntityFrameworkCore;
using RRJConverter.Domain;
using RRJConverter.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRJConverter.Database
{
    public class ConvertingOperationsRepository : IRepository
    {
        private readonly ApplicationContext _applicationContext;

        public ConvertingOperationsRepository(ApplicationContext context)
        {
            _applicationContext = context;
        }

        public void Create(DomainConvertingOperationModel item)
        {
            _applicationContext.ConvertingOperations.Add(item);
        }

        public void Delete(int id)
        {
            var operation = _applicationContext.ConvertingOperations.Find(id);
            if(operation != null)
            {
                _applicationContext.ConvertingOperations.Remove(operation);
            }
        }

        public DomainConvertingOperationModel GetOperation(int id)
        {
            return _applicationContext.ConvertingOperations.Find(id);
        }

        public IEnumerable<DomainConvertingOperationModel> GetOperationList()
        {
            return _applicationContext.ConvertingOperations;
        }

        public void Save()
        {
            _applicationContext.SaveChanges();
        }

        public void Update(DomainConvertingOperationModel item)
        {
            _applicationContext.Entry(item).State = EntityState.Modified;
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _applicationContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
