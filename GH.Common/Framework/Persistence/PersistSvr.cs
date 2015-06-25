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
using System.Linq;
using System.Linq.Expressions;
using GH.Common.ServiceLocator;

namespace GH.Common.Framework.Persistence
{
    public static class PersistSvr<T> where T : class /*, INotifyPropertyChanged*/
    {
        private static IPersistence<T> _provider;

        public static IPersistence<T> PersistenceProvider
        {
            get
            {
                if (_provider == null) _provider = ServiceLocator<IPersistence<T>>.GetService();
                return _provider;
            }
            set { _provider = value; }
        }

        public static void Insert(T entity, bool commit)
        {
            PersistenceProvider.Insert(entity, commit);
        }

        public static void Update(T entity, bool commit)
        {
            PersistenceProvider.Update(entity, commit);
        }

        public static void Delete(T entity, bool commit)
        {
            PersistenceProvider.Delete(entity, commit);
        }

        public static void Commit()
        {
            PersistenceProvider.Commit();
        }


        public static IQueryable<T> SearchBy(Expression<Func<T, bool>> predicate)
        {
            return PersistenceProvider.SearchBy(predicate);
        }

        public static IQueryable<T> GetAll()
        {
            return PersistenceProvider.GetAll();
        }
    }
}