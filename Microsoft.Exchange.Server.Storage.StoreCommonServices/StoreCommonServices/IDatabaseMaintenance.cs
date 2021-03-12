using System;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x020000C8 RID: 200
	public interface IDatabaseMaintenance
	{
		// Token: 0x06000838 RID: 2104
		bool MarkForMaintenance(Context context);

		// Token: 0x06000839 RID: 2105
		void ScheduleMarkForMaintenance(Context context, TimeSpan interval);
	}
}
