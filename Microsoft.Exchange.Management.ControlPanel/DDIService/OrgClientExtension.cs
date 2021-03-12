using System;
using System.Data;
using Microsoft.Exchange.Management.ControlPanel;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x020001DC RID: 476
	public static class OrgClientExtension
	{
		// Token: 0x06002591 RID: 9617 RVA: 0x000735F0 File Offset: 0x000717F0
		public static void GetPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			ExtensionUtility.ExtensionGetPostAction(true, inputRow, dataTable, store);
		}
	}
}
