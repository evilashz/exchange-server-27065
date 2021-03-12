using System;
using System.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000330 RID: 816
	public class MailboxDatabasePicker : DDICodeBehind
	{
		// Token: 0x06002EF4 RID: 12020 RVA: 0x0008F360 File Offset: 0x0008D560
		public static void GetMailboxDatabasePostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			DatabasePropertiesHelper.FilterRecoveryDatabase(dataTable);
			int currentExchangeMajorVersion = Server.CurrentExchangeMajorVersion;
			string text = inputRow["Version"] as string;
			if (text == "*")
			{
				return;
			}
			if (!string.IsNullOrEmpty(text))
			{
				int.TryParse(text, out currentExchangeMajorVersion);
			}
			DatabasePropertiesHelper.FilterRowsByAdminDisplayVersion(inputRow, dataTable, store, currentExchangeMajorVersion, null);
		}
	}
}
