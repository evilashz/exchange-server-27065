using System;
using System.Security.Permissions;
using System.ServiceModel.Activation;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000512 RID: 1298
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class MailboxPlans : DataSourceService, IMailboxPlans, IGetListService<MailboxPlanFilter, MailboxPlan>
	{
		// Token: 0x06003E1B RID: 15899 RVA: 0x000BA7DF File Offset: 0x000B89DF
		[PrincipalPermission(SecurityAction.Demand, Role = "Dedicated+Get-MailboxPlan@R:Organization")]
		[PrincipalPermission(SecurityAction.Demand, Role = "MultiTenant+Get-MailboxPlan@R:Organization")]
		public PowerShellResults<MailboxPlan> GetList(MailboxPlanFilter filter, SortOptions sort)
		{
			return base.GetList<MailboxPlan, MailboxPlanFilter>("Get-MailboxPlan", filter, sort);
		}

		// Token: 0x04002854 RID: 10324
		private const string Noun = "MailboxPlan";

		// Token: 0x04002855 RID: 10325
		internal const string GetCmdlet = "Get-MailboxPlan";

		// Token: 0x04002856 RID: 10326
		internal const string ReadScope = "@R:Organization";

		// Token: 0x04002857 RID: 10327
		internal const string GetListRole_MultiTenant = "MultiTenant+Get-MailboxPlan@R:Organization";

		// Token: 0x04002858 RID: 10328
		internal const string GetListRole_Dedicated = "Dedicated+Get-MailboxPlan@R:Organization";
	}
}
