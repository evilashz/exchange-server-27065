using System;
using System.Data;
using Microsoft.Exchange.Management.ControlPanel;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x0200002C RID: 44
	public class AddressList
	{
		// Token: 0x060018E2 RID: 6370 RVA: 0x0004E3A2 File Offset: 0x0004C5A2
		public static void GetObjectPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			EmailAddressPolicy.GetObjectPostAction(inputRow, dataTable, store);
		}

		// Token: 0x060018E3 RID: 6371 RVA: 0x0004E3AC File Offset: 0x0004C5AC
		public static void NewObjectPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store, PowerShellResults[] results)
		{
			DDIUtil.InsertWarningIfSucceded(results, Strings.NewAddressListWarning);
		}

		// Token: 0x060018E4 RID: 6372 RVA: 0x0004E3BF File Offset: 0x0004C5BF
		public static void SetObjectPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store, PowerShellResults[] results)
		{
			DDIUtil.InsertWarningIfSucceded(results, Strings.EditAddressListWarning);
		}
	}
}
