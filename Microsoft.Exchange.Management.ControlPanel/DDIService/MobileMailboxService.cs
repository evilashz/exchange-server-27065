using System;
using System.Data;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x020004F6 RID: 1270
	public class MobileMailboxService
	{
		// Token: 0x06003D76 RID: 15734 RVA: 0x000B891C File Offset: 0x000B6B1C
		public static void AddToAllowList(DataRow row, DataTable dataTable, DataObjectStore store)
		{
			MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)dataTable.Rows[0]["ActiveSyncAllowedDeviceIDs"];
			multiValuedProperty.Add((string)store.GetValue("MobileDevice", "DeviceId"));
		}

		// Token: 0x06003D77 RID: 15735 RVA: 0x000B8960 File Offset: 0x000B6B60
		public static void AddToBlockList(DataRow row, DataTable dataTable, DataObjectStore store)
		{
			MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)dataTable.Rows[0]["ActiveSyncBlockedDeviceIDs"];
			multiValuedProperty.Add((string)store.GetValue("MobileDevice", "DeviceId"));
		}
	}
}
