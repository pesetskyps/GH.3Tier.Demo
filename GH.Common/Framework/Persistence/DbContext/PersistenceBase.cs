using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.Services.Client;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity; 
using GH.Common.LogService;
using GH.Common.Utils;
using GH.Common.Framework.DomainEntities;
using GH.Common.LogService;
using GH.Common.Utils;
using GH.Common.Framework.DomainEntities;


namespace GH.Common.Framework.Persistence.DbContext
{

    public class PersistenceBase<T> : IPersistence<T> where T : class /*DomainEntityBase*/
    {
        protected String _entitySetName = String.Empty;
        public ILogger<PersistenceBase<T>> Logger { get; set; }

        static public ILogger<PersistenceBase<T>> Log
        {
            get
            {
                return Log<PersistenceBase<T>>.LogProvider;
            }
        }

        public static System.Data.Entity.DbContext DataContext { get; set; }

        public PersistenceBase()
        {
        }

        #region IPersistence<T> Members

        virtual public void Insert(T entity, bool commit)
        {
            InsertObject(entity, commit);
        }

        virtual public void Update(T entity, bool commit)
        {
            UpdateObject(entity, commit);
        }

        virtual public void Delete(T entity, bool commit)
        {
            DeleteObject(entity, commit);
        }

        virtual public void Commit()
        {
            DataContext.SaveChanges();
        }

        virtual public IQueryable<T> SearchBy(Expression<Func<T, bool>> predicate)
        {
            return EntitySet.Where(predicate);
        }

        
        virtual public IQueryable<T> GetAll()
        {
            return EntitySet;
        }

        virtual public T SearchById(object id)
        {
            return null;
        }

        #endregion

        virtual protected IQueryable<T> EntitySet
        {
            get { throw new ApplicationException("PersistenceBase.EntitySet: Shouldn't get here."); }
        }

        virtual protected String EntitySetName
        {
            get { throw new ApplicationException("PersistenceBase.EntitySetName: Shouldn't get here."); }
        }

        protected void InsertObject(T entity, bool commit)
        {
            DataContext.Entry(entity).State = EntityState.Added;
            try
            {
                if (commit) DataContext.SaveChanges();
            }
            catch (Exception e)
            {
                //Need to restore back the original data if DataContext.SaveChanges() fails; otherwise, next time, these bad data is still here
                //DataContext.LoadProperty(DataContext, EntitySetName);
                (EntitySet as DbSet<T>).Remove(entity);
                Log.Error(e);
                throw;
            }
        }

        protected void UpdateObject(T entity, bool commit)
        {
            DataContext.Entry(entity).State = EntityState.Modified;
            try
            {
                if (commit) DataContext.SaveChanges();
            }
            catch (Exception e)
            {
                //Need to restore back the original data if DataContext.SaveChanges() fails; otherwise, next time, these bad data is still here
                //EntitySet.Load();
                Log.Error(e);
                throw;
            }
        }

        protected void DeleteObject(T entity, bool commit)
        {
            (EntitySet as DbSet<T>).Remove(entity);
            try
            {
                if (commit) DataContext.SaveChanges();
            }
            catch (Exception e)
            {
                //Need to restore back the original data if DataContext.SaveChanges() fails; otherwise, next time, these bad data is still here
                //DataContext.LoadProperty(DataContext, EntitySetName);
                //DataContext.AddObject(EntitySetName, entity);
                Log.Error(e);
                throw;
            }
        }
    }
}
