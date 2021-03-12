using System;
using System.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x020002D2 RID: 722
	public static class PasswordSettingHelper
	{
		// Token: 0x06002C97 RID: 11415 RVA: 0x0008970C File Offset: 0x0008790C
		public static void GetObjectPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			DataRow dataRow = dataTable.Rows[0];
			RecipientTypeDetails recipientTypeDetails = (RecipientTypeDetails)dataRow["RecipientTypeDetails"];
			if (recipientTypeDetails == RecipientTypeDetails.LinkedMailbox)
			{
				dataRow["DomainUserName"] = (string)dataRow["LinkedMasterAccount"];
				return;
			}
			RbacPrincipal rbacPrincipal = RbacPrincipal.Current;
			if (rbacPrincipal.RbacConfiguration.SecurityAccessToken != null && rbacPrincipal.RbacConfiguration.SecurityAccessToken.LogonName != null)
			{
				dataRow["DomainUserName"] = rbacPrincipal.RbacConfiguration.SecurityAccessToken.LogonName;
				return;
			}
			dataRow["DomainUserName"] = string.Format("{0}\\{1}", ((ADObjectId)dataRow["Identity"]).DomainId.Name, (string)dataRow["SamAccountName"]);
		}
	}
}
