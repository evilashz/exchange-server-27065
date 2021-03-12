using System;
using System.Data;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x020004D6 RID: 1238
	public class UMHuntGroupService : DDICodeBehind
	{
		// Token: 0x06003C83 RID: 15491 RVA: 0x000B5E24 File Offset: 0x000B4024
		public static void OnPostGetObjectForNew(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			DataRow dataRow = dataTable.Rows[0];
			dataRow["UMDialPlan"] = (string)store.GetValue("UMDialPlan", "Name");
		}

		// Token: 0x040027AF RID: 10159
		private const string UMHuntGroupName = "UMHuntGroup";

		// Token: 0x040027B0 RID: 10160
		private const string UMDialPlanName = "UMDialPlan";

		// Token: 0x040027B1 RID: 10161
		private const string UMIPGatewayName = "UMIPGateway";
	}
}
