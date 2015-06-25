using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.Services.Client;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Data.Objects;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.ServiceModel.DomainServices.EntityFramework; 
using GH.Common.LogService;
using GH.Common.Utils;
using GH.Common.Framework.DomainEntities;


namespace GH.Common.Framework.Persistence.ObjectContext
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

        public static System.Data.Objects.ObjectContext DataContext { get; set; }

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
                if (commit) DataContext.SaveChanges();
            }
            catch (Exception e)
            {
                //Need to restore back the original data if DataContext.SaveChanges() fails; otherwise, next time, these bad data is still here
                //DataContext.LoadProperty(DataContext, EntitySetName);
                DataContext.DeleteObject(entity);
                Log.Error(e);
                throw;
            }
        }

        protected void UpdateObject(T entity, bool commit)
        {
            (EntitySet as ObjectSet<T>).AttachAsModified(entity);
            try
            {
                if (commit) DataContext.SaveChanges();
            }
            catch (Exception e)
            {
                //Need to restore back the original data if DataContext.SaveChanges() fails; otherwise, next time, these bad data is still here
                DataContext.LoadProperty(DataContext, EntitySetName);
                Log.Error(e);
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
                if (commit) DataContext.SaveChanges();
            }
            catch (Exception e)
            {
                //Need to restore back the original data if DataContext.SaveChanges() fails; otherwise, next time, these bad data is still here
                DataContext.LoadProperty(DataContext, EntitySetName);
                //DataContext.AddObject(EntitySetName, entity);
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
    }
}
