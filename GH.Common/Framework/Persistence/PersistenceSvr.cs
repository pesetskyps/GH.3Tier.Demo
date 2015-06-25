using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using GH.Common.ServiceLocator;

namespace GH.Common.Framework.Persistence
{
    public static class PersistenceSvr<T> where T : class/*, INotifyPropertyChanged*/ 
    {
        static private IPersistence<T> _provider = null;
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
