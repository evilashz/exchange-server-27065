using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x020000D7 RID: 215
	internal class FfoPagedReader<T> : ADPagedReader<T> where T : IConfigurable, new()
	{
		// Token: 0x060007A8 RID: 1960 RVA: 0x00019364 File Offset: 0x00017564
		public FfoPagedReader(IDirectorySession session, QueryFilter filter, ADObjectId rootId, int pageSize) : this(session, rootId, QueryScope.SubTree, filter, null, pageSize, null, false)
		{
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x00019380 File Offset: 0x00017580
		public FfoPagedReader(IDirectorySession session, QueryFilter filter, ADObjectId rootId) : this(session, rootId, QueryScope.SubTree, filter, null, ADGenericPagedReader<T>.DefaultPageSize, null, false)
		{
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x000193A0 File Offset: 0x000175A0
		public FfoPagedReader(IDirectorySession session, ADObjectId rootId, QueryScope queryScope, QueryFilter filter, SortBy sortBy, int pageSize, IEnumerable<PropertyDefinition> properties, bool skipCheckVirtualIndex) : base(session, rootId, queryScope, filter, sortBy, pageSize, properties, skipCheckVirtualIndex)
		{
			this.properties = properties;
			this.queryFilter = filter;
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x000193D0 File Offset: 0x000175D0
		protected override T[] GetNextPage()
		{
			QueryFilter pagingQueryFilter = PagingHelper.GetPagingQueryFilter(this.queryFilter, this.cookie);
			T[] result = null;
			try
			{
				result = this.Find(pagingQueryFilter);
			}
			catch (PermanentDALException)
			{
				base.RetrievedAllData = new bool?(true);
				throw;
			}
			bool value = false;
			this.cookie = PagingHelper.GetProcessedCookie(pagingQueryFilter, out value);
			base.RetrievedAllData = new bool?(value);
			return result;
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x00019438 File Offset: 0x00017638
		private T[] Find(QueryFilter queryFilterWithPageCookie)
		{
			T[] array = this.FindInferredTypeObject<ExchangeRoleAssignment, ExchangeRoleAssignmentSchema>(queryFilterWithPageCookie);
			if (array != null)
			{
				return array;
			}
			T[] array2 = this.FindInferredTypeObject<ExchangeRole, ExchangeRoleSchema>(queryFilterWithPageCookie);
			if (array2 != null)
			{
				return array2;
			}
			T[] array3 = this.FindInferredTypeObject<ADRecipient, ExtendedSecurityPrincipalSchema>(queryFilterWithPageCookie);
			if (array3 != null)
			{
				return array3;
			}
			return ((IConfigDataProvider)base.Session).FindPaged<T>(queryFilterWithPageCookie, base.RootId, true, null, base.PageSize).ToArray<T>();
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x000194BC File Offset: 0x000176BC
		private T[] FindInferredTypeObject<IT, ITS>(QueryFilter queryFilterWithPageCookie) where IT : IConfigurable, new()
		{
			Func<IT, T> func = null;
			BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
			IEnumerable<PropertyDefinition> adPropertyDefinitions = (from field in typeof(ITS).GetFields(bindingAttr)
			select field.GetValue(null)).OfType<PropertyDefinition>();
			if (typeof(T) == typeof(ADRawEntry) && this.properties != null && this.properties.Any((PropertyDefinition prop) => adPropertyDefinitions.Contains(prop)))
			{
				IEnumerable<IT> enumerable = ((IConfigDataProvider)base.Session).FindPaged<IT>(queryFilterWithPageCookie, base.RootId, true, null, base.PageSize);
				IEnumerable<IT> source = enumerable;
				if (func == null)
				{
					func = ((IT item) => (T)((object)item));
				}
				return source.Select(func).ToArray<T>();
			}
			return null;
		}

		// Token: 0x04000463 RID: 1123
		private readonly QueryFilter queryFilter;

		// Token: 0x04000464 RID: 1124
		private readonly IEnumerable<PropertyDefinition> properties;

		// Token: 0x04000465 RID: 1125
		private string cookie;
	}
}
