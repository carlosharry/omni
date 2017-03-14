using Omni.Core.Service;
using Omni.ServiceProviders.EntityFramework.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Omni.ServiceProviders.EntityFramework.Service
{
    public class EntityDataService<T> : IDataService<T> where T : class
    {
        public DbContext Context { get; private set; }

        public EntityDataService(DbContext context)
        {
            this.Context = context ?? throw new ArgumentNullException("The DbContext cannot be null");
        }

        public T Create(T newItem)
        {
            var item = Context.Set<T>().Add(newItem);
            return item;

        }

        public void Delete(T newItem)
        {
            Context.Set<T>().Remove(newItem);

        }

        public IEnumerable<T> Read(IEnumerable<KeyValuePair<string, string>> items)
        {
            if(items == null || items.Count() == 0)
            {
                return Context.Set<T>().ToList();
            }

            var theSet = Context.Set<T>();

            var tableProperties = typeof(T).GetProperties();

            var matchingData = MatchKeysWithProperties(tableProperties, items);

            IQueryable<T> theQuery = theSet;

            foreach(var data in matchingData)
            {
                theQuery = StringSearch(theQuery, data);
            }

            return theQuery;
        }

        public static IQueryable<T> StringSearch(IQueryable<T> query, DynamicSearchModel model)
        {

            ParameterExpression e = Expression.Parameter(typeof(T), model.objectProperty.Name);
            MemberExpression m = Expression.MakeMemberAccess(e, model.objectProperty);

            Expression buildingExpression = m;

            if (model.objectProperty.PropertyType == typeof(string))
            {
                Expression callExpr = Expression.Call(buildingExpression, typeof(object).GetMethod("ToString", new Type[] { }));
                Expression callExpr1 = Expression.Call(callExpr, typeof(string).GetMethod("ToLower", new Type[] { }));
                buildingExpression = Expression.Call(callExpr1, typeof(string).GetMethod("Trim", new Type[] { }));
            }

            var Constants = new List<Expression>();

            foreach (var prop in model.Values)
            {
                Expression expression = Expression.Constant(prop, prop.GetType());

                expression = Expression.Convert(expression, model.objectProperty.PropertyType);

                Constants.Add(Expression.Call(buildingExpression, model.objectProperty.PropertyType.GetMethod("Equals", new Type[] { model.objectProperty.PropertyType }), expression));
            }

            Expression chainedOrExp = null;

            foreach (var binExp in Constants)
            {

                if (chainedOrExp == null)
                {
                    chainedOrExp = binExp;
                    continue;
                }
                else
                {
                    chainedOrExp = Expression.OrElse(chainedOrExp, binExp);
                    continue;
                }
            }

            if (chainedOrExp == null)
            {
                chainedOrExp = Expression.Constant(true);
            }

            Expression<Func<T, bool>> lambda = Expression.Lambda<Func<T, bool>>(chainedOrExp, e);
            return query.Where(lambda);
        }



        private IEnumerable<DynamicSearchModel> MatchKeysWithProperties(PropertyInfo[] properties, IEnumerable<KeyValuePair<string, string>> items)
        {
            var theItems = new Dictionary<PropertyInfo,DynamicSearchModel>();

            var quickProperties = properties.Select(x => new { Name = x.Name.ToLower(), Property = x });

            foreach(var prop in items)
            {
                var matchingProp = quickProperties.SingleOrDefault(x => x.Name == prop.Key);

                if(matchingProp == null)
                {
                    throw new Exception("Unable to find a property that matches the property name.");
                }

                var result = theItems.TryGetValue(matchingProp.Property, out var value);

                if (result)
                {
                    value.Values.Add(prop.Value);
                }
                else
                {
                    theItems.Add(matchingProp.Property, value = new DynamicSearchModel { objectProperty = matchingProp.Property, Values = { prop.Value } });
                }
            }

            return theItems.Select(x => x.Value).ToList();
        }

        public T Update(T editedItem)
        {
            Context.Entry(editedItem).State = EntityState.Modified;
            return editedItem;
        }

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
