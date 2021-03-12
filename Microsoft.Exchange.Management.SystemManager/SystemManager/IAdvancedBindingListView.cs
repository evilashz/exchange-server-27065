using System;
using System.Collections;
using System.ComponentModel;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x02000016 RID: 22
	public interface IAdvancedBindingListView : IBindingListView, IBindingList, IList, ICollection, IEnumerable
	{
		// Token: 0x06000109 RID: 265
		bool IsSortSupported(string propertyName);

		// Token: 0x0600010A RID: 266
		bool IsFilterSupported(string propertyName);

		// Token: 0x0600010B RID: 267
		void ApplyFilter(QueryFilter filter);

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600010C RID: 268
		QueryFilter QueryFilter { get; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600010D RID: 269
		bool Filtering { get; }

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x0600010E RID: 270
		// (remove) Token: 0x0600010F RID: 271
		event EventHandler FilteringChanged;

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000110 RID: 272
		bool SupportCancelFiltering { get; }

		// Token: 0x06000111 RID: 273
		void CancelFiltering();
	}
}
