using System;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x0200009E RID: 158
	public interface IColumnValueBag
	{
		// Token: 0x0600059D RID: 1437
		object GetColumnValue(Context context, Column column);

		// Token: 0x0600059E RID: 1438
		object GetOriginalColumnValue(Context context, Column column);

		// Token: 0x0600059F RID: 1439
		bool IsColumnChanged(Context context, Column column);

		// Token: 0x060005A0 RID: 1440
		void SetInstanceNumber(Context context, object instanceNumber);
	}
}
