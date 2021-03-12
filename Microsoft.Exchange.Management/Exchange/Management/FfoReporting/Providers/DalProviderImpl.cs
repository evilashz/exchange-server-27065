using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.FfoReporting.Common;

namespace Microsoft.Exchange.Management.FfoReporting.Providers
{
	// Token: 0x02000405 RID: 1029
	internal class DalProviderImpl : IDalProvider
	{
		// Token: 0x06002422 RID: 9250 RVA: 0x00090409 File Offset: 0x0008E609
		internal DalProviderImpl() : this(new Func<string, object>(DalProviderImpl.CreateTargetObject), 5000)
		{
		}

		// Token: 0x06002423 RID: 9251 RVA: 0x00090422 File Offset: 0x0008E622
		internal DalProviderImpl(Func<string, object> activator, int maxPageSize)
		{
			this.createTargetFunction = activator;
			this.maxPageSizeForPagedRequest = maxPageSize;
		}

		// Token: 0x06002424 RID: 9252 RVA: 0x00090438 File Offset: 0x0008E638
		public IEnumerable GetSingleDataPage(string targetObjectTypeName, string dalObjectTypeName, string methodName, QueryFilter queryFilter)
		{
			object targetInstance = this.createTargetFunction(targetObjectTypeName);
			MethodInfo retrievalMethod = this.GetRetrievalMethod(targetInstance, dalObjectTypeName, methodName);
			return (IEnumerable)this.RetrieveDalObjects(retrievalMethod, targetInstance, queryFilter);
		}

		// Token: 0x06002425 RID: 9253 RVA: 0x0009046C File Offset: 0x0008E66C
		public IEnumerable GetAllDataPages(string targetObjectTypeName, string dalObjectTypeName, string methodName, QueryFilter queryFilter)
		{
			object targetInstance = this.createTargetFunction(targetObjectTypeName);
			MethodInfo retrievalMethod = this.GetRetrievalMethod(targetInstance, dalObjectTypeName, methodName);
			IReadOnlyList<ComparisonFilter> nonPagingFilters = this.GetNonPagingFilters(queryFilter);
			List<object> list = new List<object>();
			int num = this.maxPageSizeForPagedRequest;
			int num2 = 1;
			while (num == this.maxPageSizeForPagedRequest)
			{
				QueryFilter filter = this.BuildPagingQueryFilter(nonPagingFilters, num2);
				int count = list.Count;
				list.AddRange((IEnumerable<object>)this.RetrieveDalObjects(retrievalMethod, targetInstance, filter));
				num = list.Count - count;
				num2++;
			}
			return list;
		}

		// Token: 0x06002426 RID: 9254 RVA: 0x000904F0 File Offset: 0x0008E6F0
		private object RetrieveDalObjects(MethodInfo method, object targetInstance, QueryFilter filter)
		{
			object result = null;
			try
			{
				FaultInjection.FaultInjectionTracer.TraceTest(4270206269U);
				result = Schema.Utilities.Invoke(method, targetInstance, new object[]
				{
					filter
				});
			}
			catch (Exception ex)
			{
				throw new DalRetrievalException(string.Format("Failed to retrieve DAL objects: {0}", ex), ex.InnerException);
			}
			return result;
		}

		// Token: 0x06002427 RID: 9255 RVA: 0x00090550 File Offset: 0x0008E750
		private static object CreateTargetObject(string targetInstanceTypeName)
		{
			return Activator.CreateInstance("Microsoft.Exchange.Hygiene.Data", targetInstanceTypeName).Unwrap();
		}

		// Token: 0x06002428 RID: 9256 RVA: 0x00090564 File Offset: 0x0008E764
		private MethodInfo GetRetrievalMethod(object targetInstance, string objectTypeName, string methodName)
		{
			Type type = Type.GetType(objectTypeName);
			MethodInfo method = targetInstance.GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public);
			return method.MakeGenericMethod(new Type[]
			{
				type
			});
		}

		// Token: 0x06002429 RID: 9257 RVA: 0x000905D4 File Offset: 0x0008E7D4
		private IReadOnlyList<ComparisonFilter> GetNonPagingFilters(QueryFilter originalFilter)
		{
			string pagePropertyName = Schema.Utilities.GetSchemaPropertyDefinition("PageQueryDefinition").Name;
			string pageSizePropertyName = Schema.Utilities.GetSchemaPropertyDefinition("PageSizeQueryDefinition").Name;
			List<ComparisonFilter> list = new List<ComparisonFilter>();
			CompositeFilter compositeFilter = originalFilter as CompositeFilter;
			if (compositeFilter != null)
			{
				list.AddRange(from ComparisonFilter filter in compositeFilter.Filters
				where filter.Property.Name != pagePropertyName && filter.Property.Name != pageSizePropertyName
				select filter);
			}
			list.Add(new ComparisonFilter(ComparisonOperator.Equal, Schema.Utilities.GetSchemaPropertyDefinition("PageSizeQueryDefinition"), this.maxPageSizeForPagedRequest));
			return list;
		}

		// Token: 0x0600242A RID: 9258 RVA: 0x0009066C File Offset: 0x0008E86C
		private QueryFilter BuildPagingQueryFilter(IEnumerable<ComparisonFilter> baseFilters, int page)
		{
			return new AndFilter(new List<ComparisonFilter>(baseFilters)
			{
				new ComparisonFilter(ComparisonOperator.Equal, Schema.Utilities.GetSchemaPropertyDefinition("PageQueryDefinition"), page)
			}.ToArray<QueryFilter>());
		}

		// Token: 0x04001CD1 RID: 7377
		private readonly Func<string, object> createTargetFunction;

		// Token: 0x04001CD2 RID: 7378
		private readonly int maxPageSizeForPagedRequest;
	}
}
