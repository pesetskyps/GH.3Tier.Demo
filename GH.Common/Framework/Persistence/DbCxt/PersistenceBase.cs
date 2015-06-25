/*
 * Copyright © 2012
 * This code is for the codeproject article "A N-Tier Architecture Sample with ASP.NET MVC3, WCF and Entity Framework" at  
 * http://www.codeproject.com/Articles/434282/A-N-Tier-Architecture-Sample-with-ASP.NET-MVC3-WCF-and-Entity-Framework. 
 * Permission to use, copy or modify this software freely is hereby granted, 
 * provided that this copyright notice appears in the orginal or modified copies. 
 * 
 * This code isn't guaranteed to work correctly; it is your own responsibility for 
 * any result from using this code. 
 *                           
 * @description
 * 
 * @author  
 * @version July 18, 2012
 * @see
 * @since
 */

using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Linq;
using System.Linq.Expressions;
using GH.Common.LogService;
using GH.Common.Framework.Business;

namespace GH.Common.Framework.Persistence.DbCxt
{
    public class PersistenceBase<T> : IPersistence<T> where T : BusinessEntityBase
    {
        protected String _entitySetName = String.Empty;
        public ILogger<PersistenceBase<T>> Logger { get; set; }

        public static ILogger<PersistenceBase<T>> Log
        {
            get { return Log<PersistenceBase<T>>.LogProvider; }
        }

        public static DbContext DataContext
        {
            get { return DataCxt.Cxt; }
        }

        #region IPersistence<T> Members

        public virtual void Insert(T entity, bool commit)
        {
            InsertObject(entity, commit);
        }

        public virtual void Update(T entity, bool commit)
        {
            UpdateObject(entity, commit);
        }

        public virtual void Delete(T entity, bool commit)
        {
            DeleteObject(entity, commit);
        }

        public virtual void Commit()
        {
            SaveChanges();
        }

        public Expression<Func<T, bool>> predicate { get; set; }

        public virtual IQueryable<T> SearchBy(Expression<Func<T, bool>> predicate)
        {
            return EntitySet.Where(predicate);
        }
   
        public virtual IQueryable<T> GetAll()
        {
            return EntitySet;
        }
        #endregion

        protected virtual T FindMatchedOne(T toBeMatched)
        {
            throw new ApplicationException("PersistenceBase.EntitySet: Shouldn't get here.");
        }

        protected virtual IQueryable<T> EntitySet
        {
            get { throw new ApplicationException("PersistenceBase.EntitySet: Shouldn't get here."); }
        }

        protected virtual String EntitySetName
        {
            get { throw new ApplicationException("PersistenceBase.EntitySetName: Shouldn't get here."); }
        }

        protected void InsertObject(T entity, bool commit)
        {
            DataContext.Entry(entity).State = EntityState.Added;
            try
            {
                if (commit) SaveChanges();
            }
            catch (Exception e)
            {
                Log.Error(e);
                throw;
            }
        }

        protected void UpdateObject(T entity, bool commit)
        {
            try
            {
                DbEntityEntry entry = DataContext.Entry(entity);
                DataContext.Entry(entity).State = EntityState.Modified;
                if (commit) SaveChanges();
            }
            catch (InvalidOperationException e) // Usually the error getting here will have a message: "an object with the same key already exists in the ObjectStateManager. The ObjectStateManager cannot track multiple objects with the same key"
            {
                T t = FindMatchedOne(entity);
                if(t==null) throw new ApplicationException("Entity doesn't exist in the repository");
                try
                {
                    DataContext.Entry(t).State = EntityState.Detached;
                    (EntitySet as DbSet<T>).Attach(entity);
                    DataContext.Entry(entity).State = EntityState.Modified;
                    if (commit) SaveChanges();
                } catch(Exception exx)
                {
                    //Roll back
                    DataContext.Entry(entity).State = EntityState.Detached;
                    (EntitySet as DbSet<T>).Attach(t);
                    Log.Error(exx);
                    throw;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }
        }

        protected void DeleteObject(T entity, bool commit)
        {
            T t = FindMatchedOne(entity);
            (EntitySet as DbSet<T>).Remove(t);
            try
            {
                if (commit) SaveChanges();
            }
            catch (Exception e)
            {
                Log.Error(e);
                throw;
            }
        }

        protected void SaveChanges()
        {
            try
            {
                DataContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Update original values from the database (Similar ClientWins in ObjectContext.Refresh)
                var entry = ex.Entries.Single();
                entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                DataContext.SaveChanges(); 
            }
        } // End of function
    }
}