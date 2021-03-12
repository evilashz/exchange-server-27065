using System;
using System.Data;
using Microsoft.Exchange.Management.ControlPanel;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x020001DB RID: 475
	public static class ClientExtension
	{
		// Token: 0x06002590 RID: 9616 RVA: 0x000735E5 File Offset: 0x000717E5
		public static void GetPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			ExtensionUtility.ExtensionGetPostAction(false, inputRow, dataTable, store);
		}
	}
}
