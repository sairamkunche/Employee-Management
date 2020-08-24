using EmployeeManagement.BusinessLogic.CustomExceptions;
using EmployeeManagement.Contracts.Interfaces;
using EmployeeManagement.DataAccess.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.DataAccess.Repository
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private EmployeeManagementDBContext _context;
        private DbSet<TEntity> dbSet;

        public BaseRepository(EmployeeManagementDBContext context)
        {
            this._context = context;
            this.dbSet = context.Set<TEntity>();
        }
        public void Delete(int id)
        {
            TEntity entityToDelete = this.dbSet.Find(id);
            if (entityToDelete != null)
            {
                this.dbSet.Remove(entityToDelete);
                _context.SaveChanges();
            }
            else
            {
                throw new ItemNotFoundException();
            }
        }

        public IEnumerable<TEntity> GetAll()
        {
            return this.dbSet;
        }

        public TEntity GetById(int id)
        {
            return this.dbSet.Find(id);
        }

        public void Insert(TEntity entity)
        {
            this.dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            this.dbSet.Update(entity);
            _context.SaveChanges();
        }
    }
}
