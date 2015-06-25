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
using System.Data.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.ServiceModel.DomainServices.EntityFramework;
using GH.Common.LogService;
using GH.Common.Framework.Business;

namespace GH.Common.Framework.Persistence.ObjectCxt
{
    public class PersistenceBase<T> : IPersistence<T> where T : BusinessEntityBase
    {
        protected String _entitySetName = String.Empty;
        public ILogger<PersistenceBase<T>> Logger { get; set; }

        public static ILogger<PersistenceBase<T>> Log
        {
            get { return Log<PersistenceBase<T>>.LogProvider; }
        }

        public static ObjectContext DataContext
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
            if ((GetEntityState(entity) != EntityState.Detached))
            {
                DataContext.ObjectStateManager.ChangeObjectState(entity, EntityState.Added);
            }
            else
            {
                (EntitySet as ObjectSet<T>).AddObject(entity);
            }

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
                (EntitySet as ObjectSet<T>).AttachAsModified(entity);
                if (commit) SaveChanges();
            }
            catch (InvalidOperationException e) // Usually the error getting here will have a message: "an object with the same key already exists in the ObjectStateManager. The ObjectStateManager cannot track multiple objects with the same key"
            {
                T t = FindMatchedOne(entity);
                if (t == null) throw new ApplicationException("Entity doesn't exist in the repository");
                try
                {
                    DataContext.Detach(t);
                    (EntitySet as ObjectSet<T>).AttachAsModified(entity);
                    if (commit) SaveChanges();
                }
                catch (Exception exx)
                {
                    //Roll back
                    DataContext.Detach(entity);
                    (EntitySet as ObjectSet<T>).Attach(t);
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
            if ((GetEntityState(entity) != EntityState.Detached))
            {
                DataContext.ObjectStateManager.ChangeObjectState(entity, EntityState.Deleted);
            }
            else
            {
                (EntitySet as ObjectSet<T>).Attach(entity);
                (EntitySet as ObjectSet<T>).DeleteObject(entity);
            }

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

        private static EntityState GetEntityState(T entity)
        {
            ObjectStateEntry stateEntry = null;
            if ((DataContext.ObjectStateManager.TryGetObjectStateEntry(entity, out stateEntry) == false))
            {
                return EntityState.Detached;
            }
            return stateEntry.State;
        }

        protected void SaveChanges()
        {
            try
            {
                DataContext.SaveChanges();
            }
            catch (OptimisticConcurrencyException e)
            {
                // Resolve the concurrency conflict by refreshing the  
                // object context before re-saving changes. 
                DataContext.Refresh(RefreshMode.ClientWins, EntitySet);
                DataContext.SaveChanges();
            }
        } // End of function
    }
}