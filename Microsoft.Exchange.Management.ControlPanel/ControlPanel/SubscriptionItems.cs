using System;
using System.Security.Permissions;
using System.ServiceModel.Activation;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000451 RID: 1105
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class SubscriptionItems : DataSourceService, ISubscriptionItems, IGetListService<SubscriptionItemFilter, SubscriptionItem>
	{
		// Token: 0x06003645 RID: 13893 RVA: 0x000A7C38 File Offset: 0x000A5E38
		[PrincipalPermission(SecurityAction.Demand, Role = "MultiTenant+Get-Subscription@R:Self")]
		public PowerShellResults<SubscriptionItem> GetList(SubscriptionItemFilter filter, SortOptions sort)
		{
			PowerShellResults<SubscriptionItem> list = base.GetList<SubscriptionItem, SubscriptionItemFilter>("Get-Subscription", filter, sort);
			if (list.Output != null && list.Output.Length > 0)
			{
				list.Output = Array.FindAll<SubscriptionItem>(list.Output, (SubscriptionItem x) => x.IsValid);
			}
			return list;
		}

		// Token: 0x040025D5 RID: 9685
		internal const string GetCmdlet = "Get-Subscription";

		// Token: 0x040025D6 RID: 9686
		internal const string ReadScope = "@R:Self";

		// Token: 0x040025D7 RID: 9687
		internal const string GetListRole = "MultiTenant+Get-Subscription@R:Self";

		// Token: 0x040025D8 RID: 9688
		private const string Noun = "Subscription";
	}
}
