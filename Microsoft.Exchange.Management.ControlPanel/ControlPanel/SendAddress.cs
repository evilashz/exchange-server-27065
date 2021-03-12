using System;
using System.Security.Permissions;
using System.ServiceModel.Activation;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002E6 RID: 742
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public class SendAddress : DataSourceService, ISendAddress, IGetListService<SendAddressFilter, SendAddressRow>
	{
		// Token: 0x06002D07 RID: 11527 RVA: 0x0008A1B0 File Offset: 0x000883B0
		[PrincipalPermission(SecurityAction.Demand, Role = "MultiTenant+Get-SendAddress?Mailbox@R:Self")]
		public PowerShellResults<SendAddressRow> GetList(SendAddressFilter filter, SortOptions sort)
		{
			return base.GetList<SendAddressRow, SendAddressFilter>("Get-SendAddress", filter, sort);
		}

		// Token: 0x04002231 RID: 8753
		internal const string GetCmdlet = "Get-SendAddress";

		// Token: 0x04002232 RID: 8754
		internal const string ReadScope = "@R:Self";

		// Token: 0x04002233 RID: 8755
		private const string GetListRole = "MultiTenant+Get-SendAddress?Mailbox@R:Self";
	}
}
