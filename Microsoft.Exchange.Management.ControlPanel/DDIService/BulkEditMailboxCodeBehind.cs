using System;
using System.Data;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000038 RID: 56
	public static class BulkEditMailboxCodeBehind
	{
		// Token: 0x0600194E RID: 6478 RVA: 0x0004F776 File Offset: 0x0004D976
		public static void SetADPermissionPreAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			MailboxPropertiesHelper.SetADPermissionPreAction(inputRow, dataTable, store);
		}

		// Token: 0x0600194F RID: 6479 RVA: 0x0004F780 File Offset: 0x0004D980
		public static int CalculateExpectedEvents(DataRow inputRow, DataRow dataTable)
		{
			int num = ((object[])inputRow["Identity"]).Length;
			int num2 = BulkEditMailboxCodeBehind.RetrieveCount(dataTable, "GrantSendOnBehalfToAdded");
			int num3 = BulkEditMailboxCodeBehind.RetrieveCount(dataTable, "GrantSendOnBehalfToRemoved");
			int num4 = BulkEditMailboxCodeBehind.RetrieveCount(dataTable, "SendAsPermissionsAdded");
			int num5 = BulkEditMailboxCodeBehind.RetrieveCount(dataTable, "SendAsPermissionsRemoved");
			int num6 = BulkEditMailboxCodeBehind.RetrieveCount(dataTable, "FullAccessPermissionsAdded");
			int num7 = BulkEditMailboxCodeBehind.RetrieveCount(dataTable, "FullAccessPermissionsRemoved");
			int num8 = 0;
			num8 += (num6 + num7) * 4;
			num8 += num4 + num5;
			if (num2 > 0)
			{
				num8++;
			}
			if (num3 > 0)
			{
				num8++;
			}
			return num8 * num;
		}

		// Token: 0x06001950 RID: 6480 RVA: 0x0004F81B File Offset: 0x0004DA1B
		private static int RetrieveCount(DataRow row, string columnName)
		{
			if (row[columnName] != DBNull.Value)
			{
				return ((object[])row[columnName]).Length;
			}
			return 0;
		}
	}
}
