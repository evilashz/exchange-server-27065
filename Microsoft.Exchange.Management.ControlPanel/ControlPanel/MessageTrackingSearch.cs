using System;
using System.Security.Permissions;
using System.ServiceModel.Activation;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002CF RID: 719
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class MessageTrackingSearch : DataSourceService, IMessageTrackingSearch, IGetListService<MessageTrackingSearchFilter, MessageTrackingSearchResultRow>
	{
		// Token: 0x06002C7A RID: 11386 RVA: 0x0008951E File Offset: 0x0008771E
		[PrincipalPermission(SecurityAction.Demand, Role = "Search-MessageTrackingReport?ResultSize&Identity&MessageId&Subject&Sender&Recipients@R:Self")]
		public PowerShellResults<MessageTrackingSearchResultRow> GetList(MessageTrackingSearchFilter filter, SortOptions sort)
		{
			return base.GetList<MessageTrackingSearchResultRow, MessageTrackingSearchFilter>("Search-MessageTrackingReport", filter, sort);
		}

		// Token: 0x040021FD RID: 8701
		internal const string GetCmdlet = "Search-MessageTrackingReport";

		// Token: 0x040021FE RID: 8702
		internal const string ReadScope = "@R:Self";

		// Token: 0x040021FF RID: 8703
		private const string GetListRole = "Search-MessageTrackingReport?ResultSize&Identity&MessageId&Subject&Sender&Recipients@R:Self";
	}
}
