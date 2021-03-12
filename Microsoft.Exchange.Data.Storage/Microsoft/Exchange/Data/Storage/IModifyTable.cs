using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000064 RID: 100
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IModifyTable : IDisposable
	{
		// Token: 0x0600072A RID: 1834
		void Clear();

		// Token: 0x0600072B RID: 1835
		void AddRow(params PropValue[] propValues);

		// Token: 0x0600072C RID: 1836
		void ModifyRow(params PropValue[] propValues);

		// Token: 0x0600072D RID: 1837
		void RemoveRow(params PropValue[] propValues);

		// Token: 0x0600072E RID: 1838
		IQueryResult GetQueryResult(QueryFilter queryFilter, ICollection<PropertyDefinition> columns);

		// Token: 0x0600072F RID: 1839
		void ApplyPendingChanges();

		// Token: 0x06000730 RID: 1840
		void SuppressRestriction();

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000731 RID: 1841
		StoreSession Session { get; }
	}
}
