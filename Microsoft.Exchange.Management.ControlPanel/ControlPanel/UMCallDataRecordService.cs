using System;
using System.Security.Permissions;
using System.ServiceModel.Activation;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004B3 RID: 1203
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class UMCallDataRecordService : DataSourceService, IUMCallDataRecordService, IGetListService<UMCallDataRecordFilter, UMCallDataRecordRow>
	{
		// Token: 0x06003B7C RID: 15228 RVA: 0x000B395A File Offset: 0x000B1B5A
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-UMCallDataRecord?Mailbox@R:Organization")]
		public PowerShellResults<UMCallDataRecordRow> GetList(UMCallDataRecordFilter filter, SortOptions sort)
		{
			return base.GetList<UMCallDataRecordRow, UMCallDataRecordFilter>("Get-UMCallDataRecord", filter, sort);
		}

		// Token: 0x0400276B RID: 10091
		internal const string GetCmdlet = "Get-UMCallDataRecord";

		// Token: 0x0400276C RID: 10092
		internal const string ReadScope = "@R:Organization";

		// Token: 0x0400276D RID: 10093
		private const string GetListRole = "Get-UMCallDataRecord?Mailbox@R:Organization";
	}
}
