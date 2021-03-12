using System;
using Microsoft.Exchange.Common.HA;

namespace Microsoft.Exchange.Server.Storage.HA
{
	// Token: 0x02000017 RID: 23
	public class FailureItem
	{
		// Token: 0x060000BE RID: 190 RVA: 0x00005F2C File Offset: 0x0000412C
		internal static void PublishHaFailure(Guid dbGuid, string dbName, FailureTag failureTag)
		{
			DatabaseFailureItem databaseFailureItem = new DatabaseFailureItem(FailureNameSpace.Store, failureTag, dbGuid)
			{
				ComponentName = "Store",
				InstanceName = dbName
			};
			databaseFailureItem.Publish();
		}
	}
}
