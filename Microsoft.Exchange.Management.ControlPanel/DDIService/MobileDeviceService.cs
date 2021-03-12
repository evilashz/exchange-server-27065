using System;
using System.Data;
using System.Management.Automation;
using Microsoft.Exchange.Management.ControlPanel;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x0200049F RID: 1183
	public static class MobileDeviceService
	{
		// Token: 0x06003ADC RID: 15068 RVA: 0x000B23D4 File Offset: 0x000B05D4
		public static void GetListRawResultAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			PowerShellResults<PSObject> powerShellResults = (PowerShellResults<PSObject>)store.GetDataObject("CASMailbox");
			if (powerShellResults != null && powerShellResults.Output.Length == 1)
			{
				foreach (object obj in dataTable.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					dataRow["IsLoggingRunning"] = powerShellResults.Output[0].Properties["ActiveSyncDebugLogging"].Value;
				}
			}
		}
	}
}
