using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace dotnet.openvehicletracker.org.Tests.Controllers
{
    class MockDbSet<T> : System.Data.Entity.IDbSet<T>, IEnumerable<T> where T : class, new()
    {
        public delegate void UpdateComplexAssociationsHandler(T entity);
        public event UpdateComplexAssociationsHandler UpdateComplexAssociations;

        internal Dictionary<T, MockContext.state> repo = new Dictionary<T, MockContext.state>();

        public T Add(T entity)
        {
            if (!repo.ContainsKey(entity))
            {
                repo.Add(entity, MockContext.state.add);
                OnAdd(entity);
            }
            return entity;
        }

        void OnAdd(T entity)
        {
            if (UpdateComplexAssociations != null) // if there are any subscribers...
                UpdateComplexAssociations(entity); // call the event, passing through T
        }
        
        public T Attach(T entity)
        {
            throw new NotImplementedException();
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T
        {
            return Activator.CreateInstance<TDerivedEntity>();
        }

        public T Create()
        {
            return new T();
        }

        public T Find(params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        public System.Collections.ObjectModel.ObservableCollection<T> Local
        {
            get { throw new NotImplementedException(); }
        }

        public T Remove(T entity)
        {
            if (repo.Keys.Contains(entity))
            {
                repo[entity] = MockContext.state.delete;
                return entity;
            }
            return default(T);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return repo.Where(m => m.Value != MockContext.state.delete).Select(m => m.Key).GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return repo.Where(m => m.Value != MockContext.state.delete).Select(m => m.Key).GetEnumerator();
        }

        public Type ElementType
        {
            get
            {
                return typeof(T);
            }
        }

        public System.Linq.Expressions.Expression Expression
        {
            get { return repo.Where(m => m.Value != MockContext.state.delete).Select(m => m.Key).ToList().AsQueryable().Expression; }
        }

        public IQueryProvider Provider
        {
            get { return repo.Where(m => m.Value != MockContext.state.delete).Select(m => m.Key).ToList().AsQueryable().Provider; }
        }
    }
}
