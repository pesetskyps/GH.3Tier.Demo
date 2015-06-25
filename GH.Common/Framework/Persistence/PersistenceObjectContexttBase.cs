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
using GH.Common.Domain.Entities;
using GH.Common.LogService;
using GH.Common.ServiceRef;
using GH.Common.Utils;
using GH.Framework.DomainEntities;


namespace GH.Framework.Persistence
{

    public class PersistenceObjectContexttBase<T> : IPersistence<T> where T : DomainEntityBase
    {
        public ILogger<PersistenceObjectContexttBase<T>> Logger { get; set; }

        static public ILogger<PersistenceObjectContexttBase<T>> Log
        {
            get
            {
                return Log<PersistenceObjectContexttBase<T>>.LogProvider;
            }
        }

        public static ObjectContext ObjectContext { get; set; }

        public PersistenceObjectContexttBase()
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
            ObjectContext.SaveChanges();
        }

        virtual public IQueryable<T> SearchBy(Expression<Func<T, bool>> predicate)
        {
            return EntitySet.Where(predicate).ToList();
        }

        
        virtual public IQueryable<T> GetAll()
        {
            return EntitySet.ToList();
        }

        #endregion

        virtual protected ObjectSet<T> EntitySet
        {
            get { throw new ApplicationException("PersistenceObjectContexttBase.EntitySet: Shouldn't get here."); }
        }

        virtual protected String EntitySetName
        {
            get { throw new ApplicationException("PersistenceObjectContexttBase.EntitySetName: Shouldn't get here."); }
        }


        protected void InsertObject(T entity, bool commit)
        {
            if ((GetEntityState(entity) != EntityState.Detached))
            {
                ObjectContext.ObjectStateManager.ChangeObjectState(entity, EntityState.Added);
            }
            else
            {
                EntitySet.AddObject(entity);
            }        

            try
            {
                if (commit) ObjectContext.SaveChanges();
            }
            catch (Exception e)
            {
                //Need to restore back the original data if ObjectContext.SaveChanges() fails; otherwise, next time, these bad data is still here
                //ObjectContext.LoadProperty(ObjectContext, EntitySetName);
                ObjectContext.DeleteObject(entity);
                Log.Error(e);
                throw;
            }
        }

        protected void UpdateObject(T entity, bool commit)
        {
            EntitySet.AttachAsModified(entity);
            try
            {
                if (commit) ObjectContext.SaveChanges();
            }
            catch (Exception e)
            {
                //Need to restore back the original data if ObjectContext.SaveChanges() fails; otherwise, next time, these bad data is still here
                ObjectContext.LoadProperty(ObjectContext, EntitySetName);
                Log.Error(e);
                throw;
            }
        }

        protected void DeleteObject(T entity, bool commit)
        {
            if ((GetEntityState(entity) != EntityState.Detached))
            {
                ObjectContext.ObjectStateManager.ChangeObjectState(entity, EntityState.Deleted);
            }
            else
            {
                EntitySet.Attach(entity);
                EntitySet.DeleteObject(entity);
            }            

            try
            {
                if (commit) ObjectContext.SaveChanges();
            }
            catch (Exception e)
            {
                //Need to restore back the original data if ObjectContext.SaveChanges() fails; otherwise, next time, these bad data is still here
                ObjectContext.LoadProperty(ObjectContext, EntitySetName);
                //ObjectContext.AddObject(EntitySetName, entity);
                Log.Error(e);
                throw;
            }
        }

        private static EntityState GetEntityState(T entity)
        {
            ObjectStateEntry stateEntry = null;
            if ((ObjectContext.ObjectStateManager.TryGetObjectStateEntry(entity, out stateEntry) == false))
            {
                return EntityState.Detached;
            }
            return stateEntry.State;
        }
    }
}
