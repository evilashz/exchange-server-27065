using System;
using System.Data;
using System.Management.Automation;
using Microsoft.Exchange.Management.ControlPanel;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMPhoneSession;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x020000C5 RID: 197
	public static class PlayOnPhoneService
	{
		// Token: 0x06001D13 RID: 7443 RVA: 0x00059780 File Offset: 0x00057980
		public static void DialPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store, PowerShellResults[] results)
		{
			if (results[0].Succeeded)
			{
				DataRow dataRow = dataTable.Rows[0];
				results[0].ProgressId = dataRow["Identity"].ToString();
			}
		}

		// Token: 0x06001D14 RID: 7444 RVA: 0x000597BC File Offset: 0x000579BC
		public static void GetProgressPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store, PowerShellResults[] results)
		{
			PowerShellResults<PSObject> powerShellResults = results[0] as PowerShellResults<PSObject>;
			if (powerShellResults != null && powerShellResults.SucceededWithValue)
			{
				UMPhoneSession umphoneSession = (UMPhoneSession)powerShellResults.Value.BaseObject;
				if (umphoneSession.OperationResult == UMOperationResult.InProgress)
				{
					powerShellResults.ProgressRecord = new Microsoft.Exchange.Management.ControlPanel.ProgressRecord
					{
						Status = umphoneSession.CallState.ToString()
					};
					return;
				}
				powerShellResults.ProgressRecord = new Microsoft.Exchange.Management.ControlPanel.ProgressRecord
				{
					Status = umphoneSession.CallState.ToString(),
					HasCompleted = true,
					Percent = 100
				};
			}
		}
	}
}
