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
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Data.Services.Client;
using System.Linq;
using System.Linq.Expressions;
using GH.Common.LogService;
using GH.Common.Framework.Business;
using MergeOption = System.Data.Services.Client.MergeOption;

namespace GH.Common.Framework.Persistence.DataServiceCxt
{
    public class PersistenceBase<T> : IPersistence<T> where T : BusinessEntityBase
    {
        protected String _entitySetName;
        public ILogger<PersistenceBase<T>> Logger { get; set; }

        public static ILogger<PersistenceBase<T>> Log
        {
            get { return Log<PersistenceBase<T>>.LogProvider; }
        }

        /// <summary>
        ///   For non-proxy way, please generate the DataContext by below way, then assign it to DataContext:  
        ///   string dataServiceUrl = ConfigurationSettings.AppSettings["DataServiceUrl"];
        ///   DataContext = new DataServiceCxt(new Uri(dataServiceUrl));
        /// </summary>
        public static DataServiceContext DataContext
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
            DataContext.SaveChanges();
        }

        public virtual IQueryable<T> SearchBy(Expression<Func<T, bool>> predicate)
        {
            return EntitySet.Where(predicate).ToList().AsQueryable();
        }
        
        public virtual IQueryable<T> GetAll()
        {
            return EntitySet;
        }
        #endregion

        protected virtual IQueryable<T> EntitySet
        {
            get { throw new ApplicationException("PersistenceBase.EntitySet: Shouldn't get here."); }
        }

        protected virtual T FindMatchedOne(T toBeMatched)
        {
            throw new ApplicationException("PersistenceBase.EntitySet: Shouldn't get here.");
        }

        protected virtual String EntitySetName
        {
            get { throw new ApplicationException("PersistenceBase.EntitySetName: Shouldn't get here."); }
        }

        protected void InsertObject(T entity, bool commit)
        {
            DataContext.AddObject(EntitySetName, entity);
            try
            {
                if (commit) DataContext.SaveChanges();
            }
            catch (Exception e)
            {
                //Need to restore back the original data in the client side if DataContext.DataContext.SaveChanges() fails; otherwise, next time, these bad data is still here                
                DataContext.DeleteObject(entity);
                Log.Error(e);
                throw;
            }
        }

        protected void UpdateObject(T entity, bool commit)
        {
            T t = FindMatchedOne(entity);
            if (t == null) throw new ApplicationException("Entity doesn't exist in the repository");
            try
            {
                DataContext.UpdateObject(entity);
            } catch(ArgumentException e) // this usually means Context didn't track this entity, so we need to detach the old one and attach this one
            {  
                try
                {
                    DataContext.Detach(t);
                    DataContext.AttachTo(EntitySetName, entity);
                    DataContext.UpdateObject(entity);
                }
                catch (Exception exx)
                {
                    //Roll back
                    DataContext.Detach(entity);
                    DataContext.AttachTo(EntitySetName, t);
                    Log.Error(exx);
                    throw;
                }
            }
            try
            {
                if (commit) DataContext.SaveChanges();
            }
            catch (DataServiceRequestException ex)
            {
                var response = ex.Response.FirstOrDefault();
                if (response != null && (response.StatusCode == 412 || response.StatusCode == 409)) //Concurrency Exception - PreCondition Failed (412) or Conflict occured (409)
                {
                    MergeOption oldOp = DataContext.MergeOption;
                    DataContext.MergeOption = MergeOption.PreserveChanges;
                    entity = t;
                    DataContext.SaveChanges();
                    DataContext.MergeOption = oldOp;
                }                
            }
            catch (Exception ex)
            {
                //Need to restore back the original data in the client side if DataContext.DataContext.SaveChanges() fails; otherwise, next time, these bad data is still here
                MergeOption oldOp = DataContext.MergeOption;
                DataContext.MergeOption = MergeOption.OverwriteChanges;
                entity = t;
                DataContext.MergeOption = oldOp;                
                Log.Error(ex);
                throw;
            }
        }

        protected void DeleteObject(T entity, bool commit)
        {
            T t = FindMatchedOne(entity);
            DataContext.DeleteObject(t);
            try
            {
                if (commit) DataContext.SaveChanges();
            }
            catch (Exception e)
            {
                //Need to restore back the original data in the client side if DataContext.DataContext.SaveChanges() fails; otherwise, next time, these bad data is still here                
                DataContext.AddObject(EntitySetName, entity);
                Log.Error(e);
                throw;
            }
        }
    }
}